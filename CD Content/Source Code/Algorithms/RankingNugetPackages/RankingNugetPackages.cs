using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Paket;

namespace Algorithms.RankingNugetPackages
{
  public class RankingNugetPackages
  {
    public static ImmutableList<(string packageName, float score)> Run(
      Func<IList<NuGet.NuGetPackageCache>, Func<NuGet.NuGetPackageCache, IEnumerable<(string, float)>>,
        Func<string, IEnumerable<float>, (string, float)>, IEnumerable<(string, float)>> mapReduce,
      ImmutableList<NuGet.NuGetPackageCache> data,
      int iterations)
    {
      var rankings = Enumerable.Empty<(string, float)>().ToImmutableList();

      for (var i = 0; i < iterations; i++)
      {
        Console.WriteLine("Iteration" + i);
        rankings =
          mapReduce(data,
              x => Map(x, rankings),
              Reduce)
            .ToImmutableList();
      }

      return rankings;
    }

    private static IEnumerable<(string, float score)> Map(NuGet.NuGetPackageCache package,
      ImmutableList<(string, float)> packages)
      => package.Dependencies.Select(x => (Domain.PackageName(x.Item1.Item1).Item1, GetScore(package, packages)));

    private static float GetRanking(string packageName, ImmutableList<(string, float)> packages)
      => packages.Any(x => x.First() == packageName)
        ? packages.First(x => x.First() == packageName).Second()
        : 1.0f;

    private static float GetScore(NuGet.NuGetPackageCache package, ImmutableList<(string, float)> packages)
      => GetRanking(package.PackageName, packages) / package.Dependencies.Length;

    private static (string packageName, float) Reduce(string packageName, IEnumerable<float> values)
      => (packageName, values.Sum());
  }
}