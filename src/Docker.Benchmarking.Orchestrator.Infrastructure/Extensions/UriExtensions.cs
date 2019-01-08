using System;
using System.Collections.Generic;
using System.Text;

namespace Docker.Benchmarking.Orchestrator.Infrastrcture.Extensions
{
    //https://stackoverflow.com/questions/6137718/changing-the-port-of-the-system-uri
    public static class UriExtensions
    {
        public static Uri SetPort(this Uri uri, int newPort)
        {
            var builder = new UriBuilder(uri);
            builder.Port = newPort;
            return builder.Uri;
        }
    }
}
