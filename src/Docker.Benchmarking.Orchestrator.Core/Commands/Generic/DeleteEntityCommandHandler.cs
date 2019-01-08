using AutoMapper;
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
    public class DeleteEntityCommandHandler<T> : IRequestHandler<DeleteEntityCommand<T>, bool> where T : BaseEntity
    {
        private readonly IRepository<T> _repo;
        public DeleteEntityCommandHandler(IRepository<T> repo, IMapper mapper)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(DeleteEntityCommand<T> request, CancellationToken cancellationToken)
        {
            await _repo.DeleteAsync(request.Entity);
            return true;
        }
    }
}
