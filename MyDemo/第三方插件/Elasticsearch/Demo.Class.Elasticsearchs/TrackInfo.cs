using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace Demo.Class.Elasticsearchs
{
    public class TrackInfo
    {
        public TrackInfo()
        {
            TrackEventDetails = new List<TrackEvent>();
        }


        /// <summary>
        /// 运单号
        /// </summary>
        [String(Name = "wno", Index = FieldIndexOption.NotAnalyzed)]
        public string WaybillNumber
        {
            get;
            set;
        }

        /// <summary>
        /// 跟踪号
        /// </summary>
        [String(Name = "tno", Index = FieldIndexOption.NotAnalyzed)]
        public string TrackingNumber
        {
            get;
            set;
        }
        /// <summary>
        /// 客户订单号
        /// </summary>
        [String(Name = "cno", Index = FieldIndexOption.NotAnalyzed)]
        public string CustomerOrderNumber
        {
            get;
            set;
        }
        /// <summary>
        /// 收货运输方式代码
        /// </summary>
        [String(Name = "inc", Index = FieldIndexOption.NotAnalyzed)]
        public string ChannelCodeIn { get; set; }
        /// <summary>
        /// 收货运输方式名称
        /// </summary>
        [String(Name = "inn", Index = FieldIndexOption.NotAnalyzed)]
        public string ChannelNameIn { get; set; }
        /// <summary>
        /// 发货运输方式代码
        /// </summary>
        [String(Name = "otc", Index = FieldIndexOption.NotAnalyzed)]
        public string ChannelCodeOut { get; set; }
        /// <summary>
        /// 发货运输方式名称
        /// </summary>
        [String(Name = "otn", Index = FieldIndexOption.NotAnalyzed)]
        public string ChannelNameOut { get; set; }
        /// <summary>
        /// 客户编号
        /// </summary>
        [String(Name = "cco", Index = FieldIndexOption.NotAnalyzed)]
        public string CustomerCode { get; set; }
        /// <summary>
        /// 目的地国家（IOS2）
        /// </summary>
        [String(Name = "dco", Index = FieldIndexOption.NotAnalyzed)]
        public string DestinationCountryCode { get; set; }

        /// <summary>
        /// 发件人国家（IOS2）
        /// </summary>
        [String(Name = "oco", Index = FieldIndexOption.NotAnalyzed)]
        public string OriginCountryCode { get; set; }
        /// <summary>
        /// 收件人邮编
        /// </summary>
        [String(Name = "zip", Index = FieldIndexOption.NotAnalyzed)]
        public string PostalCode { get; set; }
        /// <summary>
        /// 重量
        /// </summary>
        [Number(NumberType.Double, Name = "wet")]
        public decimal Weight { get; set; }
        /// <summary>
        /// 包裹状态 (0)->查询不到Not_Found,(10)->info_received信息接收, (20)->运输途中in_transit, (30)->到达待取out_for_delivery,(40)->投递失败attempt_failed,(50)->成功签收delivered,(60)->可能异常exception
        /// </summary>
        [Number(NumberType.Integer, Name = "pks")]
        public TrackingStatusEnum TrackingStatus { get; set; }
        /// <summary>
        /// 妥投时效, 开始时间和结束时间间隔天数(无则为-1)
        /// </summary>
        [Number(Name = "day")]
        public int IntervalDays { get; set; }
        /// <summary>
        /// 最后一条事件内容
        /// </summary>
        [Nested(Name = "levt")]
        public TrackEvent LastTrackEvent
        {
            get
            {
                if (TrackEventDetails == null || TrackEventDetails.Count == 0) return null;
                return TrackEventDetails.OrderByDescending(e => e.SortCode).First();
            }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Date(Name = "cron")]
        public DateTime CreatedOn { get; set; }
        /*/// <summary>
        /// 创建人
        /// </summary>
        [String(Name = "crby", Index = FieldIndexOption.NotAnalyzed)]
        public string CreatedBy { get; set; }*/
        /// <summary>
        /// 最后更新时间
        /// </summary>
        [Date(Name = "upon")]
        public DateTime UpdatedOn { get; set; }
        /*/// <summary>
        /// 最后更新人
        /// </summary>
        [String(Name = "upby", Index = FieldIndexOption.NotAnalyzed)]
        public string LastUpdatedBy { get; set; }*/
        /// <summary>
        /// 备注
        /// </summary>
        [String(Name = "rmk", Index = FieldIndexOption.NotAnalyzed)]
        public string Remarks { get; set; }
        /// <summary>
        /// 跟踪事件信息
        /// </summary>
        [Nested(Name = "evts")]
        public List<TrackEvent> TrackEventDetails { get; set; }





    }
    [ElasticsearchType(Name = "evt")]
    public class TrackEvent
    {
        /// <summary>
        /// 排序编号
        /// </summary>
        [Number(NumberType.Integer, Name = "no")]
        public int SortCode { get; set; }
        /// <summary>
        /// 是否显示
        /// </summary>
        [Boolean(Name = "dn")]
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 事件时间
        /// </summary>
        [Date(Name = "pd")]
        public DateTime ProcessDate { get; set; }
        /// <summary>
        /// 事件内容
        /// </summary>
        [String(Name = "pc", Index = FieldIndexOption.Analyzed)]
        public string ProcessContent { get; set; }
        /// <summary>
        /// 事件发生地点
        /// </summary>
        [String(Name = "pl", Index = FieldIndexOption.Analyzed)]
        public string ProcessLocation { get; set; }
        /// <summary>
        /// 跟踪状态 
        /// </summary>
        [Number(NumberType.Integer, Name = "ts")]
        public TrackingStatusEnum TrackingStatus { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Date(Name = "cd")]
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// 0是代表发件国家 1是代表目的的国家
        /// </summary>
        [Number(NumberType.Integer, Name = "ft")]
        public int FlowType { get; set; }

    }
}
