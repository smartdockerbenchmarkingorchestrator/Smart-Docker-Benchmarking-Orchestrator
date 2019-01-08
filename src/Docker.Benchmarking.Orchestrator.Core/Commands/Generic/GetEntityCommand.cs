using Docker.Benchmarking.Orchestrator.Core.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Docker.Benchmarking.Orchestrator.Core.Commands
{
    public class GetEntityCommand<T> : IRequest<T> where T : BaseEntity
    {
        public GetEntityCommand(Guid id)
        {
            Id = id;
        }
        public Guid Id
        {
            get;
        }
    }
}
