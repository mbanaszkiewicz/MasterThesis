using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace Algorithms.QuickSort.Imperative
{
  public class ParallelImperativeQuickSort : ISorter
  {
    public async Task<ImmutableList<int>> Sort(IEnumerable<int> source)
    {
      var arr = source.ToArray();
      await Sort(arr, 0, arr.Length - 1);

      return arr.ToImmutableList();
    }

    private static async Task Sort(int[] source, int left, int right)
    {
      if (left >= right) return;

      var pivot = Partition(source, left, right);

      await Task.Run(() => Sort(source, left, pivot));
      await Task.Run(() => Sort(source, pivot + 1, right));
    }

    private static int Partition(int[] source, int left, int right)
    {
      var pivot = (right + left) / 2;
      var pivotInt = source[pivot];

      Swap(ref source[right - 1], ref source[pivot]);
      var store = left;
      for (var i = left; i < right - 1; ++i)
      {
        if (source[i].CompareTo(pivotInt) >= 0) continue;
        Swap(ref source[i], ref source[store]);
        ++store;
      }

      Swap(ref source[right - 1], ref source[store]);
      return store;
    }

    private static void Swap(ref int a, ref int b)
    {
      var temp = a;
      a = b;
      b = temp;
    }
  }
}