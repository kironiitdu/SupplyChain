//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DMSApi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ci_pl_master
    {
        public long ci_pl_master_id { get; set; }
        public Nullable<long> supplier_id { get; set; }
        public Nullable<long> purchase_order_master_id { get; set; }
        public string ci_no { get; set; }
        public string ref_no { get; set; }
        public Nullable<System.DateTime> ci_date { get; set; }
        public string payment_term { get; set; }
        public string dc_no { get; set; }
        public Nullable<System.DateTime> issue_date { get; set; }
        public string ci_attachment_location { get; set; }
        public string pl_attachment_location { get; set; }
        public Nullable<long> created_by { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }
        public Nullable<long> updated_by { get; set; }
        public Nullable<System.DateTime> updated_date { get; set; }
        public Nullable<bool> is_active { get; set; }
        public Nullable<bool> is_received { get; set; }
    }
}