using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo.Net.ClickBombbox
{
    public class GetAllWindows
    {
        private delegate bool WNDENUMPROC(IntPtr hWnd, int lParam);
        /// <summary>
        /// 该函数枚举所有屏幕上的顶层窗口，并将窗口句柄传送给应用程序定义的回调函数
        /// </summary>
        /// <param name="lpEnumFunc">指向一个应用程序定义的回调函数指针</param>
        /// <param name="lParam">指定一个传递给回调函数的应用程序定义值</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        private static extern bool EnumWindows(WNDENUMPROC lpEnumFunc, int lParam);

        /// <summary>
        /// 检索处理顶级窗口的类名和窗口名称匹配指定的字符串。这个函数不搜索子窗口
        /// </summary>
        /// <param name="lpClassName">指向类名</param>
        /// <param name="lpWindowName">指向窗口名</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        private static extern IntPtr FindWindowW(string lpClassName, string lpWindowName);


        /// <summary>
        /// 该函数将指定窗口的标题条文本（如果存在）拷贝到一个缓存区内。如果指定的窗口是一个控件，则拷贝控件的文本
        /// </summary>
        /// <param name="hWnd">带文本的窗口或控件的句柄</param>
        /// <param name="lpString">指向接收文本的缓冲区的指针</param>
        /// <param name="nMaxCount">指定要保存在缓冲区内的字符的最大个数，其中包含NULL字符。如果文本超过界限，它就被截断</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        private static extern int GetWindowTextW(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)]StringBuilder lpString, int nMaxCount);

       
        [DllImport("user32.dll")]
        private static extern int GetClassNameW(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)]StringBuilder lpString, int nMaxCount);

        /// <summary>
        /// 为指定的窗口调用窗口程序，直到窗口程序处理完消息再返回
        /// </summary>
        /// <param name="hWnd">其窗口程序将接收消息的窗口的句柄。如果此参数为HWND_BROADCAST，则消息将被发送到系统中所有顶层窗口，包括无效或不可见的非自身拥有的窗口、被覆盖的窗口和弹出式窗口，但消息不被发送到子窗口</param>
        /// <param name="Msg">指定被发送的消息</param>
        /// <param name="wParam">指定附加的消息特定信息</param>
        /// <param name="lParam">指定附加的消息特定信息</param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// 返回与指定窗口有特定关系（如Z序或所有者）的窗口句柄
        /// </summary>
        /// <param name="hWnd">窗口句柄。这个函数要返回的窗口句柄是依据nCmd参数值相对于hWnd参数的关系</param>
        /// <param name="uCmd">说明指定窗口与要获得句柄的窗口之间的关系。该参数值可以是下列之一：
        /*GW_CHILD(&H5)：如果指定窗口是父窗口，则获得的是在Tab序顶端的子窗口的句柄，否则为NULL。函数仅检查指定父窗口的子窗口，不检查继承窗口。
        GW_ENABLEDPOPUP(&H6)：（WindowsNT 5.0）返回的句柄标识了属于指定窗口的处于使能状态弹出式窗口（检索使用第一个由GW_HWNDNEXT 查找到的满足前述条件的窗口）；如果无使能窗口，则获得的句柄与指定窗口相同。
        GW_HWNDFIRST(&H0)：返回的句柄标识了在Z序最高端的相同类型的窗口。如果指定窗口是最高端窗口，则该句柄标识了在Z序最高端的最高端窗口；如果指定窗口是顶层窗口，则该句柄标识了在z序最高端的顶层窗口：如果指定窗口是子窗口，则句柄标识了在Z序最高端的同属窗口。
        GW_HWNDLAST(&H1)：返回的句柄标识了在z序最低端的相同类型的窗口。如果指定窗口是最高端窗口，则该柄标识了在z序最低端的最高端窗口：如果指定窗口是顶层窗口，则该句柄标识了在z序最低端的顶层窗口；如果指定窗口是子窗口，则句柄标识了在Z序最低端的同属窗口。
        GW_HWNDNEXT(&H2)：返回的句柄标识了在Z序中指定窗口下的相同类型的窗口。如果指定窗口是最高端窗口，则该句柄标识了在指定窗口下的最高端窗口：如果指定窗口是顶层窗口，则该句柄标识了在指定窗口下的顶层窗口；如果指定窗口是子窗口，则句柄标识了在指定窗口下的同属窗口。
        GW HWNDPREV(&H3)：返回的句柄标识了在Z序中指定窗口上的相同类型的窗口。如果指定窗口是最高端窗口，则该句柄标识了在指定窗口上的最高端窗口；如果指定窗口是顶层窗口，则该句柄标识了在指定窗口上的顶层窗口；如果指定窗口是子窗口，则句柄标识了在指定窗口上的同属窗口。
        GW_OWNER(&H4)：返回的句柄标识了指定窗口的所有者窗口（如果存在）。GW_OWNER与GW_CHILD不是相对的参数，没有父窗口的含义，如果想得到父窗口请使用GetParent()。例如：例如有时对话框的控件的GW_OWNER，是不存在的。
        返回值：如果函数成功，返回值为窗口句柄；如果与指定窗口有特定关系的窗口不存在，则返回值为NULL。
        若想获得更多错误信息，请调用GetLastError函数。
        备注：在循环体中调用函数EnumChildWindow比调用GetWindow函数可靠。调用GetWindow函数实现该任务的应用程序可能会陷入死循环或退回一个已被销毁的窗口句柄</param>
        */
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);


        /// <summary>
        /// 获得指定窗口所属的类的类名
        /// </summary>
        /// <param name="hWnd">窗口的句柄及间接给出的窗口所属的类</param>
        /// <param name="lpClassName">指向接收窗口类名字符串的缓冲区的指针</param>
        /// <param name="nMaxCount">指定由参数lpClassName指示的缓冲区的字节数。如果类名字符串大于缓冲区的长度，则多出的部分被截断</param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        /// <summary>
        /// 为指定的窗口调用窗口程序，直到窗口程序处理完消息再返回
        /// </summary>
        /// <param name="hWnd">其窗口程序将接收消息的窗口的句柄。如果此参数为HWND_BROADCAST，则消息将被发送到系统中所有顶层窗口，包括无效或不可见的非自身拥有的窗口、被覆盖的窗口和弹出式窗口，但消息不被发送到子窗口</param>
        /// <param name="Msg">指定被发送的消息</param>
        /// <param name="wParam">指定附加的消息特定信息</param>
        /// <param name="lParam">指定附加的消息特定信息</param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hwnd, uint wMsg, int wParam, int lParam);

        /// <summary>
        /// 为指定的窗口调用窗口程序，直到窗口程序处理完消息再返回
        /// </summary>
        /// <param name="hWnd">其窗口程序将接收消息的窗口的句柄。如果此参数为HWND_BROADCAST，则消息将被发送到系统中所有顶层窗口，包括无效或不可见的非自身拥有的窗口、被覆盖的窗口和弹出式窗口，但消息不被发送到子窗口</param>
        /// <param name="Msg">指定被发送的消息</param>
        /// <param name="wParam">指定附加的消息特定信息</param>
        /// <param name="lParam">指定附加的消息特定信息</param>
        /// <returns></returns>
        [DllImport("User32.DLL")]
        public static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, string lParam);
        public const uint WM_SETTEXT = 0x000C;
        public const uint EM_SETSEL = 0x00B1;//光标移动到最后

        [DllImport("user32.dll")]
        public static extern int GetWindowText(int hWnd, StringBuilder lpString, int nMaxCount);

        /// <summary>
        /// 枚举一个父窗口的所有子窗口
        /// </summary>
        /// <param name="hWndParent">父窗口句柄</param>
        /// <param name="lpfn">回调函数的地址</param>
        /// <param name="lParam">自定义的参数</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int EnumChildWindows(int hWndParent, CallBack lpfn, int lParam);

        /// <summary>
        /// 找出某个窗口的创建者（线程或进程），返回创建者的标志符
        /// </summary>
        /// <param name="hwnd">被查找窗口的句柄</param>
        /// <param name="pid">进程号的存放地址</param>
        /// <returns></returns>
        [DllImport("user32", EntryPoint = "GetWindowThreadProcessId")]
        private static extern int GetWindowThreadProcessId(IntPtr hwnd, out int pid);

        /// <summary>
        /// 获得一个指定子窗口的父窗口句柄
        /// </summary>
        /// <param name="hWnd">子窗口句柄，函数要获得该子窗口的父窗口句柄</param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "GetParent", SetLastError = true)]
        public static extern IntPtr GetParent(IntPtr hWnd);

        /// <summary>
        /// 打开一个已存在的进程对象，并返回进程的句柄
        /// </summary>
        /// <param name="fdwAccess"></param>
        /// <param name="fInherit"></param>
        /// <param name="IDProcess"></param>
        /// <returns></returns>
        [DllImport("Kernel32.dll")]
        public extern static IntPtr OpenProcess(int fdwAccess, int fInherit, int IDProcess);

        /// <summary>
        /// 获取当前进程已加载模块的文件的完整路径，该模块必须由当前进程加载
        /// </summary>
        /// <param name="hModule">一个模块的句柄。可以是一个DLL模块，或者是一个应用程序的实例句柄。如果该参数为NULL</param>
        /// <param name="lpszFileName">指定一个字串缓冲区，要在其中容纳文件的用NULL字符中止的路径名，hModule模块就是从这个文件装载进来的</param>
        /// <param name="nSize">装载到缓冲区lpFileName的最大字符数量</param>
        /// <returns></returns>
        [DllImport("kernel32.dll", EntryPoint = "GetModuleFileName")]
        private static extern uint GetModuleFileName(IntPtr hModule, [Out] StringBuilder lpszFileName, int nSize);


        /// <summary>
        /// 回调函数代理
        /// </summary>
        public delegate bool CallBack(int hwnd, int lParam);
        /// <summary>
        /// 子窗口回调函数代理
        /// </summary>
        public static CallBack callBackEnumChildWindows = new CallBack(ChildWindowProcess);

        /// <summary>
        /// 窗口回调处理函数
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        public static bool WindowProcess(int hwnd, int lParam)
        {
            EnumChildWindows(hwnd, callBackEnumChildWindows, 0);
            return true;
        }

        private static string strImgPate;
        /// <summary>
        /// 图片路径
        /// </summary>
        public static string StrImgPate
        {
            get { return GetAllWindows.strImgPate; }
            set { GetAllWindows.strImgPate = value; }
        }

        private static bool _b_type_img;
        /// <summary>
        /// 判断图片是否上传
        /// </summary>
        public static bool B_type_img
        {
            get { return GetAllWindows._b_type_img; }
            set { GetAllWindows._b_type_img = value; }
        }

        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int uMsg, int wParam, StringBuilder lParam);

        private static IntPtr i_intPtr_hd = (IntPtr)123456;

        private static IntPtr _i_cancel;
        public static IntPtr I_cancel
        {
            get { return GetAllWindows._i_cancel; }
            set { GetAllWindows._i_cancel = value; }
        }

        /// <summary>
        /// 子窗口回调处理函数
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        public static bool ChildWindowProcess(int hwnd, int lParam)
        {
            try
            {
                //StringBuilder title = new StringBuilder(200);

                StringBuilder sb = new StringBuilder(512);
                GetWindowText(hwnd, sb, sb.Capacity);
                Console.WriteLine(sb.ToString() + " " + lParam + " " + hwnd + " " + GetWindowClassName((IntPtr)hwnd));

                if (GetWindowClassName((IntPtr)hwnd) == "Edit")
                {
                    i_intPtr_hd = (IntPtr)123456;

                    SendMessage((IntPtr)hwnd, WM_SETTEXT, 0, StrImgPate);

                    SendMessage((IntPtr)hwnd, EM_SETSEL, int.MaxValue, int.MaxValue); // 光标移到最后

                    StringBuilder sbImg = new StringBuilder(1024);
                    const int WM_GETTEXT = 0x000D;
                    SendMessage((IntPtr)hwnd, WM_GETTEXT, sbImg.Capacity, sbImg);
                    string str_edit_img = sbImg.ToString().Replace("/", "\\");
                    if (str_edit_img.Trim() != strImgPate.Trim())
                    {
                        Console.WriteLine("第二次");
                        SendMessage((IntPtr)hwnd, WM_SETTEXT, 0, StrImgPate);

                        SendMessage((IntPtr)hwnd, EM_SETSEL, int.MaxValue, int.MaxValue); // 光标移到最后

                    }

                    i_intPtr_hd = (IntPtr)hwnd;

                    Console.WriteLine(sbImg.ToString());
                }
                if (GetWindowClassName((IntPtr)hwnd) == "Button" && (sb.ToString().Contains("打开") || sb.ToString().Contains("确定")))
                {
                    if (i_intPtr_hd != (IntPtr)123456)
                    {

                        StringBuilder sbImg = new StringBuilder(1024);
                        const int WM_GETTEXT = 0x000D;
                        SendMessage(i_intPtr_hd, WM_GETTEXT, sbImg.Capacity, sbImg);
                        string str_edit_img = sbImg.ToString().Replace("/", "\\");
                        if (str_edit_img.Trim() == strImgPate.Trim())
                        {
                            const uint downCode = 0x201; // Left click down code 
                            const uint upCode = 0x202; // Left click up code 

                            SendMessage((IntPtr)hwnd, downCode, (IntPtr)0, (IntPtr)0); // Mouse button down 
                            SendMessage((IntPtr)hwnd, upCode, (IntPtr)0, (IntPtr)0); // Mouse button up 
                            GetAllWindows.B_type_img = true;
                        }
                        Console.WriteLine("点击了");
                    }


                }
                //if (!GetAllWindows.B_type_img)
                //{

                if (GetWindowClassName((IntPtr)hwnd) == "Button" && (sb.ToString().Contains("取消") || sb.ToString().Contains("关闭")))
                {

                    _i_cancel = (IntPtr)hwnd;

                    //const uint downCode = 0x201; // Left click down code 
                    //const uint upCode = 0x202; // Left click up code 

                    //SendMessage((IntPtr)hwnd, downCode, (IntPtr)0, (IntPtr)0); // Mouse button down 
                    //SendMessage((IntPtr)hwnd, upCode, (IntPtr)0, (IntPtr)0); // Mouse button up 
                    //GetAllWindows.B_type_img = true;
                    //Console.WriteLine("关闭");
                    //log.Write("点击取消");

                }

                //}

                return true;
            }
            catch { return false; }
        }

       

        /// <summary>
        /// 根据句柄获取进程
        /// </summary>
        /// <param name="hwnd"></param>
        /// <returns></returns>
        public static String GetAppRunPathFromHandle(IntPtr hwnd)
        {
            try
            {
                string str_value = "";
                //获取父窗体句柄
                IntPtr PathIntPtr = GetParent(hwnd);

                System.Windows.Forms.Form f = (System.Windows.Forms.Form)Form.FromHandle(PathIntPtr); //获得父窗体
                if (f != null)
                {
                    str_value = f.Text.Trim();
                }

                if (string.IsNullOrEmpty(str_value))
                {
                    int i_process_id1 = 0;
                    int i_process_id = GetWindowThreadProcessId(hwnd, out i_process_id1);
                    //根据进程id获取进程路径
                    System.Diagnostics.Process pr = System.Diagnostics.Process.GetProcessById(i_process_id1);
                    string str_process_name = pr.MainModule.FileName;

                    //获取句柄的进程路径 先获取进程id 根据进程id 获取进程句柄 根据进程句柄获取进程路径
                    //int i_process_id = GetWindowThreadProcessId(hwnd, out i_process_id);
                    //IntPtr processHandle = IntPtr.Zero;
                    //processHandle = OpenProcess(0, 0, i_process_id);

                    //StringBuilder sb = new StringBuilder(512);
                    //GetModuleFileName((IntPtr)processHandle, sb, sb.Capacity);

                    str_value = str_process_name.ToString();

                }

                return str_value;

                //System.Diagnostics.Process pr = System.Diagnostics.Process.GetProcessById(i_process_id111);
                //string strrrr = pr.MainModule.FileName;

                //获取句柄的进程路径 先获取进程id 根据进程id 获取进程句柄 根据进程句柄获取进程路径
                //int i_process_id = GetWindowThreadProcessId(hwnd, out i_process_id);
                //IntPtr processHandle = IntPtr.Zero;
                //processHandle = OpenProcess(0, 0, i_process_id);

                //StringBuilder sb = new StringBuilder(512);
                //GetModuleFileName((IntPtr)processHandle, sb, sb.Capacity);

                //return sb.ToString();
            }
            catch (Exception ex) { return ""; }
        }

        //[DllImport("user32.dll", CharSet = CharSet.Auto)]
        //static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);
        /// <summary>
        /// 根据句柄 获取 类
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        public static string GetWindowClassName(IntPtr hWnd)
        {
            try
            {
                StringBuilder buffer = new StringBuilder(128);

                GetClassName(hWnd, buffer, buffer.Capacity);

                return buffer.ToString();
            }
            catch { return ""; }
        }

        public struct WindowInfo
        {
            public IntPtr hWnd;
            public string szWindowName;
            public string szClassName;
        }

        /// <summary>
        /// 获取所有窗体的句柄
        /// </summary>
        /// <returns></returns>
        public WindowInfo[] GetAllDesktopWindows()
        {
            List<WindowInfo> wndList = new List<WindowInfo>();

            //enum all desktop windows 
            EnumWindows(delegate(IntPtr hWnd, int lParam)
            {
                WindowInfo wnd = new WindowInfo();
                StringBuilder sb = new StringBuilder(256);
                //get hwnd 
                wnd.hWnd = hWnd;
                //get window name 
                GetWindowTextW(hWnd, sb, sb.Capacity);
                wnd.szWindowName = sb.ToString();
                //get window class 
                GetClassNameW(hWnd, sb, sb.Capacity);
                wnd.szClassName = sb.ToString();

                //StringBuilder className = new StringBuilder(100);
                //GetClassName(hWnd, className, className.Capacity);

                //add it into list 你可以在这里修改 过滤你要的控件进入列表

                try
                {

                    if ((wnd.szWindowName.Contains("选择") || wnd.szWindowName.Contains("文件") || wnd.szWindowName.Contains("打开") || wnd.szWindowName.Contains("file") || wnd.szWindowName.Contains("File")) || wnd.szClassName.Contains("对话框"))
                        wndList.Add(wnd);
                }
                catch { }

                return true;
            }, 0);
            //foreach (WindowInfo w in wndList)
            //{
            //    Console.WriteLine(w.szWindowName + " ");
            //}
            return wndList.ToArray();
        }

        public static WindowInfo[] GetAllDesktopWindows1()
        {
            List<WindowInfo> wndList = new List<WindowInfo>();

            //enum all desktop windows 
            EnumWindows(delegate(IntPtr hWnd, int lParam)
            {
                WindowInfo wnd = new WindowInfo();
                StringBuilder sb = new StringBuilder(256);
                //get hwnd 
                wnd.hWnd = hWnd;
                //get window name 
                GetWindowTextW(hWnd, sb, sb.Capacity);
                wnd.szWindowName = sb.ToString();
                //get window class 
                GetClassNameW(hWnd, sb, sb.Capacity);
                wnd.szClassName = sb.ToString();

                //StringBuilder className = new StringBuilder(100);
                //GetClassName(hWnd, className, className.Capacity);

                //add it into list 你可以在这里修改 过滤你要的控件进入列表

                try
                {
                    //Console.WriteLine(GetWindowClassName(hWnd).ToString());

                    if (wnd.szClassName.Contains("MozillaWindowClass"))
                        wndList.Add(wnd);
                }
                catch { }

                return true;
            }, 0);
            //foreach (WindowInfo w in wndList)
            //{
            //    Console.WriteLine(w.szWindowName + " ");
            //}
            return wndList.ToArray();
        }


        /// <summary>
        /// gecko 窗体获取 发送tab
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        public static bool WindowProcessGecko(int hwnd, int lParam)
        {
            EnumChildWindows(hwnd, callBackEnumChildWindowsGecko, 0);
            return true;
        }

        /// <summary>
        /// 子窗口回调函数代理 gecko
        /// </summary>
        public static CallBack callBackEnumChildWindowsGecko = new CallBack(ChildWindowProcessGecko);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        public static bool ChildWindowProcessGecko(int hwnd, int lParam)
        {
            try
            {
                //StringBuilder title = new StringBuilder(200);

                StringBuilder sb = new StringBuilder(512);
                GetWindowText(hwnd, sb, sb.Capacity);
                Console.WriteLine(sb.ToString() + " " + lParam + " " + hwnd + " " + GetWindowClassName((IntPtr)hwnd));

                if (GetWindowClassName((IntPtr)hwnd) == "MozillaWindowClass")
                {
                    const uint WM_KEYDOWN = 0x0100;
                    const uint WM_KEYUP = 0x0101;
                    const uint WM_SETTEXT = 0x000C;

                    const int VK_SHIFT = 0x10;
                    const int VK_TAB = 0x9;
                    const int VK_ENTER = 0xD;

                    // System.Windows.Forms.SendKeys.Flush();

                    //webBrowser1.Document.GetElementById("SWFUpload_0").Focus();

                    SendMessage((IntPtr)hwnd, WM_KEYDOWN, VK_TAB, 0);//发送tab
                    SendMessage((IntPtr)hwnd, WM_KEYDOWN, VK_ENTER, 0);//回车
                    SendMessage((IntPtr)hwnd, WM_KEYUP, VK_ENTER, 0);//回车
                    SendMessage((IntPtr)hwnd, WM_KEYUP, VK_TAB, 0);//释放tab


                    Console.WriteLine(hwnd);
                }

                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// 获取来自网页消息窗体句柄
        /// </summary>
        /// <returns></returns>
        public WindowInfo[] GetAllDesktopWindowsWeb()
        {
            List<WindowInfo> wndList = new List<WindowInfo>();

            //enum all desktop windows 
            EnumWindows(delegate(IntPtr hWnd, int lParam)
            {
                WindowInfo wnd = new WindowInfo();
                StringBuilder sb = new StringBuilder(256);
                //get hwnd 
                wnd.hWnd = hWnd;
                //提取 windows 窗口中的文字
                GetWindowTextW(hWnd, sb, sb.Capacity);
                wnd.szWindowName = sb.ToString();
                //get window class 
                GetClassNameW(hWnd, sb, sb.Capacity);
                wnd.szClassName = sb.ToString();

                //add it into list 你可以在这里修改 过滤你要的控件进入列表

                try
                {

                    if (wnd.szWindowName.Contains("mshta") || wnd.szWindowName.Contains("提示框"))
                         wndList.Add(wnd);

                    //if (wnd.szWindowName.Contains("来自网页的消息") || wnd.szClassName.Contains("MozillaDialogClass") ||
                    //    wnd.szClassName.ToLower().Contains("mozilladialogclass") ||
                    //    wnd.szWindowName.Contains("NetWin.Tools.Client") ||
                    //    wnd.szWindowName.ToLower().Contains("SynthEmailVerification2") ||
                    //    wnd.szWindowName.Contains("synthemailverification2") ||
                    //    wnd.szWindowName.ToLower().Contains("netwin.tools.client") ||
                    //    wnd.szWindowName.ToLower().Contains("plugin container for xulrunner") ||
                    //    wnd.szWindowName.ToLower().Contains("plugin container for firefox"))
                    //    wndList.Add(wnd);
                }
                catch { }

                return true;
            }, 0);
            //foreach (WindowInfo w in wndList)
            //{
            //    Console.WriteLine(w.szWindowName + " ");
            //}
            return wndList.ToArray();
        }


        /// <summary>
        /// 子窗口回调函数代理，点击来自网页信息
        /// </summary>
        public static CallBack callBackEnumChildWindowsWeb = new CallBack(ChildWindowProcessWeb);

        /// <summary>
        /// 窗口回调处理函数，点击来自网页信息
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        public static bool WindowProcessWeb(int hwnd, int lParam)
        {
            EnumChildWindows(hwnd, callBackEnumChildWindowsWeb, 0);
            return true;
        }

        /// <summary>
        /// 子窗口回调处理函数，点击来自网页信息
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        public static bool ChildWindowProcessWeb(int hwnd, int lParam)
        {
            try
            {
                //StringBuilder title = new StringBuilder(200);

                StringBuilder sb = new StringBuilder(512);
                GetWindowText(hwnd, sb, sb.Capacity);
                Console.WriteLine(sb.ToString() + " " + lParam + " " + hwnd + " " + GetWindowClassName((IntPtr)hwnd));

                if (GetWindowClassName((IntPtr)hwnd) == "Button" && (sb.ToString().Contains("确定") || sb.ToString().ToLower().Contains("ok") || sb.ToString().ToLower().Contains("关闭程序")))
                //if (GetWindowClassName((IntPtr)hwnd) == "Button" && (sb.ToString().Contains("切换网络")))
                {

                    const uint downCode = 0x201; // Left click down code 
                    const uint upCode = 0x202; // Left click up code 

                    SendMessage((IntPtr)hwnd, downCode, (IntPtr)0, (IntPtr)0); // Mouse button down 
                    SendMessage((IntPtr)hwnd, upCode, (IntPtr)0, (IntPtr)0); // Mouse button up 

                    // }
                    //if (!GetAllWindows.B_type_img)
                    //{


                }

                return true;
            }
            catch { return false; }
        }


        /// <summary>
        /// 发送 tab 点击
        /// </summary>
        /// <param name="iHandle"></param>
        /// <returns></returns>
        public static bool GetWindowTab(IntPtr iHandle)
        {
            try
            {
                IntPtr handle = iHandle;
                StringBuilder className = new StringBuilder(100);

                WindowInfo[] sdf = GetAllDesktopWindows1();

                // while (className.ToString() != "Internet Explorer_Server" || className.ToString() != "MozillaWindowClass" || className.ToString().ToLower() != "firefox") // The class control for the browser 
                while (className.ToString() != "MozillaWindowClass") // The class control for the browser 
                {
                    //className.Clear();
                    handle = GetWindow(handle, 5); // Get a handle to the child window 
                    // GetClassName(handle, className, className.Capacity);
                    className.Append(GetWindowClassName(handle));
                }

                try
                {
                    //className.Clear();
                    while (className.ToString() != "MozillaWindowClass") // The class control for the browser 
                    {
                        // className.Clear();
                        handle = GetWindow(handle, 5); // Get a handle to the child window 
                        // GetClassName(handle, className, className.Capacity);
                        className.Append(GetWindowClassName(handle));
                        //handle = GetWindow(handle, 5); // Get a handle to the child window 
                        //GetClassName(handle, className, className.Capacity);

                    }
                }
                catch { }



                const uint WM_KEYDOWN = 0x0100;
                const uint WM_KEYUP = 0x0101;
                const uint WM_SETTEXT = 0x000C;

                const int VK_SHIFT = 0x10;
                const int VK_TAB = 0x9;
                const int VK_ENTER = 0xD;

                System.Windows.Forms.SendKeys.Flush();

                //webBrowser1.Document.GetElementById("SWFUpload_0").Focus();

                SendMessage(handle, WM_KEYDOWN, VK_TAB, 0);//发送tab
                SendMessage(handle, WM_KEYDOWN, VK_ENTER, 0);//回车
                SendMessage(handle, WM_KEYUP, VK_ENTER, 0);//回车
                SendMessage(handle, WM_KEYUP, VK_TAB, 0);//释放tab

                return true;
            }
            catch { return false; }
        }



        /// <summary>
        /// 获取所有alert 弹框
        /// </summary>
        /// <returns></returns>
        public WindowInfo[] GetAllDesktopWindowsAlert()
        {
            List<WindowInfo> wndList = new List<WindowInfo>();

            //enum all desktop windows 
            EnumWindows(delegate(IntPtr hWnd, int lParam)
            {
                WindowInfo wnd = new WindowInfo();
                StringBuilder sb = new StringBuilder(256);
                //get hwnd 
                wnd.hWnd = hWnd;
                //get window name 
                GetWindowTextW(hWnd, sb, sb.Capacity);
                wnd.szWindowName = sb.ToString();
                //get window class 
                GetClassNameW(hWnd, sb, sb.Capacity);
                wnd.szClassName = sb.ToString();

                //StringBuilder className = new StringBuilder(100);
                //GetClassName(hWnd, className, className.Capacity);

                //add it into list 你可以在这里修改 过滤你要的控件进入列表

                try
                {

                    // if ((wnd.szWindowName.Contains("The page at")) || GetWindowClassName(hWnd).ToString().Contains("MozillaDialogClass"))
                    if (GetWindowClassName(hWnd).ToString().Contains("MozillaDialogClass"))
                        wndList.Add(wnd);
                }
                catch { }

                return true;
            }, 0);
            //foreach (WindowInfo w in wndList)
            //{
            //    Console.WriteLine(w.szWindowName + " ");
            //}
            return wndList.ToArray();
        }

        public static bool WindowProcessGeckoAlert(int hwnd, int lParam)
        {
            EnumChildWindows(hwnd, callBackEnumChildWindowsGeckoAlert, 0);
            return true;
        }

        /// <summary>
        /// 子窗口回调函数代理 gecko
        /// </summary>
        public static CallBack callBackEnumChildWindowsGeckoAlert = new CallBack(ChildWindowProcessGeckoAlert);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        public static bool ChildWindowProcessGeckoAlert(int hwnd, int lParam)
        {
            try
            {
                //StringBuilder title = new StringBuilder(200);

                StringBuilder sb = new StringBuilder(512);
                GetWindowText(hwnd, sb, sb.Capacity);
                Console.WriteLine(sb.ToString() + " " + lParam + " " + hwnd + " " + GetWindowClassName((IntPtr)hwnd));

                if (GetWindowClassName((IntPtr)hwnd) == "MozillaWindowClass")
                {
                    const uint WM_KEYDOWN = 0x0100;
                    const uint WM_KEYUP = 0x0101;
                    const uint WM_SETTEXT = 0x000C;

                    const int VK_SHIFT = 0x10;
                    const int VK_TAB = 0x9;
                    const int VK_ENTER = 0xD;

                    // System.Windows.Forms.SendKeys.Flush();

                    //webBrowser1.Document.GetElementById("SWFUpload_0").Focus();

                    SendMessage((IntPtr)hwnd, WM_KEYDOWN, VK_TAB, 0);//发送tab
                    SendMessage((IntPtr)hwnd, WM_KEYDOWN, VK_ENTER, 0);//回车
                    SendMessage((IntPtr)hwnd, WM_KEYUP, VK_ENTER, 0);//回车
                    SendMessage((IntPtr)hwnd, WM_KEYUP, VK_TAB, 0);//释放tab


                    Console.WriteLine(hwnd);
                }

                return true;
            }
            catch { return false; }
        }

        public static void WindowProcessGeckoAlert(int hwnd)
        {
            try
            {
                const uint WM_KEYDOWN = 0x0100;
                const uint WM_KEYUP = 0x0101;
                const uint WM_SETTEXT = 0x000C;

                const int VK_SHIFT = 0x10;
                const int VK_TAB = 0x9;
                const int VK_ENTER = 0xD;

                // System.Windows.Forms.SendKeys.Flush();

                //webBrowser1.Document.GetElementById("SWFUpload_0").Focus();

                SendMessage((IntPtr)hwnd, WM_KEYDOWN, VK_TAB, 0);//发送tab
                SendMessage((IntPtr)hwnd, WM_KEYDOWN, VK_ENTER, 0);//回车
                SendMessage((IntPtr)hwnd, WM_KEYUP, VK_ENTER, 0);//回车
                SendMessage((IntPtr)hwnd, WM_KEYUP, VK_TAB, 0);//释放tab

            }
            catch { }
        }


    }
}
