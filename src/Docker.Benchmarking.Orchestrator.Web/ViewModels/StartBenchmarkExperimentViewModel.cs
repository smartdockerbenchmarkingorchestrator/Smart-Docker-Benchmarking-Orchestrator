using Docker.Benchmarking.Orchestrator.Core.Entities;
using Docker.Benchmarking.Orchestrator.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Docker.Benchmarking.Orchestrator.Web.ViewModels
{
    public class StartBenchmarkExperimentViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DockerHost Host { get; set; }

        public string Comments { get; set; }

        public Application Application { get; set; }

        public ApacheJmeterTestFile TestFile { get; set; }

        public DockerHost BenchmarkHost { get; set; }

        public bool Started { get; set; }

        public DateTimeOffset StartedAt { get; set; }

        public bool Completed { get; set; }

        public DateTimeOffset CompletedAt { get; set; }

        public int BenchmarkTimeLength { get; set; }

        public bool JmeterResultsProcessed { get; set; }

        public TimeSpan BenchmarkLength { get; set; }

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

        //Memory
        public double MemoryPercentageMean { get; set; }

        public double MemoryPercentageRange { get; set; }

        public double MemoryPercentageHighest { get; set; }

        public double MemoryPercentageLowest { get; set; }

        public double MemoryStandDeviation { get; set; }

        public double NetworkInputTotalBytes { get; set; }

        public double NetworkOutputTotalBytes { get; set; }

        public double BlockInputTotal { get; set; }

        public double BlockOutputTotal { get; set; }

        //Network
        //public double MemoryPercentageMean { get; set; }

        //public double MemoryPercentageRange { get; set; }

        //public double MemoryPercentageHighest { get; set; }

        //public double MemoryPercentageLowest { get; set; }

        //public double MemoryStandDeviation { get; set; }

        public double Memory { get; set; }

        public virtual IEnumerable<BenchmarkExperimentVariable> Variables { get; set; }

        public double ThroughputPerSecond { get; set; }

        public double StandardDeviationWebServerResponse { get; set; }

        public double StandardDeviationSameWebServerResponse { get; set; }

        public double ApdexTSeconds { get; set; }

        public double ApdexScore { get; set; }

        public double ApdexSatisfied { get; set; }

        public double ApdexTolerating { get; set; }

        public double ApdexFustrated { get; set; }

        public ApdexRating ApdexRating { get; set; }

        public int NumberOfSamples { get; set; }

        public int NumberOfErrors { get; set; }

        public double AverageResponseTime { get; set; }

        public double MinResponseTime { get; set; }

        public double MaxResponseTime { get; set; }

        public double AverageLatecy { get; set; }

        public double MaxLatency { get; set; }

        public double MinLatency { get; set; }

        [Display(Name = "Reference to Benchmark")]
        public string BenchmarkingName { get; set; }

        [Display(Name = "Secondary Reference")]
        public string ExperimentReference { get; set; }

        public bool ShowCreateAWSResourcesApplication { get; set; }

        public bool ShowCreateAzureResourcesApplication { get; set; }

        /// <summary>
        /// Executes functionality for whether start benchmarking button should be shown
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool ShowStartBenchmarking { get; set; }

        public bool ShowResourceResults()
        {
            if (Completed)
            {
                return true;
            }

            return false;
        }

        public bool AzureApplicationResourcesCreated { get; set; }

        public bool AWSApplicationResourcesCreated { get; set; }

        public bool AWSBenchmarkingResourcesCreated { get; set; }

        public bool AzureBenchmarkingResourcesCreated { get; set; }

        public bool ShowBenchmarkResults()
        {
            if (JmeterResultsProcessed) return true;
            return false;
        }
    }
}
