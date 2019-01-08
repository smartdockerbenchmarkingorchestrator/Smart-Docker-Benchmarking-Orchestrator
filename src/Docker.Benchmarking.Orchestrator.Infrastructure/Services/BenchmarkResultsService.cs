using Ardalis.GuardClauses;
using Docker.Benchmarking.Orchestrator.Core.Entities;
using Docker.Benchmarking.Orchestrator.Core.Interfaces;
using Docker.Benchmarking.Orchestrator.Infrastrcture.Mappings;
using Hangfire;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyCsvParser;

namespace Docker.Benchmarking.Orchestrator.Infrastrcture.Services
{
    public class BenchmarkResultsService : IBenchmarkResultsService
    {
        private readonly IHostingEnvironment _env;
        private readonly IRepository<BenchmarkTestItem> _benchmarkTestItemService;
        private readonly IRepository<ContainerMetric> _containerMetricService;

        private readonly IRepository<BenchmarkExperiment> _benchmarkExperimentRepo;
        private readonly IAzureResourceService _azureResourceService;
        private readonly IBackgroundJobClient _backgroungJobClient;
        private readonly IRepository<CSVUpload> _csvUploadRepo;
        private readonly ILogger _logger;
        public BenchmarkResultsService(IRepository<BenchmarkTestItem> benchmarkTestItemService,
            IRepository<BenchmarkExperiment> benchmarkExperimentRepo,
            IAzureResourceService azureResourceService,
            IHostingEnvironment env, IBackgroundJobClient backgroungJobClient, IRepository<CSVUpload> csvUploadRepo, ILogger<BenchmarkResultsService> logger,
            IRepository<ContainerMetric> containerMetricService)
        {
            _benchmarkTestItemService = benchmarkTestItemService;
            _benchmarkExperimentRepo = benchmarkExperimentRepo;
            _azureResourceService = azureResourceService;
            _env = env;
            _backgroungJobClient = backgroungJobClient;
            _csvUploadRepo = csvUploadRepo;
            _logger = logger;
            _containerMetricService = containerMetricService;
        }

        private async Task ProcessResults(string filePath, Guid benchmarkExperimentId)
        {
            Guard.Against.GuidNullOrDefault(benchmarkExperimentId, nameof(benchmarkExperimentId));
            Guard.Against.NullOrEmpty(filePath, nameof(filePath));

            if (!filePath.ToLower().EndsWith(".csv"))
                throw new ArgumentException("File must be csv");

            var application = await _benchmarkExperimentRepo.GetByIdAsync(benchmarkExperimentId);
            Guard.Against.Null(application, nameof(application));

            if (application.JmeterResultsProcessed) return;

            CsvParserOptions csvParserOptions = new CsvParserOptions(true, ',');
            CsvApacheJmeterResultMapping csvMapper = new CsvApacheJmeterResultMapping();
            CsvParser<BenchmarkTestItem> csvParser = new CsvParser<BenchmarkTestItem>(csvParserOptions, csvMapper);

            var result = csvParser
                .ReadFromFile(filePath, Encoding.ASCII)
                .ToList();

            if (result.Count == 0)
                return;

            var benchmarkItemList = result.Where(x => x.IsValid).Select(c => c.Result).ToList();

            var ignoreThreadNames = application.TestFile.ThreadNamesToIgnore;

            //remove results with threadname to ignore

            if (ignoreThreadNames != null)
                benchmarkItemList = benchmarkItemList.Where(c => !ignoreThreadNames.Contains(c.Label)).ToList();


            //Exclude certain fields around experiment
            var excludeArray = new string[] { "Send Start Of Experiment", "Send End of Experiment Files", "Process End Of Experiment Operations", "Send End Of Experiment" };

            //list arrays are immutable
            benchmarkItemList = benchmarkItemList.Where(c => !excludeArray.Contains(c.Label)).ToList();

            foreach (var row in benchmarkItemList)
            {
                row.ConvertToTimestamp();
                row.BenchmarkExperiment = application;
            }

            //calculate stats calculation
            application.BenchmarkStatsCalculation(benchmarkItemList.ToList());
            application.JmeterResultsProcessed = true;

            await _benchmarkExperimentRepo.UpdateAsync(application);
            await _benchmarkTestItemService.AddRangeAsync(benchmarkItemList);

            //Delete AWS or Azure Resources if marked
            await SendDeleteResourcesRequests(application);
        }

        public async Task<bool> ProcessResultsWithFile(IFormFile formFile, Guid experiment)
        {
            Guard.Against.Null(formFile, nameof(formFile));
            Guard.Against.GuidNullOrDefault(experiment, nameof(experiment));

            var filePath = await SaveUploadFile(formFile);

            await ProcessResults(filePath, experiment);

            return true;
        }

        public async Task<string> SaveUploadFile(IFormFile formFile)
        {
            Guard.Against.Null(formFile, nameof(formFile));

            if (Path.GetExtension(formFile.FileName).ToLower() != ".csv") throw new Exception("File must be CSV.");

            var uploadsFilePath = Path.Combine(_env.WebRootPath, @"uploads/").ToLower();

            if (!Directory.Exists(uploadsFilePath))
            {
                Directory.CreateDirectory(uploadsFilePath);
            }

            //assign filePath
            var filePath = Path.Combine(uploadsFilePath + formFile.FileName);

            if (formFile.Length > 0)
            {
                //Save CSV File to CSV File Table

                _logger.LogInformation(filePath);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    formFile.CopyTo(stream);
                }

                try
                {
                    string csvBase64 = await formFileToBase64(formFile);

                    if (csvBase64 != null)
                    {
                        var csvUpload = new CSVUpload
                        {
                            Name = formFile.FileName,
                            CSVResultsFile = csvBase64
                        };

                        await _csvUploadRepo.AddAsync(csvUpload);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.Message);
                }

                return filePath;

            }
            else
            {
                throw new Exception("No Uploaded File To Save.");
            }

            throw new Exception("Can't Upload File");
        }

        #region private

        private async Task SendDeleteResourcesRequests(BenchmarkExperiment experiment)
        {
            Guard.Against.Null(experiment, nameof(experiment));

            if (experiment.Host.AzureHost != null)
            {
                if (experiment.Host.AzureHost.DestroyResourcesAfterBenchmark)
                {
                    experiment.Host.Active = false;
                    _backgroungJobClient.Enqueue<IAzureResourceService>(c => c.DeleteResources(experiment.Host.AzureHost.Id));
                }
            }

            if (experiment.Host.AWSHost != null)
            {
                if (experiment.Host.AWSHost.DestroyResourcesAfterBenchmark)
                {
                    experiment.Host.Active = false;
                    _backgroungJobClient.Enqueue<IAWSHostService>(c => c.DeleteResourcesAfterExperiment(experiment.Host.AWSHost.Id));
                }
            }

            if (experiment.BenchmarkHost.AzureHost != null)
            {
                if (experiment.BenchmarkHost.AzureHost.DestroyResourcesAfterBenchmark)
                {
                    experiment.BenchmarkHost.Active = false;
                    _backgroungJobClient.Enqueue<IAzureResourceService>(c => c.DeleteResources(experiment.BenchmarkHost.AzureHost.Id));
                }
            }

            if (experiment.BenchmarkHost.AWSHost != null)
            {
                if (experiment.BenchmarkHost.AWSHost.DestroyResourcesAfterBenchmark)
                {
                    experiment.BenchmarkHost.Active = false;
                    _backgroungJobClient.Enqueue<IAWSHostService>(c => c.DeleteResourcesAfterExperiment(experiment.BenchmarkHost.AWSHost.Id));
                }
            }

            if (experiment.DatabaseHost != null)
            {
                if (experiment.DatabaseHost.AzureHost != null)
                {
                    if (experiment.DatabaseHost.AzureHost.DestroyResourcesAfterBenchmark)
                    {
                        experiment.DatabaseHost.Active = false;
                        _backgroungJobClient.Enqueue<IAzureResourceService>(c => c.DeleteResources(experiment.DatabaseHost.AzureHost.Id));
                    }
                }

                if (experiment.DatabaseHost.AWSHost != null)
                {
                    if (experiment.DatabaseHost.AWSHost.DestroyResourcesAfterBenchmark)
                    {
                        experiment.DatabaseHost.Active = false;
                        _backgroungJobClient.Enqueue<IAWSHostService>(c => c.DeleteResourcesAfterExperiment(experiment.DatabaseHost.AWSHost.Id));
                    }
                }
            }

            await _benchmarkExperimentRepo.UpdateAsync(experiment);
        }

        private async Task saveExperimentCSVToDatabase(IFormFile formFile, BenchmarkExperiment experiment)
        {
            Guard.Against.Null(formFile, nameof(formFile));
            Guard.Against.Null(experiment, nameof(experiment));

            string csvBase64 = await formFileToBase64(formFile);

            if (csvBase64 != null)
            {
                experiment.CSVResultsFile = csvBase64;
                await _benchmarkExperimentRepo.UpdateAsync(experiment);
            }

            throw new Exception("Error cannot save file to database.");
        }

        private async Task<string> formFileToBase64(IFormFile formFile)
        {
            Guard.Against.Null(formFile, nameof(formFile));

            string csvBase64 = null;
            if (formFile.Length > 0)
            {
                //Save 

                using (var ms = new MemoryStream())
                {
                    await formFile.CopyToAsync(ms);
                    var fileBytes = ms.ToArray();
                    csvBase64 = Convert.ToBase64String(fileBytes);
                    // act on the Base64 data
                }
            }

            return csvBase64;
        }

        public async Task<bool> FixBlockNetworkError(Guid experiment)
        {
            Guard.Against.GuidNullOrDefault(experiment, nameof(experiment));

            var application = await _benchmarkExperimentRepo.GetByIdAsync(experiment);

            var containerMetrics = application.ContainerMetrics.OrderByDescending(c => c.DateTimeCreated).ToList();

            for (int idx = 0; idx < containerMetrics.Count() - 1; idx++)
            {
                if (idx < containerMetrics.Count() - 1)
                {
                    var previousMetric = containerMetrics[idx + 1];

                    var currentMetric = containerMetrics[idx];
                    currentMetric.CalculateNetworksFix(previousMetric);
                    currentMetric.CalculateBlockIOFix(previousMetric);

                    await _containerMetricService.UpdateAsync(currentMetric);

                    //index++;
                }
            }

            application.CreateBenchmarkStats(containerMetrics);

            await _benchmarkExperimentRepo.UpdateAsync(application);

            return true;
        }

        #endregion
    }
}
