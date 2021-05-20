using System;

namespace Algorithms.Mandelbrot
{
  public interface IComplex
  {
    double ImaginaryPart { get; }
    double RealPart { get; } 
    IComplex Create(double imaginaryPart, double realPart);
  }

  public class ComplexNumber : IComplex
  {
    public double ImaginaryPart { get; }
    public double RealPart { get; }

    public ComplexNumber(double imaginaryPart, double realPart)
    {
      ImaginaryPart = imaginaryPart;
      RealPart = realPart;
    }

    public IComplex Create(double imaginaryPart, double realPart)
    {
      return new ComplexNumber(imaginaryPart, realPart);
    }
  }

  public readonly struct ComplexNumberStruct : IComplex
  {
    public double ImaginaryPart { get; }
    public double RealPart { get; }

    public ComplexNumberStruct(double imaginaryPart, double realPart)
    {
      ImaginaryPart = imaginaryPart;
      RealPart = realPart;
    }

    public IComplex Create(double imaginaryPart, double realPart)
    {
      return new ComplexNumberStruct(imaginaryPart, realPart);
    }
  }

  public static class ComplexExtensions
  {
    public static double Magnitude(this IComplex number)
      => Math.Sqrt(number.RealPart * number.RealPart * number.ImaginaryPart * number.ImaginaryPart);

    public static IComplex Add(this IComplex n1, IComplex n2)
      => n1.Create(n1.RealPart + n2.RealPart, n1.ImaginaryPart + n2.ImaginaryPart);

    public static IComplex Multiply(this IComplex n1, IComplex n2)
      => n1.Create(n1.RealPart * n2.RealPart - n1.ImaginaryPart * n2.ImaginaryPart,
        n1.RealPart * n2.ImaginaryPart + n1.ImaginaryPart * n2.RealPart);

    public static double ComputeRow(this IComplex center, int columns, int col, float width)
      => center.RealPart - width / 2.0f + (float)col * width / (float)columns;

    public static double ComputeColumn(this IComplex center, int rows, int row, float height)
      => center.ImaginaryPart - height / 2.0f + (float)row * height / (float)rows;
  }
}