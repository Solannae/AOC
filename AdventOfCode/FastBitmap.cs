using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace AdventOfCode
{
    public class FastBitmap : IDisposable
    {
        public Bitmap Bitmap { get; private set; }
        public byte[] Bits { get; private set; }
        public bool Disposed { get; private set; }
        public int Height { get; private set; }
        public int Width { get; private set; }

        protected GCHandle BitsHandle { get; private set; }

        public FastBitmap(int width, int height)
        {
            Width = width;
            Height = height;
            Bits = new byte[width * height * 4];
            BitsHandle = GCHandle.Alloc(Bits, GCHandleType.Pinned);
            Bitmap = new Bitmap(width, height, width * 4, PixelFormat.Format32bppPArgb, BitsHandle.AddrOfPinnedObject());
        }

        public void SetPixel(int x, int y, Color color)
        {
            var index = x * 4 + y * Width * 4;

            Bits[index] = color.A;
            Bits[index + 1] = color.R;
            Bits[index + 2] = color.G;
            Bits[index + 3] = color.B;
        }

        public Color GetPixel(int x, int y)
        {
            var index = x * 4 + y * Width * 4;
            var result = Color.FromArgb(Bits[index], Bits[index + 1], Bits[index + 2], Bits[index + 3]);

            return result;
        }

        public void Dispose()
        {
            if (Disposed) return;
            Disposed = true;
            Bitmap.Dispose();
            BitsHandle.Free();
        }

        public static FastBitmap LoadFastBitmap(string imagePath)
        {
            FastBitmap img;

            using (var bitmap = new Bitmap(imagePath))
            {
                img = new FastBitmap(bitmap.Width, bitmap.Height);
                var dest = Graphics.FromImage(img.Bitmap);
                dest.DrawImage(bitmap, new Rectangle { X = 0, Y = 0, Width = img.Width, Height = img.Height });
            }

            return img;
        }
    }
}
