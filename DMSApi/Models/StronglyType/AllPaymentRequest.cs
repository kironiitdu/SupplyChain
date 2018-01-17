using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMSApi.Models.StronglyType
{
    public class AllPaymentRequest
    {
        public long payment_req_id { get; set; }
        public decimal amount { get; set; }
        public string deposite_date { get; set; }
        public long payment_method_id { get; set; }
        public string payment_method_name { get; set; }
        public string cheque_no { get; set; }
        public bool approved { get; set; }
        public string document_attachment { get; set; }
        public long receive_id { get; set; } 
       
       
        public long party_id { get; set; }
        public string party_name { get; set; }
        public long party_type_id { get; set; }
        public long bank_branch_id { get; set; }
        public string bank_account_name { get; set; }
        public string party_type_name { get; set; }
        public string territory_name { get; set; }

        public string status { get; set; }
        public string bank_name { get; set; }
        public string bank_branch_name { get; set; }
        public string approved_by { get; set; }
        public decimal? opening_balance { get; set; }
        public string receipt_no { get; set; }
        
    }
}