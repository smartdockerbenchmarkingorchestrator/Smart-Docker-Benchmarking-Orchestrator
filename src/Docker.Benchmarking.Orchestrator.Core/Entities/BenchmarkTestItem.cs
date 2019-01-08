using Docker.Benchmarking.Orchestrator.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Docker.Benchmarking.Orchestrator.Core.Entities
{
    //Based on results from ApacheJmeter output
    public class BenchmarkTestItem : BaseEntity
    {
        public BenchmarkTestItem()
        {
            ConvertToTimestamp();
        }

        //All Datetime to be offsets and start at UTC
        [Required]
        public DateTimeOffset Timestamp { get; set; }

        [Required]
        public double Elapsed { get; set; }

        new public string Name { get; set; }

        [Required]
        public string Label { get; set; }

        public int? ResponseCode { get; set; }

        [Required]
        public string ResponseMessage { get; set; }

        [Required]
        public string ThreadName { get; set; }

        public string DataType { get; set; }


        public bool Success { get; set; }

        public string FailureMessage { get; set; }

        [Required]
        public double Bytes { get; set; }

        [Required]
        public double SentBytes { get; set; }

        [Required]
        public double GroupThreads { get; set; }

        [Required]
        public double AllThreads { get; set; }

        [Required]
        public double Latency { get; set; }

        [Required]
        public double IdleTime { get; set; }

        [Required]
        public double Connect { get; set; }

        public virtual BenchmarkExperiment BenchmarkExperiment { get; set; }

        [NotMapped]
        public double UnixTimestamp { get; set; }

        public void ConvertToTimestamp()
        {
            Timestamp = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddMilliseconds(UnixTimestamp);
        }
    }
}
