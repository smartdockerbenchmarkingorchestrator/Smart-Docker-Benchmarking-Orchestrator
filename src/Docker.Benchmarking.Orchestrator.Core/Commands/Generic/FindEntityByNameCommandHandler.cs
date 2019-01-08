using Docker.Benchmarking.Orchestrator.Core.Interfaces;
using Docker.Benchmarking.Orchestrator.Core.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Docker.Benchmarking.Orchestrator.Core.Commands
{
    public class FindEntityByNameCommandHandler<T> : IRequestHandler<FindEntityByNameCommand<T>, bool> where T : BaseEntity
    {
        private readonly IRepository<T> _repo;
        public FindEntityByNameCommandHandler(IRepository<T> repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(FindEntityByNameCommand<T> request, CancellationToken cancellationToken)

        {
            if(request.Id == default(Guid))
                return await Task.FromResult(_repo.FindBy(c => c.Name.ToLower() == request.Name.ToLower()).Any());

            //If name is same as current model then it's valid so return false for it alreadying existing
            var currentModel = await _repo.GetByIdAsync(request.Id);
            if (currentModel.Name.ToLower() == request.Name.ToLower()) return false;

            return await Task.FromResult(_repo.FindBy(c => c.Name.ToLower() == request.Name.ToLower()).Any());
        }
    }
}
