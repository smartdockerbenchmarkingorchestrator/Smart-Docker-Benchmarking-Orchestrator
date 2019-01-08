using Docker.Benchmarking.Orchestrator.Core.Interfaces;
using Docker.Benchmarking.Orchestrator.Core.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Core.Commands
{
    public class FindEntitiesCommandHandler<T> : IRequestHandler<FindEntitiesCommand<T>, IQueryable<T>> where T : BaseEntity
    {
        private readonly IRepository<T> _repo;
        public FindEntitiesCommandHandler(IRepository<T> repo)
        {
            _repo = repo;
        }

        public Task<IQueryable<T>> Handle(FindEntitiesCommand<T> request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_repo.FindBy(request.Predicate));
        }
    }
}
