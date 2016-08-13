using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BreakTheCode.Geetest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Image bg = LoadImage("http://static.geetest.com/pictures/gt/496156f80/bg/2a177eca.jpg");
                Image full = LoadImage("http://static.geetest.com/pictures/gt/496156f80/496156f80.jpg");
                bg = AlignImage(bg, 26);
                full = AlignImage(full, 26);
                int xpos = GetPositionX(bg, full);
                Console.WriteLine(xpos);

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            Console.Read();
        }


        private static Image LoadImage(string url)
        {
            Image image;
            HttpWebRequest hreq = (HttpWebRequest)HttpWebRequest.Create(url);
            HttpWebResponse hres = (HttpWebResponse)hreq.GetResponse();
            using (var stream = hres.GetResponseStream())
            {
                image = Image.FromStream(stream);
            }
            hres.Close();
            return image;
        }

        private static Image AlignImage(Image img, int ypos = 0, int height = 52)
        {
            const int width = 260;
            Bitmap bmp = new Bitmap(width, height);
            var pos = new int[] {157, 145, 265, 277,181, 169, 241, 253, 109, 97, 289, 301, 85, 73, 25, 37, 13, 1, 121, 133, 61, 49, 217, 229, 205, 193,
                145, 157, 277, 265, 169, 181, 253, 241, 97, 109, 301, 289, 73, 85, 37, 25, 1, 13, 133, 121, 49, 61, 229, 217, 193, 205};
            int dx = 0, sy = 58, dy = 0;
            var g = Graphics.FromImage(bmp);
            for (var i = 0; i < pos.Length; i++)
            {
                g.DrawImage(img, new Rectangle(dx, dy - ypos, 10, 58), new Rectangle(pos[i], sy, 10, 58), GraphicsUnit.Pixel);
                dx += 10;
                if (dx == width)
                {
                    dx = 0;
                    dy = 58;
                    sy = 0;
                }
            }
            g.Dispose();
            return bmp;
        }

        private static int GetPositionX(Image imgBg, Image imgFullBg, Image imgSlice = null)
        {
            var bg = new Bitmap(imgBg);
            var full = new Bitmap(imgFullBg);
            Rectangle rect = new Rectangle(0, 0, bg.Width, bg.Height);
            const int bytesCount = 4;
            var bgData = bg.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            var fullData = full.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            int xpos = -1;
            unsafe
            {
                byte* pBg = (byte*)bgData.Scan0;
                byte* pFull = (byte*)fullData.Scan0;
                //sub 2 images
                for (var i = 0; i < bgData.Stride * bgData.Height; i += 4)
                {
                    pBg[i] = (byte)Math.Abs((int)pBg[i] - pFull[i]);
                    pBg[i + 1] = (byte)Math.Abs((int)pBg[i + 1] - pFull[i + 1]);
                    pBg[i + 2] = (byte)Math.Abs((int)pBg[i + 2] - pFull[i + 2]);
                }
                var w = bgData.Width;
                // Roberts edge detect and calculate histgram
                int[] histgram = new int[w];
                int[] histSum = new int[w];
                for (var y = 0; y < bgData.Height - 1; y++)
                {
                    for (var x = 0; x < w - 1; x++)
                    {
                        var i00 = (x + y * w);
                        var i11 = (i00 + w + 1) * bytesCount;
                        var i01 = (i00 + 1) * bytesCount;
                        var i10 = (i00 + w) * bytesCount;
                        i00 *= bytesCount;
                        pFull[i00] = (byte)(Math.Abs(pBg[i00] - pBg[i11]) + Math.Abs(pBg[i01] - pBg[i10])); // b
                        pFull[i00 + 1] = (byte)(Math.Abs(pBg[i00 + 1] - pBg[i11 + 1]) + Math.Abs(pBg[i01 + 1] - pBg[i10 + 1])); // g
                        pFull[i00 + 2] = (byte)(Math.Abs(pBg[i00 + 2] - pBg[i11 + 2]) + Math.Abs(pBg[i01 + 2] - pBg[i10 + 2])); // r
                        histgram[x] += pFull[i00] + pFull[i00 + 1] + pFull[i00 + 2];
                    }
                }
                // find xpos
                int ww = 48, maxValue = -1;
                for (var i = 0; i < ww; i++)
                    histSum[0] += histgram[i];
                for (var x = 1; x < w - ww; x++)
                {
                    histSum[x] = histSum[x - 1] + histgram[x + ww - 1] - histgram[x - 1];
                    if (histSum[x] > maxValue)
                    {
                        xpos = x;
                        maxValue = histSum[x];
                    }
                }
            } // exit unsafe
            bg.UnlockBits(bgData);
            full.UnlockBits(fullData);
            //offset 6 pixels
            return xpos - 6;
        }
    }
}
