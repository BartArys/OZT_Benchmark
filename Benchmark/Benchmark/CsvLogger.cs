using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Benchmark.Benchmark
{
    class CsvLogger : IBenchmarkResultHandler
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Handle(BenchmarkResult result, Query query)
        {
            string fileName = $"{result.DataBaseName}_{query.Identifier}.csv";
            string folder = @"c:\results";
            string path = Path.Combine(folder, fileName);
            Directory.CreateDirectory(folder);
            FileStream stream = File.Create(path);

            StreamWriter writer = new StreamWriter(stream);
            string header = $"CpuUsage,MemoryUsage,ExecutionTime";
            writer.WriteLine(header);
            writer.Flush();

            foreach (QueryBenchmarkResult qr in result.Results) {
                var cpuUsage = qr.CpuUsages.Any(usage => Math.Abs(usage) > 0f) ? qr.CpuUsages.Where(usage => Math.Abs(usage) > 0f).Average() : 0;
                var memoryUsage = qr.MemoryUsages.Any(usage => Math.Abs(usage) > 0f) ? qr.MemoryUsages.Where(usage => Math.Abs(usage) > 0f).Average() : 0;
                string data = $"{cpuUsage},{memoryUsage/1024/1024},{qr.ExecutionTime}";
                //Console.WriteLine(data);
                writer.WriteLine(data);
                writer.Flush();
            }
            writer.Close();
        }
    }
}
