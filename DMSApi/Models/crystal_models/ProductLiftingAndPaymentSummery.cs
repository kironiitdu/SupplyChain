using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMSApi.Models.crystal_models
{
    public class ProductLiftingAndPaymentSummery
    {

        public string invoice_date { get; set; }
        public string invoice_no { get; set; }
        public long? lifting_quantity { get; set; }
        public decimal? product_billing_amount { get; set; }
        public decimal? rebate_total { get; set; }
        public long? live_dummey { get; set; }
        public decimal? adjusment { get; set; }
        public decimal? net_bill { get; set; }
        public decimal? received_amount { get; set; }
        public decimal? balance_amount { get; set; }
        public string balance_status { get; set; }
        public long? party_id { get; set; }
        public string party_name { get; set; }
        public string party_code { get; set; }
        public string address { get; set; }
        public string party_type_name { get; set; }
        public string area_name { get; set; }
        public string zone_name { get; set; }
        public decimal? total_net_billing_amount { get; set; }
        public decimal? previous_due { get; set; }
        public decimal? previous_advance { get; set; }
        public decimal? sum_received_amount { get; set; }
        public decimal? sum_advance_amount { get; set; }
        public string representaive_name { get; set; }

    }
}