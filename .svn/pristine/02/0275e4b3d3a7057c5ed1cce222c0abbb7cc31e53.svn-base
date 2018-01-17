using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMSApi.Models.IRepository;

namespace DMSApi.Models.Repository
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private DMSEntities _entities;

        public ProductCategoryRepository()
        {
            this._entities = new DMSEntities();
        }
        public List<product_category> GetAllProductCategories()
        {
            var productCategory = _entities.product_category.Where(b => b.is_active == true && b.is_deleted == false).OrderBy(u => u.product_category_name).ToList();

            return productCategory;
        }

        public long AddProductCategory(product_category product_category)
        {
            try
            {
                product_category insert_product_category = new product_category
                {
                    product_category_name = product_category.product_category_name,
                    product_category_code = product_category.product_category_code,
                    //created_by = 
                    created_date = DateTime.Now,
                    //updated_by = 
                    updated_date = DateTime.Now,
                    is_active = true,
                    is_deleted = false,

                };
                _entities.product_category.Add(insert_product_category);
                _entities.SaveChanges();
                long last_insert_id = insert_product_category.product_category_id;
                return last_insert_id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public product_category GetProductCategoryById(long product_category_id)
        {
            var productCategory = _entities.product_category.Find(product_category_id);
            return productCategory;
        }

        public bool EditProductCategory(product_category product_category)
        {
            try
            {

                product_category oProductCategory = _entities.product_category.Find(product_category.product_category_id);
                oProductCategory.product_category_name = product_category.product_category_name;
                oProductCategory.product_category_code = product_category.product_category_code;
                //updated_by = 
                oProductCategory.updated_date = DateTime.Now;
                _entities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteProductCategory(long product_category_id)
        {
            try
            {
                product_category oProductCategory = _entities.product_category.FirstOrDefault(c => c.product_category_id == product_category_id);
                oProductCategory.is_active = false;
                oProductCategory.is_deleted = true;
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public long GetProductCategoryByProductId(long product_id)
        {
            try
            {
                var kkk = _entities.products.FirstOrDefault(p => p.product_id == product_id).product_category_id;
                
                return (long)kkk;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}