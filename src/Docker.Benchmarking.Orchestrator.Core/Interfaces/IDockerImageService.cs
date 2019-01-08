using Docker.Benchmarking.Orchestrator.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Core.Interfaces
{
    public interface IDockerImageService
    {
        //Add New Docker Image to where business logic runs
        Task AddNewImage(DockerImage entity);

        //Check If Name Already Taken
        bool DockerImageNameTaken(string name);

        //List Just Application Images
        List<DockerImage> ApplicationImages();

        //List Just Benchmarking Images
        List<DockerImage> BenchmarkingImages();

        List<DockerImage> DatabaseImages();
    }
}
