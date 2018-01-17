using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMSApi.Models.crystal_models
{
    public class EshopDeliveryChallanModel
    {
        public string delivery_no { get; set; }
        
        public DateTime delivery_date { get; set; }
        public string full_name { get; set; }
        public string requisition_code { get; set; }
        public string customer_name { get; set; }
        public string from_warehouse_name { get; set; }
        public string to_warehouse_name { get; set; }
        public string product_name { get; set; }
        public string product_category_name { get; set; }
        public string brand_name { get; set; }
        public string color_name { get; set; }
        public string imei_no { get; set; }
        public string remarks { get; set; }
        public int? delivered_quantity { get; set; }                
        public string imei_no2 { get; set; }
        public decimal? line_total { get; set; }
        public string moblie_no { get; set; }
        public string customer_email { get; set; }
        public string billing_address { get; set; }
        public string shipping_address { get; set; }
        public string reference_no { get; set; }
        public string shipping_name { get; set; }
        public string method_name { get; set; }
        public string product_version_name { get; set; }
        
    }
}