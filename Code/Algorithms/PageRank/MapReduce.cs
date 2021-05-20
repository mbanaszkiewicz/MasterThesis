using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.PageRank
{
  public static class SequentialMapReduce
  {
    public static IEnumerable<TReduced> MapReduce<TIn, TOutKey, TOutValue, TReduced>(
      this IEnumerable<TIn> source,
      Func<TIn, IEnumerable<(TOutKey, TOutValue)>> map,
      Func<TOutKey, IEnumerable<TOutValue>, TReduced> reduce)
    {
      return source
        .SelectMany(map)
        .GroupBy(Tuple.First)
        .Select(x => reduce(x.Key, x.Select(Tuple.Second)));
    }
  }

  public static class ParallelMapReduce
  {
    public static IEnumerable<IGrouping<TOutKey, (TOutKey, TOutValue)>> Map<TIn, TOutKey, TOutValue>(
      this IEnumerable<TIn> source,
      Func<TIn, IEnumerable<(TOutKey, TOutValue)>> map,
      int degreeOfParallelism)
      => source
        .AsParallel()
        .WithExecutionMode(ParallelExecutionMode.ForceParallelism)
        .WithDegreeOfParallelism(degreeOfParallelism)
        .SelectMany(map)
        .GroupBy(Tuple.First)
        .ToList();

    public static IEnumerable<TReduced> Reduce<TKey, TValue, TReduced>
    (this IEnumerable<IGrouping<TKey, (TKey, TValue)>> source,
      Func<TKey, IEnumerable<TValue>, TReduced> reduce, int degreeOfParallelism)
      => source
        .AsParallel()
        .WithExecutionMode(ParallelExecutionMode.ForceParallelism)
        .WithDegreeOfParallelism(degreeOfParallelism)
        .Select(x => reduce(x.Key, x.Select(Tuple.Second)))
        .ToList();

    public static IEnumerable<TReduced> MapReduce<TIn, TOutKey, TOutValue, TReduced>(
      this IEnumerable<TIn> source,
      Func<TIn, IEnumerable<(TOutKey, TOutValue)>> map,
      Func<TOutKey, IEnumerable<TOutValue>, TReduced> reduce)
      => source
        .Map(map, 10)
        .Reduce(reduce, 5);
  }
}