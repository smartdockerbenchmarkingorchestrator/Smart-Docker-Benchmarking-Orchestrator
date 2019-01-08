using Docker.Benchmarking.Orchestrator.Optimizer.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Docker.Benchmarking.Orchestrator.Optimizer.Models
{
    public class CloudServiceProvider
    {
        public Guid CloudTemplateId { get; set; }
        public CloudProvider CloudProvider { get; set; }

        public VMSize VMSize { get; set; }

        public string Name { get; set; }

        public int Time { get; set; }

        public decimal CostPerHour { get; set; }

        public decimal TotalCost { get { return decimal.Multiply((Time / 3600M), CostPerHour); } }
    }
}
