using Docker.Benchmarking.Orchestrator.Core.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Docker.Benchmarking.Orchestrator.Core.Commands
{
    public class ListActiveEntitiesCommand<T> : IRequest<IEnumerable<T>> where T : BaseEntity
    {
        
    }
}
