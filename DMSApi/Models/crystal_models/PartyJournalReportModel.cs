using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMSApi.Models.crystal_models
{
    public class PartyJournalReportModel
    {
        public long? party_journal_id { get; set; }
        //public string transaction_date { get; set; }
        public DateTime transaction_date { get; set; }
        public string transaction_type { get; set; }
        public long? party_id { get; set; }
        public decimal? opening_balance { get; set; }
        public decimal? dr_amount { get; set; }
        public decimal? cr_amount { get; set; }
        public decimal? closing_balance { get; set; }
        public string remarks { get; set; }
        public long created_by { get; set; }
        public DateTime created_date { get; set; }
        public long updated_by { get; set; }
        public DateTime updated_date { get; set; }
        public string party_name { get; set; }
        public string full_name { get; set; }
        public string address { get; set; }
        public string proprietor_name { get; set; }
        public string phone { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        //public string location_name { get; set; }
        public string document_code { get; set; }
        public string payment_method_name { get; set; }
    }
}