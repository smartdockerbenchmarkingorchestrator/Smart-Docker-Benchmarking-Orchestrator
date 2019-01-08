using Docker.Benchmarking.Orchestrator.Web.Validators;
using Docker.Benchmarking.Orchestrator.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Xunit;

namespace Docker.Benchmarking.Orchestrator.Tests.Unit.Web.Validators
{
    public class TestAzureResourcesValidatorTest
    {
        private readonly TestAzureResourcesValidator _validator;

        public TestAzureResourcesValidatorTest()
        {
            _validator = new TestAzureResourcesValidator();
        }

        [Fact]
        public void Test_Valid_ViewModel()
        {
            var validate = _validator.Validate(ValidModel());

            Assert.True(validate.IsValid);
        }


        [Fact]
        public void Test_Invalid_ViewModel()
        {
            var validate = _validator.Validate(InvalidModel());

            Assert.False(validate.IsValid);
        }


        #region private

        private TestAzureResourcesViewModel ValidModel()
        {
            return new TestAzureResourcesViewModel
            {
                ResourceName = "TestResourceName",
                AzureRegion = "east-us",
                DeploymentJson = ValidatorHelper.ValidJsonString(),
                ParametersJson = ValidatorHelper.ValidJsonString()
            };
        }

        private TestAzureResourcesViewModel InvalidModel()
        {
            return new TestAzureResourcesViewModel
            {
                ResourceName = string.Empty,
                AzureRegion = string.Empty,
                DeploymentJson = ValidatorHelper.InvalidJsonString(),
                ParametersJson = ValidatorHelper.InvalidJsonString()
            };
        }

        #endregion
    }
}
