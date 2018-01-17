using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMSApi.Models.StronglyType
{
    public class InventoryBreakDownReportModel
    {

        public long? inventory_id { get; set; }
        public DateTime transaction_date { get; set; }
        public string transaction_type { get; set; }
        public string document_code { get; set; }
        public string product_category_name { get; set; }
        public string product_name { get; set; }
        public string color_name { get; set; }
        public string product_version_name { get; set; }
        public long?  product_id { get; set; }
        public long?  color_id { get; set; }
        public long?  product_version_id { get; set; }
        public decimal?   quantity { get; set; }
        public string territory_name { get; set; }
        public string area_name { get; set; }
        public string region_name { get; set; }
        public string warehouse_name { get; set; }
        public string party_name { get; set; }
        public string party_type_name { get; set; }

    }
}