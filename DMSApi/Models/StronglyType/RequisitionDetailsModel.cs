using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMSApi.Models.crystal_models;

namespace DMSApi.Models.StronglyType
{
    public class RequisitionDetailsModel:requisition_details
    {
        public string product_name { get; set; }
        public string color_name { get; set; }
        public string unit_name { get; set; }
        public string gift_for { get; set; }
        public int gift_quantity_for { get; set; }
        public int gift_quantity { get; set; }
        public string product_version_name { get; set; }
        public string company_name { get; set; }
        public bool has_serial { get; set; }
        public string promotion_name { get; set; }
        public string promotion_type { get; set; }
        public long? promotion_details_id { get; set; }
        public long? requisition_details_id_for_edit { get; set; }
        public string product_category_name { get; set; }
    }

    public class TempUseForRebateList
    {
        //public List<requisition_rebate> ListRequisitionRebates { get; set; }
        public List<RequisitionDetailsModel> ListRequisitionDetailsModels { get; set; } 
    }
}