using System;

namespace Algorithms.Mandelbrot
{
  public class ComplexNumber 
  {
    public float ImaginaryPart { get; }
    public float RealPart { get; }

    public ComplexNumber(float imaginaryPart, float realPart)
    {
      ImaginaryPart = imaginaryPart;
      RealPart = realPart;
    }
  }

  public readonly  struct ComplexNumberStruct 
  {
    public float ImaginaryPart { get; }
    public float RealPart { get; }

    public ComplexNumberStruct(float imaginaryPart, float realPart)
    {
      ImaginaryPart = imaginaryPart;
      RealPart = realPart;
    }
  }

  public static class ComplexExtensions
  {
    public static double Magnitude(this ComplexNumber number)
      => Math.Sqrt(number.RealPart * number.RealPart * number.ImaginaryPart * number.ImaginaryPart);

    public static ComplexNumber AddTo(this ComplexNumber n1, ComplexNumber n2)
      => new ComplexNumber(n1.RealPart + n2.RealPart, n1.ImaginaryPart + n2.ImaginaryPart);

    public static ComplexNumber MultiplyWith(this ComplexNumber n1, ComplexNumber n2)
      => new ComplexNumber(n1.RealPart * n2.RealPart - n1.ImaginaryPart * n2.ImaginaryPart,
        n1.RealPart * n2.ImaginaryPart + n1.ImaginaryPart * n2.RealPart);

    public static float ComputeRow(this ComplexNumber center, int row, float width, int columns)
      => center.RealPart - width / 2.0f + row * width / columns;

    public static float ComputeColumn(this ComplexNumber center, int col, float height, int rows)
      => center.ImaginaryPart - height / 2.0f + col * height / rows;
  }

  public static class ComplexStructExtensions
  {
    public static double Magnitude(this ComplexNumberStruct number)
      => Math.Sqrt(number.RealPart * number.RealPart * number.ImaginaryPart * number.ImaginaryPart);

    public static ComplexNumberStruct AddTo(this ComplexNumberStruct n1, ComplexNumberStruct n2)
      => new ComplexNumberStruct(n1.RealPart + n2.RealPart, n1.ImaginaryPart + n2.ImaginaryPart);

    public static ComplexNumberStruct MultiplyWith(this ComplexNumberStruct n1, ComplexNumberStruct n2)
      => new ComplexNumberStruct(n1.RealPart * n2.RealPart - n1.ImaginaryPart * n2.ImaginaryPart,
        n1.RealPart * n2.ImaginaryPart + n1.ImaginaryPart * n2.RealPart);

    public static float ComputeRow(this ComplexNumberStruct center, int row, float width, int columns)
      => center.RealPart - width / 2.0f + row * width / columns;

    public static float ComputeColumn(this ComplexNumberStruct center, int col, float height, int rows)
      => center.ImaginaryPart - height / 2.0f + col * height / rows;
  }
}