using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMSApi.Models.StronglyType
{
    public class InstallmentDetailsModel
    {
        public string installment_no { get; set; }
        public DateTime? installment_date { get; set; }
        public decimal? installment_amount { get; set; }
    }
}