using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSApi.Models.IRepository
{
   public interface ISupplierTypeRepository
    {
        List<supplier_type> GetSupplierTypeListForGrid();
        List<supplier_type> GetSupplierTypeListForDropDown();
        object GetAllSupplierType();
        bool AddSupplierType(supplier_type objSupplierType, long? created_by);
        bool EditSupplierType(supplier_type objSupplierType, long? updated_by);
        bool DeleteSupplierType(long supplier_type_id, long? updated_by);
        bool CheckDuplicateSupplierType(string supplier_type_name);
    }
}
