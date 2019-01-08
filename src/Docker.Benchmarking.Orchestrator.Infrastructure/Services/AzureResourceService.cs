using Ardalis.GuardClauses;
using Docker.Benchmarking.Orchestrator.Core.Entities;
using Docker.Benchmarking.Orchestrator.Core.Interfaces;
using Docker.Benchmarking.Orchestrator.Infrastrcture.Settings;
using Hangfire;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.Management.Compute.Fluent;
using Microsoft.Azure.Management.Compute.Fluent.Models;
using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Authentication;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Infrastrcture.Services
{
    public class AzureResourceService : IAzureResourceService
    {
        private readonly string jsonFilePath;
        private readonly string shellScriptPath;
        private readonly IOptions<AzureSettings> _azureSettings;
        private readonly IRepository<BenchmarkExperiment> _appTestRepo;
        private readonly IRepository<AzureHost> _azureHostRepo;
        private readonly IRepository<DockerHost> _dockerHostRepo;
        private readonly IRepository<AzureCredientials> _azureCredentials;
        private readonly IDockerRemoteService _dockerRemoteService;
        private readonly IHostingEnvironment _env;

        public AzureResourceService(IRepository<BenchmarkExperiment> appTestRepo,
            IRepository<AzureHost> azureHostRepo,
            IRepository<DockerHost> dockerHostRepo,
            IRepository<Core.Entities.AzureCredientials> azureCredentials,
            IDockerRemoteService dockerRemoteService,
            IHostingEnvironment env,
            IOptions<AzureSettings> azureSettings)
        {
            _appTestRepo = appTestRepo;
            _azureHostRepo = azureHostRepo;
            _dockerHostRepo = dockerHostRepo;
            _azureCredentials = azureCredentials;
            _dockerRemoteService = dockerRemoteService;
            _env = env;
            _azureSettings = azureSettings;

            jsonFilePath = Path.Combine(_env.WebRootPath, @"azureFiles/").ToLower();
            shellScriptPath = Path.Combine(_env.WebRootPath, _azureSettings.Value.ShellScriptFilePath);

            if (!Directory.Exists(jsonFilePath)) Directory.CreateDirectory(jsonFilePath);
        }

        #region public


        [AutomaticRetry(Attempts = 0)]
        public async Task<string> CreateVMForBenchmark(Guid applicationTestId)
        {
            Guard.Against.GuidNullOrDefault(applicationTestId, nameof(applicationTestId));

            var appTest = await _appTestRepo.GetByIdAsync(applicationTestId);

            Guard.Against.Null(appTest, nameof(appTest));

            if (appTest.Host.AzureHost != null)
            {
                var azureHost = appTest.Host.AzureHost;
                var ipAddress = await SetupHost(azureHost);

                appTest.Host.HostName = ipAddress;
                appTest.Host.PortNumber = 2376;

                await _dockerHostRepo.UpdateAsync(appTest.Host);
            }

            if (appTest.BenchmarkHost.AzureHost != null)
            {
                var azureHost = appTest.BenchmarkHost.AzureHost;
                var ipAddress = await SetupHost(azureHost);

                appTest.BenchmarkHost.HostName = ipAddress;
                appTest.BenchmarkHost.PortNumber = 2376;

                await _dockerHostRepo.UpdateAsync(appTest.BenchmarkHost);
            }

            if (appTest.DatabaseHost != null)
            {
                if (appTest.DatabaseHost.AzureHost != null)
                {
                    var azureHost = appTest.DatabaseHost.AzureHost;
                    var ipAddress = await SetupHost(azureHost);
                    appTest.BenchmarkHost.HostName = ipAddress;
                    appTest.BenchmarkHost.PortNumber = 2376;

                    await _dockerHostRepo.UpdateAsync(appTest.BenchmarkHost);
                }
            }

            return string.Empty;
        }

        [AutomaticRetry(Attempts = 0)]
        public async Task<string> CreateVMWithDocker(Guid azureHostId)
        {
            Guard.Against.GuidNullOrDefault(azureHostId, nameof(azureHostId));

            var azureHost = await _azureHostRepo.GetByIdAsync(azureHostId);

            if (azureHost == null) throw new NullReferenceException("No host found for given id");

            var credentials = CreateCreditials(azureHost.Credentials);

            return await DeployVMScript(azureHost.DockerHost.StackResourceName, azureHost.AzureRegion, azureHost.Template.Template, azureHost.Template.ParametersDefault, credentials);
        }

        [AutomaticRetry(Attempts = 0)]
        public async Task DeleteResources(Guid azureHostId)
        {
            Guard.Against.GuidNullOrDefault(azureHostId, nameof(azureHostId));

            var azureHost = await _azureHostRepo.GetByIdAsync(azureHostId);

            if (azureHost == null) throw new NullReferenceException("No host found for given id");

            var credentials = CreateCreditials(azureHost.Credentials);
            var azure = AzureClient(credentials);

            await azure.ResourceGroups.DeleteByNameAsync(azureHost.DockerHost.StackResourceName);

            //Mark Resources as deleted
            azureHost.ResourceDestroyedAt = DateTimeOffset.UtcNow;
            azureHost.ResourceDestroyed = true;
            await _azureHostRepo.UpdateAsync(azureHost);
        }

        [AutomaticRetry(Attempts = 0)]
        public async Task DeleteResourcesByResourceName(string resourceName, Guid credentialsId)
        {
            Guard.Against.NullOrEmpty(resourceName, nameof(resourceName));
            Guard.Against.GuidNullOrDefault(credentialsId, nameof(credentialsId));

            var credentials = await CreateCreditials(credentialsId);

            var azure = AzureClient(credentials);

            var exists = await azure.ResourceGroups.ContainAsync(resourceName, CancellationToken.None);

            if (exists) await azure.ResourceGroups.DeleteByNameAsync(resourceName);
        }

        public IEnumerable<Region> GetRegions()
        {
            return Region.Values;
        }

        public async Task<IEnumerable<VirtualMachineSizeInner>> GetVMSizes(string location, Guid credentialsId)
        {
            Guard.Against.NullOrEmpty(location, nameof(location));
            Guard.Against.GuidNullOrDefault(credentialsId, nameof(credentialsId));

            var credentials = await CreateCreditials(credentialsId);

            var computeClient = new ComputeManagementClient(credentials) { SubscriptionId = credentials.DefaultSubscriptionId };
            var virtualMachineSize = await computeClient.VirtualMachineSizes.ListAsync(location);
            return virtualMachineSize;
        }

        [AutomaticRetry(Attempts = 0)]
        public async Task<string> TestCreateResources(string resourceName, string region, string deploymentJson, string deploymentParameters, Guid credentialsId)
        {
            //Guard Clauses
            Guard.Against.NullOrEmpty(resourceName, nameof(resourceName));
            Guard.Against.NullOrEmpty(region, nameof(region));
            Guard.Against.NullOrEmpty(deploymentJson, nameof(deploymentJson));
            Guard.Against.NullOrEmpty(deploymentParameters, nameof(deploymentParameters));
            Guard.Against.GuidNullOrDefault(credentialsId, nameof(credentialsId));

            var credentials = await CreateCreditials(credentialsId);

            var azure = AzureClient(credentials);

            return await DeployVMScript(resourceName, region, deploymentJson, deploymentParameters, credentials);
        }

        [AutomaticRetry(Attempts = 0)]
        public async Task<string> GetIpAddressOfNewDeployment(string resourceName, Guid credentialsId)
        {
            Guard.Against.NullOrEmpty(resourceName, nameof(resourceName));
            Guard.Against.GuidNullOrDefault(credentialsId, nameof(credentialsId));

            var credentials = await CreateCreditials(credentialsId);

            var azure = AzureClient(credentials);

            return await GetIPAddressForResourceVM(resourceName, azure);
        }

        [AutomaticRetry(Attempts = 0)]
        public async Task<bool> DeployImageToAzureHost(string resourceName, Guid credentialsId, Guid dockerImageId)
        {
            Guard.Against.NullOrEmpty(resourceName, nameof(resourceName));
            Guard.Against.GuidNullOrDefault(credentialsId, nameof(credentialsId));
            Guard.Against.GuidNullOrDefault(dockerImageId, nameof(dockerImageId));

            var ipAddress = await GetIpAddressOfNewDeployment(resourceName, credentialsId);

            return await _dockerRemoteService.DeployImageToHost(ipAddress, 2376, dockerImageId);
        }

        #endregion

        #region private

        private async Task<AzureCredentials> CreateCreditials(Guid credentialsId)
        {
            Guard.Against.GuidNullOrDefault(credentialsId, nameof(credentialsId));

            var creds = await _azureCredentials.GetByIdAsync(credentialsId);

            if (creds == null) throw new Exception("No azure credentials found.");

            return CreateCreditials(creds);
        }

        private AzureCredentials CreateCreditials(AzureCredientials creds)
        {
            Guard.Against.Null(creds, nameof(creds));

            var azureCredentials = SdkContext.AzureCredentialsFactory
               .FromServicePrincipal(creds.ClientId,
               creds.Secret,
               creds.TenantId,
               AzureEnvironment.AzureGlobalCloud);

            azureCredentials.WithDefaultSubscription(creds.SubscriptionId);

            return azureCredentials;
        }

        private IAzure AzureClient(AzureCredentials credentials)
        {
            Guard.Against.Null(credentials, nameof(credentials));

            return Azure.Configure()
                .WithLogLevel(HttpLoggingDelegatingHandler.Level.Basic)
                .Authenticate(credentials)
                .WithSubscription(credentials.DefaultSubscriptionId);
        }

        private async Task<string> DeployVMScript(string resourceName, string region, string deploymentJson, string deploymentParameters, AzureCredentials creds)
        {
            Guard.Against.NullOrEmpty(resourceName, nameof(resourceName));
            Guard.Against.NullOrEmpty(region, nameof(region));
            Guard.Against.NullOrEmpty(deploymentJson, nameof(deploymentJson));
            Guard.Against.NullOrEmpty(deploymentParameters, nameof(deploymentParameters));
            Guard.Against.Null(creds, nameof(creds));

            var azure = AzureClient(creds);

            var resourceGroup = await azure.ResourceGroups.Define(resourceName)
                .WithRegion(region)
                .CreateAsync();

            var storageName = SdkContext.RandomResourceName("st", 10);

            var storage = await azure.StorageAccounts.Define(storageName)
                .WithRegion(region)
                .WithExistingResourceGroup(resourceGroup)
                .CreateAsync();


            var storageKeys = storage.GetKeys();
            string storageConnectionString = "DefaultEndpointsProtocol=https;"
                + "AccountName=" + storage.Name
                + ";AccountKey=" + storageKeys[0].Value
                + ";EndpointSuffix=core.windows.net";

            var account = CloudStorageAccount.Parse(storageConnectionString);
            var serviceClient = account.CreateCloudBlobClient();

            var container = serviceClient.GetContainerReference("templates");
            await container.CreateIfNotExistsAsync();

            var containerPermissions = new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Container
            };

            await container.SetPermissionsAsync(containerPermissions);

            var shellScriptBlob = container.GetBlockBlobReference(_azureSettings.Value.ShellScriptFileName);
            await shellScriptBlob.UploadFromFileAsync(shellScriptPath);

            string newParametersJson = JsonConvert.DeserializeObject(deploymentParameters).ToString();

            newParametersJson = newParametersJson.Replace("[dnsName]", resourceName, StringComparison.CurrentCultureIgnoreCase);
            newParametersJson = newParametersJson.Replace("[location]", region, StringComparison.CurrentCultureIgnoreCase);

            var newParametersFileName = resourceName + "-parameters" + ".json";

            var fileParametersPath = Path.Combine(jsonFilePath + newParametersFileName);
            //Write to New File
            File.WriteAllText(fileParametersPath, newParametersJson);

            //Template write to file
            string templateJson = JsonConvert.DeserializeObject(deploymentJson).ToString();
            var shellRemotePath = "https://" + storageName + ".blob.core.windows.net/templates/" + _azureSettings.Value.ShellScriptFileName;

            templateJson = templateJson.Replace("[shellRemotePath]", shellRemotePath, StringComparison.CurrentCultureIgnoreCase);

            var templateJsonFileName = resourceName + "-deploymentJson" + ".json";
            var fileTemplatePath = Path.Combine(jsonFilePath + templateJsonFileName);

            File.WriteAllText(fileTemplatePath, templateJson);

            var templateblob = container.GetBlockBlobReference(templateJsonFileName);
            await templateblob.UploadFromFileAsync(fileTemplatePath);

            var paramblob = container.GetBlockBlobReference(newParametersFileName);
            await paramblob.UploadFromFileAsync(fileParametersPath);

            var templatePath = "https://" + storageName + ".blob.core.windows.net/templates/" + templateJsonFileName;
            var paramPath = "https://" + storageName + ".blob.core.windows.net/templates/" + newParametersFileName;

            var deployment = await azure.Deployments.Define("myDeployment")
                .WithExistingResourceGroup(resourceName)
                .WithTemplateLink(templatePath, "1.0.0.0")
                .WithParametersLink(paramPath, "1.0.0.0")
                .WithMode(Microsoft.Azure.Management.ResourceManager.Fluent.Models.DeploymentMode.Incremental)
                .CreateAsync();

            return await GetIPAddressForResourceVM(resourceName, azure);
        }

        private async Task<string> GetIPAddressForResourceVM(string resourceName, IAzure client)
        {
            Guard.Against.NullOrEmpty(resourceName, nameof(resourceName));
            Guard.Against.Null(client, nameof(client));

            var vm = await client.VirtualMachines.ListByResourceGroupAsync(resourceName);

            if (vm == null) throw new Exception();

            var ipAddress = vm.FirstOrDefault().GetPrimaryPublicIPAddress().IPAddress;

            if (ipAddress == null) throw new Exception();

            return vm.FirstOrDefault().GetPrimaryPublicIPAddress().IPAddress;
        }

        private async Task<string> SetupHost(AzureHost host)
        {
            Guard.Against.Null(host, nameof(host));

            //Flag for Application Layer
            host.ResourceCreationStarted = true;
            await _azureHostRepo.UpdateAsync(host);

            await DeleteResourcesByResourceName(host.DockerHost.StackResourceName, host.Credentials.Id);

            var ipAddress = await CreateVMWithDocker(host.Id);

            host.IPAddress = ipAddress;
            host.ResourceCreatedAt = DateTimeOffset.Now;
            host.ResourcedCreated = true;
            await _azureHostRepo.UpdateAsync(host);

            return ipAddress;
        }

        #endregion
    }
}
