using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSApi.Models.IRepository
{
   public interface IDamageTypeRepositoty
    {
       object GetAllDamageTypes();

       damage_type GetDamageTypeByID(long damage_type_id);

       damage_type GetDamageTypeBYName(string damage_type_name);

       bool CheckDuplicateDamageTypeName(string damage_type_name);

       bool InsertDamageType(damage_type oDamageType);

       bool UpdateDamageType(damage_type oDamageType);

       bool DeleteDamageType(long damage_type_id);
    }
}
