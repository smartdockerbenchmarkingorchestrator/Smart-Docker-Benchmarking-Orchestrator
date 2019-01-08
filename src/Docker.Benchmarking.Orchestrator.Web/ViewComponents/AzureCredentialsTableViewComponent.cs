using AutoMapper;
using Docker.Benchmarking.Orchestrator.Core.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Docker.Benchmarking.Orchestrator.Core.Entities;

namespace Docker.Benchmarking.Orchestrator.Web.ViewComponents
{
    public class AzureCredentialsTableViewComponent : ViewComponent
    {
        private readonly IMediator _mediatr;
        private readonly IMapper _mapper;
        public AzureCredentialsTableViewComponent(IMediator mediatr, IMapper mapper)
        {
            _mediatr = mediatr;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await _mediatr.Send(new ListEntitiesCommand<AzureCredientials>());
            return View(items);
        }
    }
}
