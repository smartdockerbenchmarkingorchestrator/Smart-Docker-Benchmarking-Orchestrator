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
    //https://gist.github.com/thiagomajesk/9abfb649e58847289b2f65d1994636f2

    public class DeleteCommandHandler<TEntity, TCommand> : IRequestHandler<TCommand, bool> where TEntity : BaseEntity, new()
            where TCommand : class, IDeleteCommand<TEntity>, new()
    {
        private readonly IRepository<TEntity> _repo;

        public DeleteCommandHandler(IRepository<TEntity> repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(TCommand request, CancellationToken cancellationToken)
        {
            return await _repo.DeleteByIdAsync(request.Id);
        }
    }
}
