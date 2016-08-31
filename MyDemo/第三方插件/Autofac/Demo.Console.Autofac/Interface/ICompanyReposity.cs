using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Console.Autofac.Interface
{
    public interface ICompanyReposity
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        string Select();
    }
}
