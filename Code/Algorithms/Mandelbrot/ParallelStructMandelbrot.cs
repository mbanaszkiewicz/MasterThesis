﻿using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Algorithms.Mandelbrot
{
  public class ParallelStructMandelbrot
  {
    private float Width { get; }
    private float Height { get; }
    private int Rows { get; }
    private int Columns { get; }
    private ComplexNumberStruct Center { get; }

    public ParallelStructMandelbrot(float width, float height, int rows, int columns, ComplexNumberStruct center)
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
        Parallel.For(0, Columns - 1, column =>
        {
          for (var row = 0; row < Rows; row++)
          {
            var x = Center.ComputeRow(row, Width, Columns);
            var y = Center.ComputeColumn(column, Height, Rows);
            var c = new ComplexNumberStruct(x, y);
            var color = BelongsToMandelbrot(c, 100) ? Color.Black : Color.White;
            var offset = (column * bitmapData.Stride) + (3 * row);
            pixels.WriteColors(offset, color);
          }
        });
    

    private static bool BelongsToMandelbrot(ComplexNumberStruct number, int iterations)
    {
      var zNumber = new ComplexNumberStruct(0.0f, 0.0f);
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