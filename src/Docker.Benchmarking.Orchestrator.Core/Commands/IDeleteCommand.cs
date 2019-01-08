using Docker.Benchmarking.Orchestrator.Core.SharedKernel;
using MediatR;
using System;

namespace Docker.Benchmarking.Orchestrator.Core.Commands
{
    public class IDeleteCommand<TEntity> : IRequest<bool> where TEntity : BaseEntity, new()
    {
        Guid Id { get; }
    }
}
