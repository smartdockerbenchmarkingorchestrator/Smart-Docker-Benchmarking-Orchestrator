using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Docker.Benchmarking.Orchestrator.Core.Enums
{
    public enum ApdexRating
    {
        [Display(Name = "Excellent")]
        Excellent,

        [Display(Name = "Good")]
        Good,

        [Display(Name = "Fair")]
        Fair,

        [Display(Name = "Poor")]
        Poor,

        [Display(Name = "Unacceptable")]
        Unacceptable,

        [Display(Name = "No rating")]
        NoRating

    }
}
