using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Web.ViewModels.CSVFile
{
    public class CSVUploadViewModel
    {
        [Display (Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "CSV Results File")]
        public string CSVResultsFile { get; set; }

        [Display(Name = "Base64")]
        public byte[] CSVResultsFileBytes { get; set; }

        [Display(Name = "Id")]
        public Guid Id { get; set; }

        [Display(Name = "DateTime Created/Uploaded")]
        public DateTimeOffset DateTimeCreated { get; set; }

    }
}
