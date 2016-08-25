using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Net.StartUpProgram
{
    public class StartUpProgram
    {
        /// <summary>
        /// 方法一:通过路径启动
        /// </summary>
        /// <returns></returns>
        public bool Method1(string programPath)
        {
            try
            {
                Process.Start(programPath);
                return true;
            }
            catch (Exception ex)
            {
            }
            return false;
        }

        /// <summary>
        /// 方法二:调用系统dll使用其提供的方法
        /// </summary>
        /// <param name="exeName">需要启动的文件路径</param>
        /// <param name="operType">
        ///0: 隐藏, 并且任务栏也没有最小化图标  
        ///1: 用最近的大小和位置显示, 激活  
        ///2: 最小化, 激活  
        ///3: 最大化, 激活  
        ///4: 用最近的大小和位置显示, 不激活  
        ///5: 同 1  
        ///6: 最小化, 不激活  
        ///7: 同 3  
        ///8: 同 3  
        ///9: 同 1  
        ///10: 同 1 
        ///</param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern int WinExec(string exeName, int operType);


        /// <summary>
        /// 获取当前运行进程中相同进程名的进程数量
        /// </summary>
        /// <returns></returns>
        public int GetProgramCount()
        {
            Process[] localProcessCount = Process.GetProcessesByName("TestConsole");
            if (localProcessCount != null)
                return localProcessCount.Count();
            else
                return 0;
        } 
    }
}
