using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSApi.Models.IRepository
{
    public interface IProductCategoryRepository
    {
        List<product_category> GetAllProductCategories();
        long AddProductCategory(product_category product_category);
        product_category GetProductCategoryById(long product_category_id);
        bool EditProductCategory(product_category product_category);
        bool DeleteProductCategory(long product_category_id);
        long GetProductCategoryByProductId(long product_id);
    }
}
