using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMSApi.Models.IRepository;

namespace DMSApi.Models.Repository
{
    public class RegionRepository : IRegionRepository
    {
        private DMSEntities _entities;

        public RegionRepository()
        {
            this._entities = new DMSEntities();
        }

        public object GetAllRegions()
        {
            var region = (from r in _entities.regions
                         select new
                         {
                             region_id = r.region_id,
                             region_name = r.region_name,
                             region_code = r.region_code,
                             is_active = r.is_active
                         }).OrderBy(e => e.region_name).ToList();

            return region;

        }

        public bool CheckDuplicateRegions(region mRegion)
        {
            var checkduplicateregion = _entities.regions.FirstOrDefault(r => r.region_name == mRegion.region_name && r.region_id!=mRegion.region_id);
            return checkduplicateregion != null;
        }

       
        public long AddRegion(region myRegion)
        {
            try
            {
                region insert_region = new region
                {
                    region_name = myRegion.region_name,
                    region_code = myRegion.region_code,
                    is_active = myRegion.is_active,
                    created_date = DateTime.Now,
                    created_by = myRegion.created_by,
                    update_date = DateTime.Now,
                    updated_by = myRegion.updated_by
                };
                _entities.regions.Add(insert_region);
                _entities.SaveChanges();
                long last_insert_id = insert_region.region_id;
                return last_insert_id;
            }
            catch (Exception)
            {

                return 0;
            }
        }

        public region GetRegionById(long region_id)
        {
            var region = _entities.regions.Find(region_id);
            return region;
        }

        public bool EditRegion(region myRegion)
        {
            try
            {
                region rgRegion = _entities.regions.Find(myRegion.region_id);
                rgRegion.region_name = myRegion.region_name;
                rgRegion.region_code = myRegion.region_code;
                rgRegion.is_active = myRegion.is_active;
                rgRegion.update_date = DateTime.Now;
                rgRegion.updated_by = myRegion.updated_by;
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteRegion(long region_id)
        {
            try
            {
                region oRegion = _entities.regions.FirstOrDefault(r => r.region_id == region_id);
                _entities.regions.Attach(oRegion);
                _entities.regions.Remove(oRegion);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
           
        }
    }
} ;