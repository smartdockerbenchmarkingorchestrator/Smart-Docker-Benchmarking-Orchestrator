using Docker.Benchmarking.Orchestrator.Web.APIModels;
using Docker.Benchmarking.Orchestrator.Web.Validators;
using Docker.Benchmarking.Orchestrator.Web.ViewModels;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace Docker.Benchmarking.Orchestrator.Tests.Unit.Web.Validators
{
    public class ManualBenchmarkResultsUploadValidatorTest
    {
        public readonly ManualBenchmarkResultsUploadValidator _validator;

        public ManualBenchmarkResultsUploadValidatorTest()
        {
            _validator = new ManualBenchmarkResultsUploadValidator();
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

        [Fact]
        public void Test_Invalid_Filename_ViewModel()
        {
            var validate = _validator.Validate(InvalidFileNameModel());

            Assert.False(validate.IsValid);
        }

        #region private

        private ManualBenchmarkResultsUploadViewModel ValidModel()
        {
            var fileMock = new Mock<IFormFile>();
            //Setup mock file using a memory stream
            var content = "Hello World from a Fake File";
            var fileName = "test.csv";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);

            return new ManualBenchmarkResultsUploadViewModel
            {
                Id = Guid.NewGuid(),
                File = fileMock.Object         
            };
        }

        private ManualBenchmarkResultsUploadViewModel InvalidFileNameModel()
        {
            var fileMock = new Mock<IFormFile>();
            //Setup mock file using a memory stream
            var content = "Hello World from a Fake File";
            var fileName = "test.xlsx";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);

            return new ManualBenchmarkResultsUploadViewModel
            {
                Id = Guid.NewGuid(),
                File = fileMock.Object
            };
        }

        private ManualBenchmarkResultsUploadViewModel InvalidModel()
        {
            return new ManualBenchmarkResultsUploadViewModel
            {
                Id = Guid.Empty,
                File = null
            };
        }

        #endregion
    }
}
