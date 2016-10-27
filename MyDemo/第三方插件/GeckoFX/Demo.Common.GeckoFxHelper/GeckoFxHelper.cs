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
        /// 通过name设置标签特性值
        /// </summary>
        /// <param name="id"></param>
        /// <param name="attributeName"></param>
        /// <param name="content"></param>
        public void SetAttrElementByNameFirst(string name, string attributeName, string content)
        {
            using (var documents = _geckoWebBrowser.Document.GetElementsByName(name)[0])
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

        /// <summary>
        /// 通过class触发button按钮点击
        /// </summary>
        /// <param name="id"></param>
        public void BtnClientByClassFirst(string className)
        {
            using (var documents = _geckoWebBrowser.Document.GetElementsByClassName(className)[0])
            {
                GeckoButtonElement button = new GeckoButtonElement(documents.DomObject);
                button.Click();
            }
        }

        /// <summary>
        /// 获取元素特性值
        /// </summary>
        /// <param name="dom"></param>
        /// <param name="className"></param>
        /// <param name="attrElement"></param>
        /// <returns></returns>
        public string GetDomAttrByClassFirst(GeckoEnum.HtmlDom dom, string className, string attrElement)
        {
            string attribute = string.Empty;
            using (var documents = _geckoWebBrowser.Document.GetElementsByClassName(className)[0])
            {
                GeckoHtmlElement element = null;
                switch (dom)
                {
                    case GeckoEnum.HtmlDom.Div:
                        element = new GeckoDivElement(documents.DomObject);
                        break;
                }

                if (element != null)
                {
                    attribute = element.GetAttribute(attrElement);
                }
            }
            return attribute;
        }

        /// <summary>
        /// 获取元素特性值
        /// </summary>
        /// <param name="dom"></param>
        /// <param name="className"></param>
        /// <param name="attrElement"></param>
        /// <returns></returns>
        public string GetDomAttrByClassLast(GeckoEnum.HtmlDom dom, string className, string attrElement)
        {
            string attribute = string.Empty;
            var classes = _geckoWebBrowser.Document.GetElementsByClassName(className);
            using (var documents = _geckoWebBrowser.Document.GetElementsByClassName(className)[classes.Length-1])
            {
                GeckoHtmlElement element = null;
                switch (dom)
                {
                    case GeckoEnum.HtmlDom.Div:
                        element = new GeckoDivElement(documents.DomObject);
                        break;
                }

                if (element != null)
                {
                    attribute = element.GetAttribute(attrElement);
                }
            }
            return attribute;
        }

        /// <summary>
        /// 向页面中添加内容（样式、脚本等）
        /// </summary>
        /// <param name="content">要添加的内容</param>
        /// <param name="type">内容类型（“text/javascript”、“text/css”）</param>
        /// <param name="tagName">添加的标签名（link,script,body,div....）</param>
        /// <param name="appendTagName">在页面中被追加的元素（head/body...），如果目标有多个，则默认取第一个，若不填，则默认为body</param>
        public void RegString(string content, string type, string tagName, string appendTagName)
        {
            try
            {
                if (content == null || content.Length <= 0)
                {
                    return;
                }
                appendTagName = (appendTagName == null || appendTagName.Length <= 0 ? "body" : appendTagName);
                GeckoHtmlElement he = (GeckoHtmlElement)_geckoWebBrowser.Document.CreateElement(tagName);
                he.SetAttribute("type", type);
                he.InnerHtml = content;
                // he.SetAttribute("text", content);
                GeckoElementCollection hec = _geckoWebBrowser.Document.GetElementsByTagName(appendTagName);
                if (hec != null && hec.Length > 0)
                {
                    hec[0].AppendChild(he);
                    //wb.Document.StyleSheets
                }
                else
                {

                    _geckoWebBrowser.Document.Body.AppendChild(he);

                }
            }
            catch { }
        }

        public void IframeRegString(string iframeId,string content, string type, string tagName, string appendTagName)
        {
            if (content == null || content.Length <= 0)
            {
                return;
            }
            try
            {
                using (var document = _geckoWebBrowser.Document.GetElementById("alibaba-register-box"))
                {
                    var iframe = new GeckoIFrameElement(document.DOMElement).ContentDocument;

                    appendTagName = (appendTagName == null || appendTagName.Length <= 0 ? "body" : appendTagName);
                    GeckoHtmlElement he = (GeckoHtmlElement)iframe.CreateElement(tagName);
                    he.SetAttribute("type", type);
                    he.InnerHtml = content;
                    // he.SetAttribute("text", content);
                    GeckoElementCollection hec = iframe.GetElementsByTagName(appendTagName);
                    if (hec != null && hec.Length > 0)
                    {
                        hec[0].AppendChild(he);
                        //wb.Document.StyleSheets
                    }
                    else
                    {
                        iframe.Body.AppendChild(he);
                    }
                }
            }
            catch { }
        }

        public void EditJsCodebyContains() //string filname, string containsContent, string editContent
        {
            using (var document = _geckoWebBrowser.Document.GetElementsByClassName("touclick-ck-image")[0])
            {
                GeckoImageElement button = new GeckoImageElement(document.DomObject);
                button.Height = 12;
                button.Width = 12;
                button.Click();
                button.Click();


            }
        }


        /// <summary>
        /// 根据class判断是否存在该元素
        /// </summary>
        /// <param name="className"></param>
        /// <returns></returns>
        public bool ExistsElementByClass(string className)
        {
            bool result = false;
            var documents = _geckoWebBrowser.Document.GetElementsByClassName(className);
            if (documents.Length>0)
                    result = true;
            return result;
        }

        #endregion
    }
}
