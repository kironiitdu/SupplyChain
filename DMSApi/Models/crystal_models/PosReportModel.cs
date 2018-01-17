using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMSApi.Models.crystal_models
{
    public class PosReportModel
    {
        public string party_name { get; set; }
        public string party_address { get; set; }
        public string party_mobile { get; set; }
        public string pos_code { get; set; }
        public string pos_date { get; set; }
        public string invoice_no { get; set; }
        public string customer_name { get; set; }
        public string customer_mobile { get; set; }
        public string customer_address { get; set; }
        public string product_name { get; set; }
        public string color_name { get; set; }
        public string unit_name { get; set; }
        public decimal? price { get; set; }
        public long? quantity { get; set; }
        public decimal? amount { get; set; }
        public decimal? discount_amount { get; set; }
        public decimal? discount_pcnt { get; set; }
        public decimal? paid_amount { get; set; }
        public decimal? return_amount { get; set; }
        public decimal? vat_pcnt { get; set; }

        public string country_name { get; set; }
        public string province_name { get; set; }
        public string city_name { get; set; }
        public decimal? net_amount { get; set; }
        public long? party_id { get; set; }
        public long? owner_party_id { get; set; }
        public string full_name { get; set; }
        public string login_name { get; set; }
        
    }
}