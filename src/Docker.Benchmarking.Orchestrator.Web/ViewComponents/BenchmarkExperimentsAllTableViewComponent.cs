using AutoMapper;
using Docker.Benchmarking.Orchestrator.Core.Entities;
using Docker.Benchmarking.Orchestrator.Core.Interfaces;
using Docker.Benchmarking.Orchestrator.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Web.ViewComponents
{
    public class BenchmarkExperimentsAllTableViewComponent : ViewComponent
    {
        private readonly IRepository<BenchmarkExperiment> _appTestRepo;
        private readonly IMapper _mapper;

        public BenchmarkExperimentsAllTableViewComponent(IRepository<BenchmarkExperiment> appTestRepo, IMapper mapper)
        {
            _appTestRepo = appTestRepo;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(bool all = true, bool active = true)
        {
            if (all)
            {
                var items = await _appTestRepo.ListAsync();
                var apiModel = _mapper.Map<IEnumerable<BenchmarkTestViewModel>>(items).OrderBy(c => c.Name);
                return View(apiModel);
            }
            else
            {
                var items = _appTestRepo.FindBy(c => c.Started && c.Completed & c.JmeterResultsProcessed && c.Active);
                var apiModel = _mapper.Map<IEnumerable<BenchmarkTestViewModel>>(items).OrderBy(c => c.Name);
                return View(apiModel);
            }

        }
    }
}
