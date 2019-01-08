using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ardalis.GuardClauses
{
    public static class GuidNullOrDefaultGuard
    {
        public static void GuidNullOrDefault(this IGuardClause guardClause, Guid input, string parameterName)
        {
            if (input == Guid.Empty)
                throw new ArgumentException("Guid is empty/default", parameterName);
        }

        public static void GuidNullOrDefault(this IGuardClause guardClause, Guid? input, string parameterName)
        {
            if(!input.HasValue)
                throw new ArgumentException("Guid is null", parameterName);

            if (input.Value == Guid.Empty)
                throw new ArgumentException("Guid is empty/default", parameterName);
        }
    }
}
