using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Docker.Benchmarking.Orchestrator.Core.SharedKernel;
using MediatR;

namespace Docker.Benchmarking.Orchestrator.Core.Commands
{
    public class FindEntitiesCommand<T> : IRequest<IQueryable<T>> where T : BaseEntity
    {
        public FindEntitiesCommand(Expression<Func<T, bool>> predicate)
        {
            Predicate = predicate;
        }

        public Expression<Func<T, bool>> Predicate { get; }
    }
}
