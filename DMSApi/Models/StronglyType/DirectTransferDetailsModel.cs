using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMSApi.Models.StronglyType
{
    public class DirectTransferDetailsModel : direct_transfer_details
    {
        public int direct_transfer_master_id { get; set; }
    }
}