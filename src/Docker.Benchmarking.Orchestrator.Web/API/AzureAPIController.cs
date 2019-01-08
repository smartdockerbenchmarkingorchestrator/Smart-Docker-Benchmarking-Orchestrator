using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Docker.Benchmarking.Orchestrator.Core.Commands;
using Docker.Benchmarking.Orchestrator.Core.Entities;
using Docker.Benchmarking.Orchestrator.Web.Filters;
using Docker.Benchmarking.Orchestrator.Web.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Docker.Benchmarking.Orchestrator.Web.API
{
    public class AzureAPIController : BaseAPIController
    {
        public AzureAPIController(IMapper mapper, IMediator mediatr) : base(mapper, mediatr)
        {

        }

        [ValidateModel]
        [HttpPost("Add")]
        public async Task<IActionResult> Add(AddAzureTemplateViewModel viewModel)
        {
            var model = _mapper.Map<AzureVMTemplate>(viewModel);
            await _mediatr.Send(new CreateCommand<AzureVMTemplate>(model));
            return Ok(model.Id);
        }

        [HttpGet("Details")]
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == Guid.Empty)
                return NotFound();

            var azureVm = await _mediatr.Send(new GetEntityCommand<AzureVMTemplate>(id));
            return Ok(azureVm);
        }

        [ValidateModel]
        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(EditAzureTemplateViewModel viewModel)
        {
            var model = _mapper.Map<AzureVMTemplate>(viewModel);
            await _mediatr.Send(new UpdateCommand<AzureVMTemplate>(model));
            return Ok();
        }

        [HttpPost("IsVmSizeUnique")]
        public async Task<IActionResult> IsVmSizeUnique(string vmSize, Guid id = default(Guid))
        {
            if (string.IsNullOrEmpty(vmSize)) return BadRequest("vmSize is empty");

            var result = await _mediatr.Send(new AzureVMSizeValidCommand(vmSize, id));
            return Ok(!result);
        }

        [HttpGet("Template")]
        public async Task<IActionResult> Template(Guid id)
        {
            if (id == Guid.Empty)
                return NotFound();

            var azureVm = await _mediatr.Send(new GetEntityCommand<AzureVMTemplate>(id));
            return Ok(azureVm.Template);
        }

        [HttpGet("Parameters")]
        public async Task<IActionResult> Parameters(Guid id)
        {
            if (id == Guid.Empty)
                return NotFound();

            var azureVm = await _mediatr.Send(new GetEntityCommand<AzureVMTemplate>(id));
            return Ok(azureVm.ParametersDefault);
        }

        [HttpPost("Test")]
        [ValidateModel]
        public async Task<IActionResult> Test(TestAzureResourcesViewModel viewModel)
        {
            var ipAddress = await _mediatr.Send(new AzureTestCreateResourcesCommand(viewModel.ResourceName, viewModel.AzureRegion, viewModel.DeploymentJson, viewModel.ParametersJson, viewModel.CredentialsId, viewModel.DockerImage));
            return Ok(ipAddress);
        }

        [ValidateModel]
        [HttpPost("DeleteResources")]
        public async Task<IActionResult> DeleteResources(string resourceName, Guid credentialsId)
        {
            if (string.IsNullOrEmpty(resourceName)) return BadRequest();
            if (credentialsId == Guid.Empty) return BadRequest();

            await _mediatr.Send(new AzureDeleteResourcesByNameCommand(resourceName, credentialsId));

            return Ok();
        }
    }
}