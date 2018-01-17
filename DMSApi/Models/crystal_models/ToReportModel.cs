using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMSApi.Models.crystal_models
{
    public class ToReportModel
    {
        public string product_category_name { get; set; }
        public string product_name { get; set; }
        public string color_name { get; set; }
        public string product_version_name { get; set; }
        public string brand_name { get; set; }
        public string unit_name { get; set; }
        public string size_name { get; set; }
        public int? quantity { get; set; }
        public string from_warehouse_name { get; set; }
        public string to_warehouse_name { get; set; }
        public string order_no { get; set; }
        public DateTime? order_date { get; set; }
        public DateTime? created_date { get; set; }
        public DateTime? expected_transfer_date { get; set; }
        public string status { get; set; }
        public string remarks { get; set; }
        public string full_name { get; set; }
    }
}