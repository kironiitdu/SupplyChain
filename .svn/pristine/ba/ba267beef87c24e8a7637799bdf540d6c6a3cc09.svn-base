using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc.Html;
using DMSApi.Models.IRepository;

namespace DMSApi.Models.Repository
{
    public class ProductColorRepository : IProductColorMappingRepository
    {
        private DMSEntities _entities;

        public ProductColorRepository()
        {
            this._entities = new DMSEntities();
        }

        public object GetAllColorMapping()
        {
            try
            {
                var listOfData = (from e in _entities.product_color_mapping
                                  join p in _entities.products on e.product_id equals p.product_id
                                  join c in _entities.colors on e.color_id equals c.color_id
                                  select new
                                  {
                                      e.color_id,
                                      e.product_id,
                                      e.product_color_mapping_id,
                                      e.is_active,
                                      p.product_name,
                                      c.color_name,
                                      e.created_date
                                  }).OrderBy(k=>k.color_name).ToList();
                return listOfData;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public object GetAllColorMappingForTransection()
        {
            try
            {
                var listOfData = (from e in _entities.product_color_mapping
                                  join p in _entities.products on e.product_id equals p.product_id
                                  join c in _entities.colors on e.color_id equals c.color_id
                                  where e.is_active==true
                                  select new
                                  {
                                      e.color_id,
                                      e.product_id,
                                      e.product_color_mapping_id,
                                      e.is_active,
                                      p.product_name,
                                      c.color_name,
                                      e.created_date
                                  }).OrderBy(k => k.color_name).ToList();
                return listOfData;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public object GetProductwiseColor()
        {
            try
            {
                var data = (from pcm in _entities.product_color_mapping
                            join c in _entities.colors on pcm.color_id equals c.color_id
                              select new
                              {
                                  color_id = pcm.color_id,
                                  color_name = c.color_name,
                                  product_id = pcm.product_id

                              }).OrderBy(o => o.color_id).ToList();

                return data;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public bool AddColorMapping(product_color_mapping productColor, long create_by)
        {
            try
            {
                var product = new product_color_mapping();


                product.color_id = productColor.color_id;
                product.created_by = create_by;
                product.is_active = productColor.is_active;
                product.product_id = productColor.product_id;
                product.created_date = DateTime.Now;
                _entities.product_color_mapping.Add(product);
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

        public product_color_mapping GetColorMappingById(long mappingId)
        {
            throw new NotImplementedException();
        }

        public bool EditColorMapping(product_color_mapping productColor, long update_by)
        {
            try
            {
                var product1 = _entities.product_color_mapping.Find(productColor.product_color_mapping_id);


                product1.color_id = productColor.color_id;

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
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteColorMapping(long mappingId)
        {
            try
            {
                var mapping = _entities.product_color_mapping.Find(mappingId);

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

        public bool CheckDuplicateMapping(product_color_mapping productColor)
        {
            try
            {
                var check =
                    _entities.product_color_mapping.SingleOrDefault(a => a.product_id == productColor.product_id && a.color_id == productColor.color_id);
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

        public bool CheckDuplicateMappingForUpdate(product_color_mapping productColor)
        {
            try
            {
                var check =
                    _entities.product_color_mapping.SingleOrDefault(a => a.product_id == productColor.product_id && a.color_id == productColor.color_id);
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





        public object GetColorByProductId(long productId)
        {
            try
            {
                var data = (from pcm in _entities.product_color_mapping
                            join c in _entities.colors on pcm.color_id equals c.color_id
                            where pcm.product_id == productId
                            select new
                            {
                                color_id = pcm.color_id,
                                color_name = c.color_name,
                                product_id = pcm.product_id

                            }).OrderBy(o => o.color_id).ToList();

                return data;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}