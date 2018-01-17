using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMSApi.Models.StronglyType
{
    public class InventoryJournalModel
    {
        public string warehouse_name { get; set; }
        public string product_name { get; set; }
        public string uom_name { get; set; }
        public decimal? quantity { get; set; }
        public DateTime? transaction_date { get; set; }
        public string source_id { get; set; }
        public long product_id { get; set; }
        public long warehouse_id { get; set; }
        public string ware_to { get; set; }
        public string warehouse_type { get; set; }
        public string ware_from { get; set; }
        public long? ware_to_id { get; set; }
        public long ware_from_id { get; set; }
        public string remarks { get; set; }
        public long delivery_master_id { get; set; }
        public string delivery_master_ids { get; set; }
        public string transaction_type { get; set; }
        public decimal? productin { get; set; }
        public decimal? productout { get; set; }
        public decimal? opening_balance { get; set; }
        public decimal? closing_balance { get; set; }  

    }
}