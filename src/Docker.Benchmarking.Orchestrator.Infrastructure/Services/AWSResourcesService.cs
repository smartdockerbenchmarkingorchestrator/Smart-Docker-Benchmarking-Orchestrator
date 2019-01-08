using Amazon.CloudFormation;
using Amazon.CloudFormation.Model;
using Ardalis.GuardClauses;
using Docker.Benchmarking.Orchestrator.Core.Entities;
using Docker.Benchmarking.Orchestrator.Core.Interfaces;
using Hangfire;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Infrastrcture.Services
{
    public class AWSResourcesService : IAWSResourcesService, IDisposable
    {
        private AmazonCloudFormationClient _client;
        private readonly IRepository<AWSCredentials> _credsRepo;
        private readonly string DockerHostParameterName = "DockerHostIp";
        private readonly IDockerRemoteService _dockerRemoteService;

        public AWSResourcesService(IRepository<AWSCredentials> credsRepo, IDockerRemoteService dockerRemoteService)
        {
            _credsRepo = credsRepo;
            _dockerRemoteService = dockerRemoteService;
        }

        public async Task<string> EstimateCostsForTemplate(string json, List<Parameter> parameters, Core.Entities.AWSCredentials credentials)
        {
            Guard.Against.JsonIsNotValid(json, nameof(json));
            Guard.Against.Null(parameters, nameof(parameters));
            Guard.Against.Null(credentials, nameof(credentials));

            CreateClient(credentials);
            Guard.Against.Null(_client, nameof(_client));

            var templateCosts = await _client.EstimateTemplateCostAsync(new EstimateTemplateCostRequest
            {
                TemplateBody = json,
                Parameters = parameters
            });

            if(templateCosts.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                return templateCosts.Url;
            }

            throw new Exception("Error getting template costs, HTTPStatusCode: " + templateCosts.HttpStatusCode);
        }

        [AutomaticRetry(Attempts = 0)]
        public async Task<string> CreateDockerVM(string stackName, string json, List<Parameter> parameters, Guid credentialsId)
        {
            Guard.Against.NullOrEmpty(stackName, nameof(stackName));
            Guard.Against.JsonIsNotValid(json, nameof(json));
            Guard.Against.Null(parameters, nameof(parameters));
            Guard.Against.GuidNullOrDefault(credentialsId, nameof(credentialsId));

            var creds = await _credsRepo.GetByIdAsync(credentialsId);
            Guard.Against.Null(creds, nameof(creds));

            CreateClient(creds);

            Guard.Against.Null(_client, nameof(_client));

            var request = new CreateStackRequest
            {
                StackName = stackName,
                TemplateBody = json,
                Parameters = parameters,
                Capabilities = new List<string>
                {
                    "CAPABILITY_IAM",
                }
            };

            var createResponse = await _client.CreateStackAsync(request);

            if (createResponse.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                bool hascompleted = false;
                int maxTries = 200;
                int tries = 0;

                while (hascompleted == false)
                {
                    if(maxTries == tries)
                    {
                        throw new Exception("StackStatus Checking Failed");
                    }

                    var results = await _client.DescribeStacksAsync(new DescribeStacksRequest
                    {
                        StackName = stackName
                    });

                    if (results.HttpStatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var stack = results.Stacks.SingleOrDefault(c => c.StackName == stackName);
                        if (stack != null)
                        {
                            if (stack.StackStatus == StackStatus.CREATE_COMPLETE)
                            {
                                return stack.Outputs.SingleOrDefault(c => c.OutputKey == DockerHostParameterName).OutputValue;
                            }

                            if(stack.StackStatus == StackStatus.CREATE_FAILED)
                            {
                                throw new Exception("Stack Failed Due To: " + stack.StackStatusReason);
                            }

                            if(stack.StackStatus == StackStatus.ROLLBACK_COMPLETE)
                            {
                                throw new Exception("Stack Failed Due To: " + stack.StackStatusReason);
                            }

                            await Task.Delay(10000);
                            tries++;
                        }
                    }
                }
            }

            //TODO: Add Logging
            throw new Exception("Error creating stack, HTTPStatusCode: " + createResponse.HttpStatusCode);
        }

        public async Task<List<string>> ValidateDeploymentScript(string json, Core.Entities.AWSCredentials credentials)
        {
            Guard.Against.JsonIsNotValid(json, nameof(json));
            Guard.Against.Null(credentials, nameof(credentials));

            CreateClient(credentials);
            Guard.Against.Null(_client, nameof(_client));

            var validateTemplate = await _client.ValidateTemplateAsync(new ValidateTemplateRequest
            {
                TemplateBody = json,

            });

            if (validateTemplate.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                return validateTemplate.Capabilities;
            }

            //TODO: Add Logging
            throw new Exception("Error validating template, HTTPStatusCode: " + validateTemplate.HttpStatusCode);
        }

        public async Task<bool> DeleteStack(string stackName, Guid credentialsId)
        {
            Guard.Against.NullOrEmpty(stackName, nameof(stackName));
            Guard.Against.GuidNullOrDefault(credentialsId, nameof(credentialsId));

            var creds = await _credsRepo.GetByIdAsync(credentialsId);
            Guard.Against.Null(creds, nameof(creds));

            CreateClient(creds);
            Guard.Against.Null(_client, nameof(_client));

            var stackRessponse = await _client.DeleteStackAsync(new DeleteStackRequest
            {
                StackName = stackName,
            });

            if(stackRessponse.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }

            throw new Exception("Error deleting stack, HTTPStatusCode: " + stackRessponse.HttpStatusCode);
        }

        public void Dispose()
        {
            if (_client != null) _client.Dispose();
        }

        private void CreateClient(Core.Entities.AWSCredentials credentials)
        {
            Guard.Against.Null(credentials, nameof(credentials));
            _client = new AmazonCloudFormationClient(credentials.AccessKeyId, credentials.SecretKey, credentials.AWSEndPoint);
        }

        public async Task<string> GetIpForDeployedStack(string stackName, Guid credentialsId)
        {
            Guard.Against.NullOrEmpty(stackName, nameof(stackName));
            Guard.Against.GuidNullOrDefault(credentialsId, nameof(credentialsId));

            var creds = await _credsRepo.GetByIdAsync(credentialsId);
            Guard.Against.Null(creds, nameof(creds));

            CreateClient(creds);
            Guard.Against.Null(_client, nameof(_client));

            var results = await _client.DescribeStacksAsync(new DescribeStacksRequest
            {
                StackName = stackName
            });

            int tries = 0;
            int maxTries = 100;

            while(tries <= maxTries)
            {
                if (results.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    var stack = results.Stacks.SingleOrDefault(c => c.StackName == stackName);
                    if (stack != null)
                    {
                        if (stack.StackStatus == StackStatus.CREATE_COMPLETE)
                        {
                            return stack.Outputs.SingleOrDefault(c => c.OutputKey == DockerHostParameterName).OutputValue;
                        }

                    }
                }

                tries++;
                await Task.Delay(10000);
            }

           
            throw new Exception("Error Getting Ipaddress");
        }

        public async Task<bool> DeployImageToVM(string stackName, Guid credentialsId, Guid dockerImageId)
        {
            Guard.Against.NullOrEmpty(stackName, nameof(stackName));
            Guard.Against.GuidNullOrDefault(credentialsId, nameof(credentialsId));
            Guard.Against.GuidNullOrDefault(dockerImageId, nameof(dockerImageId));

            var ipAddress = await GetIpForDeployedStack(stackName, credentialsId);

            return await _dockerRemoteService.DeployImageToHost(ipAddress, 2376, dockerImageId);
        }
    }
}
