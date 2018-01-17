using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSApi.Models.IRepository
{
    public interface IProductVersionRepository
    {
        List<product_version> GetAllProductVersionForGrid();
        List<product_version> GetAllProductVersionForDropDown();
        bool CheckDuplicateProductVersion(string product_version_name);
        long AddProductVersion(product_version product_version, long created_by);
        product_version GetProductVersionById(long product_version_id);
        bool EditProductVersion(product_version product_version, long updated_by);
        bool DeleteProductVersion(long product_version_id);
    }
}
