using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Core.Interfaces
{
    public interface IRazorViewToStringRenderer
    {
        Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model);
    }
}
