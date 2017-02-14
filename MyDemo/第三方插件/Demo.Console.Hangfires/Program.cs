using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.Hangfire.Jobs;
using Hangfire;
using Hangfire.Redis;
using Hangfire.Redis.Tests;
using Hangfire.Server;
using Hangfire.SqlServer;
using StackExchange.Redis;

namespace Demo.Console.Hangfires
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                GlobalConfiguration.Configuration
                .UseColouredConsoleLogProvider()
                .UseRedisStorage(RedisUtils.GetHostAndPort());


                //然后需要推送的时候，调用下面的方法即可
                BackgroundJob.Enqueue(() => JobClient.Send(PerformContextToken.Null, "hello"));


                //JobStorage.Current = new RedisStorage(RedisUtils.GetHostAndPort());
                //BackgroundJobServer _server = new BackgroundJobServer();
                //_server.Start();
            }
            catch (Exception exception)
            {
                System.Console.WriteLine(exception);
                System.Console.Read();
            }
        }
    }
}
