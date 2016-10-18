using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using Demo.Test.Greetest;
using Gecko;
using Gecko.Utils;
using Timer = System.Windows.Forms.Timer;

namespace Demo.WinForm.GeckoFX
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 插件存放位置
        /// </summary>
        static private string xulrunnerPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "xulrunner");



        private GeckoWebBrowser Browser;

        public Form1()
        {
            InitializeComponent();
            Init();
        }

        System.Timers.Timer timer;
        private void Init()
        {
            textBox1.Text = Demo.Test.Greetest.WebSiteOperation.IndexUrl;
            Xpcom.Initialize(xulrunnerPath);
            #region 初始化 GeckoFx
            Browser = new GeckoWebBrowser();
            Browser.Parent = this;
            Browser.Dock = DockStyle.Fill;
            Gecko.GeckoPreferences.User["gfx.font_rendering.graphite.enabled"] = true;
            GeckoPreferences.Default["extensions.blocklist.enabled"] = false;
            clearCookie();
            this.Gecko_Web.Navigate(Demo.WebSites.ScreenPrint.url);

            Demo.WebSites.ScreenPrint.Init(this.Gecko_Web);


            #endregion
        }

        /// <summary>
        /// 清理cookie
        /// </summary>
        public static void clearCookie()
        {
            nsICookieManager CookieMan;
            CookieMan = Xpcom.GetService<nsICookieManager>("@mozilla.org/cookiemanager;1");
            CookieMan = Xpcom.QueryInterface<nsICookieManager>(CookieMan);
            CookieMan.RemoveAll();
        }

        /// <summary>
        /// 火狐浏览器配置项 
        /// </summary>
        /// <param name="strIp">代理ip </param>
        /// <param name="iPort">端口</param>
        private void InitAllCityData(string strIp, int iPort)
        {
            try
            {
                //火狐框架 路径
                Xpcom.ProfileDirectory = xulrunnerPath;
                Xpcom.Initialize(AppDomain.CurrentDomain.BaseDirectory + "xulrunner");
                clearCookie();
                //flash
                Gecko.GeckoPreferences.User["gfx.font_rendering.graphite.enabled"] = true;

                GeckoPreferences.Default["extensions.blocklist.enabled"] = false;

                //Xpcom.DeleteAllCookies();

                if (!string.IsNullOrEmpty(strIp) && iPort > 0)
                {
                    //ip 代理
                    GeckoPreferences.User["network.proxy.http"] = strIp;
                    GeckoPreferences.User["network.proxy.http_port"] = iPort;
                    GeckoPreferences.User["network.proxy.type"] = 1;

                    GeckoPreferences.User["network.proxy.ssl"] = strIp;
                    GeckoPreferences.User["network.proxy.ssl_port"] = iPort;
                }
            }
            catch { }
        }

        private void Start_Btn_Click(object sender, EventArgs e)
        {
            Demo.WebSites.ScreenPrint.Into();
        }


        private void Begin_Btn_Click(object sender, EventArgs e)
        {
            Demo.Test.Greetest.BreakCode.RunBreakCode();
        }

       // private void Begin_Btn_Click(object sender, EventArgs e)
       // {
       //     Gecko_Web.Window.ScrollByLines(1000);//设置滚动条下拉几行
       //     //Application.DoEvents();
       //     int width = Gecko_Web.Document.Body.ClientWidth;
       //     var OffsetHeight = Gecko_Web.Document.Body.OffsetHeight;
       //     var OffsetTop = Gecko_Web.Document.Body.OffsetTop;
       //     var ScrollHeight = Gecko_Web.Document.Body.ScrollHeight;
       //     var ScrollY = Gecko_Web.Window.ScrollY;

       //     ScrollY = Gecko_Web.Height + ScrollY;
       //     width = Gecko_Web.Width;
       //     width = width == 0 ? 870 : width;

       //     Bitmap bitmap = this.Gecko_Web.GetBitmap((uint)width, (uint)ScrollY);
       ////      bitmap = GetImageThumb(bitmap, new Size(width, ScrollY));
       //     MemoryStream mstream = new MemoryStream();
       //     bitmap.Save(mstream, ImageFormat.Jpeg);
       //     Bitmap bitmaps = new Bitmap(_GetPicThumbnail(mstream, 30, width, ScrollY,1));
       //     bitmaps.Save(@"C:\Users\pc\Desktop\Images\" + Guid.NewGuid() + ".jpeg", ImageFormat.Jpeg);  // 保存图片

       //     bitmap.Dispose();
       //     bitmaps.Dispose();

       //     //  var result= WebSites.JcNet.CrackRun();

       //     // Demo.WebSites.Alibaba.Registered.SlideMouse();
       //     //timer = new System.Timers.Timer(1000 * 4);
       //     //timer.Elapsed += new System.Timers.ElapsedEventHandler(Start);//到达时间的时候执行事件；
       //     //timer.AutoReset = true;//设置是执行一次（false）还是一直执行(true)；
       //     //timer.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；
       //     //timer.Start();
       // }


        /// <summary>
        /// 无损压缩图片
        /// </summary>
        /// <param name="sFile">原图片</param>
        /// <param name="flag">压缩质量 1-100</param>
        /// <param name="pictureFormat">图片格式0 jpg，1 jpeg，2 png，3 gif，4 bmp，5 ico</param>
        /// <returns></returns>
        public static MemoryStream _GetPicThumbnail(MemoryStream mstream, int flag, int iWidth, int iHeight, int pictureFormat)
        {
            MemoryStream newstream = new MemoryStream();

            System.Drawing.Image iSource = System.Drawing.Image.FromStream(mstream);

            ImageFormat tFormat = iSource.RawFormat;

            int dHeight = iSource.Height;
            int dWidth = iSource.Width;

            if (iWidth != 0)
            {
                dWidth = iWidth;
            }

            if (iHeight != 0)
            {
                dHeight = iHeight;
            }

            int sW = 0, sH = 0;

            //按比例缩放

            Size tem_size = new Size(iSource.Width, iSource.Height);

            if (tem_size.Width > dHeight || tem_size.Width > dWidth) //将**改成c#中的或者操作符号
            {

                if ((tem_size.Width * dHeight) > (tem_size.Height * dWidth))
                {

                    sW = dWidth;

                    sH = (dWidth * tem_size.Height) / tem_size.Width;

                }

                else
                {

                    sH = dHeight;

                    sW = (tem_size.Width * dHeight) / tem_size.Height;

                }

            }

            else
            {

                sW = tem_size.Width;

                sH = tem_size.Height;

            }

            Bitmap ob = new Bitmap(dWidth, dHeight);

            Graphics g = Graphics.FromImage(ob);

            g.Clear(Color.WhiteSmoke);

            g.CompositingQuality = CompositingQuality.HighQuality;

            g.SmoothingMode = SmoothingMode.HighQuality;

            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(iSource, new Rectangle((dWidth - sW) / 2, (dHeight - sH) / 2, sW, sH), 0, 0, iSource.Width, iSource.Height, GraphicsUnit.Pixel);

            g.Dispose();

            //以下代码为保存图片时，设置压缩质量

            EncoderParameters ep = new EncoderParameters();

            long[] qy = new long[1];

            qy[0] = flag;//设置压缩的比例1-100

            EncoderParameter eParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qy);

            ep.Param[0] = eParam;

            try
            {

                string imgFormat = "";
                if (pictureFormat == 0 || pictureFormat == 1)//jpeg
                {
                    imgFormat = "JPEG";
                }
                else if (pictureFormat == 2)//png
                {
                    imgFormat = "PNG";
                }
                else if (pictureFormat == 3)//gif
                {
                    imgFormat = "GIF";
                }
                else if (pictureFormat == 4)//bmp
                {
                    imgFormat = "BMP";
                }
                else if (pictureFormat == 5)//ico
                {
                    imgFormat = "ICO";
                }

                ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo jpegICIinfo = null;

                for (int x = 0; x < arrayICI.Length; x++)
                {
                    if (arrayICI[x].FormatDescription.Equals(imgFormat))
                    {
                        jpegICIinfo = arrayICI[x];
                        break;
                    }

                }

                if (jpegICIinfo != null)
                {

                    ob.Save(newstream, jpegICIinfo, ep);//dFile是压缩后的新路径

                }

                else
                {

                    ob.Save(newstream, tFormat);

                }

                return newstream;

            }

            catch
            {

                return newstream;

            }

            finally
            {

                iSource.Dispose();

                ob.Dispose();

            }

            return newstream;

        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Next_Btn_Click(object sender, EventArgs e)
        {
            Gecko_Web.Document.Location.Reload(true);
        }



        private void Start(object evg, System.Timers.ElapsedEventArgs e)
        {
            this.Invoke(new Action(() =>
            {
            }));
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string url = textBox1.Text;
            if (string.IsNullOrWhiteSpace(url))
            {
                MessageBox.Show("请输入有拖动验证码的网址");
                return;
            }

            this.Gecko_Web.Navigate(url.Trim());

        }

        private void button2_Click(object sender, EventArgs e)
        {
            WebSites.ScreenPrint.Screen();
        }
    }
}
