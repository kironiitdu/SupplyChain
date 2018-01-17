using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMSApi.Models.StronglyType
{
    public class InvoiceModel
    {
        public invoice_master InvoiceMasterData { get; set; }
        public List<InvoiceDetailsModel> InvoiceDetailsList { get; set; }
        //public List<requisition_rebate> InvoiceRebateData { get; set; }
        public List<requisition_details> RequisitionDetailsData { get; set; }
        //17.10.2016
        //public price_protection PriceProtectionList { get; set; }
        //public List<price_protection> PriceProtectionList { get; set; }
        
    }
}