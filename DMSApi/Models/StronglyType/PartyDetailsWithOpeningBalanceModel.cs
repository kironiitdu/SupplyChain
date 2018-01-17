using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMSApi.Models.StronglyType
{
    public class PartyDetailsWithOpeningBalanceModel
    {
        public long party_id { get; set; }
        public string party_name { get; set; }
        public string address { get; set; }
        public string mobile { get; set; }
        public long party_type_id { get; set; }
        public long region_id { get; set; }
        public long area_id { get; set; }
        public long territory_id { get; set; }
        public string party_type_name { get; set; }
        public string region_name { get; set; }
        public string area_name { get; set; }
        public string territory_name { get; set; }
        public decimal? opening_balance { get; set; }
        
    }
}