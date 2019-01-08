using Docker.Benchmarking.Orchestrator.Core.Enums;
using Docker.Benchmarking.Orchestrator.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Docker.Benchmarking.Orchestrator.Core.Entities
{
    public class AzureVMTemplate : BaseEntity
    {
        [Required]
        public string VMSize { get; set; }

        [Required]
        public double vCPUs { get; set; }

        [Required]
        public double Memory { get; set; }

        [Required]
        public double DiskSize { get; set; }

        [Required]
        public string Template { get; set; }

        [Required]
        public VMSize VMSizeType { get; set; } = Enums.VMSize.Small;

        [Required]
        public string ParametersDefault { get; set; }

        public virtual ICollection<AzureHost> AzureHosts { get; set; }

        public decimal PricePerHour { get; set; }

        public AzureDeploymentType DeploymentType { get; set; }
    }
}
