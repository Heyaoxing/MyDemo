using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gecko;

namespace Demo.Common.GeckoFxHelper
{
    public class GeckoFxHelper
    {
        private GeckoWebBrowser _geckoWebBrowser;
        public GeckoFxHelper(GeckoWebBrowser geckoWebBrowser)
        {
            _geckoWebBrowser = geckoWebBrowser;
        }

        /// <summary>
        /// 全局环境设置
        /// </summary>
        public void Global()
        {
            
        }

        /// <summary>
        /// 一般节点属性方法
        /// </summary>
        public void ElementProperty()
        {
            var documents = _geckoWebBrowser.Document.GetElementsByName("wd")[0];//通过标签名称获取DOM节点对象,此方法获取集合对象中的一个
            documents.SetAttribute("value", "你好");//设置节点属性
            var attribute = documents.GetAttribute("value");  //获取节点属性
            var html = documents.OuterHtml; //获取节点html代码
            var id = documents.Id;//获取节点id
        }

        /// <summary>
        /// 一般节点事件方法
        /// </summary>
        public void ElementEvent()
        {
            GeckoHtmlElement documents =(GeckoHtmlElement) _geckoWebBrowser.Document.GetElementById("kw");//通过id获取DOM节点
        }
    }
}
