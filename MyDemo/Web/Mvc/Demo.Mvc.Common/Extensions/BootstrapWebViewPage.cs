using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo.Mvc.Common.Extensions
{
    public abstract class BootstrapWebViewPage<T> : System.Web.Mvc.WebViewPage<T>
    {
        //在cshtml页面里面使用的变量
        public BootstrapHelper Bootstrap { get; set; }

        /// <summary>
        /// 初始化Bootstrap对象
        /// </summary>
        public override void InitHelpers()
        {
            base.InitHelpers();
            Bootstrap = new BootstrapHelper(ViewContext, this);
        }

        public override void Execute()
        {
            //throw new NotImplementedException();
        }
    }
}