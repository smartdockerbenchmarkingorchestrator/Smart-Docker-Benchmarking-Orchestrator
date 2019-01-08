using Docker.Benchmarking.Orchestrator.Web.Validators;
using Docker.Benchmarking.Orchestrator.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Docker.Benchmarking.Orchestrator.Tests.Unit.Web.Validators
{
    public class EditDockerImageValidatorTest
    {
        private readonly EditDockerImageValidator _validator;

        public EditDockerImageValidatorTest()
        {
            _validator = new EditDockerImageValidator();
        }

        [Fact]
        public void Test_Valid_ViewModel()
        {
            var validate = _validator.Validate(ValidModel());

            Assert.True(validate.IsValid);
        }

        [Fact]
        public void Test_Valid_PrivateRegistry_ViewModel()
        {
            var validate = _validator.Validate(ValidPrivateRegistryModel());

            Assert.True(validate.IsValid);
        }

        [Fact]
        public void Test_Invalid_ViewModel()
        {
            var validate = _validator.Validate(InvalidModel());

            Assert.False(validate.IsValid);
        }

        [Fact]
        public void Test_Invalid_PrivateRegistry_ViewModel()
        {
            var validate = _validator.Validate(InvalidPrivateRegistryModel());

            Assert.False(validate.IsValid);
        }

        #region private

        private EditDockerImageViewModel ValidModel()
        {
            return new EditDockerImageViewModel
            {
                Id = Guid.NewGuid(),
                DateTimeCreated = DateTime.UtcNow,
                Name = "Standard_F1_VM",
                ImageName = "mnee2/docker",
                ImageTag = "latest",
                ImageType = Core.Enums.ImageType.Application
            };
        }

        private EditDockerImageViewModel ValidPrivateRegistryModel()
        {
            return new EditDockerImageViewModel
            {
                Id = Guid.NewGuid(),
                DateTimeCreated = DateTime.UtcNow,
                Name = "Standard_F1_VM",
                ImageName = "mnee2/docker",
                ImageTag = "latest",
                ImageType = Orchestrator.Core.Enums.ImageType.Application,
                PrivateRepository = true,
                PrivateRepositoryHost = "hub.docker.com",
                PrivateRepositoryPassword = "testpassword",
                PrivateRepositoryUsername = "testuser"
            };
        }

        private EditDockerImageViewModel InvalidModel()
        {
            return new EditDockerImageViewModel
            {
                Name = string.Empty,
                ImageName = string.Empty,
                ImageTag = string.Empty,
                ImageType = Core.Enums.ImageType.Application
            };
        }

        private EditDockerImageViewModel InvalidPrivateRegistryModel()
        {
            return new EditDockerImageViewModel
            {
                Name = "Standard_F1_VM",
                ImageName = "mnee2/docker",
                ImageTag = "latest",
                ImageType = Orchestrator.Core.Enums.ImageType.Application,
                PrivateRepository = true,
                PrivateRepositoryHost = string.Empty,
                PrivateRepositoryPassword = string.Empty,
                PrivateRepositoryUsername = string.Empty
            };
        }

        #endregion
    }
}
