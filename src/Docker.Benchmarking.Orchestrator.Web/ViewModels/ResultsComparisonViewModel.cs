using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Web.ViewModels
{
    public class ResultsComparisonViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<SelectListItem> CompletedExperiments { get; set; }
    }
}
