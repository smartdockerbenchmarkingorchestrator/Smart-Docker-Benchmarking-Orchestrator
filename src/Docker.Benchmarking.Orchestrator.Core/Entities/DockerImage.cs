using Docker.Benchmarking.Orchestrator.Core.Enums;
using Docker.Benchmarking.Orchestrator.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Docker.Benchmarking.Orchestrator.Core.Entities
{
    public class DockerImage : BaseEntity
    {
        public DockerImage()
        {

        }
        public string Description { get; set; }

        [Required]
        public string ImageName { get; set; }

        [Required]
        public string ImageTag { get; set; }

        public bool PrivateRepository { get; set; }

        public string PrivateRepositoryHost { get; set; }

        public string PrivateRepositoryUsername { get; set; }

        public string PrivateRepositoryPassword { get; set; }

        public int? ExternalPort { get; set; }

        public int? InternalPort { get; set; }

        //https://docs.microsoft.com/en-us/ef/core/modeling/value-conversions
        [Required]
        public ImageType ImageType { get; set; }

        public virtual ICollection<DockerImageVariable> Variables { get; set; }

        [IgnoreMember]
        public string FullImageTag
        {
            get
            {
                return ImageName + ":" + ImageTag;
            }
        }

        public string DockerFriendlyName
        {
            get
            {
                return Name.Replace(" ", "").ToLower();
            }
        }
    }
}
