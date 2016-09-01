using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.Mvc.Data;

namespace Demo.Mvc.Services
{
    public class CompanyService : ICompanyService
    {
        private ICompanyResitory _companyResitory;

        public CompanyService(ICompanyResitory companyResitory)
        {
            _companyResitory = companyResitory;
        }
        public string Say()
        {
            return _companyResitory.Select();
        }
    }
}
