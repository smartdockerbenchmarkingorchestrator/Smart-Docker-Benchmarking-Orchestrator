// ***********************************************************************
// Assembly         : Docker.Benchmarking.Orchestrator.Web
// Author           : ***********
// Created          : 07-25-2018
//
// Last Modified By : ***********
// Last Modified On : 09-08-2018
// ***********************************************************************
// <copyright file="DockerController.cs" company="Docker.Benchmarking.Orchestrator.Web">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using AutoMapper;
using Docker.Benchmarking.Orchestrator.Core.Commands;
using Docker.Benchmarking.Orchestrator.Core.Entities;
using Docker.Benchmarking.Orchestrator.Web.Extensions;
using Docker.Benchmarking.Orchestrator.Web.Filters;
using Docker.Benchmarking.Orchestrator.Web.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Web.Controllers
{
    /// <summary>
    /// Class DockerController.
    /// </summary>
    /// <seealso cref="Docker.Benchmarking.Orchestrator.Web.Controllers.BaseController" />
    //[ApiController]
    //[JsonExceptionFilter]
    //[Route("api/[controller]")]
    public class DockerController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DockerController"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="mediatr">The mediatr.</param>
        public DockerController(IMapper mapper, IMediator mediatr) : base(mapper, mediatr)
        {

        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>IActionResult.</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Adds this instance.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        public IActionResult Add()
        {
            return View(new AddDockerHostViewModel());
        }


        /// <summary>
        /// Adds the image.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpGet(Name = "AddImage")]
        public IActionResult AddImage()
        {
            return View(new AddDockerImageViewModel());
        }

        /// <summary>
        /// Adds Docker image and sends to Mediatr Create Command.  ModelState is Checked.
        /// </summary>
        /// <param name="model">The AddDockerImage</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPost(Name = "AddImage")]
        [ServiceLayerValidationExceptionFilter]
        [ValidateModel]
        [JsonExceptionFilter]
        public async Task<IActionResult> AddImage(AddDockerImageViewModel model)
        {
            var result = _mapper.Map<DockerImage>(model);

            await _mediatr.Send(new CreateCommand<DockerImage>(result));

            return Ok(result.Id);
        }

        /// <summary>
        /// Imageses this instance.
        /// </summary>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpGet]
        public async Task<IActionResult> Images()
        {
            var images = await _mediatr.Send(new ListDockerImagesCommand());

            return View(images);
        }

        /// <summary>
        /// Images the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpGet]
        public async Task<IActionResult> Image(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Please submit a correct id");

            var image = await _mediatr.Send(new GetEntityCommand<DockerImage>(id));

            if (image == null)
                return BadRequest("No image found for ID: " + id.ToString());

            return View(image);
        }

        //[HttpGet]
        //public IActionResult JMXStreamTest()
        //{
        //    string filePath = Path.Combine(_env.WebRootPath, "uploads\\testresults.csv").ToLower();

        //    HttpContext.Response.ContentType = "application/csv";
        //    FileContentResult result = new FileContentResult(System.IO.File.ReadAllBytes(filePath), "application/csv")
        //    {
        //        FileDownloadName = "testresults.csv"
        //    };

        //    //var content = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        //    //var response = File(content, "application/octet-stream");//FileStreamResult
        //    return result;
        //}

        /// <summary>
        /// Determines whether [is name unique] [the specified name].
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> IsNameUnique(string name)
        {
            var result = await _mediatr.Send(new FindEntityByNameCommand<DockerImage>(name));
            return Json(!result);

        }

        /// <summary>
        /// Determines whether docker[is host name unique] [the specified name].
        /// </summary>
        /// <param name="name">The name of the docker host</param>
        /// <returns>Json true or false</returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> IsHostNameUnique(string name)
        {
            var result = await _mediatr.Send(new FindEntityByNameCommand<DockerHost>(name));
            return Json(!result);
        }

        /// <summary>
        /// Adds the host.
        /// </summary>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpGet]
        public async Task<IActionResult> AddHost()
        {
            var viewModel = new AddDockerHostViewModel
            {
                AzureLocations = await AzureRegionsSelectList(),
                AzureVMSizes = await _mediatr.Send(new ListActiveEntitiesCommand<AzureVMTemplate>()),
                AWSCredentials = await _mediatr.Send(new ListActiveEntitiesCommand<AWSCredentials>()),
                AWSTemplates = await _mediatr.Send(new ListActiveEntitiesCommand<AWSCloudFormationTemplate>()),
                AzureCredentials = await AzureCredentialsSelectList()
            };

            return View(viewModel);
        }

        /// <summary>
        /// Hostses this instance.
        /// </summary>
        /// <returns>IActionResult.</returns>
        public IActionResult Hosts()
        {
            return View();
        }

        /// <summary>
        /// Edits the host.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        public async Task<IActionResult> EditHost(Guid id)
        {
            if (id == Guid.Empty) return BadRequest("id is empty/not valid");

            var host = await _mediatr.Send(new GetEntityCommand<DockerHost>(id));

            if (host == null) return NotFound("No host found for given id");

            var viewModel = _mapper.Map<EditDockerHostViewModel>(host);

            return View(viewModel);
        }

        /// <summary>
        /// Hosts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        public async Task<IActionResult> Host(Guid id)
        {
            if (id == Guid.Empty) return BadRequest("id is empty/not valid");

            var host = await _mediatr.Send(new GetEntityCommand<DockerHost>(id));

            if (host == null) return NotFound("No host found for given id");

            var viewModel = _mapper.Map<DockerHostViewModel>(host);

            return View(viewModel);

        }

        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty) return BadRequest("id is empty/not valid");

            var host = await _mediatr.Send(new GetEntityCommand<DockerImage>(id));

            if (host == null) return NotFound("No image found for given id");

            var viewModel = _mapper.Map<EditDockerImageViewModel>(host);

            return View(viewModel);
        }
    }
}