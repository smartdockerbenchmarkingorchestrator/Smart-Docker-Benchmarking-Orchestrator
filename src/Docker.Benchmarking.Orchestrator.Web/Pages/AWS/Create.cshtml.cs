using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Docker.Benchmarking.Orchestrator.Core.Commands;
using Docker.Benchmarking.Orchestrator.Core.Enums;
using Docker.Benchmarking.Orchestrator.Web.ViewModels.AWS;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Docker.Benchmarking.Orchestrator.Infrastrcture.Helpers;

namespace Docker.Benchmarking.Orchestrator.Web.Pages.AWS
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        [Display(Name = "Name")]
        [Remote("ValidateAWSTemplateName", "RemoteValidator", HttpMethod = "POST", ErrorMessage = "Name already taken, use another name.")]
        public string Name { get; set; }

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

        [Display(Name = "Credentials / Location")]
        [BindProperty]
        public Guid Credential { get; set; }

        [BindProperty]
        public List<string> Capabilities { get; private set; }

        [BindProperty]
        public bool HasValidated { get; set; }

        [BindProperty]
        public AWSDeploymentType DeploymentType { get; set; }

        [BindProperty]
        [Display(Name = "VM Size")]
        public VMSize VMSizeType { get; set; }

        private readonly IMediator _mediatr;

        private readonly IMapper _mapper;

        public CreateModel(IMediator mediatr, IMapper mapper)
        {
            _mediatr = mediatr;
            _mapper = mapper;

            if (Credentials == null)
            {
                Task.Run(() => CredentialsSelectList()).Wait();
            }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await CredentialsSelectList();
            Parameters = new List<AWSParameterViewModel>
            {
                new AWSParameterViewModel
                {
                    Key = "InstanceType",
                    Value = string.Empty
                },
                new AWSParameterViewModel
                {
                    Key = "KeyName",
                    Value = string.Empty
                }
            };

            return Page();
        }

        //public async Task<IActionResult> OnPostValidateTemplate()
        //{
        //    HasValidated = true;
        //    await CredentialsSelectList();
        //    return Page();
        //}


        //public async Task<IActionResult> OnPostCreateCredentials()
        //{
        //    await CredentialsSelectList();
        //    return Page();
        //}

        public async Task<IActionResult> OnPostValidateTemplate()
        {
            await CredentialsSelectList();

            if (!ModelState.IsValid) return Page();

            if (!Template.IsValidJson())
            {
                ModelState.AddModelError("Template", "Template is not valid json");
                return Page();
            }

            var credentials = await _mediatr.Send(new GetEntityCommand<Core.Entities.AWSCredentials>(this.Credential));

            if (credentials == null)
            {
                return Page();
            }

            var request = await _mediatr.Send(new AWSValidateTemplateCommand(Template, credentials));
            Capabilities = request;
            HasValidated = true;

            return Page();
        }

        public async Task<IActionResult> OnPostCreateCredentials()
        {
            await CredentialsSelectList();

            if (!ModelState.IsValid) return Page();

            if (!HasValidated)
            {
                ModelState.AddModelError("HasValidated", "Template Not Validated");
                return Page();
            }

            var credentials = await _mediatr.Send(new GetEntityCommand<Core.Entities.AWSCredentials>(this.Credential));

            if (credentials == null)
            {
                return Page();
            }

            var modelDto = new Core.Entities.AWSCloudFormationTemplate();

            _mapper.Map(this, modelDto);

            var request = await _mediatr.Send(new CreateCommand<Core.Entities.AWSCloudFormationTemplate>(modelDto));

            return RedirectToPage("Index");
        }

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
    }
}