using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Demo.Common.GeckoFxHelper;
using Gecko;

namespace Demo.WinForm.GeckoFX
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 插件存放位置
        /// </summary>
        static private string xulrunnerPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "xulrunner");

        /// <summary>
        /// 浏览地址
        /// </summary>
        static private string testUrl = "https://www.baidu.com/";

        private GeckoWebBrowser Browser;  

        public Form1()
        {
            InitializeComponent();
            Init();
        }


        private void Init()
        {
            Xpcom.Initialize(xulrunnerPath);

            Browser = new GeckoWebBrowser();
            Browser.Parent = this;
            Browser.Dock = DockStyle.Fill;
            Gecko.GeckoPreferences.User["gfx.font_rendering.graphite.enabled"] = true;
            GeckoPreferences.Default["extensions.blocklist.enabled"] = false;
            this.Gecko_Web.Navigate(testUrl);
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

        private void Start_Btn_Click(object sender, EventArgs e)
        {
        }

        private void Begin_Btn_Click(object sender, EventArgs e)
        {
            GeckoFxHelper geckoFxHelper=new GeckoFxHelper(  this.Gecko_Web);
            geckoFxHelper.GetDocument();
        }
    }
}
    