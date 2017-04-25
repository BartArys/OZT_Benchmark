using Benchmark.Benchmark;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            DbBenchMark benchmark = new DbBenchMark();
            Console.WriteLine("loaded xml");
            Console.WriteLine($"executing {benchmark.Settings.Queries.Count} querries {benchmark.Settings.frequency} times ...");
            benchmark.Start();
            Console.ReadKey();
        }
    }
}
