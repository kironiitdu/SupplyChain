using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSApi.Models.IRepository
{
  public  interface IRetailerTypeRepository
    {

      List<retailer_type> GetAllRetailerType();
      retailer_type GetRetailerTypeByID(long retailer_type_id);
      bool InsertRetailerType(retailer_type objRetailerType, long created_by);
      bool DeleteRetailerType(long retailer_type_id, long updated_by);
      bool UpdateRetailerType(retailer_type objRetailerType, long updated_by);
      bool CheckDuplicatRetailerTypePrefix(string retailer_type_prefix);
      bool CheckDuplicatRetailerTypeType(string retailer_type_name);
    }
}
