using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Docker.Benchmarking.Orchestrator.Tests.Unit.Web.Validators
{
    public static class ValidatorHelper
    {
        public static string ValidJsonString()
        {
            var dirName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var filePath = Path.Combine(dirName + @"\\TestFiles\\Json\\valid.json");

            string json = File.ReadAllText(filePath);

            if (string.IsNullOrEmpty(json)) throw new NullReferenceException();

            return json;
        }

        public static string InvalidJsonString()
        {
            var dirName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var filePath = Path.Combine(dirName + @"\\TestFiles\\Json\\invalid.json");

            string json = File.ReadAllText(filePath);

            if (string.IsNullOrEmpty(json)) throw new NullReferenceException();

            return json;
        }

        public static string ValidXmlString()
        {
            var dirName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var filePath = Path.Combine(dirName + @"\\TestFiles\\XML\\valid.xml");

            string xml = File.ReadAllText(filePath);

            if (string.IsNullOrEmpty(xml)) throw new NullReferenceException();

            return xml;
        }

        public static string InvalidXmlString()
        {
            var dirName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var filePath = Path.Combine(dirName + @"\\TestFiles\\XML\\invalid.xml");

            string xml = File.ReadAllText(filePath);

            if (string.IsNullOrEmpty(xml)) throw new NullReferenceException();

            return xml;
        }
    }
}
