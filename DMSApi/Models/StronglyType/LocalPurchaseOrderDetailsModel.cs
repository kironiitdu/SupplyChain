using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMSApi.Models.StronglyType
{
    public class LocalPurchaseOrderDetailsModel : local_purchase_order_details
    {
        public string product_category_name { get; set; }
        public string product_name { get; set; }
        public string color_name { get; set; }
        public string brand_name { get; set; }
        public string unit_name { get; set; }
        public string size_name { get; set; }
        public bool has_serial { get; set; }
        public string product_version_name { get; set; }
    }
}