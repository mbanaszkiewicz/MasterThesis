using System;
using System.Collections.Concurrent;
using System.Linq;

namespace DataParallelism.Part2.CSharp
{
  public interface ICentroidsStrategy
  {
    double[][] UpdateCentroids(double[][] centroids);
  }
  public class PLINQWithPartitionerKMeans : ICentroidsStrategy
  {
    private int N { get; }
    private double[][] Source { get; }

    public PLINQWithPartitionerKMeans(double[][] source)
    {
      Source = source;
      N = source[0].Length;
    }

    public double[][] UpdateCentroids(double[][] centroids)
    {
      var partitioner = Partitioner.Create(Source, true); 

      var result = partitioner.AsParallel() 
        .WithExecutionMode(ParallelExecutionMode.ForceParallelism)
        .GroupBy(u => KMeans.GetNearestCentroid(centroids, u))
        .Select(points =>
          points
            .Aggregate(new double[N], //#D
              (acc, item) => acc.Zip(item, (a, b) => a + b).ToArray()) //#E
            .Select(items => items / points.Count())
            .ToArray())
        .ToArray();

      Array.Sort(result, (a, b) => {
        for (var i = 0; i < N; i++)
          if (a[i] != b[i])
            return a[i].CompareTo(b[i]);
        return 0;
      });
      return result;
    }
  }
}