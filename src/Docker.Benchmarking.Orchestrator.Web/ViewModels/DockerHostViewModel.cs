using Docker.Benchmarking.Orchestrator.Core.Entities;
using Docker.Benchmarking.Orchestrator.Core.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Web.ViewModels
{
    public class DockerHostViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Name or Tagname of Host")]
        public string Name { get; set; }

        [Display(Name = "Description of Host")]
        public string Description { get; set; }

        //Can be Hostname or IP
        [Display(Name = "Hostname")]
        public string HostName { get; set; }

        [Display(Name = "Port Number")]
        public int PortNumber { get; set; }

        [Display(Name = "Http Authentication?")]
        public bool UseHttpAuthentication { get; set; }

        [Display(Name = "Http Authentication Username")]
        public string UserName { get; set; }

        [Display(Name = "Http Authentication Password")]
        public string Password { get; set; }

        [Display(Name = "Tls Authentication")]
        public bool UseTlsAuthentication { get; set; }

        public bool Active { get; set; }

        [Display(Name = "Cloud Provider")]
        public CloudProvider CloudProvider { get; set; }

        [Display(Name = "Location of Host", Prompt = "e.g.  UK West, US East 1")]
        public string Location { get; set; }

        //
        [Display(Name = "vCPUs", Prompt = "e.g. 2")]
        public double vCPU { get; set; }

        [Display(Name = "Memory (in MB)", Prompt = "e.g. 4096")]
        public double Memory { get; set; }
    }
}
