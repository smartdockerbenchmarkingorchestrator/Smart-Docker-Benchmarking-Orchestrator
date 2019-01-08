using AutoMapper;
using Docker.Benchmarking.Orchestrator.Core.Commands;
using Docker.Benchmarking.Orchestrator.Core.Entities;
using Docker.Benchmarking.Orchestrator.Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Web.API
{
    public class ResultsController : BaseAPIController
    {
        private readonly ILogger _logger;
        private readonly IBenchmarkResultsService _resultsService;
        public ResultsController(IMediator mediatr, IMapper mapper, ILogger<ResultsController> logger, IBenchmarkResultsService resultsService) : base(mapper, mediatr)
        {
            _logger = logger;
            _resultsService = resultsService;
        }

        //public Task<HttpResponseMessage> TestResults()

        //https://docs.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads?view=aspnetcore-2.1
        [HttpPost("UploadFiles"), DisableRequestSizeLimit]
        public async Task<IActionResult> PostFile(IFormFile formFile)
        {
            try
            {
                if (formFile == null) return BadRequest("formFile is empty.");

                if (Path.GetExtension(formFile.FileName).ToLower() != ".csv") return BadRequest("File must be CSV.");

                if (formFile.Length == 0) return BadRequest("File length is 0.");

                var filePath = await _mediatr.Send(new BenchmarkExperimentResultsUploadCommand(formFile));


                if (string.IsNullOrEmpty(filePath))
                    return BadRequest("Cannot uploaded file");

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        //https://docs.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads?view=aspnetcore-2.1
        [HttpPost("PostJmeterTest"), DisableRequestSizeLimit]
        public async Task<IActionResult> PostJmeterTest(IFormFile formFile, Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    return BadRequest("applicationId is empty.");

                if (formFile == null)
                    return BadRequest("formFile is empty.");

                if (formFile.Length == 0) return BadRequest("formFile length is empty.");

                var success = await _mediatr.Send(new ProcessBenchmarkResultsWithFileCommand(formFile, id));

                return Ok(success);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);

                return BadRequest(ex.Message);
            }

        }

        [HttpGet("HasExperimentCompleted")]
        public async Task<IActionResult> HasExperimentCompleted(Guid id)
        {
            if (id == Guid.Empty)
                return NotFound();

            var appTest = await _mediatr.Send(new GetEntityCommand<BenchmarkExperiment>(id));

            if (appTest == null) return NotFound();

            return Ok(appTest.Completed);
        }

        [HttpPost("StartExperiment")]
        public async Task<IActionResult> StartExperiment(Guid id)
        {
            if (id == Guid.Empty)
                return NotFound();

            var appTest = await _mediatr.Send(new GetEntityCommand<BenchmarkExperiment>(id));

            if (appTest == null) return NotFound();

            if (appTest.Started) return Ok();

            //TODO
            var started = await _mediatr.Send(new StartBenchmarkExperimentStep2Command(appTest));

            return Ok(started);
        }

        [HttpPost("EndExperiment")]
        public async Task<IActionResult> EndExperiment(Guid id)
        {
            if (id == Guid.Empty)
                return NotFound();

            var appTest = await _mediatr.Send(new GetEntityCommand<BenchmarkExperiment>(id));

            if (appTest == null) return NotFound();

            //if (appTest.Completed) return Ok();

            //ToDo
            var completed = await _mediatr.Send(new StopBenchmarkingExperimentCommand(appTest));
            return Ok(completed);
        }

        [HttpPost("FixContainerMetrics")]
        public async Task<IActionResult> FixContainerMetrics(Guid id)
        {
            if (id == Guid.Empty)
                return NotFound();

            var completed = await _resultsService.FixBlockNetworkError(id);
            return Ok(completed);
        }
    }
}