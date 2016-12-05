using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Class.Elasticsearchs
{
    /// <summary>
    /// 包裹状态
    /// </summary>
    public enum TrackingStatusEnum
    {
        /// <summary>
        /// 未找到
        /// </summary>
        NotFound = 0,
        /// <summary>
        /// 电子预报信息已接收
        /// </summary>
        InfoReceived = 10,
        /// <summary>
        /// 运输途中
        /// </summary>
        InTransit = 20,
        /// <summary>
        /// 到达待取
        /// </summary>
        OutForDelivery = 30,
        /// <summary>
        /// 投递失败
        /// </summary>
        AttemptFailed = 40,
        /// <summary>
        /// 
        /// </summary>
        Delivered = 50,
        /// <summary>
        /// 可能异常
        /// </summary>
        Exception = 60
    }

    /// <summary>
    /// 轨迹查询状态
    /// </summary>
    public enum InfoStatusEnum
    {

        /// <summary>
        /// 待查询
        /// </summary>
        Pending = 0,

        /// <summary>
        /// 查询还未完成
        /// </summary>
        Incomplete = 10,


        /// <summary>
        /// 查询已经完成
        /// </summary>
        Completed = 20,

        /// <summary>
        /// 查询出现异常
        /// </summary>
        Exception = 30
    }
}
