using System;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.Redis.Tests;
using log4net.Repository.Hierarchy;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Demo.Mvc.Common.Startup))]

namespace Demo.Mvc.Common
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // 有关如何配置应用程序的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=316888
            GlobalConfiguration.Configuration.UseRedisStorage(RedisUtils.GetHostAndPort());


            app.UseHangfireDashboard();
          //  app.UseHangfireDashboard("/jobs");   //指定访问路径
            app.UseHangfireServer();
        }
    }
}
