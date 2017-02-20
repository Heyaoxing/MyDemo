using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.Redis;
using Hangfire.Redis.Tests;
using Hangfire.Server;
using Hangfire.Storage.Monitoring;
using Microsoft.Owin.Builder;
using Owin;

namespace Demo.Console.Hangfires.server
{
    class Program
    {
      

        static void Main(string[] args)
        {
           

            try
            {
                JobStorage.Current = new RedisStorage(RedisUtils.GetHostAndPort());

         
                BackgroundJobServer _server = new BackgroundJobServer(new BackgroundJobServerOptions()
                {
                    ServerName = "Console端",
                    WorkerCount = 1
                });

            }
            catch (Exception exception)
            {
                System.Console.WriteLine(exception);
            }
            System.Console.Read();
        }
    }
}
