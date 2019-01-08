using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Docker.Benchmarking.Orchestrator.Core.Commands;
using Docker.Benchmarking.Orchestrator.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Docker.Benchmarking.Orchestrator.Web.API
{
    public class AWSAPIController : BaseAPIController
    {
        public AWSAPIController(IMapper mapper, IMediator mediatr) : base(mapper, mediatr)
        {

        }

        [HttpGet("Template")]
        public async Task<ActionResult<string>> Template(Guid id)
        {
            if (id == Guid.Empty)
                return NotFound();

            var azureVm = await _mediatr.Send(new GetEntityCommand<AWSCloudFormationTemplate>(id));
            return azureVm.Template;
        }
    }
}
