using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Docker.Benchmarking.Orchestrator.Core.Commands;
using Docker.Benchmarking.Orchestrator.Core.Enums;
using Docker.Benchmarking.Orchestrator.Infrastrcture.Helpers;
using Docker.Benchmarking.Orchestrator.Web.ViewModels.AWS;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Docker.Benchmarking.Orchestrator.Web.Pages.AWS
{
    public class EditModel : PageModel
    {
        [BindProperty]
        [Display(Name = "Name")]
        [Remote("ValidateAWSTemplateName", "RemoteValidator", HttpMethod = "POST", AdditionalFields = "Id", ErrorMessage = "Name already taken, use another name.")]
        public string Name { get; set; }

        [BindProperty]
        public Guid Id { get; set; }

        [BindProperty]
        public DateTimeOffset DateTimeCreated { get; set; }

        [BindProperty]
        [Display(Name = "Instance Name", Prompt = "e.g. t2.micro")]
        public string InstanceName { get; set; }

        [BindProperty]
        [Display(Name = "Price Per Hour", Prompt = "0.1")]
        public double PricePerHour { get; set; }

        [BindProperty]
        [Display(Name = "CPUs")]
        public double vCPUs { get; set; }

        [BindProperty]
        [Display(Name = "Memory in MB")]
        public double Memory { get; set; }

        [BindProperty]
        [Display(Name = "Disk Size in GB")]
        public double DiskSize { get; set; }

        [BindProperty]
        [Display(Name = "Template")]
        public string Template { get; set; }

        [BindProperty]
        [Display(Name = "Parameters")]
        public List<AWSParameterViewModel> Parameters { get; set; }

        [BindProperty]
        public IEnumerable<SelectListItem> Credentials { get; set; }

        [Display(Name = "Credentials / Location (Required for Validating Template)")]
        [BindProperty]
        public Guid Credential { get; set; }

        [BindProperty]
        public List<string> Capabilities { get; private set; }

        [BindProperty]
        public bool HasValidated { get; private set; }

        [BindProperty]
        public string EstimatedCostsUrl { get; private set; }

        [BindProperty]
        public AWSDeploymentType DeploymentType { get; set; }

        [BindProperty]
        public bool Active { get; set; }

        [BindProperty]
        [Display(Name = "VM Size")]
        public VMSize VMSizeType { get; set; }

        private readonly IMediator _mediatr;

        private readonly IMapper _mapper;

        public EditModel(IMediator mediatr, IMapper mapper)
        {
            _mediatr = mediatr;
            _mapper = mapper;

            if(Credentials == null)
            {
                Task.Run(() => CredentialsSelectList()).Wait();
            }
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (id == Guid.Empty) return BadRequest("Please submit a correct id");

            var model = await _mediatr.Send(new GetEntityCommand<Core.Entities.AWSCloudFormationTemplate>(id));

            if (model == null) return BadRequest("No entity found for given id");

            _mapper.Map(model, this);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();


            var credentials = await _mediatr.Send(new GetEntityCommand<Core.Entities.AWSCredentials>(Credential));

            if (credentials == null)
            {
                ModelState.AddModelError("Credentials", "No Credentials Found.");
                return Page();
            }

            var request = await _mediatr.Send(new AWSValidateTemplateCommand(Template, credentials));

            if (request != null)
            {
                HasValidated = true;
            }


            if (!HasValidated)
            {
                ModelState.AddModelError("HasValidated", "Template Not Validated");
                return Page();
            }


            var modelDto = await _mediatr.Send(new GetEntityCommand<Core.Entities.AWSCloudFormationTemplate>(Id));

            if (modelDto == null)
            {
                ModelState.AddModelError("Model", "No AWS Template Found To Update.");
                return Page();
            }

            _mapper.Map(this, modelDto);

            var updateRequest = await _mediatr.Send(new UpdateCommand<Core.Entities.AWSCloudFormationTemplate>(modelDto));

            return RedirectToPage("Index");
        }
        

        #region private

        private async Task CredentialsSelectList()
        {
            var credentials = await _mediatr.Send(new ListActiveEntitiesCommand<Core.Entities.AWSCredentials>());

            //Map AWS Credentials to SelectListItem List
            Credentials = credentials.Select(provider => new SelectListItem
            {
                Text = provider.Name + " - " + provider.AWSEndPoint.DisplayName,
                Value = provider.Id.ToString()
            });
        }

        #endregion
    }
}