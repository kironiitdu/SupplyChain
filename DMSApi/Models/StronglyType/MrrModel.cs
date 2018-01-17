using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMSApi.Models.StronglyType
{
    public class MrrModel
    {
        public grn_master GrnMasterData { get; set; }
        public List<GrnDetailsModel> GrnDetailsList { get; set; }
        public List<ReceiveSerialsDetailsForMrr> ReceiveSerialNoDetails { get; set; }
        public mrr_master MrrMasterData { get; set; }
        public List<mrr_details> MrrDetailsList { get; set; }
        
      
    }
}