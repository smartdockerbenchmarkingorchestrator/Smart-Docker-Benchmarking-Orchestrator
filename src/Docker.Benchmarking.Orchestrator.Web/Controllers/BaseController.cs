using AutoMapper;
using Docker.Benchmarking.Orchestrator.Core.Commands;
using Docker.Benchmarking.Orchestrator.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Web.Controllers
{
    public abstract partial class BaseController : Controller
    {
        protected readonly IMapper _mapper;
        protected readonly IMediator _mediatr;

        public BaseController(IMapper mapper, IMediator mediatr)
        {
            _mapper = mapper;
            _mediatr = mediatr;
        }

        protected async Task<IEnumerable<SelectListItem>> DockerApplicationImagesSelectList()
        {
            var appImages = await _mediatr.Send(new DockerApplicationImagesCommand());

            return appImages.Select(provider => new SelectListItem
            {
                Text = provider.Name,
                Value = provider.Id.ToString()
            });
        }

        protected async Task<IEnumerable<SelectListItem>> DockerBenchmarkImagesSelectList()
        {
            var appImages = await _mediatr.Send(new DockerBenchmarkImagesCommand());

            return appImages.Select(provider => new SelectListItem
            {
                Text = provider.Name,
                Value = provider.Id.ToString()
            });
        }

        protected async Task<IEnumerable<SelectListItem>> DockerDatabaseImagesSelectList()
        {
            var appImages = await _mediatr.Send(new DockerDatabaseImagesCommand());

            return appImages.Select(provider => new SelectListItem
            {
                Text = provider.Name,
                Value = provider.Id.ToString()
            });
        }

        protected async Task<IEnumerable<SelectListItem>> ApacheTestFilesSelectList()
        {
            var apacheTestFiles = await _mediatr.Send(new ListActiveEntitiesCommand<ApacheJmeterTestFile>());

            return apacheTestFiles.Select(provider => new SelectListItem
            {
                Text = provider.Name,
                Value = provider.Id.ToString()
            });
        }

        protected Task<IEnumerable<SelectListItem>> AzureRegionsSelectList()
        {
            return Task.FromResult(Region.Values.Select(provider => new SelectListItem
            {
                Text = provider.Name,
                Value = provider.Name
            }));
        }

        protected async Task<IEnumerable<SelectListItem>> ApplicationHosts()
        {
            var hosts = await _mediatr.Send(new ListActiveEntitiesCommand<DockerHost>());

            return hosts.Where(c => c.HostType == Core.Enums.HostType.Application).Select(provider => new SelectListItem
            {
                Text = provider.Name,
                Value = provider.Id.ToString()
            });

        }

        protected async Task<IEnumerable<SelectListItem>> BenchmarkHosts()
        {
            var hosts = await _mediatr.Send(new ListActiveEntitiesCommand<DockerHost>());

            return hosts.Where(c => c.HostType == Core.Enums.HostType.Benchmark).Select(provider => new SelectListItem
            {
                Text = provider.Name,
                Value = provider.Id.ToString()
            });

        }

        protected async Task<IEnumerable<SelectListItem>> DatabaseHosts()
        {
            var hosts = await _mediatr.Send(new ListActiveEntitiesCommand<DockerHost>());

            return hosts.Where(c => c.HostType == Core.Enums.HostType.Database).Select(provider => new SelectListItem
            {
                Text = provider.Name,
                Value = provider.Id.ToString()
            });

        }

        protected async Task<IEnumerable<SelectListItem>> AWSCredentialsSelectList()
        {
            var list = await _mediatr.Send(new ListActiveEntitiesCommand<AWSCredentials>());

            return list.Select(c => new SelectListItem
            {
                Text = c.Name + " - " + c.AWSEndPoint.DisplayName,
                Value = c.Id.ToString()
            });
        }

        protected async Task<IEnumerable<SelectListItem>> AzureCredentialsSelectList()
        {
            var list = await _mediatr.Send(new ListActiveEntitiesCommand<AzureCredientials>());

            return list.Select(c => new SelectListItem
            {
                Text = c.Name + " - " + c.SubscriptionId,
                Value = c.Id.ToString()
            });
        }

        protected async Task<IEnumerable<SelectListItem>> ApplicationsSelectList(Guid selectedValue = default(Guid))
        {
            var apps = await _mediatr.Send(new ListActiveEntitiesCommand<Application>());

            return apps.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString(),
                Selected = (selectedValue != default(Guid) ? c.Id == selectedValue ? true : false : false)
            });
        }

        protected async Task<IEnumerable<SelectListItem>> DockerHostsSelectList()
        {
            var apps = await _mediatr.Send(new ListEntitiesCommand<DockerHost>());

            return apps.Select(provider => new SelectListItem
            {
                Text = provider.Name,
                Value = provider.Id.ToString()
            });
        }

        protected async Task<IEnumerable<SelectListItem>> BenchmarkExperimentsManualSelectList()
        {
            var items = await _mediatr.Send(new BenchmarkExperimentsListManualUploadCommand());

            return items.Select(provider => new SelectListItem
            {
                Text = provider.Name,
                Value = provider.Id.ToString()
            });
        }

        protected async Task<IEnumerable<SelectListItem>> AzureActiveVMTemplates()
        {
            var items = await _mediatr.Send(new ListActiveEntitiesCommand<AzureVMTemplate>());

            return items.Select(provider => new SelectListItem
            {
                Text = provider.Name,
                Value = provider.Id.ToString()
            });
        }
    }
}