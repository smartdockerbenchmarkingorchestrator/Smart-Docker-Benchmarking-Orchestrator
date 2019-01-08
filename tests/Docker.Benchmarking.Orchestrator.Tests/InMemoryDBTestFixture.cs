using Docker.Benchmarking.Orchestrator.Infrastrcture.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Tests
{
    public class InMemoryDBTestFixture : IDisposable
    {
        readonly AppDbContext context;
        public InMemoryDBTestFixture()
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "DockerBenchmarkingOrch");

            var options = builder.Options;
            context = new AppDbContext(options);

            AddDataToInMemoryDb();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        private async Task AddDataToInMemoryDb()
        {
            var appImage = new Core.Entities.DockerImage
            {
                Name = "AppImage",
                Description = "",
                ExternalPort = 80,
                InternalPort = 80,
                ImageName = "Image",
                ImageTag = "latest",
                ImageType = Core.Enums.ImageType.Application
            };

            await context.DockerImage.AddAsync(appImage);

            var jmeterImage = new Core.Entities.DockerImage
            {
                Name = "JmeterImage",
                Description = "",
                ImageName = "JmeterImage",
                ImageTag = "latest",
                ImageType = Core.Enums.ImageType.ApacheJmeter
            };

            await context.DockerImage.AddAsync(jmeterImage);

            await context.SaveChangesAsync();

            var application = new Core.Entities.Application
            {
                Name = "Test Application",
                ApplicationImage = appImage,
                BenchmarkingImage = jmeterImage,
                ApplicationType = Core.Enums.ApplicationType.ECommerce,
                DelayToStartApplication = 60000,
            };

            await context.Application.AddAsync(application);
        }
    }
}
