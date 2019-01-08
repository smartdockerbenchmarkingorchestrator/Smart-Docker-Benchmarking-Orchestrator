using Docker.Benchmarking.Orchestrator.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Docker.Benchmarking.Orchestrator.Core.Entities
{
    public class ApacheJmeterTestFile : BaseEntity
    {
        public string Description { get; set; }

        //Stores Testfile XML inside database
        [Required]
        public string FileName { get; set; }

        public virtual ICollection<BenchmarkExperiment> BenchmarkExperiments { get; set; }

        public virtual ICollection<Application> Applications { get; set; }

        //Array for labels to ignore, required for ensuring only thread requests are recorded in the test result (not thread groups etc)
        public string[] ThreadNamesToIgnore { get; set; }
    }
}
