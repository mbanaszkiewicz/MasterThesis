using System;
using System.Collections.Immutable;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Algorithms.KMeans;
using DataSet = System.Collections.Immutable.ImmutableList<System.Collections.Immutable.ImmutableList<double>>;

namespace Benchmarking
{
  [SimpleJob(RuntimeMoniker.NetCoreApp31)]
  [MemoryDiagnoser]
  [RPlotExporter]
  [CsvMeasurementsExporter]
  public class KMeansBenchmark
  {
    private DataSet _data;
    private static DataSet _initialCentroids;
    private LINQKMeans _linqKMeans;
    private PLINQKMeans _plinqKMeans;
    private PLINQWithPartitionerKMeans _plinqWithPartitionerKMeans;

    [GlobalSetup]
    public void Setup()
    {
      _data = KMeans.LoadDataSet();
      _initialCentroids = KMeans.GetRandomCentroids(_data, 1986, 11).ToImmutableList();
      _linqKMeans = new LINQKMeans(_data);
      _plinqKMeans = new PLINQKMeans(_data);
      _plinqWithPartitionerKMeans = new PLINQWithPartitionerKMeans(_data);
    }

    [Benchmark(Baseline = true)]
    public DataSet LINQKMeans() => _linqKMeans.ComputeCentroids(_initialCentroids);

    [Benchmark]
    public DataSet PLINQKMeans() => _plinqKMeans.ComputeCentroids(_initialCentroids);

    [Benchmark]
    public DataSet PLINQWithPartitionerKMeans() => _plinqWithPartitionerKMeans.ComputeCentroids(_initialCentroids);

  }
}