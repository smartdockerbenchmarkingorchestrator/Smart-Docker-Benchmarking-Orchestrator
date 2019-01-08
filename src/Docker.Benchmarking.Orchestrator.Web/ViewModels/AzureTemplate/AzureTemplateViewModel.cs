using Docker.Benchmarking.Orchestrator.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Web.ViewModels
{
    public class AzureTemplateViewModel
    {
        public Guid Id { get; set; }

        public DateTimeOffset DateTimeCreated { get; set; }

        public string Name { get; set; }

        public string VMSize { get; set; }

        [Display(Name = "CPUs")]
        public double vCPUs { get; set; }

        [Display(Name = "Memory (MB)")]
        public double Memory { get; set; }

        [Display(Name = "Disk Size (GB)")]
        public double DiskSize { get; set; }

        [Display(Name = "Template Json")]
        public string Template { get; set; }

        [Display(Name = "Parameters Json")]
        public string ParametersDefault { get; set; }

        [Display(Name = "Price per hour")]
        public decimal PricePerHour { get; set; }

        public bool Active { get; set; }

        [Display(Name = "VM Size")]
        public VMSize VMSizeType { get; set; }
    }
}
