using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Docker.Benchmarking.Orchestrator.Core.Enums
{
    public enum ImageType
    {
        [Display(Name = "Application")]
        Application = 1,

        [Display(Name = "Apache Jmeter")]
        ApacheJmeter,

        [Display(Name = "Database")]
        Database
    }
}
