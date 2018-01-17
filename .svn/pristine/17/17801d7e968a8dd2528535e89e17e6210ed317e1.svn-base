using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMSApi.Models.IRepository;

namespace DMSApi.Models.Repository
{
    public class AccessoriesCategoryRepository : IAccessoriesCategoryRepository
    {
          private DMSEntities _entities;

        public AccessoriesCategoryRepository()
        {
            this._entities = new DMSEntities();
        }

        public List<accessories_category> GetAllAccessoriesCategory()
        {
            var ac = _entities.accessories_category.Where(b => b.is_active == true && b.is_deleted == false).OrderBy(u => u.accessories_category_name).ToList();
            return ac;
        }

        public bool CheckDuplicateAccessoriesCategory(string accessories_category_name)
        {
            var checkDuplicateAccessoriesCategory = _entities.accessories_category.FirstOrDefault(b => b.accessories_category_name == accessories_category_name);
            bool return_type = checkDuplicateAccessoriesCategory == null ? false : true;
            return return_type;
        }

        public long AddAccessoriesCategory(accessories_category objAccessoriesCategory, long created_by)
        {
            try
            {
                accessories_category insert= new accessories_category              
                {
                    accessories_category_name = objAccessoriesCategory.accessories_category_name,
                    price = objAccessoriesCategory.price,
                    created_by = created_by,
                    created_date = DateTime.Now,                   
                    is_active = true,
                    is_deleted = false

                };
                _entities.accessories_category.Add(insert);
                _entities.SaveChanges();
                long last_insert_id = insert.accessories_category_id;
                return last_insert_id;
               
            }
            catch (Exception)
            {
                
                return 0;
            }
        }
        public bool EditAccessoriesCategory(accessories_category objAccessoriesCategory, long updated_by)
        {
            try
            {
                accessories_category objAc = _entities.accessories_category.Find(objAccessoriesCategory.accessories_category_id);
                objAc.accessories_category_name = objAccessoriesCategory.accessories_category_name;
                objAc.price = objAccessoriesCategory.price;
                objAc.updated_by =updated_by;
                objAc.updated_date = DateTime.Now;
                objAc.is_active = objAccessoriesCategory.is_active;
                objAc.is_deleted = false;
                _entities.SaveChanges();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeleteAccessoriesCategory(long accessories_category_id)
        {
            try
            {
                accessories_category objAc = _entities.accessories_category.FirstOrDefault(c => c.accessories_category_id == accessories_category_id);
                objAc.is_active = false;
                objAc.is_deleted = true;
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }


        public accessories_category GetAccessoriesCategoryById(long accessories_category_id)
        {
            throw new NotImplementedException();
        }



        public List<accessories_category> GetAllAccessoriesCategoryForGrid()
        {
            try
            {
                var ac = _entities.accessories_category.Where(b => b.is_deleted == false).OrderBy(u => u.accessories_category_name).ToList();
                return ac;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}