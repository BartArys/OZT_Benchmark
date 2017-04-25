using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmark.Benchmark
{
    public class Query
    {
        public string QueryString { get; set; }
        public IEnumerable<Database> Databases;
        public string Identifier;
    }
}
