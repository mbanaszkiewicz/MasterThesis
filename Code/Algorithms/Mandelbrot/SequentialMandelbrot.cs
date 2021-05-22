using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Algorithms.Mandelbrot
{
  public class SequentialMandelbrot
  {
    private float Width { get; }
    private float Height { get; }
    private int Rows { get; }
    private int Columns { get; }
    private ComplexNumber Center { get; }

    public SequentialMandelbrot(float width, float height, int rows, int columns, ComplexNumber center)
    {
      Width = width;
      Height = height;
      Rows = rows;
      Columns = columns;
      Center = center;
    }

    public Bitmap Draw()
    {
      return Mandelbrot.DrawMandelbrotBitmap(DrawingStrategy(), Rows, Columns);
    }

    private Action<BitmapData, byte[]> DrawingStrategy()
      => (bitmapData, pixels) =>
      {
        for (var column = 0; column < Columns; column++) 
        {
          for (var row = 0; row < Rows; row++) 
          {
            var x = Center.ComputeRow(row, Width, Columns); 
            var y = Center.ComputeColumn(column, Height, Rows); 
            var c = new ComplexNumber(x, y);
            var color = BelongsToMandelbrot(c, 100) ? Color.Black : Color.White; 
            var offset = (column * bitmapData.Stride) + (3 * row);
            pixels.WriteColors(offset, color);
          }
        }
      };

    private static bool BelongsToMandelbrot(ComplexNumber number, int iterations)
    {
      var zNumber = new ComplexNumber(0.0f, 0.0f);
      var accumulator = 0;
      while (accumulator < iterations && zNumber.Magnitude() < 2.0)
      {
        zNumber = zNumber.MultiplyWith(zNumber).AddTo(number);
        ++accumulator;
      }

      return accumulator == iterations;
    }
  }
}