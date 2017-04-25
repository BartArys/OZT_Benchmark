using System;
using System.Linq;

namespace Benchmark.Benchmark
{
    class ConsoleLogger : IBenchmarkResultHandler
    {
        public void Handle(BenchmarkResult result, Query query)
        {
            Console.WriteLine($"{query.Identifier} completed");
            Console.WriteLine($"average memory usage :{result.Results.Average(res => res.MemoryUsages.Any()? res.MemoryUsages.Average() : 0)}");
            Console.WriteLine($"average cpu usage    :{result.Results.Average(res => res.CpuUsages.Any() ? res.CpuUsages.Average() : 0)}");
            Console.WriteLine($"core usage    :{result.CoreUsage}");
            Console.WriteLine($"average time :{result.Results.Average(res => res.ExecutionTime)}");
            Console.WriteLine("--- --- --- --- --- ---");
            Console.WriteLine();
        }
    }
}
