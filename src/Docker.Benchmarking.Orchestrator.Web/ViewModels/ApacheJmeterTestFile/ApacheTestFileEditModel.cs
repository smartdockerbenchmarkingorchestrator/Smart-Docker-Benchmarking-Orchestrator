using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Web.ViewModels
{
    public class ApacheTestFileEditModel
    {
        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        [HiddenInput(DisplayValue = false)]
        public DateTimeOffset DateTimeCreated { get; set; }

        [Remote("ValidateApacheJmeterName", controller: "RemoteValidator", HttpMethod = "POST", AdditionalFields = "Id", ErrorMessage = "Name already taken, use another name.")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Active")]
        public bool Active { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "JMX Source File")]
        public string TestUpload { get; set; }

        public string[] ThreadNamesToIgnore { get; set; }
    }
}
