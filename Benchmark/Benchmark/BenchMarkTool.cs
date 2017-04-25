using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using MySql.Data.MySqlClient;

namespace Benchmark.Benchmark
{
    public class BenchMarkTool
    {
        private readonly Database _database;
        private readonly PolledBenchmark _cpuBenchmark;
        private readonly PolledBenchmark _memoryBenchmark;

        public int ExecutionFrequency { get; set; } = 50;

        public BenchMarkTool(Database database)
        {
            _database = database;
            _memoryBenchmark = PolledBenchmark.NewMemoryBenchMark(database);
            _cpuBenchmark = PolledBenchmark.NewCpuBenchMark(database);
        }

        public BenchmarkResult Execute(SqlCommand commandSQL) {
            MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(_database.Connection); //verander dit naar SQL voor de SQL Server (blijkbaar is MS's interpretatie van 'SQL' enkel en alleen SQL Server)
            MySqlCommand command = new MySqlCommand(commandSQL.CommandText, connection); 
            Stopwatch stopwatch = new Stopwatch();

            IList<QueryBenchmarkResult> results = new List<QueryBenchmarkResult>();

            connection.Open();
            command.CommandTimeout = 0;
            var reader = command.ExecuteReaderAsync().Result;
            while (reader.NextResult())
            {
                reader.Read();
            }
            reader.Close();

            Console.WriteLine("starting query");
            for (int i = 0; i < ExecutionFrequency; i++) {
                stopwatch.Reset();
                _memoryBenchmark.Reset();
                _cpuBenchmark.Reset();
                Console.Write($"run {i}\t");
                _cpuBenchmark.Start();
                _memoryBenchmark.Start();
                stopwatch.Start();

                reader = command.ExecuteReaderAsync().Result;
                while (reader.NextResult())
                {
                    reader.Read();
                }
                reader.Close();

                _cpuBenchmark.Stop();
                _memoryBenchmark.Stop();
                stopwatch.Stop();

                Console.WriteLine($"{stopwatch.ElapsedMilliseconds}ms \t");
                
                results.Add(new QueryBenchmarkResult() {
                    ExecutionTime = stopwatch.ElapsedMilliseconds,
                    CpuUsages = _cpuBenchmark.Results,
                    MemoryUsages = _memoryBenchmark.Results
                });
            }

            connection.Close();
            return new BenchmarkResult() { Results = results, DataBaseName = _database.Identifier };
        }
    }
}
