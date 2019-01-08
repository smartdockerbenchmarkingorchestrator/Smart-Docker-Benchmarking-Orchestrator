using System;
using Docker.Benchmarking.Orchestrator.Core.Entities;
using TinyCsvParser.Mapping;
using TinyCsvParser.TypeConverter;

namespace Docker.Benchmarking.Orchestrator.Infrastrcture.Mappings
{
    public class CsvApacheJmeterResultMapping : CsvMapping<BenchmarkTestItem>
    {
        public CsvApacheJmeterResultMapping()
       : base()
        {
            MapProperty(0, x => x.UnixTimestamp);
            MapProperty(1, x => x.Elapsed);
            MapProperty(2, x => x.Label);
            MapProperty(3, x => x.ResponseCode);
            MapProperty(4, x => x.ResponseMessage);
            MapProperty(5, x => x.ThreadName);
            MapProperty(6, x => x.DataType);
            MapProperty(7, x => x.Success, new BoolConverter("TRUE", "FALSE", StringComparison.InvariantCultureIgnoreCase));
            MapProperty(8, x => x.FailureMessage);
            MapProperty(9, x => x.Bytes);
            MapProperty(10, x => x.SentBytes);
            MapProperty(11, x => x.GroupThreads);
            MapProperty(12, x => x.AllThreads);
            MapProperty(13, x => x.Latency);
            MapProperty(14, x => x.IdleTime);
            MapProperty(15, x => x.Connect);
        }
    }
}
