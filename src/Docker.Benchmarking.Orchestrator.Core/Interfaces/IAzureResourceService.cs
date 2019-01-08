using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using Microsoft.Azure.Management.Compute.Fluent.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Docker.Benchmarking.Orchestrator.Core.Entities;

namespace Docker.Benchmarking.Orchestrator.Core.Interfaces
{
    public interface IAzureResourceService
    {
        Task DeleteResources(Guid azureHostId);

        IEnumerable<Region> GetRegions();

        Task<string> CreateVMForBenchmark(Guid AzureHost);

        Task<IEnumerable<VirtualMachineSizeInner>> GetVMSizes(string location, Guid credentialsId);

        Task<string> TestCreateResources(string resourceName, string region, string deploymentJson, string deploymentParameters, Guid credentialsId);

        Task DeleteResourcesByResourceName(string resourceName, Guid credentialsId);

        Task<string> GetIpAddressOfNewDeployment(string resourceName, Guid credentialsId);

        Task<bool> DeployImageToAzureHost(string resourceName, Guid credentialsId, Guid dockerImageId);
    }
}
