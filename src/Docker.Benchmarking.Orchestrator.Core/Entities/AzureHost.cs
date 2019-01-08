using Docker.Benchmarking.Orchestrator.Core.SharedKernel;
using System;
using System.ComponentModel.DataAnnotations;

namespace Docker.Benchmarking.Orchestrator.Core.Entities
{
    public class AzureHost : BaseEntity
    {
        public string IPAddress { get; set; }

        [Required]
        public string AzureRegion { get; set; }

        public Guid? DockerHostId { get; set; }

        [Required]
        public virtual DockerHost DockerHost { get; set; }

        public DateTimeOffset ResourceCreatedAt { get; set; }

        public DateTimeOffset ResourceDestroyedAt { get; set; }

        public bool DestroyResourcesAfterBenchmark { get; set; }

        [Required]
        public virtual AzureVMTemplate Template { get; set; }

        [Required]
        public virtual AzureCredientials Credentials { get; set; }

        public bool ResourcedCreated { get; set; }
        public bool ResourceCreationStarted { get; set; }
        public bool ResourceDestroyed { get; set; }
    }
}
