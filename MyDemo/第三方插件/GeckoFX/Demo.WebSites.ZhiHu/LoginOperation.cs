using Demo.Common.GeckoFxHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.WebSites.ZhiHu
{
    public class LoginOperation : BaseOperation
    {
        public static bool isLogin = true;
        public static bool isScroll = true;
        public static void Login()
        {
            _geckoFxHelper.SetAttrElementByNameFirst("account", "value", "15217189317");
            _geckoFxHelper.SetAttrElementByNameFirst("password", "value", "a08142400070044");
            _geckoFxHelper.BtnClientByClassFirst("sign-button submit");
            isLogin = false;
        }

        static int line = 0;
        public static void Scroll()
        {
            _geckoWebBrowser.Window.ScrollByLines(1000);//设置滚动条下拉几行
            line++;
            if (line == 2)
                isScroll = false;
        }

        public static void LoadContent()
        {
            var content = _geckoWebBrowser.DomDocument;
            var nodes=_geckoWebBrowser.DomDocument.ChildNodes;
        }
    }
}
