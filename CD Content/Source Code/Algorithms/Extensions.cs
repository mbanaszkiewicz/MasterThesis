using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Algorithms
{
  public static class Extensions
  {
    public static (ImmutableList<TSource> trueGroup, ImmutableList<TSource> falseGroup) Partition<TSource>(
      this IEnumerable<TSource> source,
      Func<TSource, bool> predicate)
    {
      var trueGroup = new List<TSource>();
      var falseGroup = new List<TSource>();

      foreach (var element in source)
      {
        if (predicate(element))
          trueGroup.Add(element);
        else
          falseGroup.Add(element);
      }

      return (trueGroup.ToImmutableList(), falseGroup.ToImmutableList());
    }

    public static bool None<T>(this IEnumerable<T> target)
      => !target.Any();
  }

  public static class Tuple
  {
    public static T1 First<T1, T2>(this (T1, T2) tuple)
      => tuple.Item1;
    public static T2 Second<T1, T2>(this (T1, T2) tuple)
      => tuple.Item2;
  }
}