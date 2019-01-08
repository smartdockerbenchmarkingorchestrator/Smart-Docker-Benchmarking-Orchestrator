using AutoMapper;
using Docker.Benchmarking.Orchestrator.Core.Entities;
using Docker.Benchmarking.Orchestrator.Core.Interfaces;
using Docker.Benchmarking.Orchestrator.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Web.ViewComponents
{
    public class DockerHostTableViewComponent : ViewComponent
    {
        private readonly IRepository<DockerHost> _hostRepo;
        private readonly IMapper _mapper;
        public DockerHostTableViewComponent(IRepository<DockerHost> hostRepo, IMapper mapper)
        {
            _hostRepo = hostRepo;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await _hostRepo.ListAsync();

            var apiModel = _mapper.Map<IEnumerable<DockerHostViewModel>>(items).OrderBy(c => c.Name);

            return View(apiModel);
        }
    }
}
