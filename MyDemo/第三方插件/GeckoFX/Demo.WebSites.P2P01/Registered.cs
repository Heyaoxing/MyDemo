using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Demo.Common.GeckoFxHelper;
using Gecko.DOM;

namespace Demo.WebSites.P2P01
{
    public class Registered : BaseOperation
    {
        public const string URL = "http://www.01p2p.net/member.php?mod=register";

        /// <summary>
        /// 色块对应值
        /// </summary>
        private static Dictionary<string, string> color = new Dictionary<string, string>()
        {
            {"红","red.gif"},
            {"黑","black.gif"},
            {"橙","yellow.gif"},
        };
        public static void TouchBreaking()
        {
            using (var document = _geckoWebBrowser.Document.GetElementsByClassName("myrfm")[0])
            {
                var imgs = new GeckoDivElement(document.DomObject).GetElementsByTagName("img");
                var font = document.TextContent;
                Regex regex = new Regex("[红黑橙]");
                var results = regex.Matches(font);
                foreach (var item in results)
                {
                    var attribute = "source/plugin/yaoqing_robot/images/" + color[item.ToString()];
                    foreach (var img in imgs)
                    {
                        if (img != null && img.GetAttribute("src") == attribute && img.GetAttribute("class").Contains("btnimg"))
                        {
                            var a =new GeckoAnchorElement(_geckoWebBrowser.Document.GetElementsByClassName(img.GetAttribute("class"))[0].DomObject);
                            a.Click();
                            for (int i = 0; i < 200; i++)
                            {
                                Application.DoEvents();
                                Thread.Sleep(10);
                            }
                            break;
                        }
                    }
                }
            }
        }
    }
}
