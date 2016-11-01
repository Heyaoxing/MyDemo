using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Demo.Mvc.Common.Extensions
{
    public class BootstrapHelper:HtmlHelper
    {
        public BootstrapHelper(ViewContext viewContext, IViewDataContainer viewDataContainer) : base(viewContext, viewDataContainer)
        {
        }

        public BootstrapHelper(ViewContext viewContext, IViewDataContainer viewDataContainer, RouteCollection routeCollection) : base(viewContext, viewDataContainer, routeCollection)
        {
        }
    }

    public static class LabelExtensions
    {
        /// <summary>
        /// 通过使用指定的 HTML 帮助器和窗体字段的名称，返回Label标签
        /// </summary>
        /// <param name="html">扩展方法实例</param>
        /// <param name="id">标签的id</param>
        /// <param name="content">标签的内容</param>
        /// <param name="cssClass">标签的class样式</param>
        /// <param name="htmlAttributes">标签的额外属性（如果属性里面含有“-”，请用“_”代替）</param>
        /// <returns>label标签的html字符串</returns>
        public static MvcHtmlString Label(this BootstrapHelper html, string id, string content, string cssClass, object htmlAttributes)
        {
            //定义标签的名称
            TagBuilder tag = new TagBuilder("label");
            //给标签增加额外的属性
            IDictionary<string, object> attributes = BootstrapHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            if (!string.IsNullOrEmpty(id))
            {
                tag.GenerateId(id);
            }
            if (!string.IsNullOrEmpty(cssClass))
            {
                //给标签增加样式
                tag.AddCssClass(cssClass);
            }
            //给标签增加文本
            tag.SetInnerText(content);
            tag.AddCssClass("control-label");
            tag.MergeAttributes(attributes);
            return MvcHtmlString.Create(tag.ToString());
        }
    }
}