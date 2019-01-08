using Docker.Benchmarking.Orchestrator.Web.Validators;
using Docker.Benchmarking.Orchestrator.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Docker.Benchmarking.Orchestrator.Tests.Web.Validators
{
    public class AddDockerHostValidatorTest
    {
        private readonly AddDockerHostValidator _validator;
        public AddDockerHostValidatorTest()
        {
            _validator = new AddDockerHostValidator();
        }

        [Fact]
        public void Test_Valid_ViewModel()
        {
            var validate = _validator.Validate(ValidModel());

            Assert.True(validate.IsValid);
        }


        [Fact]
        public void Test_Valid_Azure_ViewModel()
        {
            var validate = _validator.Validate(ValidAzureModel());

            Assert.True(validate.IsValid);
        }


        [Fact]
        public void Test_Valid_PrivateRegistry_ViewModel()
        {
            var validate = _validator.Validate(ValidPrivateRegistryModel());

            Assert.True(validate.IsValid);
        }

        [Fact]
        public void Test_InValid_ViewModel()
        {
            var validate = _validator.Validate(InvalidModel());

            Assert.False(validate.IsValid);
        }

        [Fact]
        public void Test_InValid_Azure_ViewModel()
        {
            var validate = _validator.Validate(InvalidAzureModel());

            Assert.False(validate.IsValid);
        }

        [Fact]
        public void Test_InValid_PrivateRegistry_ViewModel()
        {
            var validate = _validator.Validate(InValidPrivateRegistryModel());

            Assert.False(validate.IsValid);
        }

        #region private

        private AddDockerHostViewModel ValidModel()
        {
            return new AddDockerHostViewModel
            {
                Name = "Standard_F1_VM",
                Active = true,
                CloudProvider = Orchestrator.Core.Enums.CloudProvider.AWS,
                HostName = "localhost",
                Location = "Ireland",
                PortNumber = 2376,
                vCPU = 2,
                Memory = 4000,
                DestroyResourcesAfterBenchmark = true
            };
        }

        private AddDockerHostViewModel ValidPrivateRegistryModel()
        {
            return new AddDockerHostViewModel
            {
                Name = "Standard_F1_VM",
                Active = true,
                CloudProvider = Orchestrator.Core.Enums.CloudProvider.AWS,
                HostName = "localhost",
                Location = "Ireland",
                PortNumber = 2376,
                vCPU = 2,
                Memory = 4000,
                DestroyResourcesAfterBenchmark = true,
                UseHttpAuthentication = true,
                UserName = "testuser",
                Password = "testpassword"
            };
        }

        private AddDockerHostViewModel ValidAzureModel()
        {
            return new AddDockerHostViewModel
            {
                Name = "Standard_F1_VM",
                Active = true,
                CloudProvider = Orchestrator.Core.Enums.CloudProvider.Azure,
                AzureLocation = "Ireland",
                AzureTemplate = Guid.NewGuid(),
                HostName = "localhost",
                Location = "Ireland",
                PortNumber = 2376,
                vCPU = 2,
                Memory = 4000,
                DestroyResourcesAfterBenchmark = true,
                HostType = Core.Enums.HostType.Application
            };
        }

        private AddDockerHostViewModel InvalidModel()
        {
            return new AddDockerHostViewModel
            {
                Name = string.Empty,
                Active = true,
                CloudProvider = Orchestrator.Core.Enums.CloudProvider.AWS,
                HostName = "localhost",
                Location = "Ireland",
                PortNumber = 2376,
                vCPU = 2,
                Memory = 4000,
                DestroyResourcesAfterBenchmark = true
            };
        }

        private AddDockerHostViewModel InvalidAzureModel()
        {
            return new AddDockerHostViewModel
            {
                Name = "Standard_F1_VM",
                Active = true,
                CloudProvider = Orchestrator.Core.Enums.CloudProvider.Azure,
                AzureLocation = string.Empty,
                AzureTemplate = Guid.Empty,
                HostName = "localhost",
                Location = "Ireland",
                PortNumber = 2376,
                vCPU = 2,
                Memory = 4000,
                DestroyResourcesAfterBenchmark = true
            };
        }

        private AddDockerHostViewModel InValidPrivateRegistryModel()
        {
            return new AddDockerHostViewModel
            {
                Name = "Standard_F1_VM",
                Active = true,
                CloudProvider = Orchestrator.Core.Enums.CloudProvider.AWS,
                HostName = "localhost",
                Location = "Ireland",
                PortNumber = 2376,
                vCPU = 2,
                Memory = 4000,
                DestroyResourcesAfterBenchmark = true,
                UseHttpAuthentication = true,
                UserName = string.Empty,
                Password = string.Empty
            };
        }

        #endregion
    }
}
