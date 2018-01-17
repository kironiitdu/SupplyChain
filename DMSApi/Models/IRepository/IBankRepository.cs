using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSApi.Models.IRepository
{
   public interface IBankRepository
    {
       List<bank> GetAllBank();
       bool InsertBank(bank oBank);
       bool UpdateBank(bank oBank);
       bool DeleteBank(long bank_id, long updated_by);

       bool DuplicateCheck(string bank_name);
       bool DuplicateCheckEdit(long bank_id, string bName);
    }
}
