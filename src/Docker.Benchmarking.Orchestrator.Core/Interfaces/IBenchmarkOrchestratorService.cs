using Docker.Benchmarking.Orchestrator.Core.Entities;
using Hangfire;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Core.Interfaces
{
    /// <summary>
    /// Interface IBenchmarkOrchestratorService
    /// </summary>
    public interface IBenchmarkOrchestratorService
    {
        //This starts of process of deploying the images to the required hosts
        /// <summary>
        /// Starts the benchmarking step1.
        /// </summary>
        /// <param name="applicationTestId">The application test identifier.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        Task<bool> StartBenchmarkingStep1(Guid applicationTestId);

        /// <summary>
        /// Stops the application container.
        /// </summary>
        /// <param name="appTest">The application test.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        Task<bool> StopApplicationContainer(BenchmarkExperiment appTest);

        /// <summary>
        /// Captures the docker stats asynchronous.
        /// </summary>
        /// <param name="appTest">The application test.</param>
        /// <param name="token">The token.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        Task<bool> CaptureDockerStatsAsync(Guid appTest, IJobCancellationToken token);

        /// <summary>
        /// Captures the docker stats.
        /// </summary>
        /// <param name="appTest">The application test.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool CaptureDockerStats(BenchmarkExperiment appTest);

        //This initiates that the benchmarking experiment is to start from benchmarking host and docker stats
        /// <summary>
        /// Starts the benchmarking step2.
        /// </summary>
        /// <param name="benchmarkExperiment">The benchmark experiment.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        Task<bool> StartBenchmarkingStep2(BenchmarkExperiment benchmarkExperiment);

        /// <summary>
        /// Ends the benchmark experiment.
        /// </summary>
        /// <param name="benchmarkExperiment">The benchmark experiment.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        Task<bool> EndBenchmarkExperiment(BenchmarkExperiment benchmarkExperiment);

    }
}
