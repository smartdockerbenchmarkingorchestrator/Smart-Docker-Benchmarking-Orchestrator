using Docker.Benchmarking.Orchestrator.Web.Validators;
using Docker.Benchmarking.Orchestrator.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Docker.Benchmarking.Orchestrator.Tests.Web.Validators
{
    public class AddDockerImageValidatorTest
    {
        private readonly AddDockerImageValidator _validator;
        public AddDockerImageValidatorTest()
        {
            _validator = new AddDockerImageValidator();
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

        private AddDockerImageViewModel ValidModel()
        {
            return new AddDockerImageViewModel
            {
                Name = "Standard_F1_VM",
                ImageName = "mnee2/docker",
                ImageTag = "latest",
                ImageType = Orchestrator.Core.Enums.ImageType.Application
            };
        }

        private AddDockerImageViewModel ValidPrivateRegistryModel()
        {
            return new AddDockerImageViewModel
            {
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

        private AddDockerImageViewModel InvalidModel()
        {
            return new AddDockerImageViewModel
            {
                Name = string.Empty,
                ImageName = string.Empty,
                ImageTag = string.Empty,
                ImageType = Orchestrator.Core.Enums.ImageType.Application
            };
        }

        private AddDockerImageViewModel InvalidPrivateRegistryModel()
        {
            return new AddDockerImageViewModel
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
