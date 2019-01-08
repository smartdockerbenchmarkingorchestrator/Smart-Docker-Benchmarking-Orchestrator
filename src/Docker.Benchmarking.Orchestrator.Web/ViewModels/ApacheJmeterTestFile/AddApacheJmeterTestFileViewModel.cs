using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Web.ViewModels
{
    public class AddApacheJmeterTestFileViewModel
    {

        [Remote("ValidateApacheJmeterName", "RemoteValidator", HttpMethod = "POST", ErrorMessage = "Name already taken, use another name.")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DataType(DataType.MultilineText)]
        public string TestUpload { get; set; }

        public string[] ThreadNamesToIgnore { get; set; }
    }
}
