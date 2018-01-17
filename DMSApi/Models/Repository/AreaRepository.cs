using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMSApi.Models.IRepository;

namespace DMSApi.Models.Repository
{
    public class AreaRepository:IAreaRepository
    {

        private DMSEntities _entities;

        public AreaRepository()
        {
            this._entities = new DMSEntities();
        }
        public object GetAllAreas()
        {

            try
            {
                var areas = (from are in _entities.areas                           
                            join r in _entities.regions on are.region_id equals r.region_id
                             select new
                             {
                                 region_id = are.region_id,
                                 region_name = r.region_name,
                                 area_id= are.area_id,
                                 area_name = are.area_name,
                                 area_code=are.area_code,
                                 is_active=are.is_active
                             }).OrderBy(o=>o.region_id)
                             .OrderBy(ar => ar.area_name).ToList();

                return areas;

            }
            catch (Exception)
            {

                return 0;
            }
        }
        //mohiuddin to load area according to region
        public List<area> GetRegionwiseAreaForDropdown(long region_id)
        {
            var area = _entities.areas.Where(a => a.region_id == region_id)
                .OrderByDescending(a => a.area_id).ToList();

            return area;
        }
        public bool AddArea(area oArea)
        {
            try
            {
                area insert_area = new area
                {
                    region_id = oArea.region_id,
                    area_name = oArea.area_name,
                    area_code = oArea.area_code,
                    is_active =oArea.is_active,
                    created_date = DateTime.Now,
                    created_by = oArea.created_by,
                    updated_date = DateTime.Now,
                    updated_by = oArea.updated_by
                };
                _entities.areas.Add(insert_area);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public area GetAreaById(long area_id)
        {
            throw new NotImplementedException();
        }

        public bool EditArea(area oArea)
        {
            try
            {
                area ar = _entities.areas.Find(oArea.area_id);
                ar.region_id = oArea.region_id;
                ar.area_name = oArea.area_name;
                ar.area_code = oArea.area_code;
                ar.is_active = oArea.is_active;
                ar.updated_by = oArea.updated_by;
                ar.updated_date = DateTime.Now;
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteArea(long area_id)
        {
            try
            {
                area objArea = _entities.areas.FirstOrDefault(area => area.area_id == area_id);
                _entities.areas.Attach(objArea);
                _entities.areas.Remove(objArea);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }




        public bool CheckDuplicateZoneArea(area mArea)
        {
            var checkDuplicateArea = _entities.areas.FirstOrDefault(a => a.area_name == mArea.area_name && a.region_id == mArea.region_id && a.area_id!=mArea.area_id);         
            bool return_type = checkDuplicateArea  == null ? false : true;
            return return_type;
        }
    }
}