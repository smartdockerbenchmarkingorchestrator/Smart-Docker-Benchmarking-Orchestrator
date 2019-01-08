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
    public class ApacheTestFileEditValidatorTest
    {
        private readonly ApacheTestFileEditValidator _validator;

        public ApacheTestFileEditValidatorTest()
        {
            _validator = new ApacheTestFileEditValidator();
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

       
        private ApacheTestFileEditModel ValidModel()
        {
            return new ApacheTestFileEditModel
            {
                Id = Guid.NewGuid(),
                DateTimeCreated = DateTime.UtcNow,
                Name = "Test",
                TestUpload = ValidatorHelper.ValidXmlString()
            };
        }

        private ApacheTestFileEditModel InvalidModel()
        {
            return new ApacheTestFileEditModel
            {
                Id = Guid.Empty,
                DateTimeCreated = DateTime.UtcNow,
                Name = string.Empty,
                TestUpload = ValidatorHelper.InvalidXmlString()
            };
        }

        #endregion
    }
}
