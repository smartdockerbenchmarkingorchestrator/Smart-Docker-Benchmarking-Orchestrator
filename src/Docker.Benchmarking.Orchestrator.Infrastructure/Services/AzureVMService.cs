using Docker.Benchmarking.Orchestrator.Core.Entities;
using Docker.Benchmarking.Orchestrator.Core.Interfaces;
using System.Threading.Tasks;
using System.Linq;
using Ardalis.GuardClauses;
using System;

namespace Docker.Benchmarking.Orchestrator.Infrastrcture.Services
{
    public class AzureVMService : IAzureVMService
    {
        private readonly IRepository<AzureVMTemplate> _repo;
        public AzureVMService(IRepository<AzureVMTemplate> repo)
        {
            _repo = repo;
        }

        public async Task<bool> IsVMSizeUnique(string vmSize, Guid id = default(Guid))
        {
            Guard.Against.NullOrEmpty(vmSize, nameof(vmSize));

            if(id == default(Guid)) return await Task.FromResult(_repo.FindBy(c => c.VMSize.ToLower() == vmSize.ToLower()).Any());

            var model = await _repo.GetByIdAsync(id);

            if (model.VMSize.ToLower() == vmSize.ToLower()) return false;
            return await Task.FromResult(_repo.FindBy(c => c.VMSize.ToLower() == vmSize.ToLower()).Any());
        }
    }
}
