using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Docker.Benchmarking.Orchestrator.Core.Commands;
using Docker.Benchmarking.Orchestrator.Core.Entities;
using Docker.Benchmarking.Orchestrator.Web.APIModels;
using Docker.Benchmarking.Orchestrator.Web.Filters;
using Docker.Benchmarking.Orchestrator.Web.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Docker.Benchmarking.Orchestrator.Web.API
{
    public class ApplicationAPIController : BaseAPIController
    {
        public ApplicationAPIController(IMapper mapper, IMediator mediatr) : base(mapper, mediatr)
        {

        }

        /// <summary>
        /// Get Details of Application
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Json(applicationViewModel)</returns>
        [HttpGet("Details")]
        public async Task<ActionResult<ApplicationDetailsAPIModel>> Details(Guid id)
        {
            if (id == Guid.Empty)
                return NotFound();

            var application = await _mediatr.Send(new GetEntityCommand<Application>(id));

            if (application == null)
                return NotFound();

            var applicationViewModel = _mapper.Map<ApplicationDetailsAPIModel>(application);

            return applicationViewModel;
        }

        [HttpGet("List")]
        public async Task<ActionResult<IEnumerable<ApplicationDetailsAPIModel>>> List()
        {
            var applications = await _mediatr.Send(new ListEntitiesCommand<Application>());
            var applicationViewModel = _mapper.Map<IEnumerable<ApplicationDetailsAPIModel>>(applications);

            return applicationViewModel.ToList();
        }


        [HttpPost("Add")]
        [ValidateModel]
        public async Task<IActionResult> Add(AddApplicationViewModel viewModel)
        {
            var model = _mapper.Map<Application>(viewModel);

            if (viewModel.ApplicationImage != Guid.Empty)
                model.ApplicationImage = await _mediatr.Send(new GetEntityCommand<DockerImage>(viewModel.ApplicationImage));

            if (viewModel.BenchmarkingImage != Guid.Empty)
                model.BenchmarkingImage = await _mediatr.Send(new GetEntityCommand<DockerImage>(viewModel.BenchmarkingImage));

            if (viewModel.ApacheTestFileId.HasValue)
                model.TestFile = await _mediatr.Send(new GetEntityCommand<ApacheJmeterTestFile>(viewModel.ApacheTestFileId.Value));

            if (viewModel.DatabaseImage.HasValue)
                model.DatabaseImage = await _mediatr.Send(new GetEntityCommand<DockerImage>(viewModel.DatabaseImage.Value));

            await _mediatr.Send(new CreateCommand<Application>(model));

            return Ok(model.Id);
        }

        [HttpPost("Edit")]
        [ValidateModel]
        public async Task<IActionResult> Edit(EditApplicationViewModel viewModel)
        {
            var model = await _mediatr.Send(new GetEntityCommand<Application>(viewModel.Id));

            var updateModel = _mapper.Map(viewModel, model);

            if (viewModel.ApplicationImage != Guid.Empty)
                updateModel.ApplicationImage = await _mediatr.Send(new GetEntityCommand<DockerImage>(viewModel.ApplicationImage));

            if (viewModel.BenchmarkingImage != Guid.Empty)
                updateModel.BenchmarkingImage = await _mediatr.Send(new GetEntityCommand<DockerImage>(viewModel.BenchmarkingImage));

            if (viewModel.ApacheTestFileId.HasValue)
            {
                updateModel.TestFile = await _mediatr.Send(new GetEntityCommand<ApacheJmeterTestFile>(viewModel.ApacheTestFileId.Value));
                updateModel.ApacheJmeterTestId = viewModel.ApacheTestFileId.Value;
            }

            if (viewModel.DatabaseImage.HasValue)
            {
                updateModel.DatabaseImage = await _mediatr.Send(new GetEntityCommand<DockerImage>(viewModel.DatabaseImage.Value));
                updateModel.DatabaseImageId = viewModel.DatabaseImage.Value;
            }

            await _mediatr.Send(new UpdateCommand<Application>(model));

            return Ok();
        }
    }
}
