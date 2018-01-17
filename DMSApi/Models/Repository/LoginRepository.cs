using System.Security.Cryptography.X509Certificates;

using DMSApi.Models.IRepository;
using DMSApi.Models.StronglyType;
using System;
using System.Linq;
using Microsoft.Ajax.Utilities;

namespace DMSApi.Models.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private DMSEntities _entities;
        private LoginLogRepository LoginLogRepository;

        public LoginRepository()
        {
            this._entities = new DMSEntities();
            this.LoginLogRepository=new LoginLogRepository();
            //this.partyJournalRepository = new PartyJournalRepository();
        }

        //public object LoginInformation(string user_name, string password)
        public object LoginInformation(string user_name, string password, string ClientIpAddress)
        {
            Int64 uId = 0;
            string loginName = "";
                
            try
            {
                var userInfo = _entities.users.FirstOrDefault(x => x.login_name == user_name);
                if (userInfo != null )
                {
                    var passwordValid = PasswordHash.ValidatePassword(password, userInfo.password);
                    if (passwordValid)
                    {
                        var login = new LoginModel();
                        login.user_id = userInfo.user_id;
                        login.login_name = userInfo.login_name;
                        login.is_new_password = userInfo.is_new_pass ?? false;
                        login.full_name = userInfo.full_name;
                        login.role_id = userInfo.role_id;
                        login.party_id = userInfo.party_id;
                        login.company_id = userInfo.company_id;
                        login.party_type_id = _entities.parties.Where(r => r.party_id == userInfo.party_id).Select(c => c.party_type_id).SingleOrDefault();
                        login.user_role_name = _entities.roles.Where(r => r.role_id == userInfo.role_id).Select(c => c.role_name).SingleOrDefault();
                        uId = (long)userInfo.user_id;
                        loginName = userInfo.login_name;
                        return login;
                    }
                    return null;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                //24.04.2017 user login log
                if (uId != 0 && loginName != "")
                {
                    LoginLogRepository.LoginInfoEntry(uId, ClientIpAddress, loginName);
                }
                //end
            }
        }
    }
}