using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Providers.Entities;

namespace DMSApi.Models.IRepository
{
    public interface IUserRepository
    {
        object GetAllUsers();
        object GetAllPartyUsers();
        long AddUser(user user);
        user GetUserById(long user_id);
        bool EditUser(user user);
        bool DeleteUser(long user_id);

        Confirmation ChangeOwnProfile(StronglyType.UserPasswordModel userPasswordModel);
    }
}
