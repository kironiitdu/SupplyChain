using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMSApi.Models.crystal_models
{
    public class RebateReportModel
    {
        public string rebate_for { get; set; }
        //public decimal? quantity { get; set; }//
        public decimal? rebatequantity { get; set; }
        //public decimal? amount { get; set; }
        public decimal? rbamount { get; set; }
        public decimal? rebate { get; set; }
        public string rebate_type { get; set; }
        public decimal? rebate_amount { get; set; }
        
    }
}