using System.Drawing;
using Algorithms.Mandelbrot;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;


namespace Benchmarking
{
  [SimpleJob(RuntimeMoniker.NetCoreApp31)]
  [MemoryDiagnoser]
  [RPlotExporter]
  [CsvMeasurementsExporter]
  public class MandelbrotBenchmark
  {
    private const float Width = 5f;
    private const float Height = 5f;
    private const int Rows = 4000;
    private const int Columns = 4000;
    private static readonly ComplexNumber Center = new ComplexNumber(-1.5f, 0.0f);
    private static readonly ComplexNumberStruct CenterStruct = new ComplexNumberStruct(-0.75f, 0.0f);

    private readonly SequentialMandelbrot _sequentialMandelbrot = new SequentialMandelbrot(Width, Height, Rows, Columns, Center);
    private readonly ParallelMandelbrot _parallelMandelbrot = new ParallelMandelbrot(Width, Height, Rows, Columns, Center);
    private readonly DoubleParallelMandelbrot _doubleParallelMandelbrot = new DoubleParallelMandelbrot(Width, Height, Rows, Columns, Center);
    private readonly ParallelStructMandelbrot _parallelStructMandelbrot = new ParallelStructMandelbrot(Width, Height, Rows, Columns, CenterStruct);

    [Benchmark(Baseline = true)]
    public Bitmap SequentialMandelbrot()
      => _sequentialMandelbrot.Draw();
    [Benchmark]
    public Bitmap ParallelMandelbrot()
      => _parallelMandelbrot.Draw();
    [Benchmark]
    public Bitmap DoubleParallelMandelbrot()
      => _doubleParallelMandelbrot.Draw();
    [Benchmark]
    public Bitmap ParallelStructMandelbrot()
      => _parallelStructMandelbrot.Draw();
  }
}