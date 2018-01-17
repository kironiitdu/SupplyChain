using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMSApi.Models.IRepository;

namespace DMSApi.Models.Repository
{
    public class ProductVersionRepository:IProductVersionRepository
    {
        private DMSEntities _entities;

        public ProductVersionRepository()
        {
            this._entities = new DMSEntities();
        }

        public List<product_version> GetAllProductVersionForGrid()
        {
            var data = _entities.product_version.Where(pv => pv.is_deleted == false).OrderByDescending(pv => pv.product_version_id).ToList();
            return data;
        }

        public List<product_version> GetAllProductVersionForDropDown()
        {
            var data = _entities.product_version.Where(pv => pv.is_active == true).OrderByDescending(pv => pv.product_version_id).ToList();
            return data;
        }

        public bool CheckDuplicateProductVersion(string product_version_name)
        {
            var checkDuplicateProductVersion = _entities.product_version.FirstOrDefault(b => b.product_version_name == product_version_name);
            bool return_type = checkDuplicateProductVersion == null ? false : true;
            return return_type;
        }

        public long AddProductVersion(product_version product_version, long created_by)
        {
            try
            {
                product_version insert_product_version = new product_version
                {
                    product_version_name = product_version.product_version_name,
                    created_by = created_by,
                    created_date = DateTime.Now,
                    updated_by = created_by,
                    updated_date = DateTime.Now,
                    is_active = true,
                    is_deleted = false,

                };

                _entities.product_version.Add(insert_product_version);
                _entities.SaveChanges();
                long last_insert_id = insert_product_version.product_version_id;
                return last_insert_id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public product_version GetProductVersionById(long product_version_id)
        {
            var data = _entities.product_version.Find(product_version_id);
            return data;
        }

        public bool EditProductVersion(product_version product_version, long updated_by)
        {
            try
            {

                product_version oProductVersion = _entities.product_version.Find(product_version.product_version_id);
                oProductVersion.product_version_name = product_version.product_version_name;
                oProductVersion.is_active = product_version.is_active;
                oProductVersion.updated_by = updated_by;
                oProductVersion.updated_date = DateTime.Now;
                _entities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteProductVersion(long product_version_id)
        {
            try
            {
                product_version oProductVersion = _entities.product_version.FirstOrDefault(c => c.product_version_id == product_version_id);
                oProductVersion.is_active = false;
                oProductVersion.is_deleted = true;
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}