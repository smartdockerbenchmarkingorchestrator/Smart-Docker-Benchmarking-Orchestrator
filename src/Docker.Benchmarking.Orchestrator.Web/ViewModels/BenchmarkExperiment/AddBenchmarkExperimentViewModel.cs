using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Docker.Benchmarking.Orchestrator.Web.ViewModels
{
    public class AddBenchmarkExperimentViewModel
    {
        public AddBenchmarkExperimentViewModel()
        {
            CaptureContainerMetrics = true;
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

        [Remote("IsNameUnique", "BenchmarkExperiment", HttpMethod = "POST", ErrorMessage = "Name already taken, use another name.")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public Guid Host { get; set; }

        [Display(Name = "Database Host")]
        public Guid? DatabaseHost { get; set; }

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

        [Display(Name = "Host to start benchmark experiment threads")]
        public Guid BenchmarkHost { get; set; }

        [Display(Name = "Capture Container Metrics")]
        public bool CaptureContainerMetrics { get; set; }

        [Display(Name = "Runtime length of benchmark experiment (in milliseconds)")]
        public int BenchmarkTimeLength { get; set; }

        public List<BenchmarkExperimentVariableViewModel> Variables { get; set; }

        //Default Number of Users For Test
        [Display(Name = "What is the default concurrent users for the test?")]
        public int ConcurrentUsers { get; set; }

        //Make this test the baseline
        public bool SetAsBaseLine { get; set; }

        [Display(Name = "Type of testing to be benchmarked?")]
        public Core.Enums.TestType TypeOfTest { get; set; }

        [Display(Name = "Apdex T Seconds")]
        public double ApdexTSeconds { get; set; }

        [Display(Name = "Reference to what is being benchmarked e.g. TPC-W WIPS")]
        public string BenchmarkingName { get; set; }

        [Display(Name = "Secondary Reference e.g. West-Europe Experiment 2")]
        public string ExperimentReference { get; set; }





        //public void SetEnvironmentalVariables(string environmentalVariable)
        //{
        //    string[] strings = environmentalVariable.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

        //    foreach (var variable in strings)
        //    {
        //        var test = variable.Split('=');
        //    }
        //}
    }
}
