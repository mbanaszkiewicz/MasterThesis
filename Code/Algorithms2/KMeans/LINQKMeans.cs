﻿using System;
using System.Linq;

namespace DataParallelism.Part2.CSharp
{
  public class LINQKMeans : ICentroidsStrategy
  {
    private int N { get; }
    private double[][] Source { get; }

    public LINQKMeans(double[][] source)
    {
      Source = source;
      N = source[0].Length;
    }

    public double[][] UpdateCentroids(double[][] centroids)
    {
      var result =
        Source
          .GroupBy(u => KMeans.GetNearestCentroid(centroids, u))
          .Select(points =>
            points
              .Aggregate(new double[N], //#D
                (acc, item) => acc.Zip(item, (a, b) => a + b).ToArray())
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