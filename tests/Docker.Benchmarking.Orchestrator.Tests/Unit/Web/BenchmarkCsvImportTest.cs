using Docker.Benchmarking.Orchestrator.Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Docker.Benchmarking.Orchestrator.Infrastrcture.Mappings;
using TinyCsvParser;
using Xunit;

namespace Docker.Benchmarking.Orchestrator.Tests.Web
{
    public class BenchmarkCsvImportTest
    {
        [Fact]
        public void TinyCsvTest()
        {
            CsvParserOptions csvParserOptions = new CsvParserOptions(true, ',');
            CsvApacheJmeterResultMapping csvMapper = new CsvApacheJmeterResultMapping();
            CsvParser<BenchmarkTestItem> csvParser = new CsvParser<BenchmarkTestItem>(csvParserOptions, csvMapper);

            var dirName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var filePath = Path.Combine(dirName + @"\\FileUploads\\TestCSVUpload.csv");

            var result = csvParser
                .ReadFromFile(filePath, Encoding.ASCII)
                .ToList();

            //Assert.Equal(21, result.Count);

            //Assert.True(result.All(x => x.IsValid));

            //Assert.Equal("Philipp", result[0].Result.FirstName);
            //Assert.Equal("Wagner", result[0].Result.LastName);

            //Assert.Equal(1986, result[0].Result.BirthDate.Year);
            //Assert.Equal(5, result[0].Result.BirthDate.Month);
            //Assert.Equal(12, result[0].Result.BirthDate.Day);

            //Assert.Equal("Max", result[1].Result.FirstName);
            //Assert.Equal("Mustermann", result[1].Result.LastName);

            //Assert.Equal(2014, result[1].Result.BirthDate.Year);
            //Assert.Equal(1, result[1].Result.BirthDate.Month);
            //Assert.Equal(1, result[1].Result.BirthDate.Day);
        }
    }
}
