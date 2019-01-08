using Docker.Benchmarking.Orchestrator.Tests.Unit.Web.Validators;
using Docker.Benchmarking.Orchestrator.Web.Validators;
using Docker.Benchmarking.Orchestrator.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Xunit;

namespace Docker.Benchmarking.Orchestrator.Tests.Web.Validators
{
    public class EditAzureTemplateValidatorTest
    {
        private EditAzureTemplateValidator _validator;

        public EditAzureTemplateValidatorTest()
        {
            _validator = new EditAzureTemplateValidator();
        }

        [Fact]
        public void TestValidModel()
        {
            var validate = _validator.Validate(ValidModel());

            Assert.True(validate.IsValid);
        }

        [Fact]

        public void TestNotValidModel()
        {
            var validate = _validator.Validate(InvalidModel());

            Assert.False(validate.IsValid);
        }

        #region private
        //TODO
        private EditAzureTemplateViewModel ValidModel()
        {
            return new EditAzureTemplateViewModel
            {
                Id = Guid.NewGuid(),
                Name = "Standard_F1_VM",
                DeploymentType = Core.Enums.AzureDeploymentType.VM,
                DiskSize = 80,
                Memory = 4000,
                vCPUs = 2,
                Template = ValidatorHelper.ValidJsonString(),
                ParametersDefault = ValidatorHelper.ValidJsonString(),
                VMSize = "Standard_F1",
                DateTimeCreated = DateTime.UtcNow
            };
        }

        private EditAzureTemplateViewModel InvalidModel()
        {
            return new EditAzureTemplateViewModel
            {
                Id = Guid.Empty,
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

        #endregion
    }
}
