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
    public class ApacheJmeterTestFilesTableViewComponent : ViewComponent
    {
        private readonly IRepository<ApacheJmeterTestFile> _repo;
        private readonly IMapper _mapper;
        public ApacheJmeterTestFilesTableViewComponent(IRepository<ApacheJmeterTestFile> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await _repo.ListAsync();

            var apiModel = _mapper.Map<IEnumerable<ApacheJmeterTestFileViewModel>>(items).OrderBy(c => c.Name);

            return View(apiModel);
        }
    }
}
