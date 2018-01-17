using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMSApi.Models.StronglyType
{
    //public class InvoiceDetailsModel:invoice_details
    public class InvoiceDetailsModel : requisition_details
    {
        public string product_name { get; set; }
        public string color_name { get; set; }
        public string unit_name { get; set; }
        public string gift_for { get; set; }
        public int gift_quantity_for { get; set; }
        public int gift_quantity { get; set; }
    }
}