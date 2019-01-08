using Docker.Benchmarking.Orchestrator.Core.Entities;
using Docker.Benchmarking.Orchestrator.Core.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Web.ViewModels
{
    public class AddApplicationViewModel
    {
        public AddApplicationViewModel()
        {
            DelayToStartApplication = 60000;
        }

        [Remote("ValidateApplicationName", "RemoteValidator", HttpMethod = "POST", ErrorMessage = "Name already taken, use another name.")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Benchmarking Image")]
        public Guid BenchmarkingImage { get; set; }

        [Display(Name = "Application Image")]
        public Guid ApplicationImage { get; set; }

        [Display(Name = "Database Image")]
        public Guid? DatabaseImage { get; set; }

        public IEnumerable<SelectListItem> ApplicationImages { get; set; }

        public IEnumerable<SelectListItem> BenchmarkingImages { get; set; }

        public IEnumerable<SelectListItem> DatabaseImages { get; set; }

        public ApplicationType ApplicationType { get; set; }

        [Display(Name = "Select a file for benchmark to use")]
        public Guid? ApacheTestFileId { get; set; }

        public IEnumerable<SelectListItem> ApacheTestFiles { get; set; }

        [Display(Name = "Application Startup Time Wait/Delay (ms)")]
        public int DelayToStartApplication { get; set; }

        public List<ApplicationTestVariableViewModel> Variables { get; set; }
    }
}
