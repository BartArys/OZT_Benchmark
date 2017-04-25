using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;

namespace Benchmark.Benchmark
{
    public abstract class PolledBenchmark
    {
        public IList<float> Results { get; set; }
        public Database Database { get; set; }

        public static PolledBenchmark NewMemoryBenchMark(Database database, int intervalMs = 200) {
            if (intervalMs < 100) throw new ArgumentException();
            return new MemoryBenchmark(database.Processname, intervalMs) { Database = database };
        }

        public static PolledBenchmark NewCpuBenchMark(Database database, int intervalMs = 200)
        {
            if (intervalMs < 100) throw new ArgumentException();
            return new CpuBenchmark(database.Processname, intervalMs) { Database = database };
        }

        public abstract void Start();
        public abstract void Stop();
        public abstract void Reset();
    }

    class CpuBenchmark : PolledBenchmark
    {
        protected readonly PerformanceCounter _processPerformance;
        protected readonly Timer _timer;

        public CpuBenchmark(string processName, int interval)
        {
            _processPerformance = new PerformanceCounter("Process", "% Processor Time", processName, true);
            _timer = new Timer(interval);
            _timer.Elapsed += new ElapsedEventHandler(MeasureCpu);
            Results = new List<float>();
        }

        private void MeasureCpu(object sender, ElapsedEventArgs e)
        {
            Results.Add(_processPerformance.NextValue() / Environment.ProcessorCount);
        }

        public override void Reset()
        {
            Results.Clear();
            _timer.Stop();
        }

        public override void Start()
        {
            _processPerformance.NextValue(); //initialize
            _timer.Start();
        }

        public override void Stop()
        {
            _timer.Stop();
        }
    }

    class MemoryBenchmark : PolledBenchmark {

        protected readonly PerformanceCounter _performanceCounter;
        protected readonly Timer _timer;

        public MemoryBenchmark(string processName, int interval)
        {
            _performanceCounter = new PerformanceCounter("Process", "Working Set", processName);
            _timer = new Timer(interval);
            _timer.Elapsed += new ElapsedEventHandler(MeasureMemory);
            Results = new List<float>();
        }

        public override void Reset()
        {
            Results.Clear();
            _timer.Stop();
        }

        public override void Start()
        {
            _performanceCounter.NextValue(); //initialize
            _timer.Start();
        }

        public override void Stop()
        {
            _timer.Stop();
        }

        private void MeasureMemory(object sender, ElapsedEventArgs e)
        {
            Results.Add(_performanceCounter.NextValue());
        }
    }
}
