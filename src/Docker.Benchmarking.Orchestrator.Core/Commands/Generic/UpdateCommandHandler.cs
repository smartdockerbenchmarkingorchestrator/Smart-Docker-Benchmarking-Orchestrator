using AutoMapper;
using Docker.Benchmarking.Orchestrator.Core.Entities;
using Docker.Benchmarking.Orchestrator.Core.Interfaces;
using Docker.Benchmarking.Orchestrator.Core.SharedKernel;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Core.Commands
{
    public class UpdateCommandHandler<T> : IRequestHandler<UpdateCommand<T>, T> where T : BaseEntity
    {
        private readonly IRepository<T> _repo;
        private readonly IMapper _mapper;
        private readonly IDockerRemoteService _dockerRemoteService;

        public UpdateCommandHandler(IRepository<T> repo, IMapper mapper, IDockerRemoteService dockerRemoteService)
        {
            _repo = repo;
            _mapper = mapper;
            _dockerRemoteService = dockerRemoteService;
        }

        public async Task<T> Handle(UpdateCommand<T> request, CancellationToken cancellationToken)
        {
            Type entityType = typeof(T);

            if (entityType == typeof(DockerHost))
            {
                var entity = request.Entity as DockerHost;
                await _dockerRemoteService.UpdateDockerHostAsync(entity);
                return entity as T;
            }

            await _repo.UpdateAsync(request.Entity);
            return request.Entity;
        }
    }
}
