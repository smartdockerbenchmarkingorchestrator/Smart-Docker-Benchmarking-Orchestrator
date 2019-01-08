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
    public class IndexModel : PageModel
    {
        private readonly IMediator _mediatr;

        public IndexModel(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        public IEnumerable<AWSCloudFormationTemplate> AWSCloudFormationTemplate { get; set; }

        public async Task OnGetAsync()
        {
            AWSCloudFormationTemplate = await _mediatr.Send(new ListEntitiesCommand<AWSCloudFormationTemplate>());
        }
    }
}
