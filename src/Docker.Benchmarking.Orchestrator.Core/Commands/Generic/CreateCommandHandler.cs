using AutoMapper;
using Docker.Benchmarking.Orchestrator.Core.Entities;
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
    public class CreateCommandHandler<T> : IRequestHandler<CreateCommand<T>, T> where T : BaseEntity
    {
        private readonly IRepository<T> _repo;
        private readonly IDockerRemoteService _dockerRemoteService;
        public CreateCommandHandler(IRepository<T> repo, IMapper mapper, IDockerRemoteService dockerRemoteService)
        {
            _repo = repo;
            _dockerRemoteService = dockerRemoteService;
        }

        public async Task<T> Handle(CreateCommand<T> request, CancellationToken cancellationToken)
        {
            Type entityType = typeof(T);

            if(entityType == typeof(DockerHost))
            {
               var entity = request.Entity as DockerHost;
               await _dockerRemoteService.AddDockerHostAsync(entity);
               return entity as T;
            }

            return await _repo.AddAsync(request.Entity);
        }
    }
}
