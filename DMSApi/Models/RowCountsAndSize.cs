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
    
    public partial class RowCountsAndSize
    {
        public int TableId { get; set; }
        public string TableName { get; set; }
        public string rows { get; set; }
        public string reserved { get; set; }
        public string data { get; set; }
        public string index_size { get; set; }
        public string unused { get; set; }
    }
}