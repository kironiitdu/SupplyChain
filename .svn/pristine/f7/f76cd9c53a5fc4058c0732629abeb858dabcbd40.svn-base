using DMSApi.Models.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DMSApi.Models.Repository
{
    public class RoleTypeRepository : IRoleTypeRepository
    {
        private DMSEntities _entities;

        public RoleTypeRepository()
        {
            this._entities = new DMSEntities();
        }

        public List<role_type> GetAllRolesOnly()
        {
            List<role_type> roleTypes = _entities.role_type.OrderBy(r => r.role_type_name).ToList();

            return roleTypes;
        }

        public object GetAllRoleType()
        {
            throw new NotImplementedException();
        }

        public role_type GetRoleTypeById(long role_type_id)
        {
            throw new NotImplementedException();
        }

        public role_type GetRoleTypeByName(string role_type_name)
        {
            throw new NotImplementedException();
        }

        public bool CheckRoleForDuplicateByname(string role_type_name)
        {
            var checkDuplicateRole = _entities.role_type.FirstOrDefault(r => r.role_type_name == role_type_name);

            if (checkDuplicateRole == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool InsertRoleType(role_type oRoleType)
        {
            try
            {
                role_type insert_roleType = new role_type
                {
                    role_type_name = oRoleType.role_type_name,
                    description = oRoleType.description
                };
                _entities.role_type.Add(insert_roleType);
                _entities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateRoleType(role_type oRoleType)
        {
            try
            {
                role_type ro = _entities.role_type.Find(oRoleType.role_type_id);
                ro.role_type_name = oRoleType.role_type_name;
                ro.description = oRoleType.description;
                _entities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteRoleType(long role_Type_id)
        {
            try
            {
                role_type oRoleType = _entities.role_type.FirstOrDefault(r => r.role_type_id == role_Type_id);
                _entities.role_type.Attach(oRoleType);
                _entities.role_type.Remove(oRoleType);

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