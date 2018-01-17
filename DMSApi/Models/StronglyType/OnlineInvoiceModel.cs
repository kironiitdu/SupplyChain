using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMSApi.Models.StronglyType
{
    public class OnlineInvoiceModel
    {
        public online_invoice_master OnlineInvoiceMaster { get; set; }
        public List<OnlinePaymentProductModel> OnlinePaymentProductModels { get; set; } 
    }    
}