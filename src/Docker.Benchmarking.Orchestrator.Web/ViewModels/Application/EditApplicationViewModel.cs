using Docker.Benchmarking.Orchestrator.Core.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Docker.Benchmarking.Orchestrator.Web.ViewModels
{
    public class EditApplicationViewModel
    {
        public Guid Id { get; set; }

        public DateTimeOffset DateTimeCreated { get; set; }

        [Remote("ValidateApplicationName", "RemoteValidator", HttpMethod = "POST", AdditionalFields = "Id", ErrorMessage = "Name already taken, use another name.")]
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

        public bool Active { get; set; }

        public List<DockerImageVariableViewModel> Variables { get; set; }
    }
}
