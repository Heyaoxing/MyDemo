﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.Redis;
using Hangfire.Redis.Tests;

namespace Demo.Console.Hangfires.server
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                JobStorage.Current = new RedisStorage(RedisUtils.GetHostAndPort());
                BackgroundJobServer _server = new BackgroundJobServer();
                _server.Start();
            }
            catch (Exception exception)
            {
                System.Console.WriteLine(exception);
            }
            System.Console.Read();
        }
    }
}
