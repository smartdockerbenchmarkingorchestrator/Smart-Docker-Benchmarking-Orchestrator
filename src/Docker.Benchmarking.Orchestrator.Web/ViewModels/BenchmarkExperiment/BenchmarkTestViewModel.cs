using Docker.Benchmarking.Orchestrator.Core.Entities;
using Docker.Benchmarking.Orchestrator.Core.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Docker.Benchmarking.Orchestrator.Web.ViewModels
{
    public class BenchmarkTestViewModel
    {
        public Guid Id { get; set; }
        public Guid BenchmarkExperimentId { get; set; }
        public string Name { get; set; }

        public Application Application { get; set; }

        public virtual DockerHost Host { get; set; }
        public virtual DockerHost BenchmarkHost { get; set; }

        public bool Started { get; set; }
        public DateTimeOffset StartedAt { get; set; }

        public bool Completed { get; set; }

        public DateTimeOffset CompletedAt { get; set; }
        public TimeSpan TimeToComplete => CompletedAt - StartedAt;

        //ContainerMetrics
        public bool CaptureContainerMetrics { get; set; }
        public int NumberOfContainerMetricRecords { get; set; }

        //CPU

        public double CPUPercentageMean { get; set; }


        public double CPUPercentageRange { get; set; }


        public double CPUPercentageHighest { get; set; }


        public double CPUPercentageLowest { get; set; }


        public double vCPU { get; set; }

        public double CPUStandDeviation { get; set; }
        public double CPUStandDeviationSample { get; set; }

        public double MemoryStandDeviation { get; set; }
        public double MemoryStandDeviationSample { get; set; }

        public bool JmeterResultsProcessed { get; set; }

        //Memory

        public double MemoryPercentageMean { get; set; }


        public double MemoryPercentageRange { get; set; }


        public double MemoryPercentageHighest { get; set; }


        public double MemoryPercentageLowest { get; set; }


        public double Memory { get; set; }

        //WebServerMetrics
        public double AverageLatency { get; set; }

        public bool SetAsBaseLine { get; set; }

        public int BenchmarkTimeLength { get; set; }

        public CloudProvider CloudProvider { get; set; }

        //WebServer Jmeter Benchmark Metrics
        public double WebServerBenchmarkElapsedTime { get; set; }

        public DateTimeOffset WebServerStartTime { get; set; }

        public DateTimeOffset WebServerEndTime { get; set; }

        public int NumberOfSamples { get; set; }

        public int NumberOfErrors { get; set; }

        public double AverageResponseTime { get; set; }

        public double MinResponseTime { get; set; }

        public double MaxResponseTime { get; set; }

        //bytes
        public double AverageReceivedBytes { get; set; }

        public double AverageSentBytes { get; set; }

        public double TotalReceivedBytes { get; set; }

        public double TotalSentBytes { get; set; }

        public double AverageLatecy { get; set; }

        public double MaxLatency { get; set; }

        public double MinLatency { get; set; }

        public double ThroughputPerSecond { get; set; }

        public double StandardDeviationWebServerResponse { get; set; }

        public double StandardDeviationSameWebServerResponse { get; set; }

        public double ApdexTSeconds { get; set; }

        public double ApdexScore { get; set; }

        public double ApdexSatisfied { get; set; }

        public double ApdexTolerating { get; set; }

        public double ApdexFustrated { get; set; }

        public ApdexRating ApdexRating { get; set; }

        public byte[] CSVResultsFileBytes { get; set; }

        public string CSVResultsFile { get; set; }

        public bool Active { get; set; }


        //Network and Block
        public double NetworkInputTotalBytes { get; set; }

        public double NetworkOutputTotalBytes { get; set; }

        public double BlockInputTotal { get; set; }

        public double BlockOutputTotal { get; set; }

        [Display(Name = "Reference to Benchmark")]
        public string BenchmarkingName { get; set; }

        [Display(Name = "Secondary Reference")]
        public string ExperimentReference { get; set; }

        public string InstanceName()
        {
            return (Host.AWSHost != null ? Host.AWSHost.Template.InstanceName : Host.AzureHost != null ? Host.AzureHost.Template.VMSize : "");
        }
    }
}
