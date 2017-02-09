using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Hangfire.Jobs
{
    public class JobClient
    {

        /// <summary>
        /// 这个是用来发送消息的静态方法
        /// </summary>
        /// <param name="message"></param>
        public static void Send(string message)
        {
            System.Console.WriteLine(DateTime.Now+"这是由Hangfire后台任务发送的消息:" + message);
        }
    }
}
