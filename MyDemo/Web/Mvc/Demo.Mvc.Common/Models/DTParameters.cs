using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo.Mvc.Common.Models
{
    public class DTParameters
    {
        public int sEcho { set; get; }
        public int iColumns { set; get; }
        public string sColumns { set; get; }
        public int iDisplayStart { set; get; }
        public int iDisplayLength { set; get; }
        public string iSortCol_0 { set; get; }
    }

    public class ParameterDt
    {
        public JsonParameter[] data { set; get; }
    }

    public class JsonParameter
    {
        public string name { set; get; }
        public object value { set; get; }
    }
}