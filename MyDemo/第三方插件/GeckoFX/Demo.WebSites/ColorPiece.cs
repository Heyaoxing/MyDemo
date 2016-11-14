using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Demo.Common.GeckoFxHelper;

namespace Demo.WebSites
{
    public class ColorPiece : BaseOperation
    {
        public static string URL = "http://www.01p2p.net/member.php?mod=register";

        public void GetADoms()
        {
            var link = _geckoWebBrowser.Document.GetElementsByTagName("a").ToList();
            foreach (var item in link)
            {
                if (item.GetAttribute("id").Contains("yaoqing_select"))
                {
                }
            }
        }
    }
}
