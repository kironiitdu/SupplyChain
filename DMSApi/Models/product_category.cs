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
    
    public partial class product_category
    {
        public long product_category_id { get; set; }
        public string product_category_name { get; set; }
        public string product_category_code { get; set; }
        public Nullable<long> created_by { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }
        public Nullable<long> updated_by { get; set; }
        public Nullable<System.DateTime> updated_date { get; set; }
        public Nullable<bool> is_active { get; set; }
        public Nullable<bool> is_deleted { get; set; }
    }
}