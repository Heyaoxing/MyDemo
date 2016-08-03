using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gecko.DOM;

namespace Demo.WinForm.GeckoFX.Controller
{
    public class ArticleController : BaseController
    {
        public static bool isTitle = false;
        public static bool isContent = false;
        public static bool isImage = false;
        public static void   Title()
        {
            _geckoFxHelper.SetAttrElementById("tbTitle", "value", "测试标题");
            isTitle = true;
        }

        public static void Content()
        {
            _geckoFxHelper.InsertContextByIdWithTextArea("tbContent", "测试文章内容");
            isContent = true;
        }

        public static void UpLoadImage()
        {
            using (var documents = _geckoWebBrowser.Document.GetElementById("fileProduct"))
            {
                GeckoInputElement input = new GeckoInputElement(documents.DOMElement);
                var a = input.Value = @"C:\Users\pc\Desktop\base.jpg";
                var b = input.OuterHtml;
            }
        }
    }
}
