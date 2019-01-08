using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Docker.Benchmarking.Orchestrator.Core.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Docker.Benchmarking.Orchestrator.Web.Pages.AzureCredentials
{
    public class DetailsModel : PageModel
    {
        [BindProperty]
        public Guid Id { get; private set; }

        [BindProperty]
        public Core.Entities.AzureCredientials ViewModel { get; set; }

        private readonly IMediator _mediatr;
        private readonly IMapper _mapper;

        public DetailsModel(IMediator mediatr, IMapper mapper)
        {
            _mapper = mapper;
            _mediatr = mediatr;
        }

        public async Task<IActionResult> OnGet(Guid id)
        {
            if (id == Guid.Empty) return BadRequest("Please submit a correct id");

            var model = await _mediatr.Send(new GetEntityCommand<Core.Entities.AzureCredientials>(id));

            if (model == null) return BadRequest("No item found for given id");
            ViewModel = model;
            return Page();
        }
    }
}