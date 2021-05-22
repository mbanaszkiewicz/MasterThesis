using System.IO;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Algorithms.KMeans;
namespace Benchmarking
{
  [SimpleJob(RuntimeMoniker.NetCoreApp31)]
  [MemoryDiagnoser]
  [RPlotExporter]
  [CsvMeasurementsExporter]
  public class KMeansBenchmark
  {
    private static double[][] _data;
    private static double[][] _initialCentroids;
    private LINQKMeans _linqKMeans;
    private PLINQKMeans _plinqKMeans;
    private PLINQWithPartitionerKMeans _plinqWithPartitionerKMeans;

    [GlobalSetup]
    public void Setup()
    {
      _data = KMeans.LoadDataSet();

      _linqKMeans = new LINQKMeans(_data);
      _plinqKMeans = new PLINQKMeans(_data);
      _plinqWithPartitionerKMeans = new PLINQWithPartitionerKMeans(_data);
      _initialCentroids = KMeans.GetRandomCentroid(_data, 42, 11).ToArray();
    }

    [Benchmark(Baseline = true)]
    public double[][] LINQKMeans() => KMeans.ComputeCentroids(_linqKMeans, _initialCentroids);

    [Benchmark]
    public double[][] PLINQKMeans() => KMeans.ComputeCentroids(_plinqKMeans, _initialCentroids);

    [Benchmark]
    public double[][] PLINQWithPartitionerKMeans() => KMeans.ComputeCentroids(_plinqWithPartitionerKMeans, _initialCentroids);
  }
}