using AutoMapper;
using Docker.Benchmarking.Orchestrator.Core.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Web.Pages.AzureCredentials
{
    public class EditModel : PageModel
    {
        [BindProperty]
        [Display(Name = "Name")]
        [Remote("ValidateAzureCredentialName", "RemoteValidator", HttpMethod = "POST", AdditionalFields = "Id", ErrorMessage = "Name already taken, use another name.")]
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

        [BindProperty(SupportsGet = true)]
        [Display(AutoGenerateField = false)]
        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        [BindProperty]
        [Display(AutoGenerateField = false)]
        [HiddenInput(DisplayValue = false)]
        public DateTimeOffset DateTimeCreated { get; set; }

        [BindProperty]
        [Display(Name = "Active")]
        public bool Active { get; set; }

        private readonly IMediator _mediatr;

        private readonly IMapper _mapper;

        public EditModel(IMediator mediatr, IMapper mapper)
        {
            _mediatr = mediatr;
            _mapper = mapper;
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (id == Guid.Empty) return BadRequest("Please submit a correct id");

            var model = await _mediatr.Send(new GetEntityCommand<Core.Entities.AzureCredientials>(id));

            if (model == null) return BadRequest("No item found for given id");

            _mapper.Map(model, this);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var model = await _mediatr.Send(new GetEntityCommand<Core.Entities.AzureCredientials>(Id));

            _mapper.Map(this, model);

            await _mediatr.Send(new UpdateCommand<Core.Entities.AzureCredientials>(model));

            return RedirectToPage("Index");
        }
    }
}