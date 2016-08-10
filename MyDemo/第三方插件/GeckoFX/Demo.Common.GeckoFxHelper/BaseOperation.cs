using Gecko;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo.Common.GeckoFxHelper
{
    public class BaseOperation
    {
        protected static GeckoWebBrowser _geckoWebBrowser;
        protected static GeckoFxHelper _geckoFxHelper;
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="geckoWebBrowser"></param>
        public static void Init(GeckoWebBrowser geckoWebBrowser)
        {
            _geckoWebBrowser = geckoWebBrowser;
            _geckoFxHelper = new GeckoFxHelper(geckoWebBrowser);
        }
    }
}
