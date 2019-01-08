using Docker.Benchmarking.Orchestrator.Core.Helpers;
using Docker.Benchmarking.Orchestrator.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Docker.Benchmarking.Orchestrator.Core.Entities
{
    public class CSVUpload : BaseEntity
    {
        //Store CSV inside database for retrival
        [Column(TypeName = "varchar")]
        [MaxLength]
        public string CSVResultsFile { get; set; }

        [NotMapped]
        public byte[] CSVResultsFileBytes => generateBytesFromCSVBase64();

        private byte[] generateBytesFromCSVBase64()
        {
            if (string.IsNullOrEmpty(CSVResultsFile)) return null;

            byte[] file = null;

            bool valid = CSVResultsFile.IsBase64(out file);

            return file;
        }
    }
}
