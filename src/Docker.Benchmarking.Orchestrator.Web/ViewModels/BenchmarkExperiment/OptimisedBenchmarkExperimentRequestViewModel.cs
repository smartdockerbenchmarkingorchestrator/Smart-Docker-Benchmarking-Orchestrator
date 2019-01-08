using Docker.Benchmarking.Orchestrator.Core.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Web.ViewModels
{
    public class OptimisedBenchmarkExperimentRequestViewModel
    {
        public OptimisedBenchmarkExperimentRequestViewModel()
        {
            ApdexTSeconds = 1.2;
            ConcurrentUsers = 60;

            Variables = new List<BenchmarkExperimentVariableViewModel>
            {
                new BenchmarkExperimentVariableViewModel
                {
                    Name = "${__P(ORDER_USERS)}"
                },
                new BenchmarkExperimentVariableViewModel
                {
                    Name = "${__P(BROWSE_USERS)}"
                },
                new BenchmarkExperimentVariableViewModel
                {
                    Name = "${__P(RAMP_UP_TIME)}",
                    Value = 180.ToString()
                },
                new BenchmarkExperimentVariableViewModel
                {
                    Name = "${__P(THINK_TIME)}",
                    Value = 3000.ToString()
                }
            };
        }

        [Display(Name = "Name of Optimized Experiment")]
        public string Name { get; set; }

        [Display(Name = "Application to Benchmark")]
        public Guid Application { get; set; }

        public IEnumerable<SelectListItem> Applications { get; set; }

        [Display(Name = "Cloud Provider to Benchmark")]
        public CloudProvider HostCloudProvider { get; set; }

        [Display(Name = "Cloud Provider to Launch Benchmark Threads From")]
        public CloudProvider BenchmarkCloudProvier { get; set; }

        public int BenchmarkTimeLength { get; set; }

        public int[] VMLevels { get; set; }

        [Display(Name = "Maximum Cost to spend on experiments")]
        public decimal MaxCost { get; set; }

        [Display(Name = "What is the default concurrent users for the test?")]
        public int ConcurrentUsers { get; set; }

        [Display(Name = "Reference to what is being benchmarked e.g. TPC-W WIPS")]
        public string BenchmarkingName { get; set; }

        [Display(Name = "Secondary Reference e.g. West-Europe Experiment 2")]
        public string ExperimentReference { get; set; }

        [Display(Name = "Secondary Reference e.g. West-Europe Experiment 2")]
        public List<BenchmarkExperimentVariableViewModel> Variables { get; set; }

        [Display(Name = "Apdex T Seconds")]
        public double ApdexTSeconds { get; set; }

        [Display(Name = "Select a file for benchmark to use")]
        public Guid? ApacheTestFileId { get; set; }

        public IEnumerable<SelectListItem> ApacheTestFiles { get; set; }

    }
}
