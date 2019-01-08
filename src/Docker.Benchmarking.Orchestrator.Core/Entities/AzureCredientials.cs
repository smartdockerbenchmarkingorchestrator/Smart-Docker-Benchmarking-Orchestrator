using Docker.Benchmarking.Orchestrator.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Docker.Benchmarking.Orchestrator.Core.Entities
{
    public class AzureCredientials : BaseEntity
    {
        [Required]
        public string SubscriptionId { get; set; }

        [Required]
        public string ClientId { get; set; }

        [Required]
        public string Secret { get; set; }

        [Required]
        public string TenantId { get; set; }

        public virtual ICollection<AzureHost> Hosts { get; set; }
    }
}
