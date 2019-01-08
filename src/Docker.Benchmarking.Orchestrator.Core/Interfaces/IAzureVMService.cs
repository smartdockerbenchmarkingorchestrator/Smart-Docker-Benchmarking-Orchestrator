using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Core.Interfaces
{
    public interface IAzureVMService
    {
        Task<bool> IsVMSizeUnique(string vmSize, Guid id = default(Guid));
    }
}
