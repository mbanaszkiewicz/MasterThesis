using System;
using System.Collections.Immutable;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace Algorithms.QuickSort.Imperative
{
  [SimpleJob(RuntimeMoniker.Net472, baseline: true)]
  [SimpleJob(RuntimeMoniker.NetCoreApp31)]
  [MemoryDiagnoser]
  [RPlotExporter]
  [CsvMeasurementsExporter]
  public class ImperativeQuickSortBenchmark
  {
    private readonly SequentialImperativeQuickSort _sequentialImperativeQuickSort = new SequentialImperativeQuickSort();
    private readonly ParallelImperativeQuickSort _parallelImperativeQuickSort = new ParallelImperativeQuickSort();
    private readonly OptimizedParallelImperativeQuickSort _optimizedParallelImperativeQuickSort =
      new OptimizedParallelImperativeQuickSort((int)Math.Log(Environment.ProcessorCount, 2) + 4);

    // ReSharper disable once MemberCanBePrivate.Global
    public int[] Data;

    // ReSharper disable once MemberCanBePrivate.Global
    [Params(10000, 1000000)] public int N;

    [GlobalSetup]
    public void Setup()
    {
      var random = new Random(42);

      Data = Enumerable
        .Range(0, N)
        .Select(_ => random.Next())
        .ToArray();
    }


   [Benchmark(Baseline=true)]
    public ImmutableList<int> SequentialImperativeQuickSort()
      => _sequentialImperativeQuickSort.Sort(Data).Result;

    [Benchmark]
    public ImmutableList<int> ParallelImperativeQuickSort()
      => _parallelImperativeQuickSort.Sort(Data).Result;

    [Benchmark]
    public ImmutableList<int> OptimizedParallelImperativeQuickSort()
      => _optimizedParallelImperativeQuickSort.Sort(Data).Result;
  }
}