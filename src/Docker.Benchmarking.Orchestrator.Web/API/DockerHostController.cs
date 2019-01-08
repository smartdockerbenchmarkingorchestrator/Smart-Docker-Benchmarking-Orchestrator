using AutoMapper;
using Docker.Benchmarking.Orchestrator.Core.Commands;
using Docker.Benchmarking.Orchestrator.Core.Entities;
using Docker.Benchmarking.Orchestrator.Web.Filters;
using Docker.Benchmarking.Orchestrator.Web.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Web.API
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceLayerValidationExceptionFilter]
    [Produces("application/json")]
    public class DockerHostController : BaseAPIController
    {
        public DockerHostController(IMapper mapper, IMediator mediatr) : base(mapper, mediatr)
        {

        }

        /// <summary>
        /// Checks the docker connection.
        /// </summary>
        /// <param name="hostName">Name of the host.</param>
        /// <param name="portNumber">The port number of the host.</param>
        /// <param name="httpAuthentication">if set to <c>true</c> [HTTP authentication].</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        [HttpPost("CheckDockerConnection")]
        private async Task<bool> CheckDockerConnection([Required]string hostName, [Required]int portNumber, [Required]bool httpAuthentication, [Required]string userName, [Required]string password)
        {
            bool isCorrect;

            if (httpAuthentication)
                isCorrect = await _mediatr.Send(new PingDockerHostAuthenticationCommand(hostName, portNumber, userName, password));
            else
                isCorrect = await _mediatr.Send(new PingDockerHostNoAuthenticationCommand(hostName, portNumber));

            return isCorrect;
        }

        /// <summary>
        /// Gets the azure v ms for location.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <param name="credentialsId">The credentials identifier.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        /// <exception cref="ValidationException">location is empty</exception>
        [HttpPost("GetAzureVMsForLocation")]
        public async Task<IActionResult> GetAzureVMsForLocation([Required]string location, [Required]Guid credentialsId)
        {
            if (string.IsNullOrEmpty(location)) return BadRequest("location is empty");
            if (credentialsId == Guid.Empty) return BadRequest();

            var vmSizes = await _mediatr.Send(new AzureVMSizesCommand(location, credentialsId));

            return Ok(vmSizes);
        }

        /// <summary>
        /// Determines whether this instance [can communicate with docker host] the specified host name.
        /// </summary>
        /// <param name="hostName">Name of the host.</param>
        /// <param name="portNumber">The port number.</param>
        /// <param name="httpAuthentication">if set to <c>true</c> [HTTP authentication].</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPost("CanCommunicateWithDockerHost")]
        public async Task<IActionResult> CanCommunicateWithDockerHost([Required]string hostName, [Required]int portNumber, bool httpAuthentication, [Required]string userName, [Required]string password)
        {
            bool isCorrect = await CheckDockerConnection(hostName, portNumber, httpAuthentication, userName, password);
            return Ok(isCorrect);
        }

        /// <summary>
        /// Adds the host.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPost("AddHost")]
        [ValidateModel]
        public async Task<IActionResult> Create(AddDockerHostViewModel viewModel)
        {
            if (viewModel.AzureTemplate.HasValue || viewModel.AWSTemplate.HasValue)
            {

            }
            else
            {
                bool isCorrect = await CheckDockerConnection(viewModel.HostName, viewModel.PortNumber, viewModel.UseHttpAuthentication, viewModel.UserName, viewModel.Password);

                if (!isCorrect)
                {
                    return BadRequest("Can't connect to docker host with provided details.");
                }
            }

            var dtoModel = _mapper.Map<DockerHost>(viewModel);

            //TODO: Logic needs to move out of web layer
            if (viewModel.AzureTemplate.HasValue)
            {
                var vmTemplate = await _mediatr.Send(new GetEntityCommand<AzureVMTemplate>(viewModel.AzureTemplate.Value));
                var azureCreds = await _mediatr.Send(new GetEntityCommand<AzureCredientials>(viewModel.AzureCredential.Value));

                var azureModel = _mapper.Map<AzureHost>(viewModel);
                azureModel.DockerHost = dtoModel;
                azureModel.AzureRegion = viewModel.AzureLocation;
                azureModel.Template = vmTemplate;
                azureModel.Credentials = azureCreds;
                azureModel.DestroyResourcesAfterBenchmark = viewModel.DestroyResourcesAfterBenchmark;

                //TODO: Fix this
                azureModel.Name = dtoModel.Name.Replace(" ", "", StringComparison.CurrentCulture) + DateTime.Now.Ticks;

                dtoModel.AzureHost = azureModel;
            }
            else if (viewModel.AWSTemplate.HasValue)
            {
                var vmTemplate = await _mediatr.Send(new GetEntityCommand<AWSCloudFormationTemplate>(viewModel.AWSTemplate.Value));
                var awsCreds = await _mediatr.Send(new GetEntityCommand<AWSCredentials>(viewModel.AWSCredential.Value));

                var awsHost = _mapper.Map<AWSHost>(viewModel);
                awsHost.DockerHost = dtoModel;
                awsHost.Template = vmTemplate;
                awsHost.DestroyResourcesAfterBenchmark = viewModel.DestroyResourcesAfterBenchmark;
                awsHost.Credentials = awsCreds;

                awsHost.Name = dtoModel.Name.Replace(" ", "", StringComparison.CurrentCulture) + DateTime.Now.Ticks;

                dtoModel.AWSHost = awsHost;
            }

            await _mediatr.Send(new CreateCommand<DockerHost>(dtoModel));

            return Ok();
        }

        /// <summary>
        /// Edits the host.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<IActionResult> Update(Guid id, EditDockerHostViewModel viewModel)
        {
            var model = _mapper.Map<DockerHost>(viewModel);

            if (viewModel.AzureHostId.HasValue)
            {
                var azureHost = await _mediatr.Send(new GetEntityCommand<AzureHost>(viewModel.AzureHostId.Value));

                if (azureHost == null) return BadRequest("Error Finding Azure Host");

                model.AzureHost = azureHost;

            }

            await _mediatr.Send(new UpdateCommand<DockerHost>(model));

            return Ok();
        }
    }
}