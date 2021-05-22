using System;
using System.Collections.Immutable;
using System.Globalization;
using System.IO;
using System.Linq;
using DataSet = System.Collections.Immutable.ImmutableList<System.Collections.Immutable.ImmutableList<double>>;

namespace Algorithms.KMeans
{
  public static class KMeans
  {
    public static DataSet LoadDataSet()
    {
      var lines = File.ReadAllLines("KMeans\\winequality-red.csv").Skip(1).Select(a => a.Split(';'));

      var csv = from line in lines
        select (from piece in line
          select double.Parse(piece));

      return csv.Select(x => x.ToImmutableList()).ToImmutableList();
    }

    public static DataSet GetRandomCentroids(DataSet dataSet, int seed, int n)
    {
      var random = new Random(seed);

      return Enumerable
        .Range(0, n)
        .Select(x => dataSet[random.Next(dataSet.Count)].ToImmutableList())
        .ToImmutableList();
    }

    public static DataSet ComputeCentroids(Func<DataSet, DataSet> updateCentroids, DataSet centroids,
      int rowLength)
    {
      for (var i = 0; i <= 1000; i++)
      {
        var (newCentroids, error) = GetNewCentroidsAndError(updateCentroids, rowLength, centroids);

        if (error < 1e-9)
        {
          Console.WriteLine("Iteration" + i);
          return newCentroids;
        }
          
        
        centroids = newCentroids;
      }
      Console.WriteLine("Iteration" + 1000);
      return centroids;
    }

    private static (DataSet, double) GetNewCentroidsAndError(Func<DataSet, DataSet> updateCentroids, int rowLength, DataSet centroids)
    {
      var newCentroids = updateCentroids(centroids).SortCentroids(rowLength);
      var error = double.MaxValue;

      if (centroids.Count == newCentroids.Count)
        error = centroids.Select((t, j) => DistanceTo(t, newCentroids[j])).Sum();

      return (newCentroids, error);
    }


    private static DataSet SortCentroids(this DataSet result, int RowLength)
      => result.Sort((a, b) =>
      {
        for (var i = 0; i < RowLength; i++)
          if (a[i] != b[i])
            return a[i].CompareTo(b[i]);
        return 0;
      });


    public static ImmutableList<double> GetNearestCentroid(DataSet centroids, ImmutableList<double> center) 
      => centroids.Aggregate((centroid1, centroid2) => 
        centroid2.DistanceTo(center) < centroid1.DistanceTo(center)
          ? centroid2
          : centroid1);

    private static double DistanceTo(this ImmutableList<double> @from, ImmutableList<double> to)
    {
      var results = 0.0;
      for (var i = 0; i < @from.Count; i++)
        results += Math.Pow(@from[i] - to[i], 2.0);
      return results;
    }
  }
}