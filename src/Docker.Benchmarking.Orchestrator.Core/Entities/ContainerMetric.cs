using Docker.Benchmarking.Orchestrator.Core.SharedKernel;
using Docker.DotNet.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Docker.Benchmarking.Orchestrator.Core.Entities
{
    public class ContainerMetric : BaseEntity
    {
        new public string Name { get; set; }

        [Required]
        public string ContainerId { get; set; }

        [Required]
        public string ContainerName { get; set; }

        public double CPUPercentage { get; set; }

        public double MemoryUsage { get; set; }

        public double MemoryLimit { get; set; }

        public double NetworkInput { get; set; }

        public double NetworkOutput { get; set; }

        public double BlockInput { get; set; }

        public double BlockOutput { get; set; }

        //[Required]
        //public string JsonDockerResponse { get; set; }

        [Required]
        public DateTime DockerDateTimestamp { get; set; }

        [Required]
        public virtual BenchmarkExperiment BenchmarkExperiment { get; set; }

        [NotMapped]
        public double MemoryPercentage
        {
            get
            {
                return (MemoryUsage / MemoryLimit) * 100;
            }
        }

        //https://github.com/moby/moby/blob/eb131c5383db8cac633919f82abad86c99bffbe5/cli/command/container/stats_helpers.go#L175

        public void SetCPUPercentage(CPUStats cpuStats, CPUStats preCPUStats)
        {
            try
            {
                float cpuPerc = 0;

                float cpuDelta = cpuStats.CPUUsage.TotalUsage - preCPUStats.CPUUsage.TotalUsage;
                float systemDelta = cpuStats.SystemUsage - preCPUStats.SystemUsage;

                if (cpuDelta > 0 && systemDelta > 0)
                {
                    cpuPerc = (cpuDelta / systemDelta) * cpuStats.CPUUsage.PercpuUsage.Count * 100;
                }

                CPUPercentage = cpuPerc;
            }
            catch (Exception ex)
            {

            }
        }

        public void CalculateNetworks(IDictionary<string, NetworkStats> networkStats)
        {
            foreach (var network in networkStats)
            {
                NetworkInput = network.Value.RxBytes;
                NetworkOutput = network.Value.TxBytes;
            }
        }

        public void CalculateNetworksFix(ContainerMetric previousMetric)
        {
            NetworkInput = NetworkInput - previousMetric.NetworkInput;
            NetworkOutput = NetworkOutput - previousMetric.NetworkOutput;
        }

        public void CalculateBlockIO(BlkioStats blockStats)
        {
            var stats = blockStats.IoServiceBytesRecursive;

            foreach (var stat in stats)
            {
                switch (stat.Op.ToLower())
                {
                    case "read":
                        BlockInput = stat.Value;
                        break;
                    case "write":
                        BlockOutput = stat.Value;
                        break;
                }
            }
        }

        public void CalculateBlockIOFix(ContainerMetric previousMetric)
        {
            BlockInput = BlockInput - previousMetric.BlockInput;
            BlockOutput = BlockOutput - previousMetric.BlockOutput;
        }
    }
}
