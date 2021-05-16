using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace Algorithms.QuickSort.Functional
{
  public sealed class ParallelFunctionalQuickSort : ISorter
  {
    public Task<ImmutableList<int>> Sort(IEnumerable<int> source)
    {
      var sourceList = source.ToImmutableList();

      return Sort(sourceList);
    }
    private async Task<ImmutableList<int>> Sort(ImmutableList<int> source)
    {
      if (source.None()) return source;

      var first = source.First();
      
      var (left, right) = source
        .Skip(1)
        .Partition(x => x <= first);
      var leftSorted = await Task.Run(() => Sort(left));
      var rightSorted = await Task.Run(() => Sort(right));

      return leftSorted
        .Add(first)
        .AddRange(rightSorted);
    }
  }
}