using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Net.ClickBombbox
{
    public class ClickBomb
    {
        public static void Click()
        {
            try
            {

                GetAllWindows windows = new GetAllWindows();
                GetAllWindows.WindowInfo[] wndList = windows.GetAllDesktopWindowsWeb();
                foreach (GetAllWindows.WindowInfo w in wndList)
                {
                    if ((int)w.hWnd != 0)
                    {
                        GetAllWindows.WindowProcessWeb((int)w.hWnd, 0);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
