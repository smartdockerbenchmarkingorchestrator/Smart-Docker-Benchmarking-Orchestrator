using Docker.Benchmarking.Orchestrator.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Docker.Benchmarking.Orchestrator.Core.Entities
{
    public class AWSCloudFormationParameter : BaseEntity
    {
        new public string Name { get; set; }

        [Required]
        public string ParameterKey { get; set; }

        [Required]
        public string ParameterValue { get; set; }

        [Required]
        public virtual AWSCloudFormationTemplate AWSTemplate { get; set; }
    }
}
