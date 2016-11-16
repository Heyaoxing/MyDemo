using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc.Ajax;
namespace Webdiyer.WebControls.Mvc
{
    public class MvcAjaxOptions : AjaxOptions
    {
        public bool EnablePartialLoading { get; set; }
        public string DataFormId { get; set; }
    }
}