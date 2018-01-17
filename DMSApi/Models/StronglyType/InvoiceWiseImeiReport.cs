using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMSApi.Models.StronglyType
{
    public class InvoiceWiseImeiReport
    {
        public long invoice_master_id { get; set; }
        public string invoice_no { get; set; }
        public DateTime invoice_date { get; set; }
        public string product_name { get; set; }
        public string color_name { get; set; }
        public string product_version_name { get; set; }
        public string imei_no { get; set; }
        public string imei_no2 { get; set; }
    }
}