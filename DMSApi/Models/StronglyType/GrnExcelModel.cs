using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMSApi.Models.StronglyType
{
    public class GrnExcelModel
    {
        public long grn_master_id { get; set; }
        public long product_id { get; set; }
        public long color_id { get; set; }
        public string grn_no { get; set; }
        public string grn_date { get; set; }
        public string order_no { get; set; }
        public string product_name { get; set; }
        public string color_name { get; set; }
        public long receive_quantity { get; set; }
        public string created_date { get; set; }
    }
}