using Ardalis.GuardClauses;
using Docker.Benchmarking.Orchestrator.Infrastrcture.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ardalis.GuardClauses
{
    public static class StringIsNotValidJsonGuard
    {
        public static void JsonIsNotValid(this IGuardClause guardClause, string json, string parameterName)
        {
            if (string.IsNullOrEmpty(json))
                throw new ArgumentException("json is empty/default", parameterName);

            if (!json.IsValidJson())
                throw new ArgumentException("string is not valid json", parameterName);
        }
    }
}
