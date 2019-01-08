using AutoMapper;
using Docker.Benchmarking.Orchestrator.Core.Commands;
using Docker.Benchmarking.Orchestrator.Core.Entities;
using Docker.Benchmarking.Orchestrator.Web.Filters;
using Docker.Benchmarking.Orchestrator.Web.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Web.API
{
    public class ApacheJmeterApiController : BaseAPIController
    {
        public ApacheJmeterApiController(IMapper mapper, IMediator mediatr) : base(mapper, mediatr)
        {
        }

        [HttpPost("AddTestFile")]
        [ValidateModel]
        public async Task<IActionResult> AddTestFile(AddApacheJmeterTestFileViewModel viewModel)
        {
            var model = _mapper.Map<ApacheJmeterTestFile>(viewModel);
            await _mediatr.Send(new CreateCommand<ApacheJmeterTestFile>(model));
            return Ok(model.Id);
        }

        [HttpGet("TestFile")]
        [Produces("application/xml")]
        public async Task<IActionResult> TestFile(Guid id)
        {
            if (id == Guid.Empty) return BadRequest("Id is empty");

            var testFile = await _mediatr.Send(new GetEntityCommand<ApacheJmeterTestFile>(id));

            if (testFile == null) return BadRequest("No file foind for given Id");

            return Content(testFile.FileName, "application/xml");
        }

        [HttpPost("EditTestFile")]
        [ValidateModel]
        public async Task<IActionResult> EditTestFile(ApacheTestFileEditModel viewModel)
        {
            var model = _mapper.Map<ApacheJmeterTestFile>(viewModel);
            await _mediatr.Send(new UpdateCommand<ApacheJmeterTestFile>(model));
            return Ok();
        }
    }
}