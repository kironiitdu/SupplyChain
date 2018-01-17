using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMSApi.Models.StronglyType
{
    public class InternalEmiRequisitionModel
    {
        public internal_requisition_master InternalRequisitionMaster { get; set; }
        public List<internal_requisition_details> InternalRequisitionDetails { get; set; }
        public List<receive_serial_no_details> ReceiveSerialNoDetails { get; set; }
        public List<internal_requisition_details> PromotionDetails { get; set; }
        public List<installment_details> InstallmentDetails { get; set; }
    }
}