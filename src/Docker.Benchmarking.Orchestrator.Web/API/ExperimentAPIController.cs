using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Docker.Benchmarking.Orchestrator.Core.Commands;
using Docker.Benchmarking.Orchestrator.Core.Entities;
using Docker.Benchmarking.Orchestrator.Web.Filters;
using Docker.Benchmarking.Orchestrator.Web.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Docker.Benchmarking.Orchestrator.Web.API
{
    public class ExperimentAPIController : BaseAPIController
    {
        public ExperimentAPIController(IMapper mapper, IMediator mediatr) : base(mapper, mediatr)
        {

        }

        [HttpPost("Add")]
        [ValidateModel]
        public async Task<IActionResult> Add(AddBenchmarkExperimentViewModel viewModel)
        {
            var model = _mapper.Map<BenchmarkExperiment>(viewModel);

            if (viewModel.Application != Guid.Empty)
                model.Application = await _mediatr.Send(new GetEntityCommand<Application>(viewModel.Application));

            if (viewModel.Host != Guid.Empty)
                model.Host = await _mediatr.Send(new GetEntityCommand<DockerHost>(viewModel.Host));

            if (viewModel.BenchmarkHost != Guid.Empty)
                model.BenchmarkHost = await _mediatr.Send(new GetEntityCommand<DockerHost>(viewModel.BenchmarkHost));

            if (viewModel.ApacheTestFileId != Guid.Empty)
                if (viewModel.ApacheTestFileId != null)
                    model.TestFile = await _mediatr.Send(new GetEntityCommand<ApacheJmeterTestFile>(viewModel.ApacheTestFileId.Value));

            model.ApacheJmeterTestId = viewModel.ApacheTestFileId;

            await _mediatr.Send(new CreateCommand<BenchmarkExperiment>(model));

            return Ok(model.Id);
        }

        [HttpPost("Edit")]
        [ValidateModel]
        public async Task<IActionResult> Edit([FromForm]EditBenchmarkExperimentViewModel viewModel)
        {
            var appTest = await _mediatr.Send(new GetEntityCommand<BenchmarkExperiment>(viewModel.Id));

            if (appTest == null)
                return BadRequest("No Experiment found for given Id");

            var model = _mapper.Map(viewModel, appTest);

            if (viewModel.Application != Guid.Empty)
                model.Application = await _mediatr.Send(new GetEntityCommand<Application>(viewModel.Application));

            if (viewModel.Host != Guid.Empty)
                model.Host = await _mediatr.Send(new GetEntityCommand<DockerHost>(viewModel.Host));

            if (viewModel.BenchmarkHost != Guid.Empty)
                model.BenchmarkHost = await _mediatr.Send(new GetEntityCommand<DockerHost>(viewModel.BenchmarkHost));

            if (viewModel.ApacheTestFileId != Guid.Empty)
                if (viewModel.ApacheTestFileId != null)
                    model.TestFile = await _mediatr.Send(new GetEntityCommand<ApacheJmeterTestFile>(viewModel.ApacheTestFileId.Value));

            model.ApacheJmeterTestId = viewModel.ApacheTestFileId;

            await _mediatr.Send(new UpdateCommand<BenchmarkExperiment>(model));

            return Ok();
        }

        [HttpGet("Details")]
        public async Task<ActionResult<StartBenchmarkExperimentViewModel>> Details(Guid id)
        {
            if (id == Guid.Empty) return BadRequest("Id is invalid");

            var experiment = await _mediatr.Send(new GetEntityCommand<BenchmarkExperiment>(id));

            if (experiment == null) return BadRequest("No application");

            var viewModel = _mapper.Map<StartBenchmarkExperimentViewModel>(experiment);

            return viewModel;
        }

        [HttpPost("StartApplicationBenchmark")]
        public async Task<IActionResult> StartApplicationBenchmark(Guid benchmarkExperimentId)
        {
            if (benchmarkExperimentId == Guid.Empty)
                return BadRequest("benchmarkExperimentId is empty");

            var benchmarkingStarted = await _mediatr.Send(new StartBenchmarkExperimentCommand(benchmarkExperimentId));

            return Ok(benchmarkingStarted);
        }

        [HttpGet("Results")]
        public async Task<ActionResult<BenchmarkTestViewModel>> Results(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("benchmarkExperimentId is empty");


            var experiment = await _mediatr.Send(new GetEntityCommand<BenchmarkExperiment>(id));

            if (experiment == null)
                return NotFound("No result found for given id");

            var viewModel = _mapper.Map<BenchmarkTestViewModel>(experiment);

            return viewModel;
        }


        [HttpPost("IsNameUnique")]
        public async Task<ActionResult<bool>> IsNameUnique(string name)
        {
            var unique = await _mediatr.Send(new FindEntityByNameCommand<BenchmarkExperiment>(name));
            return !unique;
        }

        [HttpPost("CreateAzureResources")]
        public async Task<ActionResult<bool>> CreateAzureResources(Guid id)
        {
            if (id == Guid.Empty) return BadRequest("id is empty");

            var jobId = await _mediatr.Send(new CreateResourcesForBenchmarkCommand(id));
            return true;
        }

        [HttpGet("ExperimentStatus")]
        public async Task<ActionResult<bool>> ExperimentStatus(Guid id)
        {
            if (id == Guid.Empty) return BadRequest("id is empty");

            var jobId = await _mediatr.Send(new CreateResourcesForBenchmarkCommand(id));
            return true;
        }
    }
}