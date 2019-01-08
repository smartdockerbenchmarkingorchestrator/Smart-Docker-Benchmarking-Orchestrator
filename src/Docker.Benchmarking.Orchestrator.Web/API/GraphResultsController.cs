using Ardalis.GuardClauses;
using AutoMapper;
using CsvHelper;
using Docker.Benchmarking.Orchestrator.Core.Commands;
using Docker.Benchmarking.Orchestrator.Core.Entities;
using Docker.Benchmarking.Orchestrator.Core.Interfaces;
using Docker.Benchmarking.Orchestrator.Web.APIModels;
using Docker.Benchmarking.Orchestrator.Web.Filters;
using Docker.Benchmarking.Orchestrator.Web.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static Docker.Benchmarking.Orchestrator.Web.ViewModels.BenchmarkTestItemViewModel;

namespace Docker.Benchmarking.Orchestrator.Web.API
{
    public class GraphResultsController : BaseAPIController
    {
        public GraphResultsController(IMapper mapper, IMediator mediatr) : base(mapper, mediatr)
        {
        }

        [HttpGet("GetContainerMetricsForBenchmark")]
        public async Task<IActionResult> GetContainerMetricsForBenchmark(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Id is null/empty");

            var application = await _mediatr.Send(new GetEntityCommand<BenchmarkExperiment>(id));

            if (application == null)
                return NotFound("No result found for given id");

            var apiModel = GetDockerApiStats(application);

            return Ok(apiModel);
        }

        [HttpPost("ExportResultsToCsv")]
        public async Task<IActionResult> ExportResultsToCsv(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Id is null/empty");

            var application = await _mediatr.Send(new GetEntityCommand<BenchmarkExperiment>(id));

            if (application == null)
                return NotFound("No result found for given id");

            var apiModel = GetDockerApiStats(application);

            var result = WriteCsvToMemoryDocker(apiModel);
            var memoryStream = new MemoryStream(result);
            return new FileStreamResult(memoryStream, "text/csv") { FileDownloadName = id + ".csv" };
        }

        [HttpPost("ExportWebServerResultsToCsv")]
        public async Task<IActionResult> ExportWebServerResultsToCsv(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Id is null/empty");

            var application = await _mediatr.Send(new GetEntityCommand<BenchmarkExperiment>(id));

            if (application == null)
                return NotFound("No result found for given id");

            var apiModel = GetWebServerBenchmarkData(application);

            var result = WriteCsvToMemoryWebServer(apiModel);
            var memoryStream = new MemoryStream(result);
            return new FileStreamResult(memoryStream, "text/csv") { FileDownloadName = id + "-webserver.csv" };
        }

        [HttpPost("GetContainerMetricsForBenchmarkComparison")]
        public async Task<IActionResult> GetContainerMetricsForBenchmarkComparison(string[] ids)
        {
            var guids = ids.Select(Guid.Parse).ToArray();

            var applications = await _mediatr.Send(new GetEntitiesCommand<BenchmarkExperiment>(guids));

            var list = new List<DockerComparisonResultsViewModel>();

            foreach(var app in applications)
            {
                var apiModel = GetDockerApiStats(app);

                list.Add(new DockerComparisonResultsViewModel
                {
                    Id = app.Id,
                    Results = apiModel
                });
            }

            return Ok(list);
        }

        private IEnumerable<DockerStatsApiModel> GetDockerApiStats(BenchmarkExperiment application)
        {
            var benchmarkStartTime = application.StartedAt.UtcDateTime;

            var benchmarkEndTime = application.CompletedAt.UtcDateTime;

            var containerMetrics = application.ContainerMetrics;

            var cpuMetrics = containerMetrics.Where(c => (c.DateTimeCreated.UtcDateTime >= benchmarkStartTime && c.DateTimeCreated.UtcDateTime <= benchmarkEndTime)).ToList();

            var apiModel = _mapper.Map<IEnumerable<DockerStatsApiModel>>(cpuMetrics).OrderBy(c => c.DateTimeUtc);

            return apiModel;
        }

        private IEnumerable<BenchmarkTestItemViewModel> GetWebServerBenchmarkData(BenchmarkExperiment application)
        {
            var webserverData = application.TestResults;

            var apiModel = _mapper.Map<IEnumerable<BenchmarkTestItemViewModel>>(webserverData).OrderBy(c => c.Timestamp);

            return apiModel;
        }

        private byte[] WriteCsvToMemoryDocker(IEnumerable<DockerStatsApiModel> records)
        {
            using (var memoryStream = new MemoryStream())
            using (var streamWriter = new StreamWriter(memoryStream))
            using (var csvWriter = new CsvWriter(streamWriter))
            {
                csvWriter.WriteRecords(records);
                streamWriter.Flush();
                return memoryStream.ToArray();
            }
        }

        private byte[] WriteCsvToMemoryWebServer(IEnumerable<BenchmarkTestItemViewModel> records)
        {
            using (var memoryStream = new MemoryStream())
            using (var streamWriter = new StreamWriter(memoryStream))
            using (var csvWriter = new CsvWriter(streamWriter))
            {
                csvWriter.Configuration.RegisterClassMap<MyClassMap>();

                try
                {
                    csvWriter.WriteRecords(records);
                    streamWriter.Flush();
                }
                catch (Exception)
                {
                    // ignored
                }

                return memoryStream.ToArray();
            }
        }
    }
}
