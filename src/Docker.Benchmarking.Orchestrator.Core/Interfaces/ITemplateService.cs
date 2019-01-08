using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Core.Interfaces
{
    /// https://gist.github.com/wmeints/c547209b69128e4ee537cb8f35c3a6dc
    /// <summary>
    /// Renders email content based on razor templates
    /// </summary>
    public interface ITemplateService
    {
        /// <summary>
        /// Renders a template given the provided view model
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <param name="filename">Filename of the template to render</param>
        /// <param name="viewModel">View model to use for rendering the template</param>
        /// <returns>Returns the rendered template content</returns>
        Task<string> RenderTemplateAsync<TViewModel>(string filename, TViewModel viewModel);
    }
}
