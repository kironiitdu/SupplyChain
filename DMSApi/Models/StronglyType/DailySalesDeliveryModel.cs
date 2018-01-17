using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMSApi.Models.StronglyType
{
    public class DailySalesDeliveryModel
    {
        public long? delivery_master_id { get; set; }
        public long? delivery_details_id { get; set; }
        public bool? is_gift { get; set; }
        public DateTime delivery_date { get; set; }
        public string product_name { get; set; }
        public string color_name { get; set; }
        public string product_version_name { get; set; }
        public long? product_id { get; set; }
        public long? color_id { get; set; }
        public long? product_version_id { get; set; }
        public long? requisition_master_id { get; set; }
        public string invoice_no { get; set; }
        public int? delivered_quantity { get; set; }
        public decimal? unit_price { get; set; }
        public decimal? per_incentive { get; set; }
        public decimal? total_price_after_incen { get; set; }
        public decimal? invoice_total { get; set; }
        public string territory_name { get; set; }
        public string area_name { get; set; }
        public string region_name { get; set; }
        public string product_category_name { get; set; }
        public string party_name { get; set; }
        public string party_type_name { get; set; }
        public decimal? total_price { get; set; }
        public decimal? total_incentive_amt { get; set; }
        public decimal? paid_amount { get; set; }
        public decimal? balance_amount { get; set; }
        public string remain_balance_status { get; set; }
    }
}