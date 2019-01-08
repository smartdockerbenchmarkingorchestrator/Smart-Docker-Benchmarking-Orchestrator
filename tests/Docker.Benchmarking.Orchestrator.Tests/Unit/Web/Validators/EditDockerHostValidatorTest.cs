using Docker.Benchmarking.Orchestrator.Web.Validators;
using Docker.Benchmarking.Orchestrator.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Docker.Benchmarking.Orchestrator.Tests.Web.Validators
{
    public class EditDockerHostValidatorTest
    {
        private readonly EditDockerHostValidator _validator;

        public EditDockerHostValidatorTest()
        {
            _validator = new EditDockerHostValidator();
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

        private EditDockerHostViewModel ValidModel()
        {
            return new EditDockerHostViewModel
            {
                Id = Guid.NewGuid(),
                DateTimeCreated = DateTime.UtcNow,
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

        private EditDockerHostViewModel ValidPrivateRegistryModel()
        {
            return new EditDockerHostViewModel
            {
                Id = Guid.NewGuid(),
                DateTimeCreated = DateTime.UtcNow,
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

        private EditDockerHostViewModel ValidAzureModel()
        {
            return new EditDockerHostViewModel
            {
                Id = Guid.NewGuid(),
                DateTimeCreated = DateTime.UtcNow,
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
                DestroyResourcesAfterBenchmark = true
            };
        }

        private EditDockerHostViewModel InvalidModel()
        {
            return new EditDockerHostViewModel
            {
                Id = Guid.Empty,
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

        private EditDockerHostViewModel InvalidAzureModel()
        {
            return new EditDockerHostViewModel
            {
                Id = Guid.Empty,
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

        private EditDockerHostViewModel InValidPrivateRegistryModel()
        {
            return new EditDockerHostViewModel
            {
                Id = Guid.Empty,
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
