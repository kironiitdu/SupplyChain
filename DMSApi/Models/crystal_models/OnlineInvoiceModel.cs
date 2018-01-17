using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMSApi.Models.crystal_models
{
    public class OnlineInvoiceModel
    {
        public string online_invoice_no { get; set; }
        public DateTime? online_invoice_date { get; set; }
        public string full_name { get; set; }
        public string party_name { get; set; }
        public string party_code { get; set; }
        public string party_type_name { get; set; }
        public string product_name { get; set; }
        public string product_category_name { get; set; }
        public string color_name { get; set; }
        public string unit_name { get; set; }
        public string brand_name { get; set; }
        public int? item_total { get; set; }
        public decimal? price { get; set; }
        public decimal? line_total { get; set; }
        public decimal? discount { get; set; }
        public decimal? discount_amount { get; set; }
        public decimal? price_amount { get; set; }
        public string requisition_code { get; set; }
        public long? online_invoice_master_id { get; set; }
    }
}