using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Paket;

namespace Algorithms.RankingNugetPackages
{
  public static class ParallelMapReduce
  {
    public static Func<IEnumerable<NuGet.NuGetPackageCache>, Func<NuGet.NuGetPackageCache, IEnumerable<(string, float)>>, Func<string, IEnumerable<float>, (string, float)>, IEnumerable<(string, float)>> MapReduce(int mDOR, int rDOR) =>
      (x, y, z) => x.MapReduce(y, z, mDOR, rDOR);

    public static Func<IList<NuGet.NuGetPackageCache>, Func<NuGet.NuGetPackageCache, IEnumerable<(string, float)>>, Func<string, IEnumerable<float>, (string, float)>, IEnumerable<(string, float)>> PartitionerMapReduce(int mDOR, int rDOR) =>
      (x, y, z) => x.PartitionerMapReduce(y, z, mDOR, rDOR);

    private static IEnumerable<IGrouping<TOutKey, (TOutKey, TOutValue)>> Map<TIn, TOutKey, TOutValue>(
      this ParallelQuery<TIn> source,
      Func<TIn, IEnumerable<(TOutKey, TOutValue)>> map)
      => source
        .SelectMany(map)
        .GroupBy(Tuple.First)
        .ToList();
    private static IEnumerable<TReduced> Reduce<TKey, TValue, TReduced>
    (this ParallelQuery<IGrouping<TKey, (TKey, TValue)>> source,
      Func<TKey, IEnumerable<TValue>, TReduced> reduce)
      => source
        .Select(x => reduce(x.Key, x.Select(Tuple.Second)))
        .ToList();
    private static ParallelQuery<TSource> ToParallel<TSource>(this IEnumerable<TSource> source, int degreeOfParallelism)
      => source
        .AsParallel()
        .WithExecutionMode(ParallelExecutionMode.ForceParallelism)
        .WithDegreeOfParallelism(degreeOfParallelism);

    private static IEnumerable<TReduced> MapReduce<TIn, TOutKey, TOutValue, TReduced>(
      this IEnumerable<TIn> source,
      Func<TIn, IEnumerable<(TOutKey, TOutValue)>> map,
      Func<TOutKey, IEnumerable<TOutValue>, TReduced> reduce, int mDOR, int rDOR)
      => source
        .ToParallel(mDOR)
        .Map(map)
        .ToParallel(rDOR)
        .Reduce(reduce)
        .ToImmutableList();

    private static ParallelQuery<TSource> ToParallel<TSource>(this OrderablePartitioner<TSource> source, int degreeOfParallelism)
      => source
        .AsParallel()
        .WithExecutionMode(ParallelExecutionMode.ForceParallelism)
        .WithDegreeOfParallelism(degreeOfParallelism);

    private static IEnumerable<TReduced> PartitionerMapReduce<TIn, TOutKey, TOutValue, TReduced>(
      this IList<TIn> source,
      Func<TIn, IEnumerable<(TOutKey, TOutValue)>> map,
      Func<TOutKey, IEnumerable<TOutValue>, TReduced> reduce, int mDOR, int rDOR)
      => Partitioner
        .Create(source, true)
        .ToParallel(mDOR)
        .Map(map)
        .ToParallel(rDOR)
        .Reduce(reduce)
        .ToImmutableList();

  }
}