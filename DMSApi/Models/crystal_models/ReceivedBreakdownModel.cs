using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMSApi.Models.crystal_models
{
    public class ReceivedBreakdownModel
    {
        public string payment_method_name { get; set; }
        public string bank_name { get; set; }
        public DateTime receive_date { get; set; }
        public decimal? receivedAmount { get; set; }
        public string cheque_no { get; set; }
    }
}