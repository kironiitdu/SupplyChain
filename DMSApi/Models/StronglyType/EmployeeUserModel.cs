using System;

namespace DMSApi.Models.StronglyType
{
    public class EmployeeUserModel
    {
        public long employee_id { get; set; }
        public string employee_name { get; set; }
        public string employee_dob { get; set; }
        public string employee_address { get; set; }
        public Nullable<long> city_id { get; set; }
        public Nullable<long> country_id { get; set; }
        public string employee_phone { get; set; }
        public string employee_notes { get; set; }
        public string employee_photo { get; set; }
        public string employee_password { get; set; }
        public string employee_email { get; set; }
        public Nullable<long> created_by { get; set; }
        public string created_date { get; set; }
        public Nullable<long> updated_by { get; set; }
        public string updated_date { get; set; }
        public Nullable<long> company_id { get; set; }
        public Nullable<bool> is_active { get; set; }
        public string employee_status { get; set; }
        public Nullable<long> employee_role { get; set; }

        public long user_id { get; set; }
        public string user_name { get; set; }
        public string password { get; set; }
        public Nullable<long> role_id { get; set; }
        public Nullable<long> role_type_id { get; set; }
        public Nullable<bool> StatusFlag { get; set; }

        //25.04.2017
        public string ClientIpAddress { get; set; }
       
    }
}