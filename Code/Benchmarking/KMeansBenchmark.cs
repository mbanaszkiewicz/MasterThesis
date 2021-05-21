//using System.IO;
//using System.Linq;
//using BenchmarkDotNet.Attributes;
//using BenchmarkDotNet.Jobs;
//using Algorithms.KMeans;
//namespace Benchmarking
//{
//  [SimpleJob(RuntimeMoniker.NetCoreApp31)]
//  [MemoryDiagnoser]
//  [RPlotExporter]
//  [CsvMeasurementsExporter]
//  public class KMeansBenchmark
//  {
//    private static double[][] _data;
//    private static double[][] _initialCentroids;
//    private  LINQKMeans _linqKMeans;
//    private  PLINQKMeans _plinqKMeans;
//    private  PLINQWithPartitionerKMeans _plinqWithPartitionerKMeans;

//    [GlobalSetup]
//    public void Setup()
//    {
//      var lines = File.ReadAllLines("KMeans\\winequality-red.csv").Skip(1).Select(a => a.Split(';'));
//      var csv = from line in lines
//        select (from piece in line
//          select double.Parse(piece));

//      _data = csv.Select(x => x.ToArray()).ToArray();
 
//      _linqKMeans = new LINQKMeans(_data);
//      _plinqKMeans = new PLINQKMeans(_data);
//      _plinqWithPartitionerKMeans = new PLINQWithPartitionerKMeans(_data);
//      _initialCentroids = new double[][]
//      {
//        new double[]{7.5, 0.4, 0.2, 2.5, 0.05, 20, 50, 0.9995, 3.2, 0.5, 9.8, 5.0},
//        new double[]{7, 0.42, 0.15, 3.1, 0.45, 20, 30, 0.99955,2.5, 0.55, 9.5, 5.2},
//        new double[]{7.7, 0.3, 0.28, 2, 0.06, 12, 70, 0.9992, 3.1, 0.4, 9.82342, 4.5},
//        new double[]{7.7, 0.42, 0.28, 3.1, 0.06, 15, 70, 0.99955, 3.1, 0.55, 9.82342, 5.2},
//        new double[]{7.9, 0.45, 0.32, 3.5, 0.04, 25, 100, 0.9996, 3.25, 0.6, 10, 5.4},
//        new double[]{8.0, 0.5, 0.35, 4.5, 0.05, 30, 150, 0.99965, 3.3, 0.7, 10.5, 5.5},
//        new double[]{8.2, 0.55, 0.37, 5, 0.06, 35, 200, 0.9997, 3.35, 0.75, 10.7, 5.8},
//        new double[]{8.2, 0.4, 1, 5, 0.08, 35, 400, 0.9990, 3.39, 0.75, 10.5, 5.8},
//        new double[]{8.4, 0.57, 0.38, 5.2, 0.08, 37, 250, 0.9996, 3.37, 0.77, 10.8, 5.9},
//        new double[]{9, 0.57, 0.5, 6, 0.1, 50, 250, 0.9996, 3.5, 0.77, 12, 5.9},
//        new double[]{8.4, 0.8, 0.38, 6.0, 0.12, 45, 300, 1, 3.37, 0.85, 10.8, 6.5},
//        new double[]{8.5, 0.6, 0.4, 5.5, 0.1, 40, 300, 0.9999, 3.4, 0.8, 11, 6.0}
//      };

//    }

//    [Benchmark()]
//    public double[][] LINQKMeans() => KMeans.Run(_linqKMeans, _initialCentroids);

//    [Benchmark]
//    public double[][] PLINQKMeans() => KMeans.Run(_plinqKMeans, _initialCentroids);

//    [Benchmark]
//    public double[][] PLINQWithPartitionerKMeans() => KMeans.Run(_plinqWithPartitionerKMeans, _initialCentroids);
//  }
//}