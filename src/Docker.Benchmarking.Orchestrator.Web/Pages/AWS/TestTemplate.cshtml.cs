using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Docker.Benchmarking.Orchestrator.Core.Commands;
using Docker.Benchmarking.Orchestrator.Core.Commands.AWS;
using Docker.Benchmarking.Orchestrator.Core.Entities;
using Docker.Benchmarking.Orchestrator.Core.Interfaces;
using Docker.Benchmarking.Orchestrator.Web.Hubs;
using Docker.Benchmarking.Orchestrator.Web.ViewModels.AWS;
using Hangfire.Storage;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;

namespace Docker.Benchmarking.Orchestrator.Web.Pages.AWS
{
    public class TestTemplateModel : PageModel
    {
        [BindProperty]
        [Display(Name = "Template")]
        public string Template { get; set; }

        [BindProperty]
        [Display(Name = "Stack Name")]
        public string StackName { get; set; }

        [BindProperty]
        [Display(Name = "Parameters")]
        public List<AWSParameterViewModel> Parameters { get; set; }

        [BindProperty]
        public IEnumerable<SelectListItem> Credentials { get; set; }

        [BindProperty]
        public IEnumerable<SelectListItem> DockerImages { get; set; }

        [Display(Name = "Docker Image to Deploy")]
        [BindProperty]
        public Guid DockerImageId { get; set; }

        [Display(Name = "Credentials / Location")]
        [BindProperty]
        public Guid Credential { get; set; }

        [BindProperty]
        public List<string> Capabilities { get; set; }

        [BindProperty]
        [HiddenInput(DisplayValue = false)]
        public bool HasValidated { get; set; }

        [BindProperty]
        [HiddenInput(DisplayValue = false)]
        public string EstimatedCostsUrl { get; set; }

        [BindProperty]
        [HiddenInput(DisplayValue = false)]
        public string JobId { get; set; }

        private readonly IMediator _mediatr;
        private readonly IMapper _mapper;

        public TestTemplateModel(IMediator mediatr,
            IMapper mapper
            )
        {
            _mediatr = mediatr;
            _mapper = mapper;

            Task.Run(() => BindSelectLists()).Wait();
        }


        public IActionResult OnGet()
        {
            StackName = "DockerBenchmarkingArchistrator".ToLower() + DateTime.UtcNow.Ticks;

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

        public async Task<IActionResult> OnPostValidateAsync()
        {
            await BindSelectLists();
            if (!ModelState.IsValid) return Page();

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

        public async Task<IActionResult> OnPostEstimatedCostsAsync()
        {
            await BindSelectLists();

            if (!ModelState.IsValid) return Page();

            var credentials = await _mediatr.Send(new GetEntityCommand<Core.Entities.AWSCredentials>(Credential));

            var parameters = new List<Amazon.CloudFormation.Model.Parameter>();

            _mapper.Map(Parameters, parameters);

            var request = await _mediatr.Send(new AWSEstimatedCostsCommand(Template, parameters, credentials));

            EstimatedCostsUrl = request;
            HasValidated = true;

            return Page();
        }

        public async Task<IActionResult> OnPostTestDeployAsync()
        {
            await BindSelectLists();

            if (!ModelState.IsValid) return Page();

            var parameters = new List<Amazon.CloudFormation.Model.Parameter>();
            _mapper.Map(Parameters, parameters);

            var jobId = await _mediatr.Send(new AWSTestDeploymentScriptCommand(StackName, Template, parameters, Credential, DockerImageId));

            JobId = jobId;
            HasValidated = true;

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteResourcesAsync()
        {
            await BindSelectLists();

            if (!ModelState.IsValid) return Page();

            var success = await _mediatr.Send(new AWSDeleteResourcesCommand(StackName, Credential));

            return RedirectToAction("TestTemplate");
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

        private async Task DockerImagesSelectList()
        {
            var credentials = await _mediatr.Send(new ListActiveEntitiesCommand<DockerImage>());

            //Map AWS Credentials to SelectListItem List
            DockerImages = credentials.Select(provider => new SelectListItem
            {
                Text = provider.Name,
                Value = provider.Id.ToString()
            });
        }

        private async Task BindSelectLists()
        {
            await CredentialsSelectList();
            await DockerImagesSelectList();
        }
    }
}