using Docker.Benchmarking.Orchestrator.Core.SharedKernel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Docker.Benchmarking.Orchestrator.Core.Entities
{
    public class AWSCredentials : BaseEntity
    {
        [Required]
        public string AccessKeyId { get; set; }

        [Required]
        public string SecretKey { get; set; }

        [Required]
        public string RegionEndPoint { get; set; }

        [NotMapped]
        public Amazon.RegionEndpoint AWSEndPoint { get { return Amazon.RegionEndpoint.GetBySystemName(RegionEndPoint); } }

        public virtual ICollection<AWSHost> Hosts { get; set; }
    }
}
