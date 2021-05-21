using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Algorithms.Mandelbrot
{
  public static class Mandelbrot
  {
    public static Bitmap DrawMandelbrotBitmap(Action<BitmapData, byte[]> mandelbrotDrawingStrategy, int rows, int columns)
    {
      var bitmap = new Bitmap(rows, columns, PixelFormat.Format24bppRgb);
      var bitmapData = LockBitmap(bitmap);
      var pixels = new byte[bitmapData.Stride * bitmap.Height];
      var ptrFirstPixel = bitmapData.Scan0;
      Marshal.Copy(ptrFirstPixel, pixels, 0, pixels.Length);

      mandelbrotDrawingStrategy(bitmapData, pixels);

      Marshal.Copy(pixels, 0, ptrFirstPixel, pixels.Length);
      UnlockBitmap(bitmap, bitmapData);

      return (Bitmap)bitmap.Clone();
    }

    private static BitmapData LockBitmap(Bitmap bitmap)
    {
      var bitmapData =
        bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
          ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
      return bitmapData;
    }

    private static void UnlockBitmap(Bitmap bitmap, BitmapData bitmapData)
    {
      bitmap.UnlockBits(bitmapData);
    }

    public static void WriteColors(this byte[] pixels, int offset, Color color)
    {
      pixels[offset + 0] = color.B;
      pixels[offset + 1] = color.G;
      pixels[offset + 2] = color.R;
    }
  }
}