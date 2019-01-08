using AutoMapper;
using Docker.Benchmarking.Orchestrator.Core.Commands;
using Docker.Benchmarking.Orchestrator.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Web.Controllers
{
    public class RemoteValidatorController : BaseController
    {
        public RemoteValidatorController(IMapper mapper, IMediator mediatr) : base(mapper, mediatr) { }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ValidateAWSCredentialName(string Name, Guid Id = default(Guid))
        {
            if (string.IsNullOrEmpty(Name)) return BadRequest(nameof(Name) + " is null/empty.");

            var result = await _mediatr.Send(new FindEntityByNameCommand<AWSCredentials>(Name, Id));

            return Ok(!result);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ValidateAzureCredentialName(string Name, Guid Id = default(Guid))
        {
            if (string.IsNullOrEmpty(Name)) return BadRequest(nameof(Name) + " is null/empty.");

            var result = await _mediatr.Send(new FindEntityByNameCommand<AzureCredientials>(Name, Id));

            return Ok(!result);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ValidateApacheJmeterName(string Name, Guid Id = default(Guid))
        {
            if (string.IsNullOrEmpty(Name)) return BadRequest(nameof(Name) + " is null/empty.");

            var result = await _mediatr.Send(new FindEntityByNameCommand<ApacheJmeterTestFile>(Name, Id));

            return Ok(!result);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ValidateApplicationName(string Name, Guid Id = default(Guid))
        {
            if (string.IsNullOrEmpty(Name)) return BadRequest(nameof(Name) + " is null/empty.");

            var result = await _mediatr.Send(new FindEntityByNameCommand<Application>(Name, Id));

            return Ok(!result);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ValidateAzureTemplateName(string Name, Guid Id = default(Guid))
        {
            if (string.IsNullOrEmpty(Name)) return BadRequest(nameof(Name) + " is null/empty.");

            var result = await _mediatr.Send(new FindEntityByNameCommand<AzureVMTemplate>(Name, Id));

            return Ok(!result);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ValidateAWSTemplateName(string Name, Guid Id = default(Guid))
        {
            if (string.IsNullOrEmpty(Name)) return BadRequest(nameof(Name) + " is null/empty.");

            var result = await _mediatr.Send(new FindEntityByNameCommand<AWSCloudFormationTemplate>(Name, Id));

            return Ok(!result);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ValidateAWSHostName(string Name, Guid Id = default(Guid))
        {
            if (string.IsNullOrEmpty(Name)) return BadRequest(nameof(Name) + " is null/empty.");

            var result = await _mediatr.Send(new FindEntityByNameCommand<AWSHost>(Name, Id));

            return Ok(!result);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ValidateDockerHostName(string Name, Guid Id = default(Guid))
        {
            if (string.IsNullOrEmpty(Name)) return BadRequest(nameof(Name) + " is null/empty.");

            var result = await _mediatr.Send(new FindEntityByNameCommand<DockerHost>(Name, Id));

            return Ok(!result);
        }
    }
}
