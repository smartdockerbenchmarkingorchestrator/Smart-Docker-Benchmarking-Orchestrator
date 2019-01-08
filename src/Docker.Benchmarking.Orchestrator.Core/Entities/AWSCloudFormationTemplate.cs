using Docker.Benchmarking.Orchestrator.Core.Enums;
using Docker.Benchmarking.Orchestrator.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Docker.Benchmarking.Orchestrator.Core.Entities
{
    public class AWSCloudFormationTemplate : BaseEntity
    {
        [Required]
        public string Template { get; set; }

        [Required]
        public double vCPUs { get; set; }

        [Required]
        public string InstanceName { get; set; }

        [Required]
        public double Memory { get; set; }

        [Required]
        public double DiskSize { get; set; }

        [Required]
        public VMSize VMSizeType { get; set; } = Enums.VMSize.Small;

        [Required]
        public decimal PricePerHour { get; set; }

        public AWSDeploymentType DeploymentType { get; set; }

        public virtual ICollection<AWSHost> AwsHosts { get; set; }

        public virtual ICollection<AWSCloudFormationParameter> Parameters { get; set; }
    }
}
