using Docker.Benchmarking.Orchestrator.Web.Validators;
using Docker.Benchmarking.Orchestrator.Web.ViewModels;
using Docker.Benchmarking.Orchestrator.Infrastrcture.Helpers;
using Xunit;
using System.IO;
using System.Reflection;
using System;
using Docker.Benchmarking.Orchestrator.Tests.Unit.Web.Validators;

namespace Docker.Benchmarking.Orchestrator.Tests.Web.Validators
{
    public class AddAzureTemplateValidatorTest
    {
        private readonly AddAzureTemplateValidator _validator;
        public AddAzureTemplateValidatorTest()
        {
            _validator = new AddAzureTemplateValidator();
        }

        [Fact]
        public void TestValidViewModel()
        {
            var validate = _validator.Validate(ValidModel());

            Assert.True(validate.IsValid);
        }

        [Fact]
        public void TestInValidViewModel()
        {
            var validate = _validator.Validate(InvalidModel());

            Assert.False(validate.IsValid);
        }

        [Fact]
        public void TestValidJson()
        {
            Assert.True(ValidatorHelper.ValidJsonString().IsValidJson());
        }

        [Fact]
        public void TestInvalidJson()
        {
            Assert.False(ValidatorHelper.InvalidJsonString().IsValidJson());

        }

        private AddAzureTemplateViewModel ValidModel()
        {
            return new AddAzureTemplateViewModel
            {
                Name = "Standard_F1_VM",
                DeploymentType = Orchestrator.Core.Enums.AzureDeploymentType.VM,
                DiskSize = 80,
                Memory = 4000,
                vCPUs = 2,
                Template = ValidatorHelper.ValidJsonString(),
                ParametersDefault = ValidatorHelper.ValidJsonString(),
                VMSize = "Standard_F1"
            };
        }

        private AddAzureTemplateViewModel InvalidModel()
        {
            return new AddAzureTemplateViewModel
            {
                Name = string.Empty,
                DeploymentType = Core.Enums.AzureDeploymentType.VM,
                DiskSize = 80,
                Memory = 4000,
                vCPUs = 2,
                Template = ValidatorHelper.InvalidJsonString(),
                ParametersDefault = ValidatorHelper.InvalidJsonString(),
                VMSize = "Standard_F1"
            };
        }
    }
}
