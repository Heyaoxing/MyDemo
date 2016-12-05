using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace Demo.Class.Elasticsearchs
{
    [ElasticsearchType(Name = "trackinfo", IdProperty = "Id")]
    public class TrackInfoIndex : TrackInfo
    {

        /// <summary>
        /// 运单号（用于查询）
        /// </summary>
        [String(Name = "id", Index = FieldIndexOption.NotAnalyzed)]
        public string Id
        {
            get { return WaybillNumber.ToLowerInvariant(); }
        }

        /// <summary>
        /// 客户订单号（用于查询）
        /// </summary>
        [String(Name = "co", Index = FieldIndexOption.NotAnalyzed)]
        public string FilterCustomerOrderNumber
        {
            get
            {
                if (string.IsNullOrWhiteSpace(CustomerOrderNumber)) return "";
                return CustomerOrderNumber.ToLowerInvariant();
            }
        }
        /// <summary>
        /// 跟踪号（用于查询）
        /// </summary>
        [String(Name = "to", Index = FieldIndexOption.NotAnalyzed)]
        public string FilterTrackingNumber
        {
            get
            {
                if (string.IsNullOrWhiteSpace(TrackingNumber)) return "";
                return TrackingNumber.ToLowerInvariant();
            }
        }
        /// <summary>
        /// 轨迹查询状态
        /// </summary>
        [Number(NumberType.Integer, Name = "ifs")]
        public InfoStatusEnum InfoStatus { get; set; }

        /// <summary>
        /// 信息更新时间
        /// </summary>
        [Date(Name = "icon")]
        public DateTime InfoCreatedOn { get; set; }

        /// <summary>
        /// 信息更新时间
        /// </summary>
        [Date(Name = "iuon")]
        public DateTime InfoUpdatedOn { get; set; }
    }
}
