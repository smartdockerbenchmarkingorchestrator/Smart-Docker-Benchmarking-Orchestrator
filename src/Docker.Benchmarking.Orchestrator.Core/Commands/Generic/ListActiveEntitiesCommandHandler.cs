using Docker.Benchmarking.Orchestrator.Core.Interfaces;
using Docker.Benchmarking.Orchestrator.Core.SharedKernel;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Core.Commands
{
    public class ListActiveEntitiesCommandHandler<T> : IRequestHandler<ListActiveEntitiesCommand<T>, IEnumerable<T>> where T : BaseEntity
    {
        private readonly IRepository<T> _repo;
        public ListActiveEntitiesCommandHandler(IRepository<T> repo)
        {
            _repo = repo;
        }
        public async Task<IEnumerable<T>> Handle(ListActiveEntitiesCommand<T> request, CancellationToken cancellationToken)
        {
            return await _repo.ListActiveAsync();
        }
    }
}
