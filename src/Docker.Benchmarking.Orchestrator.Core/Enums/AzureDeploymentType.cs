using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Docker.Benchmarking.Orchestrator.Core.Enums
{
    public enum AzureDeploymentType
    {
        [Display(Name = "Virtual Machine")]
        VM = 1,

        [Display(Name = "Container Instance")]
        ContainerInstance,

        [Display(Name = "WebApp Service")]
        WebAppService,

        [Display(Name = "Kubernetes Service Cluster")]
        KubernetesService
    }
}
