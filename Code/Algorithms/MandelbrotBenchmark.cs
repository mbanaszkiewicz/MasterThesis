//using System.Drawing;
//using Algorithms.Mandelbrot;
//using BenchmarkDotNet.Attributes;
//using BenchmarkDotNet.Jobs;

//namespace Benchmarking
//{
//  [SimpleJob(RuntimeMoniker.Net472, baseline: true)]
//  [SimpleJob(RuntimeMoniker.NetCoreApp31)]
//  [MemoryDiagnoser]
//  [RPlotExporter]
//  [CsvMeasurementsExporter]
//  public class MandelbrotBenchmark
//  {
//    private const int Rows = 8000;
//    private const int Columns = 8000;
//    private static readonly IComplex Center = new ComplexNumber(-0.75f, 0.0f);
//    private const float Width = 3f;
//    private const float Height = 3f;

//    private readonly SequentialMandelbrot _sequentialMandelbrot = new SequentialMandelbrot();
//    private readonly ParallelMandelbrot _parallelMandelbrot = new ParallelMandelbrot();
//    private readonly ParallelStructMandelbrot _parallelStructMandelbrot = new ParallelStructMandelbrot();


//    [Benchmark(Baseline = true)]
//    public Bitmap SequentialMandelbrot()
//      => _sequentialMandelbrot.Compute(Rows, Columns, Width, Height, Center);

//    [Benchmark]
//    public Bitmap ParallelMandelbrot()
//      => _parallelMandelbrot.Compute(Rows, Columns, Width, Height, Center);

//    [Benchmark]
//    public Bitmap ParallelStructMandelbrot()
//      => _parallelStructMandelbrot.Compute(Rows, Columns, Width, Height, Center);
//  }
//}