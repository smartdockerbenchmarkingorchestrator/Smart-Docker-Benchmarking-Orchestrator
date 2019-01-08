using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Docker.Benchmarking.Orchestrator.Web.APIModels;

namespace Docker.Benchmarking.Orchestrator.Web.ViewModels
{
    public class ManualBenchmarkResultsUploadViewModel
    {
        [Display(Name = "Select Benchmark Experiment to Upload manually")]
        public Guid Id { get; set; }

        public IEnumerable<SelectListItem> Items { get; set; }

        [Display(Name = "Select File to Upload")]
        public IFormFile File { get; set; }
    }
}
