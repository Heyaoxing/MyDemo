using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Hangfire.Console;
using Hangfire.Server;

namespace Demo.Hangfire.Jobs
{
    public class JobClient
    {

        /// <summary>
        /// 这个是用来发送消息的静态方法
        /// </summary>
        /// <param name="message"></param>
        [DisplayName("这里可以注明job名称,方便查看")]
        public static void Send(PerformContext context,string message)
        {
            System.Console.WriteLine(DateTime.Now + "开始执行任务");
            var progressBar = context.WriteProgressBar();
            context.SetTextColor(ConsoleTextColor.Red);
            for (int i = 0; i < 100; i++)
            {
                progressBar.SetValue(i);
                Thread.Sleep(1000);
                Console.WriteLine(context+"运行时间为:" + DateTime.Now);
            }
            System.Console.WriteLine(DateTime.Now + "这是由Hangfire后台任务发送的消息:" + message);
        }

       

    }
}
