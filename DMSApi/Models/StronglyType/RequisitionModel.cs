using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMSApi.Models.StronglyType
{
    public class RequisitionModel
    {
        public  requisition_master RequisitionMasterData { get; set; }
        public List<RequisitionDetailsModel> RequisitionDetailsList { get; set; }
        public party Party { get; set; }
    }
}