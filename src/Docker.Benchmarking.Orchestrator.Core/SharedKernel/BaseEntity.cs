using Docker.Benchmarking.Orchestrator.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Docker.Benchmarking.Orchestrator.Core.SharedKernel
{
    // This can be modified to BaseEntity<TId> to support multiple key types (e.g. Guid)
    public abstract class BaseEntity : IBaseEntity
    {
        public BaseEntity()
        {
            Active = true;
        }

        [Required]
        public DateTimeOffset DateTimeCreated { get; set; } = DateTime.UtcNow;

        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public bool Active { get; set; } = true;


        public List<BaseDomainEvent> Events = new List<BaseDomainEvent>();
    }
}
