using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMSApi.Models.StronglyType
{
    public class ToDeliveryModel
    {
        public to_delivery_master ToDeliveryMasterData { get; set; }
        public List<ToDeliveryDetailsModel> ToDeliveryDetailsList { get; set; }
        public List<ReceiveSerialNoDetailsModel> ReceiveSerialNoDetails { get; set; }
    }
}