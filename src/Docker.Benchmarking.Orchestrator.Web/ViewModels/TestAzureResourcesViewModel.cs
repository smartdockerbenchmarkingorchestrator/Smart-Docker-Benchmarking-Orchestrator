using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Web.ViewModels
{
    public class TestAzureResourcesViewModel
    {
        public TestAzureResourcesViewModel()
        {
            if (string.IsNullOrEmpty(ResourceName))
            {
                ResourceName = "DockerBA" + DateTime.UtcNow.Ticks;
                ResourceName = ResourceName.ToLower();
            }
        }

        public string ResourceName { get; set; }

        public string AzureRegion { get; set; }

        public IEnumerable<SelectListItem> AzureRegions { get; set; }

        public string DeploymentJson { get; set; }

        public string ParametersJson { get; set; }

        public string IpAddress { get; set; }

        [Display(Name = "Credentials")]
        public Guid CredentialsId { get; set; }

        public IEnumerable<SelectListItem> Credentials { get; set; }

        [Display(Name = "Image To Deploy")]
        public Guid DockerImage { get; set; }

        public IEnumerable<SelectListItem> DockerImages { get; set; }
    }
}
