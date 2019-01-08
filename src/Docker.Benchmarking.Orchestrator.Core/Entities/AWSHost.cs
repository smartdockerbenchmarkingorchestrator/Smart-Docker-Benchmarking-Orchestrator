using Docker.Benchmarking.Orchestrator.Core.SharedKernel;
using System;
using System.ComponentModel.DataAnnotations;

namespace Docker.Benchmarking.Orchestrator.Core.Entities
{
    public class AWSHost : BaseEntity
    {
        public string IPAddress { get; set; }

        public Guid? DockerHostId { get; set; }

        [Required]
        public virtual DockerHost DockerHost { get; set; }

        public DateTimeOffset ResourceCreatedAt { get; set; }

        public DateTimeOffset ResourceDestroyedAt { get; set; }

        public bool DestroyResourcesAfterBenchmark { get; set; }

        [Required]
        public virtual AWSCloudFormationTemplate Template { get; set; }

        [Required]
        public virtual AWSCredentials Credentials { get; set; }

        public bool ResourcedCreated { get; set; }
        public bool ResourceCreationStarted { get; set; }
        public bool ResourceDestroyed { get; set; }
    }
}
