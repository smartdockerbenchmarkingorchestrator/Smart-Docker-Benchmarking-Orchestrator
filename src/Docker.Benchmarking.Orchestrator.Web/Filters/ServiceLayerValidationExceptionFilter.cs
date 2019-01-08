using Docker.Benchmarking.Orchestrator.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Docker.Benchmarking.Orchestrator.Web.Filters
{
    public class ServiceLayerValidationExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is ServiceLayerValidationException)
            {
                context.Result = new BadRequestObjectResult(context.Exception.Message);
            }
        }
    }
}
