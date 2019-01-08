using Docker.Benchmarking.Orchestrator.Core.Entities;
using Docker.Benchmarking.Orchestrator.Core.Interfaces;
using Docker.Benchmarking.Orchestrator.Infrastrcture.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;

namespace Docker.Benchmarking.Orchestrator.Tests.Integration
{
    public class DockerImageRepositoryTest
    {
        [Fact]
        public void AddDockerImage()
        {
            IRepository<DockerImage> sut = GetInMemoryDockerImageRepository();
            DockerImage dockImage = new DockerImage()
            {
                Name = "Standard_F1_VM",
                ImageName = "mnee2/docker",
                ImageTag = "latest",
                ImageType = Core.Enums.ImageType.Application
            };

            DockerImage savedPerson = sut.Add(dockImage);

            Assert.Equal(1, sut.GetAll().Count());
            Assert.Equal("Standard_F1_VM", savedPerson.Name);
            Assert.Equal("mnee2/docker", savedPerson.ImageName);
            Assert.Equal(Core.Enums.ImageType.Application, savedPerson.ImageType);

            Assert.NotEqual(Guid.Empty, savedPerson.Id);
        }

        private IRepository<DockerImage> GetInMemoryDockerImageRepository()
        {
            DbContextOptions<AppDbContext> options;
            var builder = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "docker_benchmarking_orchestrator");
            options = builder.Options;
            AppDbContext personDataContext = new AppDbContext(options);
            personDataContext.Database.EnsureDeleted();
            personDataContext.Database.EnsureCreated();
            return new EfRepository<DockerImage>(personDataContext);
        }
    }
}
