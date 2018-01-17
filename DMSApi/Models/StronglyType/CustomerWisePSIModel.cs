using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMSApi.Models.StronglyType
{
    public class CustomerWisePSIModel
    {
        public string party_name { get; set; }
        public long party_id { get; set; }
        public string party_code { get; set; }
        public long party_type_id { get; set; }
        public string party_type_name { get; set; }
        public string product_name { get; set; }
        public long product_id { get; set; }
        public string color_name { get; set; }
        public long color_id { get; set; }
        public decimal? previous_balance { get; set; }
        public decimal? stock_in { get; set; }
        public decimal? sales { get; set; }
        public decimal? current_Stock { get; set; }
        public string from_date { get; set; }
        public string to_date { get; set; }
    }
}