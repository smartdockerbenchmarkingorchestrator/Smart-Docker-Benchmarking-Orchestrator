using Docker.Benchmarking.Orchestrator.Core.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Docker.Benchmarking.Orchestrator.Core.Commands
{
    public class DeleteEntityCommand<T> : IRequest<bool> where T : BaseEntity
    {
        public T Entity
        {
            get; set;
        }
    }
}
