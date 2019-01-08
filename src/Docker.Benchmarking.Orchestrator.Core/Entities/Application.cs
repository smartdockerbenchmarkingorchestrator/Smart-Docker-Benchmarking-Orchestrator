using Docker.Benchmarking.Orchestrator.Core.Enums;
using Docker.Benchmarking.Orchestrator.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Docker.Benchmarking.Orchestrator.Core.Entities
{
    public class Application : BaseEntity
    {
        public Application()
        {
            DelayToStartApplication = 60000;
        }

        public string Description { get; set; }

        public virtual ICollection<BenchmarkExperiment> Benchmarks { get; set; }

        [Required]
        public virtual ApplicationType ApplicationType { get; set; }

        public Guid? DatabaseImageId { get; set; }

        public virtual DockerImage DatabaseImage { get; set; }

        public virtual DockerImage BenchmarkingImage { get; set; }

        public virtual DockerImage ApplicationImage { get; set; }

        public Guid? ApacheJmeterTestId { get; set; }

        public virtual ApacheJmeterTestFile TestFile { get; set; }

        [Required]
        //Delay between starting application container to starting benchmark experiment
        public int DelayToStartApplication
        {
            get; set;
        }

        public virtual ICollection<ApplicationTestVariable> Variables { get; set; }
    }
}
