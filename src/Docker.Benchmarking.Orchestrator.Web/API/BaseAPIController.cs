// ***********************************************************************
// Assembly         : Docker.Benchmarking.Orchestrator.Web
// Author           : ***********
// Created          : 08-27-2018
//
// Last Modified By : ***********
// Last Modified On : 08-27-2018
// ***********************************************************************
// <copyright file="BaseAPIController.cs" company="Docker.Benchmarking.Orchestrator.Web">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using AutoMapper;
using Docker.Benchmarking.Orchestrator.Web.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Web.API
{
    /// <summary>
    /// Class BaseAPIController.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    [JsonExceptionFilter]
    public abstract partial class BaseAPIController : ControllerBase
    {
        protected readonly IMapper _mapper;
        protected readonly IMediator _mediatr;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseAPIController"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="mediatr">The mediatr.</param>
        public BaseAPIController(IMapper mapper, IMediator mediatr)
        {
            _mapper = mapper;
            _mediatr = mediatr;
        }
    }
}
