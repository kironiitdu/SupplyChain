using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMSApi.Models.crystal_models
{
    public class RequisitionReportModel
    {

        public long? requisition_master_id { get; set; }
        public long? party_id { get; set; }
        public string requisition_code { get; set; }
        public DateTime requisition_date { get; set; }
        public string status { get; set; }
        public decimal? item_total { get; set; }
        public long? created_by { get; set; }
        public DateTime created_date { get; set; }
        public string remarks { get; set; }
        public string delivery_status { get; set; }
        //public DateTime expected_receiving_date { get; set; }
        public long? warehouse_from { get; set; }
        //public string payment_method { get; set; }
        //public string dc_or_check_no { get; set; }
        public decimal? amount { get; set; }


        public string product_category_name { get; set; }
        public string product_name { get; set; }
        public string color_name { get; set; }
        public string brand_name { get; set; }
        public string unit_name { get; set; }
        public string size_name { get; set; }

        public long? requisition_details_id { get; set; }
        public long? category_id { get; set; }
        public long? product_id { get; set; }
        public long? brand_id { get; set; }
        public long? color_id { get; set; }
        public int? quantity { get; set; }
        public decimal? price { get; set; }
        public decimal? line_total { get; set; }
        //public long? campaign_details_id { get; set; }
        public long? unit_id { get; set; }
        //public long? discount { get; set; }
        public string full_name { get; set; }


        //public Boolean? is_gift { get; set; }
        public string party { get; set; }
        public string party_address { get; set; }
        public string party_mobile { get; set; }
        public string party_type_name { get; set; }
        //public decimal? rebatetotal { get; set; }
        public string party_prefix { get; set; }
        public string product_version_name { get; set; }
        public string company_name { get; set; }
        public bool? is_gift { get; set; }
        public string gift_type { get; set; }

        public decimal? discount_amount { get; set; }
        public decimal? discount { get; set; }
        
    }
}