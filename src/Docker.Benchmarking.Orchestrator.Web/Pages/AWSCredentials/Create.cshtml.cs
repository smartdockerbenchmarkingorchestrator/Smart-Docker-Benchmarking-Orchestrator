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
    public class CreateModel : PageModel
    {

        [BindProperty]
        [Display(Name = "Name")]
        [Remote("ValidateAWSTemplateName", "RemoteValidator", HttpMethod = "POST", ErrorMessage = "Name already taken, use another name.")]
        public string Name { get; set; }

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
        public IEnumerable<SelectListItem> RegionEndPointsList { get; set; }

        private readonly IMediator _mediatr;

        private readonly IMapper _mapper;
        public CreateModel(IMediator mediatr, IMapper mapper)
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

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var model = _mapper.Map<Core.Entities.AWSCredentials>(this);

            await _mediatr.Send(new CreateCommand<Core.Entities.AWSCredentials>(model));

            return RedirectToPage("Index");
        }
    }
}