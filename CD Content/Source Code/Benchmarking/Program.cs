using BenchmarkDotNet.Running;

namespace Benchmarking
{
  public class Program
  {
    public static void Main(string[] args)
    {
      //var bench = new NugetPackageRankingBenchmark();
      //bench.Setup();
      //bench.SequentialPageRanking();

      BenchmarkRunner.Run(typeof(Program).Assembly);
    }
  }
}