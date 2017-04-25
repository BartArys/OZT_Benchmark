using System.Collections.Generic;

namespace Benchmark.Benchmark
{
    public class BenchmarkResult
    {
        public int CoreUsage { get; set; }
        public string DataBaseName { get; set; }
        public IEnumerable<QueryBenchmarkResult> Results { get; set; }
    }
}
