using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Demo.Mvc.Services;
using Demo.Mvc.Common.Models;
using Webdiyer.WebControls.Mvc;

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
            return View();
        }

        public ActionResult GetList(JsonParameter[] param)
        {
            List<Customer> data = Bind();
            DTResult<Customer> result = new DTResult<Customer>
            {
                draw =1,
                data = data,
                recordsFiltered = data.Count,
                recordsTotal = data.Count
            };

            return Json(result);
        }


        /// <summary>
        /// 分页控件
        /// </summary>
        /// <returns></returns>
        public ActionResult Person(int pageindex = 1)
        {
            List<Customer> data = Bind();
            PagedList<Customer> InfoPager = data.AsQueryable().ToPagedList(pageindex, 2);
            InfoPager.TotalItemCount = data.Count;
            InfoPager.CurrentPageIndex = pageindex;
            return View(InfoPager);
        }





        private List<Customer> Bind()
        {
            List<Customer> data = new List<Customer>();
            data.Add(new Customer()
            {
                Id = 1,
                Name = "我是谁",
                Descript = "不可描述"
            });
            data.Add(new Customer()
            {
                Id = 2,
                Name = "我是谁",
                Descript = "不可描述"
            });
            data.Add(new Customer()
            {
                Id = 3,
                Name = "我是谁",
                Descript = "不可描述"
            });
            for (int i = 0; i < 20; i++)
            {
                data.Add(new Customer()
                {
                    Id = i + 3,
                    Name = "我是谁",
                    Descript = "不可描述"
                });
            }

            return data;
        }
    }
}