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

    private static async Task Sort(int[] arr, int left, int right)
    {
      if (left >= right) return;

      var pivot = Partition(arr, left, right);

      if (pivot > 1)
      {
        await Task.Run(() => Sort(arr, left, pivot - 1));
      }

      if (pivot + 1 < right)
      {
        await Task.Run(() => Sort(arr, pivot + 1, right));
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

          lock (arr)
          {
            int temp = arr[left];
            arr[left] = arr[right];
            arr[right] = temp;
          }
        }
        else
        {
          return right;
        }
      }
    }
  }
}