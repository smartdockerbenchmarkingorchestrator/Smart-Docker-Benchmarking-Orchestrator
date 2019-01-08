using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    [Route("api/[controller]")]
    [ApiController]
    [ServiceLayerValidationExceptionFilter]
    [Produces("application/json")]
    public class DockerImageController : BaseAPIController
    {
        public DockerImageController(IMapper mapper, IMediator mediatr) : base(mapper, mediatr)
        {

        }

        [HttpPost("Create")]
        [ValidateModel]
        public async Task<IActionResult> Create(AddDockerImageViewModel model)
        {
            var result = _mapper.Map<DockerImage>(model);
            await _mediatr.Send(new CreateCommand<DockerImage>(result));

            return CreatedAtRoute("GetDockerImage", new { id = result.Id }, result);
        }

        [HttpGet("{id}", Name = "GetDockerImage")]
        public async Task<ActionResult<DockerImage>> GetById([Required]Guid id)
        {
            var item = await _mediatr.Send(new GetEntityCommand<DockerImage>(id));

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        /// <summary>
        /// Edits the specified view model.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<IActionResult> Update(Guid id, EditDockerImageViewModel viewModel)
        {
            var image = await _mediatr.Send(new GetEntityCommand<DockerImage>(id));

            if (image == null) return NotFound();

            _mapper.Map(viewModel, image);

            await _mediatr.Send(new UpdateCommand<DockerImage>(image));

            return NoContent();
        }
    }
}