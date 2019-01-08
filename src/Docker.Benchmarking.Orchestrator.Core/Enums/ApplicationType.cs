using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Docker.Benchmarking.Orchestrator.Core.Enums
{
    //MN: Used these sites for different type of applications:
    //https://www.methodandclass.com/articles/what-are-the-different-types-of-website
    public enum ApplicationType
    {
        [Display(Name = "E-Commerce")]
        ECommerce = 1,
        [Display(Name = "Content Management System")]
        CMS = 2,
        [Display(Name = "Static")]
        Static = 3,
        [Display(Name = "Portal")]
        Portal = 4,
        [Display(Name = "Bespoke")]
        Bespoke = 5,
        [Display(Name = "API")]
        API = 6,
        [Display(Name = "Microservice")]
        Microservice = 7,
        [Display(Name = "High Performance")]
        HighPerformance = 8,
        [Display(Name = "Other")]
        Other = 100
    }
}
