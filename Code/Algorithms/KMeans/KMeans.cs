using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Algorithms.KMeans
{
  public static class KMeans
  {
    public static double[][] LoadDataSet()
    {
      var lines = File.ReadAllLines("KMeans\\winequality-red.csv").Skip(1).Select(a => a.Split(';'));
      var csv = from line in lines
        select (from piece in line
          select double.Parse(piece));

     return csv.Select(x => x.ToArray()).ToArray();
    }

    public static IEnumerable<double[]> GetRandomCentroid(double[][] dataset, int seed, int n)
    {
      var random = new Random(seed);

      return Enumerable
        .Range(0, n)
        .Select(x => dataset[random.Next(dataset.Length)]);
    }
    public static double[][] ComputeCentroids(ICentroidsStrategy strategy, double[][] initialCentroids)
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