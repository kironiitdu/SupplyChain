using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMSApi.Models.StronglyType
{
    public class EshopReturnModel
    {
        public eshop_return_master EshopReturn { get; set; }
        public List<eshop_return_details> EshopReturnDetails { get; set; }
    }
}