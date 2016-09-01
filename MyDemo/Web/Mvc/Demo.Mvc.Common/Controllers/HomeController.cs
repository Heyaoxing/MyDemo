using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Demo.Mvc.Services;

namespace Demo.Mvc.Common.Controllers
{
    public class HomeController : Controller
    {
        public ICompanyService _companyReposity { set; get; }
       
        public ActionResult Index()
        {
            ViewBag.Message = _companyReposity.Say();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}