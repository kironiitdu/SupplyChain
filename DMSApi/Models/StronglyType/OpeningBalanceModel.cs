using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMSApi.Models.StronglyType
{
    public class OpeningBalanceModel
    {
        public long party_id { get; set; }
        public decimal opening_balance { get; set; }
    }
}