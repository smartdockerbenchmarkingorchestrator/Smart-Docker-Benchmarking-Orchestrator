// ***********************************************************************
// Assembly         : Docker.Benchmarking.Orchestrator.Core
// Author           : ***********
// Created          : 09-02-2018
//
// Last Modified By : ***********
// Last Modified On : 08-26-2018
// ***********************************************************************
// <copyright file="AddRangeEntitiesCommandHandler.cs" company="Docker.Benchmarking.Orchestrator.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Docker.Benchmarking.Orchestrator.Core.Interfaces;
using Docker.Benchmarking.Orchestrator.Core.SharedKernel;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Core.Commands
{
    /// <summary>
    /// Class AddRangeEntitiesCommandHandler.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="MediatR.IRequestHandler{Docker.Benchmarking.Orchestrator.Core.Commands.AddRangeEntitiesCommand{T}, System.Boolean}" />
    public class AddRangeEntitiesCommandHandler<T> : IRequestHandler<AddRangeEntitiesCommand<T>, bool> where T : BaseEntity
    {
        /// <summary>
        /// The repo
        /// </summary>
        private readonly IRepository<T> _repo;
        /// <summary>
        /// Initializes a new instance of the <see cref="AddRangeEntitiesCommandHandler{T}"/> class.
        /// </summary>
        /// <param name="repo">The repo.</param>
        public AddRangeEntitiesCommandHandler(IRepository<T> repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        public async Task<bool> Handle(AddRangeEntitiesCommand<T> request, CancellationToken cancellationToken)
        {
            await _repo.AddRangeAsync(request.Entities);
            return true;
        }
    }
}
