using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Docker.Benchmarking.Orchestrator.Core.Commands;
using Docker.Benchmarking.Orchestrator.Core.Entities;
using Docker.Benchmarking.Orchestrator.Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Docker.Benchmarking.Orchestrator.Web.Pages.DockerHost
{
    public class TestDeploy : PageModel
    {
        [BindProperty]
        [Display( Name = "Docker Host IP/Hostname")]
        public string DockerHost { get; set; }

        [BindProperty]
        [Display(Name = "Port Number")]
        public int PortNumber { get; set; }

        [BindProperty]
        [Display(Name = "UserName (if HTTP Authentication)")]
        public string UserName { get; set; }

        [BindProperty]
        [Display(Name = "Password (if HTTP Authentication)")]
        public string Password { get; set; }

        [BindProperty]
        [Display(Name = "Docker Image to Deploy")]
        public Guid DockerImage { get; set; }

        [BindProperty]
        public IEnumerable<SelectListItem> DockerImages { get; set; }

        private readonly IMediator _mediatr;
        private readonly IDockerRemoteService _dockerRemoteService;

        public TestDeploy(IMediator mediatr, IDockerRemoteService dockerRemoteService)
        {
            _mediatr = mediatr;
            _dockerRemoteService = dockerRemoteService;

            if (PortNumber == default(int)) PortNumber = 2376;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await CreateSelectList();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await CreateSelectList();

            if (!ModelState.IsValid) return Page();

            var imageDeployed = await _dockerRemoteService.DeployImageToHost(DockerHost, PortNumber, DockerImage, UserName, Password);

            return Page();
        }


        private async Task CreateSelectList()
        {
            var images = await _mediatr.Send(new ListActiveEntitiesCommand<DockerImage>());

            images = images.Where(c => c.ImageType == Core.Enums.ImageType.Application).ToList();
            //Map AWS Credentials to SelectListItem List
            DockerImages = images.Select(provider => new SelectListItem
            {
                Text = provider.Name,
                Value = provider.Id.ToString()
            });
        }
    }
}