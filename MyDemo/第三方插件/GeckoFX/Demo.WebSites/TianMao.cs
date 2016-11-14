using Demo.Common.GeckoFxHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo.WebSites
{
    public class TianMao : BaseOperation
    {
        public static string url = "https://pages.tmall.com/wow/act/16495/tm-my1111?acm=lb-zebra-159335-883439.1003.4.1197898&wh_weex=true&lb-zebra-159335-883439.OTHER_1_1197898&scm=1048.1.7.5";

        public static string Qiang()
        {
            string result = string.Empty;
            try
            {
                _geckoFxHelper.BtnClientByClassFirst("opera-block J_CouponStatus");
                result = "完成一次点击";
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
    }
}
