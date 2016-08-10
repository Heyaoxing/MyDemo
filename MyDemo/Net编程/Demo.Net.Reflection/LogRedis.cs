using Demo.Net.IReflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Net.Reflection
{
   public class LogRedis:ILog
    {
       public void WriteLog(string content)
       {
           Console.WriteLine("LogRedis:" + content);
       }
    }
}
