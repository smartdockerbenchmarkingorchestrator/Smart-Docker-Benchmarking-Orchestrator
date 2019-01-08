using Docker.Benchmarking.Orchestrator.Core.Interfaces;
using Docker.Benchmarking.Orchestrator.Core.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Core.Commands
{
    public class GetEntityCommandHandler<T> : IRequestHandler<GetEntityCommand<T>, T> where T : BaseEntity
    {
        private readonly IRepository<T> _repo;
        public GetEntityCommandHandler(IRepository<T> repo)
        {
            _repo = repo;
        }
        public async Task<T> Handle(GetEntityCommand<T> request, CancellationToken cancellationToken)
        {
            return await _repo.GetByIdAsync(request.Id);
        }
    }
}
