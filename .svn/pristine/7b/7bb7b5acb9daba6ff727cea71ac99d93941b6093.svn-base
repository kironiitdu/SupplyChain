using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSApi.Models.IRepository
{
   public interface IDealerTypeRepository
    {
       List<dealer_type> GetAllDealerType();
       dealer_type GetDealerTypeByID(long dealer_type_id);
       bool InsertDealerType(dealer_type objDealerType, long created_by);
       bool DeleteDealerType(long dealer_type_id, long updated_by);
       bool UpdateDealerType(dealer_type objDealerType, long updated_by);
       bool CheckDuplicatDealerTypePrefix(string dealer_type_prefix);
       bool CheckDuplicatDealerTypeType(string dealer_type_name);
       decimal GetDealerTypeWiseCreditLimit(long dealer_type_id);


    }
}
