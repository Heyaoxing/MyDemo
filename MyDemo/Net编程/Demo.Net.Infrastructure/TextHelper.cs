using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Net.Infrastructure
{
    public class TextHelper
    {
        /// <summary>
        /// 过滤数字
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static string FilterNumber(string txt)
        {
            if (string.IsNullOrWhiteSpace(txt))
                return txt;

            return System.Text.RegularExpressions.Regex.Replace(txt, @"\d", "");
        }


        /// <summary>
        /// 过滤英文
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static string FilterEnglish(string txt)
        {
            if (string.IsNullOrWhiteSpace(txt))
                return txt;

            return System.Text.RegularExpressions.Regex.Replace(txt, @"[a-zA-Z]", "");
        }
    }
}
