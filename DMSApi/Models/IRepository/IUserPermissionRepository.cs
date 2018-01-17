using System.Collections.Generic;

namespace DMSApi.Models.IRepository
{
    public interface IUserPermissionRepository
    {
        List<user_permission> GetAllUserPermission();

        user_permission GetAllUserPermissionByRoleTypeId(long role_type_id);

        List<user_permission> GetAllUserPermissionByRoleId(long role_id);

        List<user_permission> GetAllUserPermissionByUserId(long user_id);

        user_permission GetAllUserPermissionByRoleTypeAndRoleId(long role_id, long role_type_id);

        bool InsertUserPermission(user_permission ouserUserPermission);

        bool UpdateUserPermission(user_permission ouserUserPermission);

        bool DeleteUserPermission(long user_permission_id);

        bool DeleteUserPermissionByRole(long user_role_id, List<user_permission> userPermissions);

        bool DeleteUserPermissionByUser(long user_id, List<user_permission> userPermissions);

        List<control> GetAllControls();
    }
}