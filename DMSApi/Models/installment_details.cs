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
    
    public partial class installment_details
    {
        public long installment_details_id { get; set; }
        public Nullable<long> internal_requisition_master_id { get; set; }
        public string installment_no { get; set; }
        public Nullable<System.DateTime> installment_date { get; set; }
        public Nullable<decimal> installment_amount { get; set; }
    }
}