using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMSApi.Models.StronglyType
{
    public class DailyPaymentReportModel
    {
       
      
        public long party_id { get; set; }
        public string receive_date { get; set; }
        public string party_name { get; set; }
        public string party_type_name { get; set; }
        public string territory_name { get; set; }
        public string area_name { get; set; }
        public string region_name { get; set; }
        public string payment_method_name { get; set; }
        public string bank_name { get; set; }
        public string bank_branch_name { get; set; }
        public string bank_account_name { get; set; }
        public decimal? amount { get; set; }
        public string sales_representative { get; set; }
        public string received_invoice_no { get; set; }
        public string reference { get; set; }
        public string full_name { get; set; }
    }
}