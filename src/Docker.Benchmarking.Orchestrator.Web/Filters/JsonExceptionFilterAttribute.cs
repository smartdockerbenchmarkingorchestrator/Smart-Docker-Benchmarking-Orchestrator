using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Web.Filters
{
    //https://stackoverflow.com/questions/38664207/returning-exceptions-as-json-messages
    public class JsonExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var result = new ObjectResult(new
            {
                code = 500,
                message = "A server error occurred.",
                detailedMessage = context.Exception.Message
            })
            {
                StatusCode = 500
            };
            context.Result = result;
        }
    }
}
