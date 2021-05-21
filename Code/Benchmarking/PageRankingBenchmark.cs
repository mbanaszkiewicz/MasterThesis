//using System.Collections;
//using System.Collections.Generic;
//using System.Collections.Immutable;
//using System.Linq;
//using Algorithms;
//using BenchmarkDotNet.Attributes;
//using BenchmarkDotNet.Jobs;
//using Paket;
//using Data = PageRanking.Data;
//namespace Benchmarking
//{
//  [SimpleJob(RuntimeMoniker.NetCoreApp31)]
//  [MemoryDiagnoser]
//  [RPlotExporter]
//  [CsvMeasurementsExporter]
//  public class PageRankingBenchmark
//  {

//    [GlobalSetup]
//    public void Setup()
//    {
//      var data = Data.downloadPackageData().ToImmutableList();
//      var mapDOR = 10; 
//      var reduceDOR = 5;

//    }

//    public static void Execute(int iterations)
//    {
//      for (int i = 0; i < iterations; i++)
//      {
        
//      }
//    }
//  }

//  public class PageRanking
//  {
//    public static float GetRanking(string packageName, ImmutableList<(string, float)> packages) =>
//      packages.Any(x => x.First() == packageName)
//        ? packages.First(x => x.First() == packageName).Second()
//        : 1.0f;

//    public static IEnumerable<(string, float score)> Map(NuGet.NuGetPackageCache package, ImmutableList<(string, float)> packages)
//    {
//      var score = GetRanking(package.PackageName, packages) / package.Dependencies.Length;
//      return package.Dependencies.Select(x => (Domain.PackageName(x.Item1.Item1).Item1, score));
//    }

//    public static (string packageName, float) Reduce(string packageName, IEnumerable<float> values)
//      => (packageName, values.Sum());
//  }
//}