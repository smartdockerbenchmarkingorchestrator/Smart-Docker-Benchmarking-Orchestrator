using AutoMapper;
using Docker.Benchmarking.Orchestrator.Core.Commands;
using Docker.Benchmarking.Orchestrator.Core.Commands.BenchmarkExperiment;
using Docker.Benchmarking.Orchestrator.Core.Commands.OptimizedBenchmarkExperiments.Commands;
using Docker.Benchmarking.Orchestrator.Core.Entities;
using Docker.Benchmarking.Orchestrator.Core.Enums;
using Docker.Benchmarking.Orchestrator.Web.Extensions;
using Docker.Benchmarking.Orchestrator.Web.ViewComponents;
using Docker.Benchmarking.Orchestrator.Web.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Web.Controllers
{
    public class BenchmarkExperimentController : BaseController
    {


        public BenchmarkExperimentController(IMapper mapper, IMediator mediatr
             ) : base(mapper, mediatr)
        {

        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var viewModel = new AddBenchmarkExperimentViewModel
            {
                Applications = await ApplicationsSelectList(),
                ApacheTestFiles = await ApacheTestFilesSelectList(),
                Hosts = await ApplicationHosts(),
                BenchmarkHosts = await BenchmarkHosts(),
                DatabaseHosts = await DatabaseHosts()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm]AddBenchmarkExperimentViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Applications = await ApplicationsSelectList();
                viewModel.ApacheTestFiles = await ApacheTestFilesSelectList();
                viewModel.Hosts = await ApplicationHosts();
                viewModel.BenchmarkHosts = await BenchmarkHosts();
                viewModel.DatabaseHosts = await DatabaseHosts();
                return View(viewModel);
            }

            var model = _mapper.Map<BenchmarkExperiment>(viewModel);

            if (viewModel.Application != Guid.Empty)
                model.Application = await _mediatr.Send(new GetEntityCommand<Application>(viewModel.Application));

            if (viewModel.Host != Guid.Empty)
                model.Host = await _mediatr.Send(new GetEntityCommand<DockerHost>(viewModel.Host));

            if (viewModel.BenchmarkHost != Guid.Empty)
                model.BenchmarkHost = await _mediatr.Send(new GetEntityCommand<DockerHost>(viewModel.BenchmarkHost));

            if (viewModel.ApacheTestFileId != Guid.Empty)
                if (viewModel.ApacheTestFileId != null)
                    model.TestFile = await _mediatr.Send(new GetEntityCommand<ApacheJmeterTestFile>(viewModel.ApacheTestFileId.Value));

            model.ApacheJmeterTestId = viewModel.ApacheTestFileId;

            await _mediatr.Send(new CreateCommand<BenchmarkExperiment>(model));

            return RedirectToAction("Details", new { id = model.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty)
                return NotFound();

            var appTest = await _mediatr.Send(new GetEntityCommand<BenchmarkExperiment>(id));

            if (appTest == null) return NotFound();

            var viewModel = _mapper.Map<EditBenchmarkExperimentViewModel>(appTest);
            viewModel.Application = appTest.Application.Id;
            viewModel.ApacheTestFileId = appTest.ApacheJmeterTestId;
            viewModel.BenchmarkHost = appTest.BenchmarkHostId;
            viewModel.Host = appTest.Host.Id;

            viewModel.Applications = await ApplicationsSelectList(viewModel.Application);
            viewModel.ApacheTestFiles = await ApacheTestFilesSelectList();

            viewModel.Hosts = await ApplicationHosts();
            viewModel.BenchmarkHosts = await BenchmarkHosts();
            viewModel.DatabaseHosts = await DatabaseHosts();

            return View(viewModel);
        }



        [HttpPost]
        public async Task<IActionResult> Edit([FromForm]EditBenchmarkExperimentViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Hosts = await ApplicationHosts();
                viewModel.BenchmarkHosts = await BenchmarkHosts();
                viewModel.DatabaseHosts = await DatabaseHosts();

                return View(viewModel);
            }

            var appTest = await _mediatr.Send(new GetEntityCommand<BenchmarkExperiment>(viewModel.Id));

            if (appTest == null)
                return View(viewModel);

            var model = _mapper.Map(viewModel, appTest);

            if (viewModel.Application != Guid.Empty)
                model.Application = await _mediatr.Send(new GetEntityCommand<Application>(viewModel.Application));

            if (viewModel.Host != Guid.Empty)
                model.Host = await _mediatr.Send(new GetEntityCommand<DockerHost>(viewModel.Host));

            if (viewModel.BenchmarkHost != Guid.Empty)
                model.BenchmarkHost = await _mediatr.Send(new GetEntityCommand<DockerHost>(viewModel.BenchmarkHost));

            if (viewModel.ApacheTestFileId != Guid.Empty)
                if (viewModel.ApacheTestFileId != null)
                    model.TestFile = await _mediatr.Send(new GetEntityCommand<ApacheJmeterTestFile>(viewModel.ApacheTestFileId.Value));

            model.ApacheJmeterTestId = viewModel.ApacheTestFileId;

            await _mediatr.Send(new UpdateCommand<BenchmarkExperiment>(model));

            return RedirectToAction("Details", new { id = model.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == Guid.Empty) return BadRequest("Id is invalid");

            var experiment = await _mediatr.Send(new GetEntityCommand<BenchmarkExperiment>(id));

            if (experiment == null) return BadRequest("No application");

            var viewModel = _mapper.Map<StartBenchmarkExperimentViewModel>(experiment);

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult AddTestFile()
        {
            return View(new AddApacheJmeterTestFileViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> AddTestFile([FromForm]AddApacheJmeterTestFileViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            var model = _mapper.Map<ApacheJmeterTestFile>(viewModel);
            //model.FileName = viewModel.TestUpload;

            await _mediatr.Send(new CreateCommand<ApacheJmeterTestFile>(model));

            return View(new AddApacheJmeterTestFileViewModel()).WithSuccess("Success", "Apache Jmeter Test File Uploaded.  Application ID: " + model.Id);
        }

        [HttpGet]
        public async Task<IActionResult> TestFile(Guid id)
        {
            if (id == Guid.Empty) return BadRequest("Id is empty");

            var testFile = await _mediatr.Send(new GetEntityCommand<ApacheJmeterTestFile>(id));

            if (testFile == null) return BadRequest("No file foind for given Id");

            HttpContext.Response.ContentType = "text/xml";
            HttpContext.Response.Headers.Add("Content-Disposition", "inline; filename=" + testFile.Name + ".jmx");

            var bytes = Encoding.UTF8.GetBytes(testFile.FileName);
            var result = new FileContentResult(bytes, "text/xml")
            {
                FileDownloadName = testFile.Name + ".jmx"
            };

            //return result;

            return File(bytes, "text/xml", testFile.Name + ".jmx");
        }

        [HttpGet]
        public async Task<IActionResult> TestFileForBenchmarkExperiment(Guid id)
        {
            if (id == Guid.Empty) return BadRequest("Id is empty");

            var file = await _mediatr.Send(new JmeterTestFileForBenchmarkCommand(id));

            HttpContext.Response.ContentType = "text/xml";

            var bytes = Encoding.UTF8.GetBytes(file);
            var result = new FileContentResult(bytes, "text/xml")
            {
                FileDownloadName = id + ".jmx"
            };

            return File(bytes, "text/xml", result.FileDownloadName);
        }

        [HttpGet]
        public async Task<IActionResult> EditTestFile(Guid id)
        {
            if (id == Guid.Empty) return BadRequest("Id is invalid");

            var testFile = await _mediatr.Send(new GetEntityCommand<ApacheJmeterTestFile>(id));

            if (testFile == null) return BadRequest("No test file found for given Id.");

            var viewModel = _mapper.Map<ApacheTestFileEditModel>(testFile);

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult TestFiles()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> EditTestFile([FromForm]ApacheTestFileEditModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            var model = _mapper.Map<ApacheJmeterTestFile>(viewModel);

            await _mediatr.Send(new UpdateCommand<ApacheJmeterTestFile>(model));

            return View(viewModel).WithSuccess("Success", "Apache Jmeter Test File Updated.");
        }

        [HttpPost]
        public async Task<IActionResult> StartApplicationBenchmark(Guid benchmarkExperimentId)
        {
            if (benchmarkExperimentId == Guid.Empty)
                return BadRequest("benchmarkExperimentId is empty");

            var benchmarkingStarted = await _mediatr.Send(new StartBenchmarkExperimentCommand(benchmarkExperimentId));

            return Ok(benchmarkingStarted);
        }

        [HttpGet]
        public async Task<IActionResult> Results(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("benchmarkExperimentId is empty");


            var experiment = await _mediatr.Send(new GetEntityCommand<BenchmarkExperiment>(id));

            if (experiment == null)
                return NotFound("No result found for given id");

            var viewModel = _mapper.Map<BenchmarkTestViewModel>(experiment);

            return View(viewModel);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> IsNameUnique(string name)
        {
            var unique = await _mediatr.Send(new FindEntityByNameCommand<BenchmarkExperiment>(name));
            return Json(!unique);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateAzureResources(Guid id)
        {
            if (id == Guid.Empty) return BadRequest("id is empty");

            var jobId = await _mediatr.Send(new CreateResourcesForBenchmarkCommand(id));

            return Json(true);
        }

        [HttpGet]
        public async Task<IActionResult> BenchmarkServerResults(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("benchmarkExperimentId is empty");

            var experiment = await _mediatr.Send(new GetEntityCommand<BenchmarkExperiment>(id));

            if (experiment == null)
                return NotFound("No result found for given id");

            var viewModel = _mapper.Map<BenchmarkTestViewModel>(experiment);

            return View(viewModel);
        }

        //[HttpPost]
        //public async Task<IActionResult> RecalculateMetrics(Guid id)
        //{
        //    if (id == Guid.Empty)
        //        return BadRequest("applicationId is empty");

        //    return Json(await _mediatr.Send(new RecalculateBenchmarkMetrics(id)));
        //}

        [HttpGet]
        public async Task<IActionResult> ResultsComparison(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("applicationId is empty");


            var application = await _mediatr.Send(new GetEntityCommand<Application>(id));

            if (application == null)
                return NotFound("No application found for given id");
            //TODO
            var viewModel = _mapper.Map<ResultsComparisonViewModel>(application);

            var experimentsCompare = application.Benchmarks.Where(c => c.Completed && c.CaptureContainerMetrics).OrderBy(c => c.BenchmarkHost.vCPU).ThenBy(c => c.BenchmarkHost.Memory).ToList();

            var appsSelectList = experimentsCompare.Select(provider => new SelectListItem
            {
                Text = provider.Name + "- vCPUs:" + provider.BenchmarkHost.vCPU + " - Memory: " + provider.BenchmarkHost.Memory,
                Value = provider.Id.ToString()
            });

            viewModel.CompletedExperiments = appsSelectList;

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult List()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Completed()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ManualResultsUpload()
        {
            var viewModel = new ManualBenchmarkResultsUploadViewModel
            {
                Items = await BenchmarkExperimentsManualSelectList()
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> CreateOptimizedExperiments()
        {
            var viewModel = new OptimisedBenchmarkExperimentRequestViewModel
            {
                Applications = await ApplicationsSelectList(),
                ApacheTestFiles = await ApacheTestFilesSelectList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOptimizedExperiments([FromForm]OptimisedBenchmarkExperimentRequestViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var allErrors = ModelState.Values.SelectMany(v => v.Errors);

                return View(viewModel);
            }


            var command = _mapper.Map<CreateOptimizedExperimentCommand>(viewModel);

            var result = await _mediatr.Send(command);
           
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult OptimizedExperimentsResults([Required]CloudProvider hostProvider,
            [Required]CloudProvider benchmarkProvider,
            [Required]int experimentLength,
            [Required]int concurrentUsers,
            [Required]decimal maxCost)
        {
            return ViewComponent(typeof(OptimisedBenchmarkExperimentResultsViewComponent), new { hostProvider, benchmarkProvider, experimentLength, concurrentUsers, maxCost });
        }
    }
}