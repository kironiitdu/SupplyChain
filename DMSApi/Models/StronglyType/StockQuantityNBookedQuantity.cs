using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMSApi.Models.StronglyType
{
    public class StockQuantityNBookedQuantity
    {
        public int stock_quantity { get; set; }
        public int booked_quantity { get; set; }
        public int available_quantity { get; set; }
    }
}