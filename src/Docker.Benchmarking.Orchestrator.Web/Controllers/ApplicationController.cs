using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Docker.Benchmarking.Orchestrator.Core.Commands;
using Docker.Benchmarking.Orchestrator.Core.Entities;
using Docker.Benchmarking.Orchestrator.Core.Interfaces;
using Docker.Benchmarking.Orchestrator.Web.Extensions;
using Docker.Benchmarking.Orchestrator.Web.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace Docker.Benchmarking.Orchestrator.Web.Controllers
{
    public class ApplicationController : BaseController
    {
        public ApplicationController(IMapper mapper, IMediator mediatr) : base(mapper, mediatr) { } 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == Guid.Empty)
                return NotFound();

            var application = await _mediatr.Send(new GetEntityCommand<Application>(id));

            if (application == null)
                return NotFound();

            var applicationViewModel = _mapper.Map<ApplicationDetailsViewModel>(application);

            return View(applicationViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var applications = await _mediatr.Send(new ListEntitiesCommand<Application>());
            var applicationViewModel = _mapper.Map<IEnumerable<ApplicationDetailsViewModel>>(applications);

            return View(applicationViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var viewModel = new AddApplicationViewModel
            {
                ApplicationImages = await DockerApplicationImagesSelectList(),
                BenchmarkingImages = await DockerBenchmarkImagesSelectList(),
                ApacheTestFiles = await ApacheTestFilesSelectList(),
                DatabaseImages = await DockerDatabaseImagesSelectList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm]AddApplicationViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

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

            ModelState.Clear();

            return View(viewModel).WithSuccess("Success", "New Application Successfully Added. Application Id: " + model.Id);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> IsNameUnique(string name)
        {
            var result = await _mediatr.Send(new FindEntityByNameCommand<Application>(name));
            return Json(!result);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty) return BadRequest("id is null/empty");

            var model = await _mediatr.Send(new GetEntityCommand<Application>(id));

            if (model == null)
                return NotFound("No application found for given Id");

            var viewModel = _mapper.Map<EditApplicationViewModel>(model);
            viewModel.ApplicationImage = model.ApplicationImage.Id;
            viewModel.BenchmarkingImage = model.BenchmarkingImage.Id;

            if(model.TestFile != null)
                viewModel.ApacheTestFileId = model.TestFile.Id;

            if (model.DatabaseImage != null)
                viewModel.DatabaseImage = model.DatabaseImageId;

            viewModel.ApplicationImages = await DockerApplicationImagesSelectList();
            viewModel.BenchmarkingImages = await DockerBenchmarkImagesSelectList();
            viewModel.ApacheTestFiles = await ApacheTestFilesSelectList();
            viewModel.DatabaseImages = await DockerDatabaseImagesSelectList();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm]EditApplicationViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

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

            return RedirectToAction("List");
        }
    }
}