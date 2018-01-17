using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMSApi.Models.StronglyType
{
    public class DesignationGridModel
    {
        public long? sales_designation_id { get; set; }
        public string sales_designation { get; set; }
        public long? parent_designation_id { get; set; }
        public string parent_designation_name { get; set; }
        public long? sales_type_id { get; set; }
        public string sales_type { get; set; }
        public string sales_person_type_code { get; set; }
    }
}