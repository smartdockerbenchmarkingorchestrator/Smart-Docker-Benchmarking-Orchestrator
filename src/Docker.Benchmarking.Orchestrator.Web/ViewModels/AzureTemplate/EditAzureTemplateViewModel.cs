using Docker.Benchmarking.Orchestrator.Core.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Web.ViewModels
{
    public class EditAzureTemplateViewModel
    {
        [Display(AutoGenerateField = false)]
        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        [Display(AutoGenerateField = false)]
        [HiddenInput(DisplayValue = false)]
        public DateTimeOffset DateTimeCreated { get; set; }

        [Remote("ValidateAzureTemplateName", "RemoteValidator", HttpMethod = "POST", AdditionalFields = "Id", ErrorMessage = "Name already taken, use another name.")]
        public string Name { get; set; }

        [Remote("IsVMSizeUnique", "Azure", HttpMethod = "POST", AdditionalFields = "Id", ErrorMessage = "VM Size already taken, use another name.")]
        public string VMSize { get; set; }

        public IEnumerable<string> VMSizes { get; set; }

        [Display(Name = "CPUs")]
        public double vCPUs { get; set; }

        [Display(Name = "Memory (MB)")]
        public double Memory { get; set; }

        [Display(Name = "Disk Size (GB)")]
        public double DiskSize { get; set; }

        [Display(Name = "Template Json")]
        [DataType(DataType.MultilineText)]
        public string Template { get; set; }

        [Display(Name = "Parameters Json")]
        [DataType(DataType.MultilineText)]
        public string ParametersDefault { get; set; }

        [Display(Name = "Price per hour")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal PricePerHour { get; set; }

        public bool Active { get; set; }

        public AzureDeploymentType DeploymentType { get; set; }

        [Display(Name = "VM Size")]
        public VMSize VMSizeType { get; set; }
    }
}
