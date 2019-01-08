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
    public class GetEntitiesCommandHandler<T> : IRequestHandler<GetEntitiesCommand<T>, IEnumerable<T>> where T : BaseEntity
    {
        private readonly IRepository<T> _repo;
        public GetEntitiesCommandHandler(IRepository<T> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<T>> Handle(GetEntitiesCommand<T> request, CancellationToken cancellationToken)
        {
            return await _repo.GetByIdsAsync(request.Ids);
        }
    }
}
