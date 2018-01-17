using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMSApi.Models.StronglyType
{
    public class ToDeliveryDetailsModel : to_delivery_details
    {
        public long transfer_order_details_id { get; set; }
        public string product_name { get; set; }
        public string color_name { get; set; }
        public string product_version_name { get; set; }
        public string unit_name { get; set; }
    }
}