using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Docker.Benchmarking.Orchestrator.Core.Enums
{
    public enum TestType
    {
        [Display(Name = "Load Test")]
        Load = 1,

        [Display(Name = "Stress Test")]
        Stress = 2,

        [Display(Name = "Spike Test")]
        Spike = 3,

        [Display(Name = "Soak Test")]
        Soak = 4,

        [Display(Name = "Other")]
        Other = 100,
    }
}
