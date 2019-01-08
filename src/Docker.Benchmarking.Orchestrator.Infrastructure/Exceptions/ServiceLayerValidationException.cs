using System;
using System.Collections.Generic;
using System.Text;

namespace Docker.Benchmarking.Orchestrator.Core.Exceptions
{
    public class ServiceLayerValidationException : Exception
    {
        public ServiceLayerValidationException()
        {
        }

        public ServiceLayerValidationException(string message)
            : base(message)
        {
        }

        public ServiceLayerValidationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
