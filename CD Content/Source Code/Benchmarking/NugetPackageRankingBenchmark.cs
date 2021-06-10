using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Algorithms;
using Algorithms.RankingNugetPackages;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Disassemblers;
using BenchmarkDotNet.Jobs;
using Paket;
using Data = PageRanking.Data;

namespace Benchmarking
{
  [SimpleJob(RuntimeMoniker.NetCoreApp31)]
  [MemoryDiagnoser]
  [RPlotExporter]
  [CsvMeasurementsExporter]
  [ThreadingDiagnoser]
  public class NugetPackageRankingBenchmark
  {
    private ImmutableList<NuGet.NuGetPackageCache> _data;
    private const int MapDegreeOfParallelism = 10;
    private const int ReduceDegreeOfParallelism = 10;
    private const int Iterations = 10;

    [GlobalSetup]
    public void Setup()
    {
      _data = Data.downloadPackageData().Take(10000).ToImmutableList();
    }

    [Benchmark]
    public ImmutableList<(string packageName, float score)> SequentialPageRanking() => RankingNugetPackages.Run(SequentialMapReduce.MapReduce, _data, Iterations);

    [Benchmark]
    public ImmutableList<(string packageName, float score)> ParallelPageRanking() => RankingNugetPackages.Run(ParallelMapReduce.MapReduce(MapDegreeOfParallelism, ReduceDegreeOfParallelism), _data, Iterations);

    [Benchmark]
    public ImmutableList<(string packageName, float score)> PartitionerParallelPageRanking() => RankingNugetPackages.Run(ParallelMapReduce.PartitionerMapReduce(MapDegreeOfParallelism, ReduceDegreeOfParallelism), _data, Iterations);
  }
}