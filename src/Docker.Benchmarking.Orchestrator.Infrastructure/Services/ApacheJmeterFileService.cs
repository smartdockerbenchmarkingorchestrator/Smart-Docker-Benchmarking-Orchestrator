using Ardalis.GuardClauses;
using Docker.Benchmarking.Orchestrator.Core.Entities;
using Docker.Benchmarking.Orchestrator.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Infrastrcture.Services
{
    public class ApacheJmeterFileService : IApacheJmeterFileService
    {
        private readonly IRepository<BenchmarkExperiment> _repo;
        private readonly ICurrentHostSettings _hostSettings;
        public ApacheJmeterFileService(IRepository<BenchmarkExperiment> repo, ICurrentHostSettings hostSettings)
        {
            _repo = repo;
            _hostSettings = hostSettings;
        }

        public async Task<string> JmeterTestFileForBenchmark(Guid benchmarkId)
        {
            Guard.Against.GuidNullOrDefault(benchmarkId, nameof(benchmarkId));

            var benchmarkExperiment = await _repo.GetByIdAsync(benchmarkId);

            Guard.Against.Null(benchmarkExperiment, nameof(benchmarkExperiment));

            if (benchmarkExperiment.TestFile == null && benchmarkExperiment.Application.TestFile == null)
                throw new Exception("No test found found for application test.");

            var testFile = benchmarkExperiment.TestFile ?? benchmarkExperiment.Application.TestFile;
            var file = testFile.FileName;

            file = testFile.FileName
                .Replace("${__P(PORT)}", benchmarkExperiment.Application.ApplicationImage.ExternalPort.ToString())
                .Replace("${__P(HOST)}", benchmarkExperiment.Host.HostName)
                .Replace("${__P(JMETER_TEST_OUTPUT)}", benchmarkExperiment.Id.ToString())
                .Replace("${__P(BUYUSERS)}", 60.ToString())
                .Replace("${__P(RUNTIME)}", benchmarkExperiment.BenchmarkTimeLength.ToString())
                .Replace("${__P(BROWSEUSERS)}", 60.ToString())
                .Replace("${__P(SEARCHUSERS)}", 60.ToString());


            if(_hostSettings.CurrentHostUri != null)
            {
                file = file.Replace("${__P(JMETER_TEST_API_HOST)}", _hostSettings.CurrentHostUri.Host)
                .Replace("${__P(JMETER_TEST_API_PORT)}", _hostSettings.CurrentPort.ToString());
            }


            foreach (var line in benchmarkExperiment.Variables)
            {
                file = file.Replace(line.Name, line.Value);
            }

            return file;
        }
    }
}
