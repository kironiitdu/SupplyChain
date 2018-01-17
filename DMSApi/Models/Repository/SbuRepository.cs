using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMSApi.Models.IRepository;

namespace DMSApi.Models.Repository
{
    public class SbuRepository : ISbuRepository
    {

        private DMSEntities _entities;

        public SbuRepository()
        {
            this._entities = new DMSEntities();
        }
        public List<sbu> GetAllSbu()
        {
            List<sbu> bSbu = _entities.sbus.Where(s=>s.is_deleted!=true).OrderBy(b => b.sbu_name).ToList();

            return bSbu;
        }

        public bool InsertSbu(sbu oSbu)
        {
            try
            {
                _entities.sbus.Add(oSbu);
                _entities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateSbu(sbu oSbu)
        {
            try
            {
                sbu entity = _entities.sbus.Find(oSbu.sbu_id);
                entity.sbu_name = oSbu.sbu_name;
                entity.is_active = oSbu.is_active;
                entity.is_deleted = false;
                entity.updated_by = oSbu.updated_by;
                entity.updated_date = DateTime.Now;
                _entities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteSbu(long sbu_id, long updated_by)
        {
            try
            {
                sbu objSbu = _entities.sbus.FirstOrDefault(sbu => sbu.sbu_id == sbu_id);
                objSbu.is_deleted = true;
                objSbu.updated_by = updated_by;
                objSbu.updated_date = DateTime.Now;
                _entities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}