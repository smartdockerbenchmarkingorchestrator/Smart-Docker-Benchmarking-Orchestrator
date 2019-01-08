using Docker.Benchmarking.Orchestrator.Web.APIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Web.ViewModels
{
    public class DockerComparisonResultsViewModel
    {
        public Guid Id { get; set; }
        public IEnumerable<DockerStatsApiModel> Results { get; set; }
    }
}
