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
    public class BenchmarkTestTableViewComponent : ViewComponent
    {
        private readonly IRepository<BenchmarkExperiment> _appTestRepo;
        private readonly IMapper _mapper;
        public BenchmarkTestTableViewComponent(IRepository<BenchmarkExperiment> appTestRepo, IMapper mapper)
        {
            _appTestRepo = appTestRepo;
            _mapper = mapper;
        }

        public IViewComponentResult Invoke(Guid applicationId, bool completedResults = true)
        {
            var items = _appTestRepo.FindBy(c => c.Application.Id == applicationId && c.Completed == completedResults);

            var apiModel = _mapper.Map<IEnumerable<BenchmarkTestViewModel>>(items).OrderBy(c => c.Name);
            return View(apiModel);
        }

    }
}
