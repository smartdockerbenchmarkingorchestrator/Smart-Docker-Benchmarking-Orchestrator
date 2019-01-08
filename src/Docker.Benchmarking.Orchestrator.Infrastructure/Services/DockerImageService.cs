using Docker.Benchmarking.Orchestrator.Core.Entities;
using Docker.Benchmarking.Orchestrator.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Docker.Benchmarking.Orchestrator.Core.Exceptions;
using Ardalis.GuardClauses;

namespace Docker.Benchmarking.Orchestrator.Infrastrcture.Services
{
    public class DockerImageService : IDockerImageService
    {
        private readonly IRepository<DockerImage> _dockerImageRepo;
        public DockerImageService(IRepository<DockerImage> dockerImageRepo)
        {
            _dockerImageRepo = dockerImageRepo;
        }

        public async Task AddNewImage(DockerImage entity)
        {
            Guard.Against.Null(entity, nameof(entity));

            //Check if Docker Image Name already exists
            var exists = DockerImageNameTaken(entity.Name);

            if (exists)
                throw new ServiceLayerValidationException("Image Name Must be Unique");

            await _dockerImageRepo.AddAsync(entity);
        }

        public List<DockerImage> ApplicationImages()
        {
            return _dockerImageRepo.FindBy(c => c.ImageType == Core.Enums.ImageType.Application && c.Active).ToList();
        }

        public List<DockerImage> BenchmarkingImages()
        {
            return _dockerImageRepo.FindBy(c => c.ImageType == Core.Enums.ImageType.ApacheJmeter && c.Active).ToList();
        }

        public List<DockerImage> DatabaseImages()
        {
            return _dockerImageRepo.FindBy(c => c.ImageType == Core.Enums.ImageType.Database && c.Active).ToList();
        }

        public bool DockerImageNameTaken(string name)
        {
            return _dockerImageRepo.FindBy(c => c.Name.ToLower() == name.ToLower()).Any();
        }
    }
}
