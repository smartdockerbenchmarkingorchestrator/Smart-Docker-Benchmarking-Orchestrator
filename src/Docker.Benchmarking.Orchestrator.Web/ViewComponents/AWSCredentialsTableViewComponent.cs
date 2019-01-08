using AutoMapper;
using Docker.Benchmarking.Orchestrator.Core.Commands;
using Docker.Benchmarking.Orchestrator.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Web.ViewComponents
{
    public class AWSCredentialsTableViewComponent : ViewComponent
    {
        private readonly IMediator _mediatr;
        private readonly IMapper _mapper;
        public AWSCredentialsTableViewComponent(IMediator mediatr, IMapper mapper)
        {
            _mediatr = mediatr;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await _mediatr.Send(new ListEntitiesCommand<AWSCredentials>());
            return View(items);
        }
    }
}
