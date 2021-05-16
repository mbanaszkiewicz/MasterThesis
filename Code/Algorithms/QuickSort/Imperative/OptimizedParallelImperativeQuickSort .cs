using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace Algorithms.QuickSort.Imperative
{
  public class OptimizedParallelImperativeQuickSort : ISorter
  {
    private int _depth;
    public OptimizedParallelImperativeQuickSort(int depth)
    {
      _depth = depth;
    }
    public Task<ImmutableList<int>> Sort(IEnumerable<int> source)
    {
      var arr = source.ToArray();
       Sort(arr, 0, arr.Length - 1, _depth);

      return Task.FromResult(arr.ToImmutableList());
    }

    private static  void Sort(int[] arr, int left, int right, int depth)
    {
      if (left >= right) return;

      var pivot = Partition(arr, left, right);
      if (depth > 0)
      {
        if (pivot > 1)
        {
           Task.Run(() => Sort(arr, left, pivot - 1, depth -1)).Wait();
        }

        if (pivot + 1 < right)
        {
          Task.Run(() => Sort(arr, pivot + 1, right, depth - 1)).Wait();
        }
      }

      else
      {
        if (pivot > 1)
        {
          Sort(arr, left, pivot - 1, 0);
        }

        if (pivot + 1 < right)
        {
          Sort(arr, pivot + 1, right, 0);
        }

      }

    }

    private static int Partition(int[] arr, int left, int right)
    {
      int pivot = arr[left];
      while (true)
      {
        while (arr[left] < pivot)
        {
          left++;
        }

        while (arr[right] > pivot)
        {
          right--;
        }

        if (left < right)
        {
          if (arr[left] == arr[right]) return right;

          int temp = arr[left];
          arr[left] = arr[right];
          arr[right] = temp;
        }
        else
        {
          return right;
        }
      }
    }
  }
}