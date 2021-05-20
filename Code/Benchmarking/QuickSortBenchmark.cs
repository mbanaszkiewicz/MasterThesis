using System;
using System.Collections.Immutable;
using System.Linq;
using Algorithms.QuickSort.Functional;
using Algorithms.QuickSort.Imperative;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace Benchmarking
{
  [SimpleJob(RuntimeMoniker.Net472, baseline: true)]
  [SimpleJob(RuntimeMoniker.NetCoreApp31)]
  [MemoryDiagnoser]
  [RPlotExporter]
  [CsvMeasurementsExporter]
  public class QuickSortBenchmark
  {
    private readonly SequentialImperativeQuickSort _sequentialImperativeQuickSort = new SequentialImperativeQuickSort();
    private readonly ParallelImperativeQuickSort _parallelImperativeQuickSort = new ParallelImperativeQuickSort();
    private readonly OptimizedParallelImperativeQuickSort _optimizedParallelImperativeQuickSort =
      new OptimizedParallelImperativeQuickSort((int)Math.Log(Environment.ProcessorCount, 2) + 4);

    private readonly SequentialFunctionalQuickSort _sequentialFunctionalQuickSort = new SequentialFunctionalQuickSort();
    private readonly ParallelFunctionalQuickSort _parallelFunctionalQuickSort = new ParallelFunctionalQuickSort();
    private readonly OptimizedParallelFunctionalQuickSort _optimizedParallelFunctionalQuickSort =
      new OptimizedParallelFunctionalQuickSort((int)Math.Log(Environment.ProcessorCount, 2) + 4);

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


    [Benchmark()]
    public ImmutableList<int> SequentialFunctionalQuickSort()
       => _sequentialFunctionalQuickSort.Sort(Data).Result;

    [Benchmark]
    public ImmutableList<int> ParallelFunctionalQuickSort()
      => _parallelFunctionalQuickSort.Sort(Data).Result;

    [Benchmark]
    public ImmutableList<int> OptimizedParallelFunctionalQuickSort()
      => _optimizedParallelFunctionalQuickSort.Sort(Data).Result;

    [Benchmark()]
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