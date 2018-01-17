using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMSApi.Models.StronglyType
{
    public class ReceiveSerialsDetailsForMrr
    {
        public long receive_serial_no_details_id { get; set; }
        public Nullable<long> product_id { get; set; }
        public Nullable<long> brand_id { get; set; }
        public Nullable<long> color_id { get; set; }
        public Nullable<long> product_version_id { get; set; }
        public string imei_no { get; set; }
        public string imei_no2 { get; set; }
        public Nullable<long> grn_master_id { get; set; }
        public Nullable<bool> mrr_physical_damage { get; set; }
        public Nullable<bool> mrr_box_damage { get; set; }
        public Nullable<bool> customs_lost { get; set; }
        public Nullable<bool> mrr_status { get; set; }
        public Nullable<long> received_warehouse_id { get; set; }
        public Nullable<long> current_warehouse_id { get; set; }
        public string damage_type_name { get; set; }
        public Nullable<bool> mrr_saleable { get; set; }
       
    }
}