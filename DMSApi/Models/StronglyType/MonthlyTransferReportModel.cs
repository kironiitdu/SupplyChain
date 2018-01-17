using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMSApi.Models.StronglyType
{
    public class MonthlyTransferReportModel
    {

        public long? to_delivery_master_id { get; set; }
        public string to_delivery_no { get; set; }
        public DateTime? to_delivery_date { get; set; }
        public string to_warehouse_name { get; set; }
        public string product_name { get; set; }
        public string color_name { get; set; }
        public string product_version_name { get; set; }
        public int? delivered_quantity { get; set; }
        public string full_name { get; set; }
        public string delivery_method { get; set; }
        public string to_logistics_delivered_by { get; set; }
        public string courier_name { get; set; }
        public string from_date { get; set; }
        public string to_date { get; set; }
    }
}