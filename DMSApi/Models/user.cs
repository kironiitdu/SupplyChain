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
    
    public partial class user
    {
        public long user_id { get; set; }
        public string full_name { get; set; }
        public string login_name { get; set; }
        public string password { get; set; }
        public Nullable<long> role_id { get; set; }
        public Nullable<long> company_id { get; set; }
        public Nullable<long> branch_id { get; set; }
        public Nullable<long> party_id { get; set; }
        public Nullable<bool> is_new_pass { get; set; }
        public Nullable<long> emp_id { get; set; }
        public Nullable<long> created_by { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }
        public Nullable<long> updated_by { get; set; }
        public Nullable<System.DateTime> updated_date { get; set; }
        public Nullable<bool> is_active { get; set; }
        public Nullable<bool> is_deleted { get; set; }
    }
}