using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Docker.Benchmarking.Orchestrator.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Docker.Benchmarking.Orchestrator.Web.Pages.DockerHost
{
    public class TestCaptureStatsModel : PageModel
    {
        private readonly IMediator _mediatr;
        private readonly IMapper _mapper;

        [BindProperty]
        [Required]
        public string HostName { get; set; }

        [BindProperty]
        [Required]
        public int PortNumber { get; set; }

        [BindProperty]
        [Required]
        public string ContainerName { get; set; }

        [BindProperty]
        [Required]
        public int Length { get; set; }

        [BindProperty]
        public IEnumerable<ContainerMetric> ContainerMetrics { get; set; }

        public TestCaptureStatsModel(IMediator mediatr, IMapper mapper)
        {
            _mediatr = mediatr;
            _mapper = mapper;

            if (PortNumber == 0) PortNumber = 2376;
        }
        public void OnGet()
        {

        }
    }
}