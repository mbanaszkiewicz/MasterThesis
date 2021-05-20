using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace Algorithms.QuickSort.Functional
{
  public class OptimizedParallelFunctionalQuickSort : ISorter
  {
    private readonly int _depth;
    public OptimizedParallelFunctionalQuickSort(int depth)
    {
      _depth = depth;
    }
    public Task<ImmutableList<int>> Sort(IEnumerable<int> source)
    {
      return Sort(source.ToImmutableList(), _depth);
    }

    private static Task<ImmutableList<int>> Sort(ImmutableList<int> source, int depth)
    {
      if (source.None()) return Task.FromResult(source);

      var first = source.First();
      var (left, right) = source
        .Skip(1)
        .Partition(x => x <= first);
      var leftSorted = ParallelizeUnlessDepthIsReached(x => Sort(left, x), depth);
      var rightSorted = ParallelizeUnlessDepthIsReached(x => Sort(right, x), depth);

      return Task.FromResult(leftSorted.Result
        .Add(first)
        .AddRange(rightSorted.Result));
    }

    private static Task<T> ParallelizeUnlessDepthIsReached<T>(Func<int, Task<T>> func, int depth)
    {
      return depth > 0 ? Task.Run(() => func(depth - 1)) : func(0);
    }
  }
}