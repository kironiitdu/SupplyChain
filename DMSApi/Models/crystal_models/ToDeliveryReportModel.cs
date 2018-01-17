using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMSApi.Models.crystal_models
{
    public class ToDeliveryReportModel
    {
        public string to_delivery_no { get; set; }
        public DateTime? to_delivery_date { get; set; }
        public string full_name { get; set; }
        public string order_no { get; set; }
        public string from_warehouse_name { get; set; }
        public string to_warehouse_name { get; set; }
        public string product_name { get; set; }
        public string product_category_name { get; set; }
        public string brand_name { get; set; }
        public string color_name { get; set; }
        public string product_version_name { get; set; }
        public string imei_no { get; set; }
        public string imei_no2 { get; set; }
        public string remarks { get; set; }
        public int? delivered_quantity { get; set; }
        public int? deliverable_quantity { get; set; }
    }
}