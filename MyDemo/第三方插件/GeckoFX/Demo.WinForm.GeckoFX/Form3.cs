using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Gecko;
using System.Threading;

namespace Demo.WinForm.GeckoFX
{
    public partial class Form3 : Form
    {

        /// <summary>
        /// 插件存放位置
        /// </summary>
        static private string xulrunnerPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "xulrunner");

        private GeckoWebBrowser Browser;

        public Form3()
        {
            InitializeComponent();
            Init();
            Stop_Btn.Enabled = false;
        }

        private void Init()
        {
            Xpcom.Initialize(xulrunnerPath);
            #region 初始化 GeckoFx
            Browser = new GeckoWebBrowser();
            Browser.Parent = this;
            Browser.Dock = DockStyle.Fill;
            Gecko.GeckoPreferences.User["gfx.font_rendering.graphite.enabled"] = true;
            GeckoPreferences.Default["extensions.blocklist.enabled"] = false;
            clearCookie();
            this.Gecko_Web.Navigate(Demo.WebSites.TianMao.url);
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

        private CancellationTokenSource _cts;//方便做取消功能

        private void button1_Click(object sender, EventArgs e)
        {
            _cts = new CancellationTokenSource();
            try
            {
                this.Invoke(new Action(() => FormLog("系统开始")));
                ThreadPool.QueueUserWorkItem(state => Start(_cts.Token));//作为后台进程
                Begin_Btn.Enabled = false;
                Stop_Btn.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Stop_Btn_Click(object sender, EventArgs e)
        {
            if (_cts != null)
            {
                _cts.Cancel();
                this.Invoke(new Action(() => FormLog("取消")));
            }
            Begin_Btn.Enabled = true;
            Stop_Btn.Enabled = false;
        }


        private void Start(CancellationToken ct)
        {
            while (true)
            {
                //是否取消
                if (ct.IsCancellationRequested)
                {
                    break;
                }

                var now = DateTime.Now;
                if (now.Minute == 0 || now.Minute == 1)
                {
                    this.Invoke(new Action(() => FormLog("到达开抢时间!")));
                    this.Invoke(new Action(() => FormLog(Demo.WebSites.TianMao.Qiang())));
                }
                else
                {
                    this.Invoke(new Action(() => FormLog("未到时间")));
                    Thread.Sleep(2000);
                }
            }
        }


        private void FormLog(string Content)
        {
            this.ProcessMessae_LisView.Items.Add(string.Format("{0} {1}", DateTime.Now, Content));
            this.ProcessMessae_LisView.EnsureVisible(this.ProcessMessae_LisView.Items.Count - 1);
        }

    }
}
