using Docker.Benchmarking.Orchestrator.Optimizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Docker.Benchmarking.Orchestrator.Optimizer.Tests
{
    public class OptimzerTests
    {
        [Fact]
        public void CloudServerProvider_CorrectCostPerHost()
        {
            var host = AWSExperimentHost(300);

            Assert.Equal(37.5M, host.TotalCost);
        }


        [Fact]
        public void CloudServerProvider_CorrectResults()
        {
            //300 Seconds for Experiment
            var experimentTimeLength = 300;
            var maxSpend = 4; // 4p
            var benchmarkHost = AzureExperimentHost(experimentTimeLength);

            var vmHosts = AWSHosts(experimentTimeLength);

            IOptimizer optimzer = new Optimizer();

            decimal actualSpend = 0;
            var results = optimzer.SingleProvider(vmHosts.ToList(), benchmarkHost, maxSpend, out actualSpend);

            Assert.Equal(4, results.Count);
        }



        private CloudServiceProvider AWSExperimentHost(int seconds)
        {
            return new CloudServiceProvider
            {
                CloudProvider = Enums.CloudProvider.AWS,
                CostPerHour = 0.125M,
                VMSize = Enums.VMSize.Small,
                Time = seconds
            };
        }

        private CloudServiceProvider AzureExperimentHost(int seconds)
        {
            return new CloudServiceProvider
            {
                CloudProvider = Enums.CloudProvider.Azure,
                CostPerHour = 0.135M,
                VMSize = Enums.VMSize.Small,
                Time = seconds
            };
        }

        private IEnumerable<CloudServiceProvider> AWSHosts(int seconds)
        {
            return new List<CloudServiceProvider>
            {
                new CloudServiceProvider
                {
                    Name = "T3",
                    CloudProvider = Enums.CloudProvider.AWS,
                    CostPerHour = 0.0125M,
                    Time = seconds,
                    VMSize = Enums.VMSize.Burstable
                },
                new CloudServiceProvider
                {
                    Name = "M2",
                    CloudProvider = Enums.CloudProvider.AWS,
                    CostPerHour = 0.029M,
                    Time = seconds,
                    VMSize = Enums.VMSize.Small
                },
                new CloudServiceProvider
                {
                    Name = "M3",
                    CloudProvider = Enums.CloudProvider.AWS,
                    CostPerHour = 0.045M,
                    Time = seconds,
                    VMSize = Enums.VMSize.Medium
                },
                new CloudServiceProvider
                {
                    Name = "M5",
                    CloudProvider = Enums.CloudProvider.AWS,
                    CostPerHour = 0.065M,
                    Time = seconds,
                    VMSize = Enums.VMSize.Large
                },
                new CloudServiceProvider
                {
                    Name = "M7",
                    CloudProvider = Enums.CloudProvider.AWS,
                    CostPerHour = 0.1M,
                    Time = seconds,
                    VMSize = Enums.VMSize.ExtraLarge
                },
            };
        }
    }
}
