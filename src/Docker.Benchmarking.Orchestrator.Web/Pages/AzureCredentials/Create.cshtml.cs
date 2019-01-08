using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Docker.Benchmarking.Orchestrator.Core.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Docker.Benchmarking.Orchestrator.Web.Pages.AzureCredentials
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        [Display(Name = "Name")]
        [Remote("ValidateAzureCredentialName", "RemoteValidator", HttpMethod = "POST", ErrorMessage = "Name already taken, use another name.")]
        public string Name { get; set; }

        [Display(Name = "Subscription Id")]
        [BindProperty]
        public string SubscriptionId { get; set; }

        [BindProperty]
        [Display(Name = "Client Id")]
        public string ClientId { get; set; }

        [BindProperty]
        [Display(Name = "Secret")]
        public string Secret { get; set; }

        [BindProperty]
        [Display(Name = "Tenant Id")]
        public string TenantId { get; set; }

        private readonly IMediator _mediatr;
        private readonly IMapper _mapper;

        public CreateModel(IMediator mediatr, IMapper mapper)
        {
            _mediatr = mediatr;
            _mapper = mapper;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var model = _mapper.Map<Core.Entities.AzureCredientials>(this);

            await _mediatr.Send(new CreateCommand<Core.Entities.AzureCredientials>(model));

            return RedirectToPage("Index");
        }
    }
}