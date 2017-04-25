using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Benchmark.Benchmark
{
    public class DbBenchMark
    {
        public readonly Settings Settings;

        public DbBenchMark()
        {
            Settings = new Settings();
        }

        public void Start() {
            Dictionary<Database, IList<Query>> dbmsQueries = new Dictionary<Database, IList<Query>>();
            foreach (Database database in Settings.Databases) {
                dbmsQueries.Add(database, new List<Query>());
            }

            foreach (Query q in Settings.Queries) {
                foreach (Database d in q.Databases) {
                    dbmsQueries[d].Add(q);
                }
            }

            foreach (var item in dbmsQueries)
            {
                BenchMarkTool benchmarktool = new BenchMarkTool(item.Key) { ExecutionFrequency = Settings.frequency };
                foreach (var query in item.Value)
                {
                    BenchmarkResult result = benchmarktool.Execute(new SqlCommand(query.QueryString));
                    new ConsoleLogger().Handle(result, query);
                    new CsvLogger().Handle(result, query);
                }
            }

        }
    }
}
