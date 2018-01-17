using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMSApi.Models.StronglyType
{
    public class RequisitionUpdateEmailNotificationModel
    {
        public string product_name { get; set; }
        public string product_category_name { get; set; }
        public string color_name { get; set; }
        public string product_version_name { get; set; }
        public long requisition_master_id { get; set; }
        public string requisition_code { get; set; }
        public string requisition_type { get; set; }
        public string party_name { get; set; }
        public string party_type { get; set; }
        public string address { get; set; }
        public DateTime? requisition_date { get; set; }
        public int? quantity { get; set; }
        public int? price { get; set; }
        public decimal line_total { get; set; }
        public decimal? discount_amount { get; set; }
        public DateTime? approved_date { get; set; }
        public string approved_by { get; set; }
        public string reason_for_cancel_hold { get; set; }
    }
}