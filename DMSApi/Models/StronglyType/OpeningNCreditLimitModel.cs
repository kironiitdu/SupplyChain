using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMSApi.Models.StronglyType
{
    public class OpeningNCreditLimitModel
    {

        public string party_prefix { get; set; }
        public string party_type_name { get; set; }
        public long party_id { get; set; }
        
        public string party_name { get; set; }
        public string party_code { get; set; }


        public decimal? credit_limit { get; set; }
        public decimal? opening_balance { get; set; }
        public string transaction_type { get; set; }
        public bool has_transaction { get; set; }
    }
}