using AutoMapper;
using Docker.Benchmarking.Orchestrator.Core.Commands;
using Docker.Benchmarking.Orchestrator.Core.Entities;
using Docker.Benchmarking.Orchestrator.Web.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Web.Controllers
{
    public class AzureController : BaseController
    {
        public AzureController(IMapper mapper, IMediator mediatr) : base(mapper, mediatr) { }

        public IActionResult Add()
        {
            var viewModel = new AddAzureTemplateViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm]AddAzureTemplateViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            var model = _mapper.Map<AzureVMTemplate>(viewModel);

            await _mediatr.Send(new CreateCommand<AzureVMTemplate>(model));

            return View(viewModel);
        }


        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty)
                return NotFound();

            var azureVm = await _mediatr.Send(new GetEntityCommand<AzureVMTemplate>(id));

            if (azureVm == null) return NotFound();

            var viewModel = _mapper.Map<EditAzureTemplateViewModel>(azureVm);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm]EditAzureTemplateViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            var model = _mapper.Map<AzureVMTemplate>(viewModel);

            await _mediatr.Send(new UpdateCommand<AzureVMTemplate>(model));

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> IsVmSizeUnique(string vmSize, Guid id = default(Guid))
        {
            var result = await _mediatr.Send(new AzureVMSizeValidCommand(vmSize, id));
            return Json(!result);
        }

        public IActionResult List()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Template(Guid id)
        {
            if (id == Guid.Empty)
                return NotFound();

            var azureVm = await _mediatr.Send(new GetEntityCommand<AzureVMTemplate>(id));
            var responseValue = JsonConvert.DeserializeObject(azureVm.Template);

            return Json(responseValue);
        }

        [HttpGet]
        public async Task<IActionResult> Parameters(Guid id)
        {
            if (id == Guid.Empty)
                return NotFound();

            var azureVm = await _mediatr.Send(new GetEntityCommand<AzureVMTemplate>(id));

            var responseValue = JsonConvert.DeserializeObject(azureVm.ParametersDefault);

            return Json(responseValue);
        }

        public async Task<IActionResult> Test()
        {
            var viewModel = new TestAzureResourcesViewModel();

            viewModel.AzureRegions = await AzureRegionsSelectList();
            viewModel.Credentials = await AzureCredentialsSelectList();
            viewModel.DockerImages = await DockerApplicationImagesSelectList();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Test([FromForm]TestAzureResourcesViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            var ipAddress = await _mediatr.Send(new AzureTestCreateResourcesCommand(viewModel.ResourceName, viewModel.AzureRegion, viewModel.DeploymentJson, viewModel.ParametersJson, viewModel.CredentialsId, viewModel.DockerImage));

            ViewData["IPAddress"] = ipAddress;
            viewModel.IpAddress = ipAddress;

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteResources(string resourceName, Guid credentialsId)
        {
            if (string.IsNullOrEmpty(resourceName))
                return BadRequest();

            if (credentialsId == Guid.Empty) return BadRequest();

            await _mediatr.Send(new AzureDeleteResourcesByNameCommand(resourceName, credentialsId));

            return Ok();
        }
    }
}