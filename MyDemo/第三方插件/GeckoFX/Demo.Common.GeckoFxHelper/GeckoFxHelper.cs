using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gecko;
using Gecko.DOM;

namespace Demo.Common.GeckoFxHelper
{
    public class GeckoFxHelper
    {
        private GeckoWebBrowser _geckoWebBrowser;
        public GeckoFxHelper(GeckoWebBrowser geckoWebBrowser)
        {
            _geckoWebBrowser = geckoWebBrowser;
        }

         
        #region 测试方法区
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
            using (var documents = _geckoWebBrowser.Document.GetElementById("form"))
            {
                GeckoFormElement form = new GeckoFormElement(documents.DOMElement); //每个标签都有响应的对象,比如form标签的对象是GeckoFormElement,继承自GeckoHtmlElement,获取到标签对象后就可以使用此标签特有的方法
                form.submit();
            }
        }

        #endregion

        #region 封装方法区

        /// <summary>
        /// 通过id设置标签特性值
        /// </summary>
        /// <param name="id"></param>
        /// <param name="attributeName"></param>
        /// <param name="content"></param>
        public void SetAttrElementById(string id, string attributeName, string content)
        {
            using (var documents = _geckoWebBrowser.Document.GetElementById(id))
            {
                documents.SetAttribute(attributeName, content);
            }
        }

        /// <summary>
        /// 通过id插入textArea内容
        /// </summary>
        /// <param name="id"></param>
        /// <param name="attributeName"></param>
        /// <param name="content"></param>
        public void InsertContextByIdWithTextArea(string id, string textContent)
        {
            using (var documents = _geckoWebBrowser.Document.GetElementById(id))
            {
                GeckoTextAreaElement textArea = new GeckoTextAreaElement(documents.DOMElement); //每个标签都有响应的对象,比如form标签的对象是GeckoFormElement,继承自GeckoHtmlElement,获取到标签对象后就可以使用此标签特有的方法
                textArea.TextContent = textContent;
            }
        }

        /// <summary>
        /// 通过id触发button按钮点击
        /// </summary>
        /// <param name="id"></param>
        public void BtnClientById(string id)
        {
            using (var documents = _geckoWebBrowser.Document.GetElementById(id))
            {
                GeckoButtonElement button = new GeckoButtonElement(documents.DOMElement);
                button.Click();
            }
        }
        #endregion
    }
}
