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
    
    public partial class eshop_return_master
    {
        public long eshop_return_master_id { get; set; }
        public string return_code { get; set; }
        public Nullable<System.DateTime> return_date { get; set; }
        public Nullable<long> invoice_master_id { get; set; }
        public string remarks { get; set; }
        public Nullable<int> returned_quantity { get; set; }
        public Nullable<long> returned_by { get; set; }
        public Nullable<long> created_by { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }
        public Nullable<long> updated_by { get; set; }
        public Nullable<System.DateTime> updated_date { get; set; }
    }
}