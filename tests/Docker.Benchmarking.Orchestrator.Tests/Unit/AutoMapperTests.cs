using Docker.Benchmarking.Orchestrator.Web;
using Docker.Benchmarking.Orchestrator.Web.Controllers;
using FakeItEasy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace Docker.Benchmarking.Orchestrator.Tests.Unit
{
    public class AutoMapperTests
    {
        [Fact]
        public void Should_have_valid_configuration()
        {
            var host = A.Fake<IHostingEnvironment>();

            //  Setting up the stuff required for Configuration.GetConnectionString("DefaultConnection")
            Mock<IConfigurationSection> configurationSectionStub = new Mock<IConfigurationSection>();
            configurationSectionStub.Setup(x => x["DefaultConnection"]).Returns("TestConnectionString");
            Mock<Microsoft.Extensions.Configuration.IConfiguration> configurationStub = new Mock<Microsoft.Extensions.Configuration.IConfiguration>();
            configurationStub.Setup(x => x.GetSection("ConnectionStrings")).Returns(configurationSectionStub.Object);

            IServiceCollection services = new ServiceCollection();
            var target = new Startup(configurationStub.Object);

            //  Act

            target.ConfigureServices(services);
            //  Mimic internal asp.net core logic.
            services.AddTransient<DockerController>();

            //  Assert

            var serviceProvider = services.BuildServiceProvider();

            var controller = serviceProvider.GetService<DockerController>();
            Assert.NotNull(controller);

            //A.CallTo(() => host.ContentRootPath).Returns(Directory.GetCurrentDirectory());

            ////FixEntryAssembly();

            //var startup = new Startup(host);
            //var services = new ServiceCollection();
            //startup.ConfigureServices(services);

            //Mapper.AssertConfigurationIsValid();
        }
    }
}
