using Docker.Benchmarking.Orchestrator.Core.Entities;
using Docker.Benchmarking.Orchestrator.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Web.APIModels
{
    public class ApplicationDetailsAPIModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int BenchmarkCount { get; set; }

        public ApplicationType ApplicationType { get; set; }

        //public virtual DockerImage BenchmarkingImage { get; set; }

        //public virtual DockerImage ApplicationImage { get; set; }

        public Guid? ApacheJmeterTestId { get; set; }

        //public virtual ApacheJmeterTestFile TestFile { get; set; }

        public int DelayToStartApplication
        {
            get; set;
        }

        public DateTimeOffset DateTimeCreated { get; set; }
    }
}
