using System.Collections.Generic;

namespace DMSApi.Models.IRepository
{
    public interface IRoleTypeRepository
    {
        List<role_type> GetAllRolesOnly();

        object GetAllRoleType();

        role_type GetRoleTypeById(long role_type_id);

        role_type GetRoleTypeByName(string role_type_name);

        bool CheckRoleForDuplicateByname(string role_type_name);

        bool InsertRoleType(role_type oRoleType);

        bool UpdateRoleType(role_type oRoleType);

        bool DeleteRoleType(long role_Type_id);
    }
}