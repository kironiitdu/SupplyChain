using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMSApi.Models.StronglyType
{
    public class BankBranchModel
    {
        public bank_branch oBranch { get; set; }
        public List<bank_account> oAccountList { get; set; }
    }
}