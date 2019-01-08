using Docker.Benchmarking.Orchestrator.Core.Enums;
using Docker.Benchmarking.Orchestrator.Core.Events;
using Docker.Benchmarking.Orchestrator.Core.Helpers;
using Docker.Benchmarking.Orchestrator.Core.SharedKernel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Docker.Benchmarking.Orchestrator.Core.Entities
{
    public class BenchmarkExperiment : BaseEntity
    {
        public BenchmarkExperiment()
        {
            if (BenchmarkTimeLength == 0) BenchmarkTimeLength = 300000;

            //Apdex set in seconds
            if (ApdexTSeconds == 0) ApdexTSeconds = 1.2;
        }

        public string Description { get; set; }

        [Required]
        [ForeignKey("ApplicationHostId")]

        public virtual DockerHost Host { get; set; }

        public string Comments { get; set; }

        [JsonIgnore]
        public virtual ICollection<BenchmarkTestItem> TestResults { get; set; }

        [Required]
        public virtual Application Application { get; set; }

        public bool Started { get; set; }

        public DateTimeOffset StartedAt { get; set; }

        public bool Completed { get; set; }

        public DateTimeOffset CompletedAt { get; set; }

        [NotMapped]
        public TimeSpan BenchmarkLength
        {
            get
            {
                return CompletedAt - StartedAt;
            }
        }

        public Guid? ApacheJmeterTestId { get; set; }

        public virtual ApacheJmeterTestFile TestFile { get; set; }

        public Guid BenchmarkHostId { get; set; }
        public Guid ApplicationHostId { get; set; }

        //Host for the BenchmarkExperiment
        [Required]
        [ForeignKey("BenchmarkHostId")]
        public virtual DockerHost BenchmarkHost { get; set; }

        public Guid? DatabaseHostId { get; set; }

        [ForeignKey("DatabaseHostId")]
        public virtual DockerHost DatabaseHost { get; set; }

        public bool StopAppContainerAfterExperiment { get; set; }

        public string DockerFriendlyName
        {
            get
            {
                return Name.Replace(" ", "").ToLower();
            }
        }

        public bool CaptureContainerMetrics { get; set; }

        public string AppContainerId { get; set; }

        public string BenchmarkContainerId { get; set; }

        [JsonIgnore]
        public virtual ICollection<ContainerMetric> ContainerMetrics { get; set; }

        [Required]
        public int BenchmarkTimeLength { get; set; }

        //Store Hangfire JobId for reference
        public string HangfireContainerMetricsJobId { get; set; }

        //Default Number of Users For Test
        public int ConcurrentUsers { get; set; }

        //Make this test the baseline
        public bool SetAsBaseLine { get; set; }

        //Make this test the baseline
        public TestType TypeOfTest { get; set; }

        [Column(TypeName = "Text")]
        public string ResultsCsv { get; set; }

        public virtual ICollection<BenchmarkExperimentVariable> Variables { get; set; }

        //Store CSV inside database for retrival
        [Column(TypeName = "varchar")]
        [MaxLength]
        public string CSVResultsFile { get; set; }

        [NotMapped]
        public byte[] CSVResultsFileBytes => generateBytesFromCSVBase64();

        //CPU 
        public double MemoryPercentageMean { get; set; }

        public double MemoryPercentageRange { get; set; }

        public double MemoryPercentageHighest { get; set; }

        public double MemoryPercentageLowest { get; set; }

        public double Memory { get; set; }

        public double MemoryStandDeviation { get; set; }
        public double MemoryStandDeviationSample { get; set; }

        public double CPUPercentageMean { get; set; }

        public double CPUPercentageRange { get; set; }

        public double CPUPercentageHighest { get; set; }

        public double CPUPercentageLowest { get; set; }

        public double vCPU { get; set; }

        public double CPUStandDeviation { get; set; }
        public double CPUStandDeviationSample { get; set; }

        public double NetworkInputTotalBytes { get; set; }

        public double NetworkOutputTotalBytes { get; set; }

        public double BlockInputTotal { get; set; }

        public double BlockOutputTotal { get; set; }

        public bool JmeterResultsProcessed { get; set; }

        //WebServer Benchmarking Metrics
        [NotMapped]
        public double WebServerBenchmarkElapsedTime { get { return (WebServerEndTime - WebServerStartTime).TotalSeconds; } }

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

        [NotMapped]
        public double ThroughputPerSecond { get { return NumberOfSamples / WebServerBenchmarkElapsedTime; } }

        public double StandardDeviationWebServerResponse { get; set; }

        public double StandardDeviationSameWebServerResponse { get; set; }

        //Apdex 

        [Required]
        public double ApdexTSeconds { get; set; }

        public double ApdexScore { get; set; }

        public double ApdexSatisfied { get; set; }

        public double ApdexTolerating { get; set; }

        public double ApdexFustrated { get; set; }

        [NotMapped]
        public ApdexRating ApdexRating { get { return GenerateApdexRating(); } }

        public void CreateBenchmarkStats(List<ContainerMetric> metrics)
        {
            if (metrics.Count == 0) return;

            MemoryPercentageMean = metrics.DefaultIfEmpty().Average(c => c.MemoryPercentage);
            MemoryPercentageRange = metrics.DefaultIfEmpty().Max(c => c.MemoryPercentage) - metrics.DefaultIfEmpty().Min(c => c.MemoryPercentage);
            MemoryPercentageHighest = metrics.DefaultIfEmpty().Max(c => c.MemoryPercentage);
            MemoryPercentageLowest = metrics.DefaultIfEmpty().Min(c => c.MemoryPercentage);

            MemoryStandDeviation = StandardDeviation(metrics.Select(c => c.MemoryPercentage).ToList());

            CPUPercentageMean = metrics.DefaultIfEmpty().Average(c => c.CPUPercentage);
            CPUPercentageRange = metrics.DefaultIfEmpty().Max(c => c.CPUPercentage) - metrics.Min(c => c.CPUPercentage);
            CPUPercentageHighest = metrics.DefaultIfEmpty().Max(c => c.CPUPercentage);
            CPUPercentageLowest = metrics.DefaultIfEmpty().Min(c => c.CPUPercentage);

            CPUStandDeviation = StandardDeviation(metrics.Select(c => c.CPUPercentage).ToList());

            NetworkInputTotalBytes = metrics.DefaultIfEmpty().Sum(c => c.NetworkInput);
            NetworkOutputTotalBytes = metrics.DefaultIfEmpty().Sum(c => c.NetworkOutput);

            BlockInputTotal = metrics.DefaultIfEmpty().Sum(c => c.BlockInput);
            BlockOutputTotal = metrics.DefaultIfEmpty().Sum(c => c.BlockOutput);
        }

        public void BenchmarkStatsCalculation(List<BenchmarkTestItem> webServerMetrics)
        {
            if (webServerMetrics.Count == 0) return;

            //reorder the items
            webServerMetrics = webServerMetrics.OrderBy(c => c.Timestamp).ToList();

            WebServerStartTime = webServerMetrics.Min(c => c.Timestamp).ToUniversalTime();
            WebServerEndTime = webServerMetrics.Max(c => c.Timestamp).ToUniversalTime();

            NumberOfSamples = webServerMetrics.Count;

            NumberOfErrors = webServerMetrics.Where(c => !c.Success).Count();

            AverageResponseTime = webServerMetrics.Average(c => c.Elapsed);
            MinResponseTime = webServerMetrics.Min(c => c.Elapsed);
            MaxResponseTime = webServerMetrics.Max(c => c.Elapsed);

            AverageReceivedBytes = webServerMetrics.Average(c => c.Bytes);
            AverageSentBytes = webServerMetrics.Average(c => c.SentBytes);

            TotalReceivedBytes = webServerMetrics.Sum(c => c.Bytes);
            TotalSentBytes = webServerMetrics.Sum(c => c.SentBytes);

            AverageLatecy = webServerMetrics.Average(c => c.Latency);
            MinLatency = webServerMetrics.Min(c => c.Latency);
            MaxLatency = webServerMetrics.Max(c => c.Latency);

            StandardDeviationWebServerResponse = StandardDeviation(webServerMetrics.Select(c => c.Elapsed).ToList());

            //Generate Apdex Values
            var appTDexMiliSeconds = (ApdexTSeconds * 1000);

            ApdexSatisfied = webServerMetrics.Where(c => c.Success && c.Elapsed <= appTDexMiliSeconds).Count();
            ApdexTolerating = webServerMetrics.Where(c => c.Success && (c.Elapsed > appTDexMiliSeconds && c.Elapsed <= (appTDexMiliSeconds * 4))).Count();
            ApdexFustrated = webServerMetrics.Where(c => c.Success && c.Elapsed > (appTDexMiliSeconds * 4)).Count() + NumberOfErrors;

            ApdexScore = (ApdexSatisfied + (ApdexTolerating / 2)) / NumberOfSamples;
        }

        //https://stackoverflow.com/questions/895929/how-do-i-determine-the-standard-deviation-stddev-of-a-set-of-values
        double StandardDeviation(List<double> valueList)
        {
            double M = 0.0;
            double S = 0.0;
            int k = 1;
            foreach (double value in valueList)
            {
                double tmpM = M;
                M += (value - tmpM) / k;
                S += (value - tmpM) * (value - M);
                k++;
            }
            return Math.Sqrt(S / (k - 2));
        }

        public void BenchmarkExperimentCompleted()
        {
            Events.Add(new ExperimentFinishedEvent(this));
        }

        private ApdexRating GenerateApdexRating()
        {
            if (ApdexScore >= 0.94) return ApdexRating.Excellent;

            if (ApdexScore >= 0.85 && ApdexScore < 0.94) return ApdexRating.Good;

            if (ApdexScore >= 0.70 && ApdexScore < 0.85) return ApdexRating.Fair;

            if (ApdexScore >= 0.50 && ApdexScore < 0.70) return ApdexRating.Poor;

            if (ApdexScore < 0.50) return ApdexRating.Unacceptable;

            return ApdexRating.NoRating;
        }

        private byte[] generateBytesFromCSVBase64()
        {
            if (string.IsNullOrEmpty(CSVResultsFile)) return null;

            byte[] file = null;

            bool valid = CSVResultsFile.IsBase64(out file);

            return file;
        }

        public string BenchmarkingName { get; set; }

        public string ExperimentReference { get; set; }

        public bool AzureApplicationResourcesCreated()
        {
            if (Host.CloudProvider == CloudProvider.Azure && Host.AzureHost != null && Host.AzureHost.ResourceCreatedAt != new DateTimeOffset(DateTime.MinValue, TimeSpan.Zero)) return true;

            return false;
        }

        public bool AWSApplicationResourcesCreated()
        {
            if (Host.CloudProvider == CloudProvider.AWS && Host.AWSHost != null && Host.AWSHost.ResourceCreatedAt != new DateTimeOffset(DateTime.MinValue, TimeSpan.Zero)) return true;

            return false;
        }

        public bool AWSBenchmarkingResourcesCreated()
        {
            if (BenchmarkHost.CloudProvider == CloudProvider.AWS && BenchmarkHost.AWSHost != null && BenchmarkHost.AWSHost.ResourceCreatedAt != new DateTimeOffset(DateTime.MinValue, TimeSpan.Zero)) return true;
            return false;
        }

        public bool AzureBenchmarkingResourcesCreated()
        {
            if (BenchmarkHost.CloudProvider == CloudProvider.Azure && BenchmarkHost.AzureHost != null && BenchmarkHost.AzureHost.ResourceCreatedAt != new DateTimeOffset(DateTime.MinValue, TimeSpan.Zero)) return true;
            return false;
        }

        public bool ShowStartBenchmarking()
        {
            bool applicationHostReady = false;
            bool benchmarkingHostReady = false;

            if (!Started)
            {
                switch (Host.CloudProvider)
                {
                    case Core.Enums.CloudProvider.Azure: applicationHostReady = false; break;
                    case Core.Enums.CloudProvider.AWS: applicationHostReady = false; break;
                    default: applicationHostReady = true; break;
                }

                switch (BenchmarkHost.CloudProvider)
                {
                    case CloudProvider.Azure: benchmarkingHostReady = false; break;
                    case CloudProvider.AWS: benchmarkingHostReady = false; break;
                    default: benchmarkingHostReady = true; break;
                }

                if (Host.CloudProvider == Core.Enums.CloudProvider.Azure && Host.AzureHost != null)
                {
                    if (AzureApplicationResourcesCreated()) applicationHostReady = true;
                }


                if (BenchmarkHost.CloudProvider == CloudProvider.Azure && BenchmarkHost.AzureHost != null)
                {
                    if (AzureBenchmarkingResourcesCreated()) benchmarkingHostReady = true;
                }

                if (Host.CloudProvider == CloudProvider.AWS && Host.AWSHost != null)
                {
                    if (AWSApplicationResourcesCreated()) applicationHostReady = true;
                }


                if (BenchmarkHost.CloudProvider == CloudProvider.AWS && BenchmarkHost.AWSHost != null)
                {
                    if (AWSBenchmarkingResourcesCreated()) benchmarkingHostReady = true;
                }
            }

            if (applicationHostReady && benchmarkingHostReady)
                return true;

            return false;
        }

        public bool ShowCreateAzureResourcesApplication()
        {
            if (!Started && Host.CloudProvider == Core.Enums.CloudProvider.Azure && Host.AzureHost.ResourceCreationStarted == false && Host.AzureHost != null && Host.AzureHost.ResourceCreatedAt == new DateTimeOffset(DateTime.MinValue, TimeSpan.Zero)) return true;

            return false;
        }
    }
}
