using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Core.Interfaces
{
    public interface IAWSResourcesService
    {
        //Returns IP Address of Instance
        Task<string> CreateDockerVM(string stackName, string json, List<Amazon.CloudFormation.Model.Parameter> parameters, Guid credentialsId);

        //returns capabilities required
        Task<List<string>> ValidateDeploymentScript(string json, Entities.AWSCredentials credentials);

        //returns url for checking estimate billing
        Task<string> EstimateCostsForTemplate(string json, List<Amazon.CloudFormation.Model.Parameter> parameters, Entities.AWSCredentials credentials);

        Task<bool> DeleteStack(string stackName, Guid credentialsId);

        Task<string> GetIpForDeployedStack(string stackName, Guid credentialsId);

        Task<bool> DeployImageToVM(string stackName, Guid credentialsId, Guid dockerImageId);
    }
}
