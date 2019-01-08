using Ardalis.GuardClauses;
using Docker.Benchmarking.Orchestrator.Infrastrcture.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ardalis.GuardClauses
{
    public static class StringIsNotValidXMLGuard
    {
        public static void XmlIsNotValid(this IGuardClause guardClause, string xml, string parameterName)
        {
            if (string.IsNullOrEmpty(xml))
                throw new ArgumentException("xml string is empty/default", parameterName);

            if (!xml.IsValidXml())
                throw new ArgumentException("string is not valid xml", parameterName);
        }
    }
}
