using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gecko;

namespace Demo.WinForm.GeckoFX
{
    internal class FilteredPromptService : nsIPromptService2, nsIPrompt
    {
        /// <summary>  
        /// 消息推送服务  
        /// </summary>  
        /// <param name="onPromptBefor">系统弹窗前事件，返回false 将不弹窗系统警示框</param>  
        public FilteredPromptService(Func<string, string, bool> onPromptBefor)
        {
            _onPromptBefor = onPromptBefor;
        }
        //用于存储弹框内容
        public static string content = string.Empty;

        private Func<string, string, bool> _onPromptBefor = null;
        /// <summary>  
        /// 弹窗前事件  
        /// </summary>  
        /// <param name="aDialogTitle"></param>  
        /// <param name="aText"></param>  
        /// <returns></returns>  
        private bool PromptBefor(string aDialogTitle, string aText)
        {
            if (_onPromptBefor == null) return true;

            content = aText;
            return _onPromptBefor(aDialogTitle, aText);
        }

        public void Alert(nsIDOMWindow aParent, string aDialogTitle, string aText)
        {
            if (!PromptBefor(aDialogTitle ?? "Alert", aDialogTitle))
                return;
            content = aText;
            _promptService.Alert(aDialogTitle, aText);
        }

        public void AlertCheck(nsIDOMWindow aParent, string aDialogTitle, string aText, string aCheckMsg, ref bool aCheckState)
        {
            content = aText;
            _promptService.AlertCheck(aDialogTitle, aText, aCheckMsg, ref aCheckState);
        }

        public bool Confirm(nsIDOMWindow aParent, string aDialogTitle, string aText)
        {
            content = aText;
            return _promptService.Confirm(aDialogTitle, aText);
        }

        public bool ConfirmCheck(nsIDOMWindow aParent, string aDialogTitle, string aText, string aCheckMsg,
                                 ref bool aCheckState)
        {
            content = aText;
            return _promptService.ConfirmCheck(aDialogTitle, aText, aCheckMsg, ref aCheckState);
        }

        public int ConfirmEx(nsIDOMWindow aParent, string aDialogTitle, string aText, uint aButtonFlags, string aButton0Title,
                             string aButton1Title, string aButton2Title, string aCheckMsg, ref bool aCheckState)
        {
            content = aText;
            return _promptService.ConfirmEx(aDialogTitle, aText, aButtonFlags, aButton0Title, aButton1Title,
                                            aButton2Title, aCheckMsg, ref aCheckState);
        }

        public bool Prompt(nsIDOMWindow aParent, string aDialogTitle, string aText, ref string aValue, string aCheckMsg,
                           ref bool aCheckState)
        {
            content = aText;
            return _promptService.Prompt(aDialogTitle, aText, ref aValue, aCheckMsg, ref aCheckState);
        }

        public bool PromptUsernameAndPassword(nsIDOMWindow aParent, string aDialogTitle, string aText, ref string aUsername,
                                              ref string aPassword, string aCheckMsg, ref bool aCheckState)
        {
            content = aText;
            return _promptService.PromptUsernameAndPassword(aDialogTitle, aText, ref aUsername, ref aPassword, aCheckMsg,
                                                            ref aCheckState);
        }

        public bool PromptPassword(nsIDOMWindow aParent, string aDialogTitle, string aText, ref string aPassword,
                                   string aCheckMsg, ref bool aCheckState)
        {
            content = aText;
            return _promptService.PromptPassword(aDialogTitle, aText, ref aPassword, aCheckMsg, ref aCheckState);
        }

        public bool Select(nsIDOMWindow aParent, string aDialogTitle, string aText, uint aCount, IntPtr[] aSelectList,
                           ref int aOutSelection)
        {
            content = aText;
            return _promptService.Select(aDialogTitle, aText, aCount, aSelectList, ref aOutSelection);
        }

        public bool PromptAuth(nsIDOMWindow aParent, nsIChannel aChannel, uint level, nsIAuthInformation authInfo,
                               string checkboxLabel, ref bool checkValue)
        {
            return _promptService.PromptAuth(aChannel, level, authInfo);
        }

        public nsICancelable AsyncPromptAuth(nsIDOMWindow aParent, nsIChannel aChannel, nsIAuthPromptCallback aCallback,
                                             nsISupports aContext, uint level, nsIAuthInformation authInfo,
                                             string checkboxLabel, ref bool checkValue)
        {
            return _promptService.AsyncPromptAuth(aChannel, aCallback, aContext, level, authInfo);
        }

        public void Alert(string dialogTitle, string text)
        {
            if (text.Contains(_unknownProtocolFilterString))
                return;

            if (text.Contains(_proxyErrorFilterString))
                return;

            if (!PromptBefor(dialogTitle ?? "Alert", text))
                return;

            content = text;
            _promptService.Alert(dialogTitle, text);
        }

        public void AlertCheck(string dialogTitle, string text, string checkMsg, ref bool checkValue)
        {
            content = text;
            _promptService.AlertCheck(dialogTitle, text, checkMsg, ref checkValue);
        }

        public bool Confirm(string dialogTitle, string text)
        {
            content = text;
            return _promptService.Confirm(dialogTitle, text);
        }

        public bool ConfirmCheck(string dialogTitle, string text, string checkMsg, ref bool checkValue)
        {
            content = text;
            return _promptService.ConfirmCheck(dialogTitle, text, checkMsg, ref checkValue);
        }

        public int ConfirmEx(string dialogTitle, string text, uint buttonFlags, string button0Title, string button1Title,
                             string button2Title, string checkMsg, ref bool checkValue)
        {
            content = text;
            return _promptService.ConfirmEx(dialogTitle, text, buttonFlags, button0Title, button1Title, button2Title, checkMsg,
                                            ref checkValue);
        }

        public bool Prompt(string dialogTitle, string text, ref string value, string checkMsg, ref bool checkValue)
        {
            content = text;
            return _promptService.Prompt(dialogTitle, text, ref value, checkMsg, ref checkValue);
        }

        public bool PromptPassword(string dialogTitle, string text, ref string password, string checkMsg, ref bool checkValue)
        {
            return _promptService.PromptPassword(dialogTitle, text, ref password, checkMsg, ref checkValue);
        }

        public bool PromptUsernameAndPassword(string dialogTitle, string text, ref string username, ref string password,
                                              string checkMsg, ref bool checkValue)
        {
            content = text;
            return _promptService.PromptUsernameAndPassword(dialogTitle, text, ref username, ref password, checkMsg,
                                                            ref checkValue);
        }

        public bool Select(string dialogTitle, string text, uint count, IntPtr[] selectList, ref int outSelection)
        {
            content = text;
            return _promptService.Select(dialogTitle, text, count, selectList, ref outSelection);
        }

        #region Non COM methods

        private static PromptService _promptService = new PromptService();
        #endregion

        #region Filter strings.
        private const string _unknownProtocolFilterString = "Firefox doesn't know how to open this address, because the protocol";

        private const string _proxyErrorFilterString = "Firefox is configured to use a proxy server that can't be found.";
        #endregion
    }
}

