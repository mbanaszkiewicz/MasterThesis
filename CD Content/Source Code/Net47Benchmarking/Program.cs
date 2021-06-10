using BenchmarkDotNet.Running;

namespace Net47Benchmarking
{
  public class Program
  {
    public static void Main(string[] args)
    {
      BenchmarkRunner.Run(typeof(Program).Assembly);
    }
  }
}