using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSApi.Models.IRepository
{
   public interface ISalesTypeRepository
    {
        List<sales_type> GetAllsalSalesTypes();
        sales_type GetSalesTypesById(long salesTypeId);

        bool InsertSalesType(sales_type objSalesType);

        bool DeleteSalesType(long salesTypeId);

        bool UpdateSalesType(sales_type objSalesType);

        bool CheckDuplicateSalesType(string salesType);
    }
}
