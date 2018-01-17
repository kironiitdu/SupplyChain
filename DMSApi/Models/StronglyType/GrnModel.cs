using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMSApi.Models.StronglyType
{
    public class GrnModel
    {
        public grn_master GrnMasterData { get; set; }
        public List<GrnDetailsModel> GrnDetailsList { get; set; }
        public List<receive_serial_no_details> ReceiveSerialNoDetails { get; set; }
    }
}