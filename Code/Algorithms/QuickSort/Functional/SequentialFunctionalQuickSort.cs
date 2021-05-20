using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace Algorithms.QuickSort.Functional
{
  public class SequentialFunctionalQuickSort :ISorter
  {
    public Task<ImmutableList<int>> Sort(IEnumerable<int> source)
    {
      return Task.FromResult(Sort(source.ToImmutableList()));
    }

    private static ImmutableList<int> Sort(ImmutableList<int> source)
    {
      if (!source.Any()) return source;

      var first = source.First();
      var (left, right) = source
        .Skip(1)
        .Partition(x => x <= first);
      var leftSorted = Sort(left);
      var rightSorted = Sort(right);

      return leftSorted
        .Add(first)
        .AddRange(rightSorted);
    }
  }
}