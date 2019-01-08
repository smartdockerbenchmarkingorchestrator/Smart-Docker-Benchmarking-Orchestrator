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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Docker.Benchmarking.Orchestrator.Web.Pages.AWSCredentials
{
    public class EditModel : PageModel
    {
        [BindProperty]
        [Display(Name = "Name")]
        [Remote("ValidateAWSCredentialName", controller: "RemoteValidator", HttpMethod = "POST", AdditionalFields = "Id", ErrorMessage = "Name already taken, use another name.")]
        public string Name { get; set; }

        [BindProperty(SupportsGet = true)]
        [Display(AutoGenerateField = false)]
        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        [BindProperty]
        [Display(AutoGenerateField = false)]
        [HiddenInput(DisplayValue = false)]
        public DateTimeOffset DateTimeCreated { get; set; }

        [BindProperty]
        [Display(Name = "Access Key Id")]
        public string AccessKeyId { get; set; }

        [BindProperty]
        [Display(Name = "Secret Key")]
        public string SecretKey { get; set; }

        [BindProperty]
        [Display(Name = "Region End Point")]
        public string RegionEndPoint { get; set; }

        [BindProperty]
        [Display(Name = "Active")]
        public bool Active { get; set; }

        public IEnumerable<SelectListItem> RegionEndPointsList { get; set; }

        private readonly IMediator _mediatr;

        private readonly IMapper _mapper;

        public EditModel(IMediator mediatr, IMapper mapper)
        {
            _mediatr = mediatr;
            _mapper = mapper;

            var regionEndPoints = Amazon.RegionEndpoint.EnumerableAllRegions;

            RegionEndPointsList = regionEndPoints.Select(provider => new SelectListItem
            {
                Text = provider.DisplayName,
                Value = provider.SystemName
            });
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (id == Guid.Empty) return BadRequest("Please submit a correct id");

            var model = await _mediatr.Send(new GetEntityCommand<Core.Entities.AWSCredentials>(id));

            if (model == null) return BadRequest("No item found for given id");

            _mapper.Map(model, this);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var model = await _mediatr.Send(new GetEntityCommand<Core.Entities.AWSCredentials>(Id));

            _mapper.Map(this, model);

            await _mediatr.Send(new UpdateCommand<Core.Entities.AWSCredentials>(model));

            return RedirectToPage();
        }
    }
}