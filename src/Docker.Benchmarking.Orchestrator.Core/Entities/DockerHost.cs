using Docker.Benchmarking.Orchestrator.Core.Enums;
using Docker.Benchmarking.Orchestrator.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Docker.Benchmarking.Orchestrator.Core.Entities
{
    public class DockerHost : BaseEntity
    {
        public DockerHost()
        {
            Active = true;
        }

        public string Description { get; set; }

        //Can be Hostname or IP
        [Required]
        public string HostName { get; set; }

        [Required]
        public int PortNumber { get; set; }

        public bool UseHttpAuthentication { get; set; }

        public bool UseTlsAuthentication { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        [Required]
        public CloudProvider CloudProvider { get; set; }

        [Required]
        public string Location { get; set; }

        [InverseProperty("Host")]
        public virtual ICollection<BenchmarkExperiment> BenchmarkExperiments { get; set; }

        [InverseProperty("BenchmarkHost")]
        public virtual ICollection<BenchmarkExperiment> BenchmarkTests { get; set; }

        [InverseProperty("DatabaseHost")]
        public virtual ICollection<BenchmarkExperiment> DatabaseHosts { get; set; }

        public string HostString
        {
            get
            {
                string x = "tcp://" + HostName + ":" + PortNumber;
                return x;
            }
        }
        public string StackResourceName
        {
            get
            {
                return Name.Substring(0, Name.Length > 30 ? 30 : Name.Length).Replace(" ", "").ToLowerInvariant();
            }
        }

        [Required]
        public double vCPU { get; set; }

        //Memory value
        [Required]
        public double Memory { get; set; }

        //Memory value
        [Required]
        public double Storage { get; set; }

        public Guid? AzureHostId { get; set; }
        public virtual AzureHost AzureHost { get; set; }

        public Guid? AwsHostId { get; set; }
        public virtual AWSHost AWSHost { get; set; }

        public bool Used { get; set; }

        public HostType HostType { get; set; }
    }
}
