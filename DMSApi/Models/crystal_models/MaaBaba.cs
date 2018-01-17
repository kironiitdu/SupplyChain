using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMSApi.Models.crystal_models
{
    public class MaaBaba
    {
        public long? location_id { get; set; }
        public string location_name { get; set; }
        public long? employee_id { get; set; }
        public string employee_name { get; set; }
        public bool is_leaf { get; set; }
        public string parent_location_name { get; set; }
        public long? parent_location_id { get; set; }
        
    }
}