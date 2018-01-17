using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMSApi.Models.StronglyType
{
    public class UserPasswordModel
    {
        public long user_id { get; set; }
        public string full_name { get; set; }
        public bool is_password_change { get; set; }
        public string login_name { get; set; }
        public string old_password { get; set; }
        public string new_password { get; set; }
    }
}