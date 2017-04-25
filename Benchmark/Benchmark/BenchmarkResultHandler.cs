namespace Benchmark.Benchmark
{
    interface IBenchmarkResultHandler
    {
        void Handle(BenchmarkResult result, Query query);
    }
}
