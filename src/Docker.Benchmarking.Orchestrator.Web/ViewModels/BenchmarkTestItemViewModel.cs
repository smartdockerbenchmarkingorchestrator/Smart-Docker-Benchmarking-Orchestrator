using CsvHelper.Configuration;
using Docker.Benchmarking.Orchestrator.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Web.ViewModels
{
    public class BenchmarkTestItemViewModel
    {
        [Display(Name = "Id")]
        public Guid Id { get; set; }

        [Display(Name = "Timestamp")]
        public DateTime Timestamp { get; set; }

        [Display(Name = "Elapsed")]
        public double Elapsed { get; set; }

        [Display(Name = "Label")]
        public string Label { get; set; }

        [Display(Name = "Response Code")]
        public int? ResponseCode { get; set; }

        [Display(Name = "Response Message")]
        public string ResponseMessage { get; set; }

        [Display(Name = "Thread Name")]
        public string ThreadName { get; set; }

        [Display(Name = "Data Type")]
        public string DataType { get; set; }

        [Display(Name = "Success Failure")]
        public bool Success { get; set; }

        [Display(Name = "Failure Message")]
        public string FailureMessage { get; set; }

        [Display(Name = "Bytes")]
        public double Bytes { get; set; }

        [Display(Name = "Sent Bytes")]
        public double SentBytes { get; set; }

        [Display(Name = "Group Threads")]
        public double GroupThreads { get; set; }

        [Display(Name = "All threads")]
        public double AllThreads { get; set; }

        [Display(Name = "Latency")]
        public double Latency { get; set; }

        [Display(Name = "Idle Time")]
        public double IdleTime { get; set; }

        [Display(Name = "Connect Time")]
        public double Connect { get; set; }

        [Display(Name = "Benchmark")]
        public Core.Entities.BenchmarkExperiment Benchmark { get; set; }

        [Display(Name = "Unix Timestamp")]
        public double UnixTimestamp { get; set; }

        public sealed class MyClassMap : ClassMap<BenchmarkTestItemViewModel>
        {
            public MyClassMap()
            {
                Map(m => m.Timestamp);
                Map(m => m.UnixTimestamp);
                Map(m => m.Elapsed);
                Map(m => m.Label);
                Map(m => m.ResponseCode.Value);
                Map(m => m.ResponseMessage);
                Map(m => m.ThreadName);
                Map(m => m.DataType);

                Map(m => m.Success);
                Map(m => m.FailureMessage);
                Map(m => m.Bytes);
                Map(m => m.SentBytes);

                Map(m => m.GroupThreads);
                Map(m => m.AllThreads);

                Map(m => m.Latency);
                Map(m => m.IdleTime);
                Map(m => m.Connect);


                Map(m => m.Benchmark).Ignore();
            }
        }
    }
}
