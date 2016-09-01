using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.Mvc.Dll;

namespace Demo.Mvc.Data
{
    public class CompanyResitory : ICompanyResitory
    {
        public string Select()
        {
            return "来自数据层的数据";
        }
    }
}
