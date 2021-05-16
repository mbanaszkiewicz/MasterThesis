using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Algorithms;
using FluentAssertions;

namespace AlgorithmsTests
{
  [TestClass]
  public class QuickSortTests
  {
    private ImmutableList<ISorter> _algorithms;

    [TestInitialize]
    public void TestInit()
    {
      _algorithms = new List<ISorter>()
      {
        new SequentialImperativeQuickSort(),
        new SequentialFunctionalQuickSort(),
        new ParallelImperativeQuickSort(),
        new ParallelFunctionalQuickSort(),
        new OptimizedParallelImperativeQuickSort(4),
        new OptimizedParallelFunctionalQuickSort(4)
      }.ToImmutableList();
    }

    [TestMethod]
    public void EmptyInput_EmptyOutput()
    {
      var data = Enumerable.Empty<int>();
      var outputs = _algorithms.Select(x => x.Sort(data)).ToList();
      outputs.ForEach(x => x.Result.Should().BeEquivalentTo(data));
    }

    [TestMethod]
    public void RandomInput_SortedOutput()
    {
      var data = new int[]
      {
        1, 6, 1231, -234523, 22, 55, 41, -2, 6435, 9090, 5454, 123123, -2, -978697, -534534, 123131, 6546747
      };

      var expected = ((int[]) data.Clone()).ToList();
      expected.Sort((l, l1) => l.CompareTo(l1));
      var outputs = _algorithms.Select(x => x.Sort(data).Result).ToList();
      outputs.ForEach(x => x.Should().BeEquivalentTo(expected, cfg => cfg.WithStrictOrdering()));
    }
  }
}