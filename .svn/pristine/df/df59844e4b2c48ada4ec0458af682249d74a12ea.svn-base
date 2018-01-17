using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMSApi.Models.IRepository;

namespace DMSApi.Models.Repository
{
    public class BrandRepository:IBrandRepository
    {
        private DMSEntities _entities;

        public BrandRepository()
        {
            this._entities = new DMSEntities();
        }

        public List<brand> GetAllBrands()
        {
            var brand = _entities.brands.Where(b=>b.is_active==true && b.is_deleted==false).OrderByDescending(u => u.brand_id).ToList();
            return brand;
        }

        public bool CheckDuplicateBrands(string brand_name)
        {
            var checkDuplicateBrands = _entities.brands.FirstOrDefault(b => b.brand_name == brand_name);
            bool return_type = checkDuplicateBrands == null ? false : true;
            return return_type;

           
        }

        public long Addbrand(brand brand)
        {
            try
            {
                brand insert_brand = new brand
                {
                    brand_name = brand.brand_name,
                    //created_by = 
                    created_date = DateTime.Now,
                    //updated_by = 
                    updated_date = DateTime.Now,
                    is_active = true,
                    is_deleted = false,

                };
                
                _entities.brands.Add(insert_brand);
                _entities.SaveChanges();
                long last_insert_id = insert_brand.brand_id;
                return last_insert_id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public brand GetbrandById(long brand_id)
        {
            var brand = _entities.brands.Find(brand_id);
            return brand;
        }

        public bool Editbrand(brand brand)
        {
            try
            {

                brand oBrand = _entities.brands.Find(brand.brand_id);
                oBrand.brand_name = brand.brand_name;
                //updated_by = 
                oBrand.updated_date = DateTime.Now;
                _entities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Deletebrand(long brand_id)
        {
            try
            {
                brand oBrand = _entities.brands.FirstOrDefault(c => c.brand_id == brand_id);
                oBrand.is_active = false;
                oBrand.is_deleted = true;
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