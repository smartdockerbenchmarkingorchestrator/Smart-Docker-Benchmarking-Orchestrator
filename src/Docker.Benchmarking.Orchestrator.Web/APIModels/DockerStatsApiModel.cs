using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Web.APIModels
{
    public class DockerStatsApiModel
    {
        public DockerStatsApiModel()
        {
            CpuPercentage = Math.Round(CpuPercentage, 2);
        }

        public Guid Id { get; set; }
        public Guid ApplicationId { get; set; }
        public double CpuPercentage { get; set; }
        public DateTimeOffset DateTimeUtc { get; set; }

        public double MemoryUsage { get; set; }

        public double MemoryLimit { get; set; }

        public double NetworkInput { get; set; }

        public double NetworkOutput { get; set; }

        public double BlockInput { get; set; }

        public double BlockOutput { get; set; }

        public double MemoryPercentage { get; set; }
    }
}
