using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Core.Interfaces
{
    public interface IBenchmarkResultsService
    {
        Task<string> SaveUploadFile(IFormFile formFile);
        //Task ProcessResults(string filePath, Guid application);
        Task<bool> ProcessResultsWithFile(IFormFile formFile, Guid experiment);

        Task<bool> FixBlockNetworkError(Guid experiment);
    }
}
