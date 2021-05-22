using System;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Linq;
using DataSet = System.Collections.Immutable.ImmutableList<System.Collections.Immutable.ImmutableList<double>>;

namespace Algorithms.KMeans
{
  public class PLINQWithPartitionerKMeans
  {
    private int RowLength { get; }
    private DataSet Source { get; }

    public PLINQWithPartitionerKMeans(DataSet source)
    {
      Source = source;
      RowLength = source[0].Count;
    }

    public DataSet ComputeCentroids(DataSet initialCentroids)
      => KMeans.ComputeCentroids(UpdateCentroids(), initialCentroids, RowLength);

    private Func<DataSet, DataSet> UpdateCentroids()
      => centroids =>
      {
        var partitioner = Partitioner.Create(Source, true);

        return partitioner.AsParallel()
          .WithExecutionMode(ParallelExecutionMode.ForceParallelism)
          .GroupBy(u => KMeans.GetNearestCentroid(centroids, u))
          .Select(CalculateCenter)
          .ToImmutableList();
      };

    private ImmutableList<double> CalculateCenter(IGrouping<ImmutableList<double>, ImmutableList<double>> points)
      => points
        .Aggregate(new double[RowLength], (acc, item) => acc.Zip(item, (a, b) => a + b).ToArray())
        .Select(items => items / points.Count())
        .ToImmutableList();
  }
}