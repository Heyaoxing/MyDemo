﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Webdiyer.WebControls.Mvc
{
    public interface IPagedList : IEnumerable
    {
        int CurrentPageIndex { get; set; }
        int PageSize { get; set; }
        int TotalItemCount { get; set; }
    }
    public interface IPagedList<T> : IEnumerable<T>, IPagedList { }

}