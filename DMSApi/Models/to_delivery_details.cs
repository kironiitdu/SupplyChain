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
    
    public partial class to_delivery_details
    {
        public long to_delivery_details_id { get; set; }
        public Nullable<long> to_delivery_master_id { get; set; }
        public Nullable<long> product_id { get; set; }
        public Nullable<long> unit_id { get; set; }
        public Nullable<long> color_id { get; set; }
        public Nullable<long> product_version_id { get; set; }
        public Nullable<int> to_quantity { get; set; }
        public Nullable<int> delivered_quantity { get; set; }
    }
}