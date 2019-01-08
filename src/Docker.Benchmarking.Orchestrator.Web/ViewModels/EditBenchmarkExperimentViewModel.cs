using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Docker.Benchmarking.Orchestrator.Web.ViewModels
{
    public class EditBenchmarkExperimentViewModel
    {
        [ReadOnly(true)]
        public string Name { get; set; }

        [HiddenInput]
        public Guid Id { get; set; }

        [UIHint("Hidden")]
        [HiddenInput]
        public DateTimeOffset DateTimeCreated { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public Guid Host { get; set; }

        public IEnumerable<SelectListItem> Hosts { get; set; }
        public IEnumerable<SelectListItem> BenchmarkHosts { get; set; }
        public IEnumerable<SelectListItem> DatabaseHosts { get; set; }

        [DataType(DataType.MultilineText)]
        public string Comments { get; set; }

        public Guid Application { get; set; }

        public IEnumerable<SelectListItem> Applications { get; set; }

        [Display(Name = "Select a file for benchmark to use")]
        public Guid? ApacheTestFileId { get; set; }

        public IEnumerable<SelectListItem> ApacheTestFiles { get; set; }

        public Guid BenchmarkHost { get; set; }

        [Display(Name = "Capture Container Metrics")]
        public bool CaptureContainerMetrics { get; set; }

        [Display(Name = "Runtime length of benchmark experiment (in milliseconds)")]
        [Range(typeof(int), "60000", "60000000")]
        public int BenchmarkTimeLength { get; set; }

        public List<BenchmarkExperimentVariableViewModel> Variables { get; set; }

        //Default Number of Users For Test
        [Display(Name = "What is the default concurrent users for the test?")]
        [Range(typeof(int), "1", "10000")]
        public int ConcurrentUsers { get; set; }

        //Make this test the baseline
        [Display(Name = "Set as baseline")]
        public bool SetAsBaseLine { get; set; }

        [Display(Name = "Type of testing to be benchmarked?")]
        public Core.Enums.TestType TypeOfTest { get; set; }

        [Display(Name = "Active")]
        public bool Active { get; set; }

        [HiddenInput]
        public bool Completed { get; set; }

        [Display(Name = "Apdex T Seconds")]
        public double ApdexTSeconds { get; set; }


        [Display(Name = "Reference to what is being benchmarked e.g. TPC-W WIPS")]
        public string BenchmarkingName { get; set; }

        [Display(Name = "Secondary Reference e.g. West-Europe Experiment 2")]
        public string ExperimentReference { get; set; }
    }
}
