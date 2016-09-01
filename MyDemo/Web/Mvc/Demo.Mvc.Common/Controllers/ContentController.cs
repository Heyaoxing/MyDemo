using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Demo.Mvc.Services;

namespace Demo.Mvc.Common.Controllers
{
    public class ContentController : Controller
    {
           public ICompanyService _companyReposity;
           public ContentController(ICompanyService companyReposity)
        {
            _companyReposity = companyReposity;
        }
        //
        // GET: /Content/
        public ActionResult Index()
        {
            ViewBag.Message = _companyReposity.Say();
            return View();
        }
	}
}