using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMSApi.Models.crystal_models
{
    public class InventoryStockPDFModel
    {
        public long warehouse_id { get; set; }
        public string warehouse_name { get; set; }
        public string warehouse_code { get; set; }
        public string region_name { get; set; }
        public string transaction_type { get; set; }
        public string area_name { get; set; }
        public string party_name { get; set; }
        public long party_id { get; set; }
        public string transaction_date { get; set; }
        public string document_code { get; set; }
        public string product_name { get; set; }
        public string product_category_name { get; set; }
        public string color_name { get; set; }
        public string product_version_name { get; set; }
        public string imei_no { get; set; }
        public string imei_no2 { get; set; }
        public string unit_name { get; set; }
        public decimal stock_quantity { get; set; }
        public string update_date { get; set; }
        public string to_date { get; set; }
        public string current_user_full_name { get; set; }
    }
}