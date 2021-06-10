using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Algorithms.RankingNugetPackages
{
  public static class SequentialMapReduce
  {
    public static IEnumerable<TReduced> MapReduce<TIn, TOutKey, TOutValue, TReduced>(
      this IEnumerable<TIn> source,
      Func<TIn, IEnumerable<(TOutKey, TOutValue)>> map,
      Func<TOutKey, IEnumerable<TOutValue>, TReduced> reduce)
      => source
        .Map(map)
        .Reduce(reduce);

    private static IEnumerable<IGrouping<TOutKey, (TOutKey, TOutValue)>> Map<TIn, TOutKey, TOutValue>(
      this IEnumerable<TIn> source,
      Func<TIn, IEnumerable<(TOutKey, TOutValue)>> map)
      => source
        .SelectMany(map)
        .GroupBy(Tuple.First)
        .ToImmutableList();

    private static IEnumerable<TReduced> Reduce<TKey, TValue, TReduced>
    (this IEnumerable<IGrouping<TKey, (TKey, TValue)>> source,
      Func<TKey, IEnumerable<TValue>, TReduced> reduce)
      => source
        .Select(x => reduce(x.Key, x.Select(Tuple.Second)))
        .ToImmutableList();

  }
}