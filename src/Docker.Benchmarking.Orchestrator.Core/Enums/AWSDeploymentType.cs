using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Docker.Benchmarking.Orchestrator.Core.Enums
{
    public enum AWSDeploymentType
    {
        [Display (Name = "EC2 Instance")]
        EC2Instance = 1
    }
}
