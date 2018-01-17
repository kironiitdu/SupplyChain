using DMSApi.Models.StronglyType;
using System.Collections.Generic;

namespace DMSApi.Models.IRepository
{
    public interface IRoleRepository
    {
        List<RoleModel> GetAllRoles();

        role GetRoleByID(long role_id);

        role GetRoleByName(string role_name);

        object GetRolenameByID(long emp_id);

        bool CheckRoleForDuplicateByname(string role_name);

        bool InsertRole(role oRole);

        bool UpdateRole(role oRole);

        bool DeleteRole(long role_id);

        object GetAllRolesByType();

        List<role> GetEmployeeRoleType();

        List<role> GetsupllierRoleType();

        List<role> GetEmployeeRoleTypeBySource(long companyId);
        List<role> GetAllRolesForEmpUser(long company_id);
       // long
    }
}