using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMSApi.Models.StronglyType
{
    public class ImeiMovementCentralToPartyModel
    {
        public string warehouse_country { get; set; }
        public string warehouse_province { get; set; }
        public string warehouse_district_city { get; set; }
        public string imei_no { get; set; }
        public string party_code { get; set; }
        public string party_name { get; set; }
        public string party_type_name { get; set; }
        public string customer_province { get; set; }
        public string customer_district_city { get; set; }
        public string ship_to_province { get; set; }
        public string ship_to_district_city { get; set; }
        public string huawei_product_model { get; set; }
        public string customer_product_model { get; set; }
        public string color_name { get; set; }
        public string warehouse_name { get; set; }
        public string received_date_in_warehouse { get; set; }
        public string deliver_date_to_party { get; set; }
        public string retailer_name { get; set; }
        public string retailer_code { get; set; }
        public string retailer_province { get; set; }
        public string retailer_district_city { get; set; }
        public string delivery_to_retailer_date { get; set; }
        public bool   sales_status { get; set; }
       
    }
}