using Docker.Benchmarking.Orchestrator.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Docker.Benchmarking.Orchestrator.Infrastrcture
{
    public class HostSettings
    {
        public string HostName { get; set; }
        public string PortNumber { get; set; }
        public string JmeterUrl { get; set; }
    }
}
