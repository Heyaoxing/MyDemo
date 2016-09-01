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
        private string _result = "来自数据层的数据";

        public CompanyResitory()
        {
            
        }
        public CompanyResitory(string result)
        {
            _result = result;
        }
        public string Select()
        {
            return _result;
        }
    }
}
