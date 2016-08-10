using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Net.ReflectionDebugByConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Reflection.Assembly ass = Assembly.LoadFrom("Demo.Net.Reflection.dll"); //加载DLL
            System.Type t = ass.GetType("Demo.Net.Reflection.LogRedis");//获得类型

            object o = System.Activator.CreateInstance(t);//创建实例
            System.Reflection.MethodInfo mi = t.GetMethod("WriteLog");//获得方法
            mi.Invoke(o, new object[] { "'测试反射机制'" });//调用方法
           Console.Read();
        }
    }
}
