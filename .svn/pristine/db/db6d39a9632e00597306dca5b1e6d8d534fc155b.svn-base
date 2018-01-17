using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMSApi.Models.IRepository;

namespace DMSApi.Models.Repository
{
    public class UnitRepository : IUnitRepository
    {
        private DMSEntities _entities;

        public UnitRepository()
        {
            this._entities = new DMSEntities();
        }
        public List<unit> GetAllUnits()
        {
            var unit = _entities.units.Where(b => b.is_active == true && b.is_deleted == false).OrderByDescending(u => u.unit_id).ToList();

            return unit;
        }

        public long AddUnit(unit unit)
        {
            try
            {
                unit insert_unit = new unit
                {
                    unit_name = unit.unit_name,
                    //created_by = 
                    created_date = DateTime.Now,
                    //updated_by = 
                    updated_date = DateTime.Now,
                    is_active = true,
                    is_deleted = false,

                };
                _entities.units.Add(insert_unit);
                _entities.SaveChanges();
                long last_insert_id = insert_unit.unit_id;
                return last_insert_id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public unit GetUnitById(long unit_id)
        {
            var unit = _entities.units.Find(unit_id);
            return unit;
        }

        public bool EditUnit(unit unit)
        {
            try
            {

                unit oUnit = _entities.units.Find(unit.unit_id);
                oUnit.unit_name = unit.unit_name;
                //updated_by = 
                oUnit.updated_date = DateTime.Now;
                _entities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        
        public bool DeleteUnit(long unit_id)
        {
            try
            {
                unit oUnit = _entities.units.FirstOrDefault(c => c.unit_id == unit_id);
                oUnit.is_active = false;
                oUnit.is_deleted = true;
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