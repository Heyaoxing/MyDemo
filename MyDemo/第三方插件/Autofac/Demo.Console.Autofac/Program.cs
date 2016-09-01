using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Demo.Console.Autofac.Implement;
using Demo.Console.Autofac.Interface;

namespace Demo.Console.Autofac
{
    class Program
    {
        static void Main(string[] args)
        {
            ContainerBuilder builder = new ContainerBuilder();

            builder.Register(c => new AutoFacManager(c.Resolve<ICompanyReposity>()));
            builder.RegisterType<CompanyReposity>().As<ICompanyReposity>();

            using (IContainer container = builder.Build())
            {
                AutoFacManager manager = container.Resolve<AutoFacManager>();
                manager.Say();
            }

            System.Console.Read();
        }
    }

    public class AutoFacManager
    {
        ICompanyReposity person;

        public AutoFacManager(ICompanyReposity MyPerson)
        {
            person = MyPerson;
        }

        public void Say()
        {
            System.Console.WriteLine(person.Say());
        }
    }
}
