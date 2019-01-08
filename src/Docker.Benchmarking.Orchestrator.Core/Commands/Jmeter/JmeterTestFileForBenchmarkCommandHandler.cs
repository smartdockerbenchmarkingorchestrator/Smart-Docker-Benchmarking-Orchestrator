using Docker.Benchmarking.Orchestrator.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Core.Commands
{
    public class JmeterTestFileForBenchmarkCommandHandler : IRequestHandler<JmeterTestFileForBenchmarkCommand, string>
    {
        private readonly IApacheJmeterFileService _fileService;
        public JmeterTestFileForBenchmarkCommandHandler(IApacheJmeterFileService fileService)
        {
            _fileService = fileService;
        }

        public async Task<string> Handle(JmeterTestFileForBenchmarkCommand request, CancellationToken cancellationToken)
        {
            return await _fileService.JmeterTestFileForBenchmark(request.Id);
        }
    }
}
