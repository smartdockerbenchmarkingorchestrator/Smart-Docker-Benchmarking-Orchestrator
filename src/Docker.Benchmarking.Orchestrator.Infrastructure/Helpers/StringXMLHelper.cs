using System.Xml.Linq;

namespace Docker.Benchmarking.Orchestrator.Infrastrcture.Helpers
{
    public static class StringXMLHelper
    {
        public static bool IsValidXml(this string input)
        {
            try
            {
                XDocument.Parse(input);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
