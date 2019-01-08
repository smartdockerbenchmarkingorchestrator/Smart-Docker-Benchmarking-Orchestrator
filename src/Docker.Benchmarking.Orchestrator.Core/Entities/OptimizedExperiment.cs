using Docker.Benchmarking.Orchestrator.Core.Enums;
using Docker.Benchmarking.Orchestrator.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Docker.Benchmarking.Orchestrator.Core.Entities
{
    public class OptimizedExperiment : BaseEntity
    {
        [Required]
        public Guid ApplicationId { get; set; }

        [Required]
        public virtual Application Application { get; set; }

        [Required]
        public int BenchmarkTimeLength { get; set; }

        [Required]
        public double ApdexTSeconds { get; set; }

        [Required]
        public CloudProvider HostCloudProvider { get; set; }

        [Required]
        public CloudProvider BenchmarkCloudProvider { get; set; }

        [Required]
        public decimal MaxCost { get; set; }

        [Required]
        public int VMsFound { get; set; }
    }
}
