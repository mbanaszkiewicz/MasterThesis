using System;
using System.Linq;

namespace DataParallelism.Part2.CSharp
{
  public static class KMeans
  {
    public static double[][] Run(ICentroidsStrategy strategy, double[][] initialCentroids)
    {
      var centroids = initialCentroids;

      for (int i = 0; i <= 1000; i++)
      {
        var newCentroids = strategy.UpdateCentroids(centroids);
        var error = double.MaxValue;
        if (centroids.Length == newCentroids.Length)
        {
          error = 0;
          for (var j = 0; j < centroids.Length; j++)
            error += Dist(centroids[j], newCentroids[j]);
        }
        if (error < 1e-8)
        {
          return newCentroids;
        }
        centroids = newCentroids;
      }
      return centroids;
    }

    public static double[] GetNearestCentroid(double[][] centroids, double[] center)
    {
      return centroids.Aggregate((centroid1, centroid2) => //#A
        Dist(center, centroid2) < Dist(center, centroid1)
          ? centroid2
          : centroid1);
    }

    private static double Dist(double[] u, double[] v)
    {
      double results = 0.0;
      for (var i = 0; i < u.Length; i++)
        results += Math.Pow(u[i] - v[i], 2.0);
      return results;
    }
  }
}