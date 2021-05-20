using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Algorithms.Mandelbrot
{
  public class ParallelMandelbrot
  {
    public Bitmap Compute(int rows, int columns, float width, float height, IComplex center)
    {
      var bitmap = new Bitmap(rows, columns, PixelFormat.Format24bppRgb);
      var bitmapData = LockBitmap(bitmap);
      var pixels = GetPixels(bitmapData, bitmap);
      var firstPixel = bitmapData.Scan0;
      Marshal.Copy(firstPixel, pixels, 0, pixels.Length);

      Parallel.For(0, columns - 1, col =>
      {    //#A
        for (int row = 0; row < rows; row++)
        {
          var x = center.ComputeRow(row, columns, width); 
          var y = center.ComputeColumn(col, rows, height); 
          var c = new ComplexNumber(x, y);
          var color = IsMandelbrot(c, 100) ? Color.Black : Color.White; 
          var offset = col * bitmapData.Stride + 3 * row;
          WriteColorToPixels(pixels, offset, color);
        }
      });

      Marshal.Copy(pixels, 0, firstPixel, pixels.Length);
      UnlockBitmap(bitmap, bitmapData);

      return (Bitmap)bitmap.Clone();
    }

    private static void WriteColorToPixels(byte[] pixels, int offset, Color color)
    {
      pixels[offset + 0] = color.B;
      pixels[offset + 1] = color.G;
      pixels[offset + 2] = color.R;
    }

    private static bool IsMandelbrot(IComplex complex, int iterations)
    {
      IComplex z = new ComplexNumber(0.0f, 0.0f);
      var accumulator = 0;
      while (accumulator < iterations && z.Magnitude() < 2.0)
      {
        z = z.Multiply(z).Add(complex);
        accumulator++;
      }

      return accumulator == iterations;
    }

    private static byte[] GetPixels(BitmapData bitmapData, Bitmap bitmap)
      => new byte[bitmapData.Stride * bitmap.Height];

    private static BitmapData LockBitmap(Bitmap bitmap)
      => bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

    private static void UnlockBitmap(Bitmap bitmap, BitmapData bitmapData)
      => bitmap.UnlockBits(bitmapData);
  }
}