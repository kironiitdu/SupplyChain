using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSApi.Models.IRepository
{
    public interface ISupplierRepository
    {
        List<supplier> GetAllSuppliers();
        object GetAllInternationalSuppliers();
        object GetAllLocalSuppliers();
        long AddSupplier(supplier supplier);
        supplier GetSupplierById(long supplier_id);
        bool EditSupplier(supplier supplier);
        bool DeleteSupplier(long supplier_id, long updated_by);
        bool CheckDuplicateSupplier(string supplier_name);
    }
}
