using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Docker.Benchmarking.Orchestrator.Core.Entities;
using Docker.Benchmarking.Orchestrator.Core.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Docker.Benchmarking.Orchestrator.Web.ViewModels
{
    public class AddDockerHostViewModel
    {
        public AddDockerHostViewModel()
        {
            PortNumber = 2376;
            HostName = "locahost";
            Active = true;
            DestroyResourcesAfterBenchmark = true;
        }

        [Display(Name = "Name or Tagname of Host")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        [Remote("ValidateDockerHostName", "RemoteValidator", HttpMethod = "POST", ErrorMessage = "Name already taken, use another name.")]
        public string Name { get; set; }

        [Display(Name = "Description of Host")]
        public string Description { get; set; }

        //Can be Hostname or IP
        [Display(Name = "Hostname or IP Address of Host")]
        public string HostName { get; set; }

        [Display(Name = "Port Number")]
        public int PortNumber { get; set; }

        [Display(Name = "Http Authentication Required")]
        public bool UseHttpAuthentication { get; set; }

        [Display(Name = "Http Authentication Username")]
        public string UserName { get; set; }

        [Display(Name = "Http Authentication Password")]
        public string Password { get; set; }

        [Display(Name = "Tls Authentication to Host")]
        public bool UseTlsAuthentication { get; set; }

        [HiddenInput(DisplayValue = false)]
        public bool Active { get; set; } = true;

        [Display(Name = "Cloud Provider")]
        public CloudProvider CloudProvider { get; set; }

        [Display(Name = "Location of Host", Prompt = "e.g.  UK West, US East 1")]
        public string Location { get; set; }

        //
        [Display(Name = "vCPUs", Prompt = "e.g. 2")]
        public double vCPU { get; set; }

        [Display(Name = "Memory (in GB)", Prompt = "e.g. 4")]
        public double Memory { get; set; }

        [Display(Name = "Destroy Resources After Benchmark (if AWS or Azure)", Prompt = "")]
        public bool DestroyResourcesAfterBenchmark { get; set; }

        public IEnumerable<SelectListItem> AzureLocations { get; set; }

        public IEnumerable<SelectListItem> AzureCredentials { get; set; }

        public IEnumerable<AzureVMTemplate> AzureVMSizes { get; set; }

        public IEnumerable<AWSCloudFormationTemplate> AWSTemplates { get; set; }

        public Guid? AzureTemplate { get; set; }

        [Display(Name = "Azure Locations", Prompt = "")]
        public string AzureLocation { get; set; }

        public Guid? AWSTemplate { get; set; }

        [Display(Name = "AWS Credentials", Prompt = "")]
        public Guid? AWSCredential { get; set; }

        [Display(Name = "Azure Credentials", Prompt = "")]
        public Guid? AzureCredential { get; set; }


        public IEnumerable<AWSCredentials> AWSCredentials { get; set; }

        [Display(Name = "HostType")]
        public HostType HostType { get; set; }

    }
}
