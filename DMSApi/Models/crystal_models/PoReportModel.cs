using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMSApi.Models.crystal_models
{
    public class PoReportModel
    {
        public long? purchase_order_master_id { get; set; }
        public long? supplier_id { get; set; }
        public decimal? total_amount_including_vattax { get; set; }
        public DateTime order_date { get; set; }
        public string shipping_term { get; set; }
        public decimal? vat_total { get; set; }
        public string terms_n_condition { get; set; }
        public long? created_by { get; set; }
        public string created_date { get; set; }
        public long? updated_by { get; set; }
        public string updated_date { get; set; }
        public string remarks { get; set; }
        public string order_no { get; set; }
        public long? batch_id { get; set; }
        public decimal? tax_total { get; set; }
        public decimal? total_amount_without_vattax { get; set; }
        public string lc_number { get; set; }
        public string noc_number { get; set; }

        public string product_category_name { get; set; }
        public string product_name { get; set; }
        public string color_name { get; set; }
        public string brand_name { get; set; }
        public string unit_name { get; set; }
        public string size_name { get; set; }

        public long? purchase_order_details_id { get; set; }
        public long? product_category_id { get; set; }
        public long? product_id { get; set; }
        public long? brand_id { get; set; }
        public long? color_id { get; set; }
        public long? product_version_id { get; set; }
        public string product_version_name { get; set; }

        public int? quantity { get; set; }
        public int? pi_quantity { get; set; }
        public int? receive_qty { get; set; }
        public decimal? unit_price { get; set; }
        public decimal? line_total { get; set; }
        public DateTime last_modified_date { get; set; }
        public long? unit_id { get; set; }
        public long? size_id { get; set; }
        public string status { get; set; }
        public decimal? vat_pcnt { get; set; }
        public decimal? tax_pcnt { get; set; }
        public decimal? vat_amount { get; set; }
        public decimal? tax_amount { get; set; }
        public decimal? amount { get; set; }
        public string full_name { get; set; }
        public string company_name { get; set; }
        //
        public string verify_status { get; set; }
        public string pi_number { get; set; }
        public string supplier_name { get; set; }
        public string company_address { get; set; }
        public string contact_person { get; set; }
        public string supplier_type_name { get; set; }
       public string currency_name { get; set; }
    }
}