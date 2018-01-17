using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSApi.Models.IRepository
{
    public interface IProductRepository
    {
        object GetAllProducts();
        object GetAllAccessories();
        object GetAllProductsNPrice();
        List<product> GetAllProductList();
        object GetAllProductsForGiftDropdown();
        long AddProduct(product product);
        product GetProductById(long product_id);
        bool EditProduct(product product);
        bool DeleteProduct(long product_id);
        bool CheckDuplicateProductName(string product_name);
        object GetProductsWiseFirstColorVersion(long product_id);
        object GetProductsWiseFirstColorVersionForDemo(long product_id);
        object GetProductsWiseFirstColorVersionForB2b(long product_id);
        //object GetProductCategorywiseProduct(long product_category_id);
        object GetProductCategorywiseProduct();

        object GetProductWithoutAssocories();
        object GetProductStockNBookedQuantity(long product_id, long color_id, long product_version_id);
    }
}
