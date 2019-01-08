using System.Diagnostics;
using System.Linq;
using AutoMapper;
using Docker.Benchmarking.Orchestrator.Core.Interfaces;
using Docker.Benchmarking.Orchestrator.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace Docker.Benchmarking.Orchestrator.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ICurrentHostSettings _currentHostSettings;
        private readonly IFileProvider _fileProvider;
        private readonly IHostingEnvironment _env;

        public HomeController(ICurrentHostSettings currentHostSettings, IFileProvider fileProvider, IHostingEnvironment env,
            IMediator mediatr, IMapper mapper) : base(mapper, mediatr)
        {
            _currentHostSettings = currentHostSettings;
            _fileProvider = fileProvider;
            _env = env;
        }

        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(_currentHostSettings.CurrentHost) || _currentHostSettings.CurrentPort == 0)
                ViewData["RequiresHostSettings"] = true;
            else
                ViewData["RequiresHostSettings"] = false;

            return View();
        }

        [HttpPost]
        public IActionResult UpdateHostSettings(string currentHost, int portNumber)
        {
            _currentHostSettings.SetCurrentHost(currentHost);
            _currentHostSettings.SetCurrentPort(portNumber);

            return RedirectToAction("Index");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Uploads()
        {
            IDirectoryContents contents = _fileProvider.GetDirectoryContents("wwwroot/uploads");

            IOrderedEnumerable<IFileInfo> lastModified =
                      contents.ToList()
                      .OrderByDescending(f => f.LastModified);

            return View(lastModified);
        }
    }
}
