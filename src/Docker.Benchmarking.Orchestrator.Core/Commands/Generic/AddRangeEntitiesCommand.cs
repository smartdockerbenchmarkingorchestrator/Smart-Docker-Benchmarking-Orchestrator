// ***********************************************************************
// Assembly         : Docker.Benchmarking.Orchestrator.Core
// Author           : ***********
// Created          : 09-02-2018
//
// Last Modified By : ***********
// Last Modified On : 08-27-2018
// ***********************************************************************
// <copyright file="AddRangeEntitiesCommand.cs" company="Docker.Benchmarking.Orchestrator.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Docker.Benchmarking.Orchestrator.Core.SharedKernel;
using MediatR;
using System.Collections.Generic;

namespace Docker.Benchmarking.Orchestrator.Core.Commands
{
    /// <summary>
    /// Class AddRangeEntitiesCommand.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="MediatR.IRequest{System.Boolean}" />
    public class AddRangeEntitiesCommand<T> : IRequest<bool> where T : BaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddRangeEntitiesCommand{T}"/> class.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public AddRangeEntitiesCommand(IEnumerable<T> entities)
        {
            Entities = entities;
        }

        /// <summary>
        /// Gets the entities.
        /// </summary>
        /// <value>The entities.</value>
        public IEnumerable<T> Entities { get; }
    }
}
