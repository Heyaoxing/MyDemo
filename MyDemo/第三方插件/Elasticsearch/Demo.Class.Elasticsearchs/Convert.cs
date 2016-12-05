using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Class.Elasticsearchs
{
    public static class Convert
    {
        public static TrackInfoIndex ToIndex(this TrackInfo info)
        {
            if (info == null) return null;

            var index = new TrackInfoIndex
            {
                WaybillNumber = info.WaybillNumber,
                TrackingNumber = info.TrackingNumber,
                CustomerOrderNumber = info.CustomerOrderNumber,
                ChannelCodeIn = info.ChannelCodeIn,
                ChannelCodeOut = info.ChannelCodeOut,
                ChannelNameIn = info.ChannelNameIn,
                ChannelNameOut = info.ChannelNameOut,
                CustomerCode = info.CustomerCode,
                DestinationCountryCode = info.DestinationCountryCode,
                OriginCountryCode = info.OriginCountryCode,
                PostalCode = info.PostalCode,
                Weight = info.Weight,
                IntervalDays = info.IntervalDays,
                TrackingStatus = info.TrackingStatus,
                Remarks = info.Remarks,
                CreatedOn = info.CreatedOn,
                UpdatedOn = info.UpdatedOn,
                TrackEventDetails = info.TrackEventDetails
            };

            return index;
        }

        public static TrackInfo ToInfo(this TrackInfoIndex index)
        {
            if (index == null) return null;

            var info = new TrackInfo
            {
                WaybillNumber = index.WaybillNumber,
                TrackingNumber = index.TrackingNumber,
                CustomerOrderNumber = index.CustomerOrderNumber,
                ChannelCodeIn = index.ChannelCodeIn,
                ChannelCodeOut = index.ChannelCodeOut,
                ChannelNameIn = index.ChannelNameIn,
                ChannelNameOut = index.ChannelNameOut,
                CustomerCode = index.CustomerCode,
                DestinationCountryCode = index.DestinationCountryCode,
                OriginCountryCode = index.OriginCountryCode,
                PostalCode = index.PostalCode,
                Weight = index.Weight,
                IntervalDays = index.IntervalDays,
                TrackingStatus = index.TrackingStatus,
                Remarks = index.Remarks,
                CreatedOn = index.CreatedOn,
                UpdatedOn = index.UpdatedOn,
                TrackEventDetails = index.TrackEventDetails
            };

            return info;
        }

    }
}
