using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Docker.Benchmarking.Orchestrator.Core.Entities;
using MediatR;
using AutoMapper;
using Docker.Benchmarking.Orchestrator.Web.ViewModels.CSVFile;
using Docker.Benchmarking.Orchestrator.Core.Commands;

namespace Docker.Benchmarking.Orchestrator.Web.Pages.Home
{
    public class CSVUploadsModel : PageModel
    {
        private readonly IMediator _mediatr;
        private readonly IMapper _mapper;

        public CSVUploadsModel(IMediator mediatr, IMapper mapper)
        {
            _mediatr = mediatr;
            _mapper = mapper;
        }

        [BindProperty]
        public IList<CSVUploadViewModel> CSVUploadList { get;set; }

        public async Task OnGetAsync()
        {
            await GetUploadsAsync();         
        }

        public async Task<IActionResult> OnPostDownloadFile(Guid id)
        {
            await GetUploadsAsync();

            var record = CSVUploadList.SingleOrDefault(c => c.Id == id);
            return File(record.CSVResultsFileBytes, "application/octet-stream", record.Name);
        }

        private async Task GetUploadsAsync()
        {
            var csvUploads = await _mediatr.Send(new ListEntitiesCommand<CSVUpload>());

            var csvViewModel = _mapper.Map<IList<CSVUploadViewModel>>(csvUploads);
            CSVUploadList = csvViewModel;
        }
    }
}
