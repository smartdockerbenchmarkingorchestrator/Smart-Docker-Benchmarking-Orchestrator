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

namespace Docker.Benchmarking.Orchestrator.Web.Controllers
{
    public class AWSController : BaseController
    {
        public AWSController(IMapper mapper, IMediator mediatr) : base(mapper, mediatr)
        {

        }

        [HttpGet]
        public async Task<IActionResult> Template(Guid id)
        {
            if (id == Guid.Empty)
                return NotFound();

            var azureVm = await _mediatr.Send(new GetEntityCommand<AWSCloudFormationTemplate>(id));
            var responseValue = JsonConvert.DeserializeObject(azureVm.Template);

            return Json(responseValue);
        }
    }
}