using Demo.Common.GeckoFxHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.WebSites.ShouShang
{
    public class Registered : BaseOperation
    {
        public static bool IsRegistered = true;
        /// <summary>
        /// 注册
        /// </summary>
        public static void SetRegistered()
        {
            _geckoFxHelper.SetAttrElementByNameFirst("UserName", "value", "1521s9317");
            _geckoFxHelper.SetAttrElementByNameFirst("UserPass", "value", "117189317");
            _geckoFxHelper.SetAttrElementByNameFirst("ConfPass", "value", "117189317");
            _geckoFxHelper.SetAttrElementByNameFirst("Email", "value", "117189317@qq.com");
            _geckoFxHelper.BtnClientById("button");
            IsRegistered = false;
        }

        public static void EditJsCode()
        {
            _geckoFxHelper.EditJsCodebyContains();
        }

    }
}
