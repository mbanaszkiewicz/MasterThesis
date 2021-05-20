using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace Algorithms
{
  public interface ISorter
  {
    Task<ImmutableList<int>> Sort(IEnumerable<int> source);
  }
}