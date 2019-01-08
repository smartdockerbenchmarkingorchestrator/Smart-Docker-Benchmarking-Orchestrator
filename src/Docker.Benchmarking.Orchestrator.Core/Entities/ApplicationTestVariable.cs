using Docker.Benchmarking.Orchestrator.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Docker.Benchmarking.Orchestrator.Core.Entities
{
    public class ApplicationTestVariable : BaseEntity
    {
        [Required]
        public string Value { get; set; }

        [Required]
        public virtual Application Image { get; set; }
    }
}
