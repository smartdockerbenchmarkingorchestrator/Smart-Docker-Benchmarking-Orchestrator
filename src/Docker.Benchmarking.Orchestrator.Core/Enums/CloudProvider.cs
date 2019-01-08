using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Docker.Benchmarking.Orchestrator.Core.Enums
{
    public enum CloudProvider
    {
        [Display(Name = "Azure")]
        Azure = 1,
        [Display(Name = "Amazon Web Services")]
        AWS = 2,
        [Display(Name = "Google Cloud Platform")]
        GCP = 3,
        [Display(Name = "IBM")]
        IBM,

        [Display(Name = "Oracle")]
        Oracle,

        [Display(Name = "Rackspace")]
        Rackspace,

        [Display(Name = "Digital Ocean")]
        DigitalOcean,

        [Display(Name = "VMWare")]
        VMWare,

        [Display(Name = "Other Public Cloud")]
        OtherPublic = 100,
        [Display(Name = "Private Cloud")]
        PrivateCloud = 999

    }
}
