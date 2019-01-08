using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Docker.Benchmarking.Orchestrator.Core.Entities;
using Docker.Benchmarking.Orchestrator.Infrastrcture.Data;
using MediatR;
using Docker.Benchmarking.Orchestrator.Core.Commands;

namespace Docker.Benchmarking.Orchestrator.Web.Pages.AWS
{
    public class DetailsModel : PageModel
    {
        private readonly IMediator _mediatr;

        public DetailsModel(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        public AWSCloudFormationTemplate AWSCloudFormationTemplate { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AWSCloudFormationTemplate = await _mediatr.Send(new GetEntityCommand<AWSCloudFormationTemplate>(id.Value));

            if (AWSCloudFormationTemplate == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
