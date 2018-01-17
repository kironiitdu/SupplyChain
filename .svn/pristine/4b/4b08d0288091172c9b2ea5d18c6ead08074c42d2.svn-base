using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMSApi.Models.IRepository;

namespace DMSApi.Models.Repository
{
    public class DamageTypeRepositoty : IDamageTypeRepositoty
    {
        private DMSEntities _entities;

        public DamageTypeRepositoty()
        {
            this._entities = new DMSEntities();
        }

        public object GetAllDamageTypes()
        {
            var damageType = (from c in _entities.damage_type


                              select new
                              {
                                  damage_type_id = c.damage_type_id,
                                  damage_type_name = c.damage_type_name,
                                  created_by = c.created_by,
                                  created_date = c.created_date,
                                  updated_by = c.updated_by,
                                  updated_date = c.updated_date,
                                  is_active = c.is_active



                              }).OrderByDescending(c => c.damage_type_id).ToList();

            return damageType;
        }

        public damage_type GetDamageTypeByID(long damage_type_id)
        {
            throw new NotImplementedException();
        }

        public damage_type GetDamageTypeBYName(string damage_type_name)
        {
            throw new NotImplementedException();
        }

        public bool CheckDuplicateDamageTypeName(string damage_type_name)
        {
            var checkDamageTypeNameIsExist = _entities.damage_type.FirstOrDefault(co => co.damage_type_name == damage_type_name);
            bool return_type = checkDamageTypeNameIsExist == null ? false : true;
            return return_type;
        }

        public bool InsertDamageType(damage_type oDamageType)
        {
            try
            {
                damage_type insert_damage_type = new damage_type
                {
                    damage_type_name = oDamageType.damage_type_name,
                    created_by = oDamageType.created_by,
                    created_date = oDamageType.created_date,
                    updated_by = oDamageType.updated_by,
                    updated_date = oDamageType.updated_date,
                    is_active = oDamageType.is_active,
                    is_deleted = false
                };
                _entities.damage_type.Add(insert_damage_type);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateDamageType(damage_type oDamageType)
        {
            try
            {
                damage_type ci = _entities.damage_type.Find(oDamageType.damage_type_id);
                ci.damage_type_name = oDamageType.damage_type_name;
                ci.updated_by = oDamageType.updated_by;
                ci.updated_date = oDamageType.updated_date;
                ci.is_active = oDamageType.is_active;
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteDamageType(long damage_type_id)
        {
            try
            {
                damage_type objDamageType = _entities.damage_type.FirstOrDefault(c => c.damage_type_id == damage_type_id);
                _entities.damage_type.Attach(objDamageType);
                _entities.damage_type.Remove(objDamageType);
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
