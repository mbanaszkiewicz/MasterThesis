using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace Benchmarking
{
  [SimpleJob(RuntimeMoniker.NetCoreApp31)]
  [MemoryDiagnoser]
  [RPlotExporter]
  [CsvMeasurementsExporter]
  public class PageRankingBenchmark
  {


    [GlobalSetup]
    public void Setup()
    {
      var data = PageRanking.Data.downloadPackageData();
    }
  }
}