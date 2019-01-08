using Ardalis.GuardClauses;
using Docker.Benchmarking.Orchestrator.Core.Entities;
using Docker.Benchmarking.Orchestrator.Core.Enums;
using Docker.Benchmarking.Orchestrator.Core.Interfaces;
using Docker.Benchmarking.Orchestrator.Infrastrcture.Extensions;
using Docker.DotNet;
using Docker.DotNet.BasicAuth;
using Docker.DotNet.Models;
using Hangfire;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Infrastrcture.Services
{
    public class BenchmarkOrchestratorService : IBenchmarkOrchestratorService, IDisposable
    {
        private readonly IRepository<BenchmarkExperiment> _appTestRepo;
        private readonly IRepository<DockerHost> _dockerHostRepo;
        private readonly IBackgroundJobClient _backgroungJobClient;
        private DockerClient _dockerAppClient;
        private DockerClient _dockerBenchmarkClient;

        //TODO Add Database Client
        private readonly ILogger _logger;
        private readonly ICurrentHostSettings _hostSettings;
        private readonly IRepository<ContainerMetric> _containerMetricRepo;
        private readonly IMediator _mediatr;
        public BenchmarkOrchestratorService(IRepository<BenchmarkExperiment> appTestRepo,
            ILogger<BenchmarkOrchestratorService> logger,
            ICurrentHostSettings hostSettings,
            IRepository<ContainerMetric> containerMetricRepo,
            IRepository<DockerHost> dockerHostRepo,
            IBackgroundJobClient backgroungJobClient,
            IMediator mediatr)
        {
            _appTestRepo = appTestRepo;
            _logger = logger;
            _hostSettings = hostSettings;
            _containerMetricRepo = containerMetricRepo;
            _dockerHostRepo = dockerHostRepo;
            _backgroungJobClient = backgroungJobClient;
            _mediatr = mediatr;
        }

        #region public

        /// <summary>
        /// Starts the benchmarking step1.
        /// </summary>
        /// <param name="applicationTestId">The application test identifier.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        /// <exception cref="NullReferenceException">Application and Application Test do not have a Test Plan assigned to them</exception>
        /// <exception cref="Exception">
        /// Benchmark Experiment already started.
        /// or
        /// Benchmark Experiment already completed.
        /// </exception>
        public async Task<bool> StartBenchmarkingStep1(Guid applicationTestId)
        {
            Guard.Against.GuidNullOrDefault(applicationTestId, nameof(applicationTestId));

            var applicationTest = await _appTestRepo.GetByIdAsync(applicationTestId);

            Guard.Against.Null(applicationTest, nameof(applicationTest));

            if (applicationTest.TestFile == null && applicationTest.Application.TestFile == null)
                throw new NullReferenceException("Application and Application Test do not have a Test Plan assigned to them");

            if (applicationTest.Started)
                throw new Exception("Benchmark Experiment already started.");

            if (applicationTest.Completed)
                throw new Exception("Benchmark Experiment already completed.");

            //Launch Data Image to Host first, then get IP address

            //TODO
            if (applicationTest.Application.DatabaseImageId.HasValue)
            {
                var databaseContainerId = await LaunchImageToHost(applicationTest.Application.DatabaseImage, applicationTest.Host);
            }

            var containerId = await LaunchImageToHost(applicationTest.Application.ApplicationImage, applicationTest.Host);

            applicationTest.AppContainerId = containerId;
            applicationTest.vCPU = applicationTest.Host.vCPU;
            applicationTest.Memory = applicationTest.Host.Memory;

            //TODO Domain Event
            await _appTestRepo.UpdateAsync(applicationTest);

            //Delay for Start of Test to allow application to start-up
            await Task.Delay(applicationTest.Application.DelayToStartApplication);

            var benchmarkContainerId = await LaunchBenchmarkToHost(applicationTest);
            applicationTest.BenchmarkContainerId = benchmarkContainerId;

            await _appTestRepo.UpdateAsync(applicationTest);

            return true;
        }

        public void Dispose()
        {
            if (_dockerAppClient != null) _dockerAppClient.Dispose();
            if (_dockerBenchmarkClient != null) _dockerBenchmarkClient.Dispose();
        }

        //TODO
        public Task<bool> StopApplicationContainer(BenchmarkExperiment appTest) => throw new NotImplementedException();

        //Can't pass in multi-level objects when it's a background task due to Json serialization errors
        public async Task<bool> CaptureDockerStatsAsync(Guid appTestId, IJobCancellationToken token)
        {
            Guard.Against.GuidNullOrDefault(appTestId, nameof(appTestId));

            var appTest = _appTestRepo.GetById(appTestId);
            var host = appTest.Host;

            if (host.UseHttpAuthentication)
            {
                CreateBasicHttpClient(host.HostName, host.PortNumber, host.UserName, host.Password, out _dockerAppClient);
            }
            else
                CreateClientNoAuthentication(host.HostName, host.PortNumber, out _dockerAppClient);

            var testList = new List<ContainerMetric>();

            //Add Extra 2 minutes for streaming
            int timer = 0;
            var length = appTest.BenchmarkTimeLength + 5000;

            var cts = new CancellationTokenSource();
            cts.CancelAfter(length);
            //    //Ad 5 seconds.

            await Task.Run(async () =>
            {
                while (timer < length)
                {
                    //Need to supress errors
                    try
                    {
                        await insertContainerMetric(appTest);

                    }
                    catch (Exception) { }

                    token.ThrowIfCancellationRequested();

                    await Task.Delay(200);

                    if (cts.IsCancellationRequested)
                        break;
                }
            });

            //while (timer < length)
            //{

            //    await _containerMetricRepo.AddAsync(metricStat);
            //    timer += 1000;
            //    Thread.Sleep(1000);

            //}


            //try
            //{

            //    IProgress<ContainerStatsResponse> progress = new Progress<ContainerStatsResponse>(async stats =>
            //    {
            //        //var json = JsonConvert.SerializeObject(stats);
            //        var metricStat = new ContainerMetric
            //        {
            //            ContainerId = appTest.AppContainerId,
            //            ContainerName = appTest.DockerFriendlyName,
            //            DateTimeCreated = DateTime.UtcNow,
            //            DockerDateTimestamp = stats.Read,
            //            //JsonDockerResponse = json,
            //            BenchmarkExperiment = appTest,
            //            MemoryLimit = stats.MemoryStats.Limit,
            //            MemoryUsage = stats.MemoryStats.Usage,
            //        };

            //        metricStat.SetCPUPercentage(stats.CPUStats, stats.PreCPUStats);
            //        metricStat.CalculateNetworks(stats.Networks);
            //        metricStat.CalculateBlockIO(stats.BlkioStats);

            //        testList.Add(metricStat);
            //    });

            //    var cts = new CancellationTokenSource();

            //    //Add 5 seconds.
            //    cts.CancelAfter(appTest.BenchmarkTimeLength + 5000);

            //    token.ThrowIfCancellationRequested();

            //    await _dockerAppClient.Containers.GetContainerStatsAsync(appTest.AppContainerId, new ContainerStatsParameters { Stream = true }, progress, token.ShutdownToken);

            //}
            //catch (TaskCanceledException ex)
            //{
            //    _logger.LogError(ex.Message);
            //}
            //catch (OperationCanceledException ex)
            //{
            //    _logger.LogError(ex.Message);
            //}
            //catch (DockerApiException ex)
            //{
            //    _logger.LogError(ex.Message);
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(ex.Message);
            //}
            //finally
            //{
            //    RunAfterCaptureAsyncEvents(testList, appTestId);
            //    testList.Clear();
            //    //Delete container metrics after benchmark has stopped.
            //}

            await RunAfterCaptureAsyncEvents(testList, appTestId);
            //Batch Insert Metrics to DB.

            return true;
        }

        //Not Required.
        public async Task<bool> StartBenchmarkingStep2(BenchmarkExperiment benchmarkExperiment)
        {
            Guard.Against.Null(benchmarkExperiment, nameof(benchmarkExperiment));

            benchmarkExperiment.Started = true;
            benchmarkExperiment.StartedAt = DateTimeOffset.UtcNow;
            await _appTestRepo.UpdateAsync(benchmarkExperiment);

            if (benchmarkExperiment.CaptureContainerMetrics)
            {
                //Capture Docker Stats in Background Job so it's not blocking confirmation to the user.
                //var jobId = BackgroundJob.Enqueue(() => CaptureDockerStatsAsync(benchmarkExperiment.Id));

                var jobId = _backgroungJobClient.Enqueue(() => CaptureDockerStatsAsync(benchmarkExperiment.Id, JobCancellationToken.Null));
                //_backgroungJobClient.ContinueWith(jobId, () => ProcessDockerStats(benchmarkExperiment.Id));

                benchmarkExperiment.HangfireContainerMetricsJobId = jobId;
                await _appTestRepo.UpdateAsync(benchmarkExperiment);
            }

            return true;
        }

        public async Task<bool> EndBenchmarkExperiment(BenchmarkExperiment benchmarkExperiment)
        {
            Guard.Against.Null(benchmarkExperiment, nameof(benchmarkExperiment));

            benchmarkExperiment.Completed = true;
            benchmarkExperiment.CompletedAt = DateTimeOffset.UtcNow;
            await _appTestRepo.UpdateAsync(benchmarkExperiment);

            await SetHostToInactive(benchmarkExperiment.Host);
            await SetHostToInactive(benchmarkExperiment.BenchmarkHost);

            if (benchmarkExperiment.DatabaseHost != null)
                await SetHostToInactive(benchmarkExperiment.BenchmarkHost);

            _backgroungJobClient.Delete(benchmarkExperiment.HangfireContainerMetricsJobId);
            await ProcessDockerStats(benchmarkExperiment.Id);
            return true;
        }



        #endregion

        #region private

        private async Task RunAfterCaptureAsyncEvents(List<ContainerMetric> metrics, Guid experiment)
        {
            foreach (var metric in metrics)
            {
                //issues with bulk inserts
                _containerMetricRepo.Add(metric);
            }

            await ProcessDockerStats(experiment);
        }

        private async Task SetHostToInactive(DockerHost host)
        {
            Guard.Against.Null(host, nameof(host));

            if (host.AWSHost == null || host.AzureHost == null)
                return;

            //If Host has been from created resources then set to inactive and used
            host.Active = false;
            host.Used = true;
            await _dockerHostRepo.UpdateAsync(host);
        }

        private async Task PullImageOntoHost(DockerImage image, HostType hostType)
        {
            Guard.Against.Null(image, nameof(image));
            Guard.Against.Null(hostType, nameof(hostType));

            var parameters = new ImagesCreateParameters
            {
                FromImage = image.ImageName,
                Tag = image.ImageTag
            };

            var config = new AuthConfig();

            if (image.PrivateRepository)
            {
                config.Username = image.PrivateRepositoryUsername;
                config.Password = image.PrivateRepositoryPassword;
                config.ServerAddress = image.PrivateRepositoryHost;
            }

            var progress = new Progress<JSONMessage>(stats => Console.WriteLine(stats.Status));

            //Actually Pulldown image

            switch (hostType)
            {
                case HostType.Application:
                    await _dockerAppClient.Images.CreateImageAsync(parameters, config, progress, CancellationToken.None);
                    break;
                case HostType.Benchmark:
                    await _dockerBenchmarkClient.Images.CreateImageAsync(parameters, config, progress, CancellationToken.None);
                    break;
            }
        }

        private string BuildJMXPath(string hostName, Guid applicationId)
        {
            Guard.Against.NullOrEmpty(hostName, nameof(hostName));
            Guard.Against.GuidNullOrDefault(applicationId, nameof(applicationId));

            return hostName + "BenchmarkExperiment/TestFileForBenchmarkExperiment/" + applicationId;
        }

        private async Task<string> LaunchImageToHost(DockerImage image, DockerHost host, IEnumerable<ApplicationTestVariable> applicationVariables = null)
        {
            Guard.Against.Null(image, nameof(image));

            Guard.Against.Null(host, nameof(host));

            if (host.UseHttpAuthentication)
            {
                CreateBasicHttpClient(host.HostName, host.PortNumber, host.UserName, host.Password, out _dockerAppClient);
            }
            else
                CreateClientNoAuthentication(host.HostName, host.PortNumber, out _dockerAppClient);

            await RemoveContainersFromHost(_dockerAppClient);

            await PullImageOntoHost(image, HostType.Application);

            var portBindings = new Dictionary<string, IList<PortBinding>>();

            if (image.ImageType == ImageType.Application || image.ImageType == ImageType.Database)
            {
                if (image.ExternalPort == null)
                    throw new NullReferenceException("External Port must be exposed when launching web-application container.");

                if (image.InternalPort == null)
                    throw new NullReferenceException("Internal Port must be exposed when launching web-application container.");
            }

            var startParamters = new CreateContainerParameters()
            {
                Name = image.DockerFriendlyName,
                Image = image.FullImageTag,
                HostConfig = new HostConfig
                {
                    PortBindings = new Dictionary<string, IList<PortBinding>>()
                    {
                    {
                        image.InternalPort + "/tcp",
                        new PortBinding[]
                        {
                            new PortBinding
                            {
                                HostIP = "0.0.0.0",
                                HostPort = image.ExternalPort.ToString()
                            }
                        }
                    }
                }
                },
                ExposedPorts = new Dictionary<string, EmptyStruct>()
            };

            var environmentalVariablesList = new List<string>();

            foreach (var variable in image.Variables)
            {
                var variableString = variable.Name + "=" + variable.Value;
                environmentalVariablesList.Add(variableString);
            }

            if (image.ImageType == ImageType.Application)
            {
                if (applicationVariables != null)
                {
                    foreach (var variable in applicationVariables)
                    {
                        var variableString = variable.Name + "=" + variable.Value;
                        environmentalVariablesList.Add(variableString);
                    }
                }
            }

            startParamters.Env = environmentalVariablesList;
            startParamters.ExposedPorts.Add(image.InternalPort + "/tcp", default(EmptyStruct));

            var container = await _dockerAppClient.Containers.CreateContainerAsync(startParamters);

            bool task = await _dockerAppClient.Containers.StartContainerAsync(container.ID, new ContainerStartParameters(), CancellationToken.None);

            if (task)
                return container.ID;

            throw new Exception("Error Starting Benchmark Container: " + container.ID);
        }

        private async Task RemoveContainersFromHost(DockerClient client)
        {
            IList<ContainerListResponse> containers = await client.Containers.ListContainersAsync(new ContainersListParameters { All = true });

            foreach (var container in containers)
            {
                await client.Containers.StopContainerAsync(container.ID, new ContainerStopParameters(), CancellationToken.None);
                await client.Containers.RemoveContainerAsync(container.ID, new ContainerRemoveParameters(), CancellationToken.None);
            }
        }

        private async Task<string> LaunchBenchmarkToHost(BenchmarkExperiment appTest)
        {
            Guard.Against.Null(appTest, nameof(appTest));

            if (appTest.BenchmarkHost == null)
                throw new NullReferenceException("Benchmark Host is null for Application Test");

            if (appTest.Application.BenchmarkingImage == null)
                throw new NullReferenceException("BenchmarkingImage is null for Application.");

            if (appTest.BenchmarkHost.UseHttpAuthentication)
                CreateBasicHttpClient(appTest.BenchmarkHost.HostName, appTest.BenchmarkHost.PortNumber, appTest.BenchmarkHost.UserName, appTest.BenchmarkHost.Password, out _dockerBenchmarkClient);
            else
                CreateClientNoAuthentication(appTest.BenchmarkHost.HostName, appTest.BenchmarkHost.PortNumber, out _dockerBenchmarkClient);

            await RemoveContainersFromHost(_dockerBenchmarkClient);
            await PullImageOntoHost(appTest.Application.BenchmarkingImage, HostType.Benchmark);

            Uri uri = new Uri(_hostSettings.CurrentHost);
            uri.SetPort(_hostSettings.CurrentPort);
            //Get Hostname and Port for Jmeter Parameters.
            string testPlanUrl = BuildJMXPath(uri.AbsoluteUri, appTest.Id);

            string uriHost = uri.Host;

            var environmentalVariablesList = new List<string>();

            foreach (var variable in appTest.Application.BenchmarkingImage.Variables)
            {
                var variableString = variable.Name + "=" + variable.Value;
                environmentalVariablesList.Add(variableString);
            }

            environmentalVariablesList.Add("JMETER_TEST_PLAN=" + testPlanUrl);
            environmentalVariablesList.Add("JMETER_TEST_OUTPUT=" + appTest.Id.ToString());

            return await CreateContainerOntoHost(appTest.Application.BenchmarkingImage, environmentalVariablesList, _dockerBenchmarkClient);
        }

        private async Task<string> CreateContainerOntoHost(DockerImage app, List<string> environmentalVariablesList, DockerClient client)
        {
            var startParamters = new CreateContainerParameters
            {
                Name = app.DockerFriendlyName,
                Image = app.FullImageTag,
                Env = environmentalVariablesList
            };

            var container = await client.Containers.CreateContainerAsync(startParamters);

            bool task = await client.Containers.StartContainerAsync(container.ID, new ContainerStartParameters(), CancellationToken.None);

            if (task)
                return container.ID;

            throw new Exception("Error Starting Benchmark Container: " + container.ID);
        }

        private void CreateBasicHttpClient(string hostname, int port, string userId, string password, out DockerClient client)
        {
            if (port == 0) port = 2376;

            Guard.Against.NullOrEmpty(hostname, nameof(hostname));
            Guard.Against.NullOrEmpty(userId, nameof(userId));
            Guard.Against.NullOrEmpty(password, nameof(password));
            Guard.Against.OutOfRange(port, nameof(port), 80, 65000);

            _logger.LogInformation("Basic HTTP Authentication to DockerClient");

            var credentials = new BasicAuthCredentials(userId, password);
            var config = new DockerClientConfiguration(new Uri("tcp://" + hostname + ":" + port), credentials);
            client = config.CreateClient();
        }

        private void CreateClientNoAuthentication(string hostname, int port, out DockerClient client)
        {
            if (port == 0) port = 2376;

            Guard.Against.NullOrEmpty(hostname, nameof(hostname));
            Guard.Against.OutOfRange(port, nameof(port), 80, 65000);

            client = new DockerClientConfiguration(new Uri("tcp://" + hostname + ":" + port)).CreateClient();
        }

        private async Task ProcessDockerStats(Guid benchmarkExperimentId)
        {
            Guard.Against.GuidNullOrDefault(benchmarkExperimentId, nameof(benchmarkExperimentId));

            var benchmarkExperiment = _appTestRepo.GetById(benchmarkExperimentId);

            Guard.Against.Null(benchmarkExperiment, nameof(benchmarkExperiment));

            var dockerStatsList = _containerMetricRepo.FindBy(c => c.BenchmarkExperiment == benchmarkExperiment).ToList();

            //TODO
            benchmarkExperiment.CreateBenchmarkStats(dockerStatsList);

            await _appTestRepo.UpdateAsync(benchmarkExperiment);
        }

        private async Task insertContainerMetric(BenchmarkExperiment appTest)
        {
            var metricStat = new ContainerMetric();

            IProgress<ContainerStatsResponse> progress = new Progress<ContainerStatsResponse>(stats =>
            {
                //var json = JsonConvert.SerializeObject(stats);

                metricStat.ContainerId = appTest.AppContainerId;
                metricStat.ContainerName = appTest.DockerFriendlyName;
                metricStat.DateTimeCreated = DateTime.UtcNow;
                metricStat.DockerDateTimestamp = stats.Read;
                //JsonDockerResponse = json,
                metricStat.BenchmarkExperiment = appTest;
                metricStat.MemoryLimit = stats.MemoryStats.Limit;
                metricStat.MemoryUsage = stats.MemoryStats.Usage;

                metricStat.SetCPUPercentage(stats.CPUStats, stats.PreCPUStats);
                metricStat.CalculateNetworks(stats.Networks);
                metricStat.CalculateBlockIO(stats.BlkioStats);

                //testList.Add(metricStat);
            });

            await _dockerAppClient.Containers.GetContainerStatsAsync(appTest.AppContainerId, new ContainerStatsParameters { Stream = false }, progress, CancellationToken.None);
            await _containerMetricRepo.AddAsync(metricStat);
        }

        public bool CaptureDockerStats(BenchmarkExperiment appTest)
        {
            throw new NotImplementedException();
        }

        #endregion
    }


}
