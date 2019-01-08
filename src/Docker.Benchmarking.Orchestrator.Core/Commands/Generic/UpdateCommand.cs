using Docker.Benchmarking.Orchestrator.Core.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Docker.Benchmarking.Orchestrator.Core.Commands
{
    public class UpdateCommand<T> : IRequest<T> where T : BaseEntity
    {
        public UpdateCommand(T entity)
        {
            Entity = entity;
        }

        public T Entity
        {
            get;
        }
    }
}
