using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMSApi.Models.IRepository;

namespace DMSApi.Models.Repository
{
    public class ProductVersionMappingRepository : IProductVersionMappingRepository
    {
        private DMSEntities _entities;

        public ProductVersionMappingRepository()
        {
            this._entities = new DMSEntities();
        }

        public object GetAllVersionMapping()
        {
            try
            {
                var listOfData = (from e in _entities.product_version_mapping
                                  join p in _entities.products on e.product_id equals p.product_id
                                  join c in _entities.product_version on e.product_version_id equals c.product_version_id
                                  select new
                                  {
                                      e.product_version_mapping_id,
                                      e.product_id,
                                      e.product_version_id,
                                      e.is_active,
                                      p.product_name,
                                      c.product_version_name
                                  }).ToList();
                return listOfData;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public object GetProductwiseVersion()
        {
            try
            {
                var data = (from pvm in _entities.product_version_mapping
                            join v in _entities.product_version on pvm.product_version_id equals v.product_version_id
                            select new
                            {
                                product_version_id = pvm.product_version_id,
                                product_version_name = v.product_version_name,
                                product_id = pvm.product_id,
                                is_active = pvm.is_active
                            }).Where(p => p.is_active == true).OrderBy(o => o.product_version_name).ToList();

                return data;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public bool AddVersionMapping(product_version_mapping productVersion, long create_by)
        {
            try
            {
                var product = new product_version_mapping();


                product.product_version_id = productVersion.product_version_id;
                product.created_by = create_by;
                product.is_active = productVersion.is_active;
                product.product_id = productVersion.product_id;
                product.created_date = DateTime.Now;
                _entities.product_version_mapping.Add(product);
                int save = _entities.SaveChanges();
                if (save > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
            {
                return false;
            }
        }

        public product_version_mapping GetVersionMappingById(long mappingId)
        {
            throw new NotImplementedException();
        }

        public bool EditVersionrMapping(product_version_mapping productColor, long update_by)
        {
            var product1 = _entities.product_version_mapping.Find(productColor.product_version_mapping_id);


            product1.product_version_id = productColor.product_version_id;

            product1.is_active = productColor.is_active;
            product1.updated_by = update_by;
            product1.updated_date = DateTime.Now;
            product1.product_id = productColor.product_id;

            int save = _entities.SaveChanges();

            if (save > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool DeleteVersionMapping(long mappingId)
        {
            try
            {
                var mapping = _entities.product_version_mapping.Find(mappingId);

                mapping.is_deleted = true;
                mapping.is_active = false;
                int save = _entities.SaveChanges();
                if (save > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CheckDuplicateMapping(product_version_mapping productColor)
        {
            try
            {
                var check =
                    _entities.product_version_mapping.SingleOrDefault(a => a.product_id == productColor.product_id && a.product_version_id == productColor.product_version_id);
                if (check == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool CheckDuplicateMappingForUpdate(product_version_mapping productColor)
        {
            try
            {
                var check =
                    _entities.product_version_mapping.SingleOrDefault(a => a.product_id == productColor.product_id && a.product_version_id == productColor.product_version_id);
                if (check == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}