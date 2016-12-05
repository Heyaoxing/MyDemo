﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo.Mvc.Common.Extensions
{
    public class TestMyModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            //事件注册
            context.BeginRequest += context_BeginRequest; ;
            context.EndRequest += ContextOnEndRequest;
        }

        private void ContextOnEndRequest(object sender, EventArgs eventArgs)
        {
            var app = (HttpApplication)sender;
           // app.Context.Response.Write("执行ip检测开始");
        }

        void context_BeginRequest(object sender, EventArgs e)
        {
            var app = (HttpApplication)sender;
        //    app.Context.Response.Write("执行ip检测结束");
        }

        public void Dispose()
        {
           // throw new NotImplementedException();
        }
    }
}