using Docker.Benchmarking.Orchestrator.Infrastrcture.Helpers;
using Docker.Benchmarking.Orchestrator.Tests.Unit.Web.Validators;
using Docker.Benchmarking.Orchestrator.Web.Validators;
using Docker.Benchmarking.Orchestrator.Web.ViewModels;
using Xunit;

namespace Docker.Benchmarking.Orchestrator.Tests.Web.Validators
{
    public class AddApacheJmeterTestFileValidatorTest
    {
        private readonly AddApacheJmeterTestFileValidator _validator;
        public AddApacheJmeterTestFileValidatorTest()
        {
            _validator = new AddApacheJmeterTestFileValidator();
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
        public void TestValidXml()
        {
            Assert.True(ValidatorHelper.ValidXmlString().IsValidXml());
        }

        [Fact]
        public void TestInvalidXml()
        {
            Assert.False(ValidatorHelper.InvalidXmlString().IsValidXml());

        }      

        private AddApacheJmeterTestFileViewModel ValidModel()
        {
            return new AddApacheJmeterTestFileViewModel
            {
                Name = "Test File",
                Description = string.Empty,
                TestUpload = ValidatorHelper.ValidXmlString()
            };
        }

        private AddApacheJmeterTestFileViewModel InvalidModel()
        {
            return new AddApacheJmeterTestFileViewModel
            {
                Name = string.Empty,
                Description = string.Empty,
                TestUpload = ValidatorHelper.InvalidXmlString()
            };
        }
    }
}
