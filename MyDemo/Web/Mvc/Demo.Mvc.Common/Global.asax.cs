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

            builder.RegisterCallback(cr =>
            {
                // 下面的Registered事件相当类型的OnRegistered事件
                cr.Registered += (sender, eventArgs) =>
                {
                    // OnPreparing事件
                    eventArgs.ComponentRegistration.Preparing += (o, preparingEventArgs) =>
                    {
                        System.Diagnostics.Debug.WriteLine("OnPreparing事件!" +o.ToString());
                    };
                    // OnActivating事件
                    eventArgs.ComponentRegistration.Activating += (o, activatingEventArgs) =>
                    {
                        System.Diagnostics.Debug.WriteLine("OnActivating事件!");
                    };
                    // OnActivated事件
                    eventArgs.ComponentRegistration.Activated += (o, activatedEventArgs) =>
                    {
                        System.Diagnostics.Debug.WriteLine("OnActivated事件!" + activatedEventArgs.Context);
                    };
                };
            });


            builder.RegisterType<CompanyResitory>().As<ICompanyResitory>();

            builder.RegisterType<CompanyService>().As<ICompanyService>();

        }
    }
}
