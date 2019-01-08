using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Docker.Benchmarking.Orchestrator.Infrastrcture.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Docker.Benchmarking.Orchestrator.Web.Controllers
{
    //https://gist.github.com/redwards510/a26276218be2b9d104e5e590ebed5b92
    [Route("Dgml")]
    public class DgmlController : Controller
    {
        public AppDbContext Context { get; }

        public DgmlController(AppDbContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Creates a DGML class diagram of most of the entities in the project wher you go to localhost/dgml
        /// See https://github.com/ErikEJ/SqlCeToolbox/wiki/EF-Core-Power-Tools
        /// </summary>
        /// <returns>a DGML class diagram</returns>
        [HttpGet]
        public IActionResult Get()
        {

            System.IO.File.WriteAllText(Directory.GetCurrentDirectory() + "\\Entities.dgml",
                Context.AsDgml(), System.Text.Encoding.UTF8);

            var file = System.IO.File.OpenRead(Directory.GetCurrentDirectory() + "\\Entities.dgml");
            var response = File(file, "application/octet-stream", "Entities.dgml");
            return response;
        }
    }
}
