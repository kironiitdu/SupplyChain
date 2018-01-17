using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMSApi.Models.StronglyType
{
    public class DeliverySummaryReportFromADAToPartyModel
    {

        public long delivery_master_id { get; set; }
        public string delivery_no { get; set; }
        public string delivery_date { get; set; }
        public string from_warehouse { get; set; }
        public string from_warehouse_code { get; set; }
        public string from_warehouse_province { get; set; }
        public string party_name { get; set; }
        public string party_code { get; set; }
        public string party_type_name { get; set; }
        public string party_province { get; set; }
        public string party_district_city { get; set; }
        public string ship_to_province { get; set; }
        public string ship_to_district_city { get; set; }
        public string product_name { get; set; }
        public string color_name { get; set; }
        public long? delivered_quantity { get; set; }
        public string from_date { get; set; }
        public string to_date { get; set; }
    }
}