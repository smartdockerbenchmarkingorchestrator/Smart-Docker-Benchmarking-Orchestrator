using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Docker.Benchmarking.Orchestrator.Web.Pages.AWS
{
    public class TestModel : PageModel
    {
        public void OnGet()
        {

        }

        public void OnPostYogaPostures(int sessionCount)
        {
            //do your work here

            ViewData["message"] = $"Your request for{sessionCount}sessions in Yoga Postures is being processed.";
        }

        public void OnPostMeditation(int sessionCount)
        {
            //do your work here

            ViewData["message"] = $"Your request for{ sessionCount}sessions in Kriya and Meditation is being processed.";
        }


        public void OnPostRestorativeYoga(int sessionCount)
        {
            //do your work here

            ViewData["message"] = $"Your request for{sessionCount}sessions in Restorative Yoga is being processed.";
        }
    }
}