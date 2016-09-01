using System.Web;
using System.Web.Mvc;

namespace Demo.Mvc.Common
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
