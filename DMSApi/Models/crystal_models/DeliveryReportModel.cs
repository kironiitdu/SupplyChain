using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMSApi.Models.crystal_models
{
    public class DeliveryReportModel
    {
        public string delivery_no { get; set; }
        public string region_name { get; set; }
        public string area_name { get; set; }
        public string address { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string territory_name { get; set; }
        public string lot_no { get; set; }
        public DateTime delivery_date { get; set; }
        public string full_name { get; set; }
        public string requisition_code { get; set; }
        public string party_name { get; set; }
        public string from_warehouse_name { get; set; }
        public string to_warehouse_name { get; set; }
        public string product_name { get; set; }
        public string product_category_name { get; set; }
        public string brand_name { get; set; }
        public string color_name { get; set; }
        public string imei_no { get; set; }
        public string remarks { get; set; }
        public long? delivery_details_id { get; set; }
        public int? delivered_quantity { get; set; }
        public int? deliverable_quantity { get; set; }
        public decimal? unit_price { get; set; }
        public decimal? total_amount { get; set; }
        public decimal? line_total { get; set; }
        public bool? is_gift { get; set; }
        public string gift_type { get; set; }
        public bool? is_live_dummy { get; set; }
        public string company_name { get; set; }

    }
}