using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMSApi.Models.StronglyType
{
    public class EshopRequisitionModel
    {
        public requisition_master RequisitionMaster { get; set; }
        public List<requisition_details> RequisitionDetailses { get; set; }
        public List<receive_serial_no_details> ReceiveSerialNoDetails { get; set; }
        public eshop_cutomer_requisition_mapping EshopCutomerRequisition { get; set; }
    }
}