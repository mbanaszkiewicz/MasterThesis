using System.Drawing.Imaging;
using BenchmarkDotNet.Running;

namespace Benchmarking
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var bench = new MandelbrotBenchmark();
      bench.ParallelMandelbrot().Save("MandelbrotVis.png", ImageFormat.Png);
      //BenchmarkRunner.Run(typeof(Program).Assembly);
    }
  }
}