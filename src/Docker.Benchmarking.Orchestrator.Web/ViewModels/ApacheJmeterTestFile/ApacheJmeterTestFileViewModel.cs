using Docker.Benchmarking.Orchestrator.Core.Entities;
using System;

namespace Docker.Benchmarking.Orchestrator.Web.ViewModels
{
    public class ApacheJmeterTestFileViewModel
    {
        public Guid Id { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string FileName { get; set; }

        public Core.Entities.BenchmarkExperiment Benchmark { get; set; }

        public Application Application { get; set; }

        public bool Active { get; set; }

        public string[] ThreadNamesToIgnore { get; set; }
    }
}
