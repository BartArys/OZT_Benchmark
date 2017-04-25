using System.Collections.Generic;

namespace Benchmark.Benchmark
{
    public class QueryBenchmarkResult
    {
        public IEnumerable<float> MemoryUsages { get; set; }
        public IEnumerable<float> CpuUsages { get; set; }
        public long ExecutionTime { get; set; }
    }
}
