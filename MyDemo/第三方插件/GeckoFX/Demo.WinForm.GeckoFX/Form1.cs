﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using Demo.Test.Greetest;
using Gecko;
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
            Xpcom.Initialize(xulrunnerPath);
            #region 初始化 GeckoFx
            Browser = new GeckoWebBrowser();
            Browser.Parent = this;
            Browser.Dock = DockStyle.Fill;
            Gecko.GeckoPreferences.User["gfx.font_rendering.graphite.enabled"] = true;
            GeckoPreferences.Default["extensions.blocklist.enabled"] = false;
            clearCookie();
            this.Gecko_Web.Navigate(Demo.WebSites.Alibaba.WebSiteOperation.IndexUrl);

            Demo.WebSites.Alibaba.Registered.Init(this.Gecko_Web);
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

        private void Start_Btn_Click(object sender, EventArgs e)
        {
           
        }

        private void Begin_Btn_Click(object sender, EventArgs e)
        {

            Demo.WebSites.Alibaba.Registered.SlideMouse();
            //timer = new System.Timers.Timer(1000 * 4);
            //timer.Elapsed += new System.Timers.ElapsedEventHandler(Start);//到达时间的时候执行事件；
            //timer.AutoReset = true;//设置是执行一次（false）还是一直执行(true)；
            //timer.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；
            //timer.Start();
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Next_Btn_Click(object sender, EventArgs e)
        {
            timer.Dispose();
        }



        private void Start(object evg, System.Timers.ElapsedEventArgs e)
        {
            this.Invoke(new Action(() =>
            {
            }));
        }
    }
}
