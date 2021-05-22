using System;
using System.Collections.Immutable;
using System.Linq;
using DataSet = System.Collections.Immutable.ImmutableList<System.Collections.Immutable.ImmutableList<double>>;

namespace Algorithms.KMeans
{
  public class PLINQKMeans
  {
    private int RowLength { get; }
    private DataSet Source { get; }

    public PLINQKMeans(DataSet source)
    {
      Source = source;
      RowLength = source[0].Count;
    }

    public DataSet ComputeCentroids(DataSet initialCentroids)
      => KMeans.ComputeCentroids(UpdateCentroids(), initialCentroids, RowLength);

    private Func<DataSet, DataSet> UpdateCentroids()
      => centroids =>
        Source.AsParallel()
          .WithExecutionMode(ParallelExecutionMode.ForceParallelism)
          .GroupBy(u => KMeans.GetNearestCentroid(centroids, u))
          .Select(points =>
            points
              .Aggregate(new double[RowLength],
                (acc, item) => acc.Zip(item, (a, b) => a + b).ToArray())
              .Select(items => items / points.Count())
              .ToImmutableList())
          .ToImmutableList();
  }
}