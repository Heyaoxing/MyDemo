using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.Console.Autofac.Interface;

namespace Demo.Console.Autofac.Implement
{
    public class CompanyReposity : ICompanyReposity
    {
        public string Say()
        {
            return "hello";
        }
    }
}
