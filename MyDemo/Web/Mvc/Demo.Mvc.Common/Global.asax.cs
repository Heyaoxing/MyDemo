using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Demo.Mvc.Data;
using Demo.Mvc.Services;
using Demo.Mvc.Dll;

namespace Demo.Mvc.Common
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();
            SetupResolveRules(builder);
            builder.RegisterControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));


            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void SetupResolveRules(ContainerBuilder builder)
        {
            builder.RegisterType<CompanyResitory>().As<ICompanyResitory>();
            builder.RegisterType<CompanyService>().As<ICompanyService>()
                        .OnRegistered(e =>System.Diagnostics.Debug.WriteLine("在注册的时候调用!"))
                        .OnPreparing(e => System.Diagnostics.Debug.WriteLine("在准备创建的时候调用!"))
                        .OnActivating(e => System.Diagnostics.Debug.WriteLine("在创建之前调用!"))
                        .OnActivated(e => System.Diagnostics.Debug.WriteLine("创建之后调用!"+e))
                        .OnRelease(e => System.Diagnostics.Debug.WriteLine("在释放占用的资源之前调用!"));
        }
    }
}
