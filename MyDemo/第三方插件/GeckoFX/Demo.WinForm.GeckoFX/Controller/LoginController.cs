using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Demo.Common.GeckoFxHelper;
using Gecko;

namespace Demo.WinForm.GeckoFX.Controller
{
    public class LoginController : BaseController
    {
        private static string _previous = string.Empty;//保存跳转前地址
        private static string _current = string.Empty;//保存跳转后当前地址
   
        public static bool IsGo = false;

        private static string[] username = new string[] { "admin", "admi", "dmin", "432432", "admin", "admi", "dmin", "432432" };
        private static string[] password = new string[] { "123456", "4323", "234", "admin", "4323", "234", "admin", "admin" };

        public static void LoginCheck()
        {
       
            int index = new Random(DateTime.Now.Second).Next(0, 7);

            _geckoFxHelper.SetAttrElementById("tbUserName", "value", "admin");
            _geckoFxHelper.SetAttrElementById("tbPassword", "value", "123456");

            _geckoFxHelper.BtnClientById("btnLogin");
            _geckoWebBrowser.DOMContentLoaded += LoadHistoryRequest;//加载渲染页面时触发
        }

        /// <summary>
        /// 是否重新跳转到新页面
        /// </summary>
        private static void LoadHistoryRequest(object a, DomEventArgs args)
        {
            _current = _geckoWebBrowser.Url.ToString();
            _previous = _geckoWebBrowser.History[_geckoWebBrowser.History.Count - 2].Url.ToString();
            if (string.IsNullOrWhiteSpace(_current) || string.Compare(_previous, _current) == 0)
            {
                IsGo = false;
            }
            else
            {
                IsGo = true;
            }
        }
    }
}
