using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Providers.Entities;
using DMSApi.Models.IRepository;
using DMSApi.Models.StronglyType;

namespace DMSApi.Models.Repository
{
    public class UserRepository : IUserRepository
    {
        private DMSEntities _entities;

        public UserRepository()
        {
            this._entities = new DMSEntities();
        }
        public object GetAllUsers()
        {
            var users = (from us in _entities.users
                         join ro in _entities.roles on us.role_id equals ro.role_id into temRo
                         from ro in temRo.DefaultIfEmpty()
                        // join br in _entities.branches on us.branch_id equals br.branch_id into temBr
                         //from br in temBr.DefaultIfEmpty()
                         join pa in _entities.parties on us.party_id equals pa.party_id into temPa from pa in temPa.DefaultIfEmpty()
                         join pt in _entities.party_type on pa.party_type_id equals pt.party_type_id into temPt from pt in temPt.DefaultIfEmpty()
                         where pt.party_type_id ==1
                         select new
                         {

                             user_id = us.user_id,
                             full_name = us.full_name,
                             password = "password",
                             company_id = us.company_id,
                             login_name = us.login_name,
                             role_id = us.role_id,
                             role_name = ro.role_name,
                             branch_id = us.branch_id,
                            // branch_name = br.branch_name,
                             party_id = us.party_id,
                             party_name = pa.party_name,
                             party_type_id = pa.party_type_id,
                             party_type_name = pt.party_type_name

                         }).OrderByDescending(e => e.user_id).ToList();

            return users;
        }

        public long AddUser(user aUser)
        {
            try
            {
                var hashPassword = PasswordHash.HashPassword(aUser.password);
                var newUser = new user
                {
                    full_name = aUser.full_name,
                    password = hashPassword,
                    role_id = aUser.role_id,
                    login_name = aUser.login_name,
                    branch_id = aUser.branch_id,
                    party_id = aUser.party_id,
                    company_id = aUser.company_id,
                    is_new_pass = aUser.is_new_pass,
                    emp_id = aUser.emp_id
                };
                _entities.users.Add(newUser);
                _entities.SaveChanges();
                long newUserId = newUser.user_id;
                return newUserId;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public user GetUserById(long user_id)
        {
            var employee = _entities.users.Find(user_id);
            return employee;
        }

        public bool EditUser(user aUser)
        {
            try
            {
                if (aUser.password!="")
                {
                    string hashPassword;
                    if (aUser.password.Length > 60 && aUser.password.Contains("|"))
                    {   // password already encrypted, no need to hash
                        hashPassword = aUser.password;
                    }
                    else
                    {
                        hashPassword = PasswordHash.HashPassword(aUser.password);
                    }
                    user emp = _entities.users.Find(aUser.user_id);
                    emp.full_name = aUser.full_name;
                    emp.password = hashPassword;
                    emp.role_id = aUser.role_id;
                    // emp.login_name = user.login_name;
                    emp.branch_id = emp.branch_id;
                    emp.party_id = emp.party_id;
                    emp.company_id = aUser.company_id;

                    emp.is_new_pass = aUser.is_new_pass;
                    emp.emp_id = emp.emp_id;
                    _entities.SaveChanges();

                    return true;
                }
                else
                {
                    user emp = _entities.users.Find(aUser.user_id);
                    emp.full_name = aUser.full_name;
                    //emp.password = emp.password;
                    emp.role_id = aUser.role_id;
                    emp.login_name = aUser.login_name;
                    emp.branch_id = emp.branch_id;
                    emp.party_id = emp.party_id;
                    emp.company_id = aUser.company_id;
                    emp.is_new_pass = aUser.is_new_pass;
                    emp.emp_id = emp.emp_id;
                    _entities.SaveChanges();

                    return true;
                }
                

            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool DeleteUser(long user_id)
        {
            try
            {
                user oUser = _entities.users.FirstOrDefault(c => c.user_id == user_id);
                _entities.users.Attach(oUser);
                _entities.users.Remove(oUser);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Confirmation ChangeOwnProfile(UserPasswordModel userPassword)
        {
            user aUser = _entities.users.FirstOrDefault(u => u.user_id==userPassword.user_id);
            if (aUser != null)
            {
                var passwordValid = PasswordHash.ValidatePassword(userPassword.old_password, aUser.password);
                if (passwordValid)
                {
                    aUser.full_name = userPassword.full_name;
                    if (userPassword.is_password_change)
                    {
                        var hashPassword = PasswordHash.HashPassword(userPassword.new_password);
                        aUser.password = hashPassword;
                        aUser.is_new_pass = false;
                        aUser.emp_id = aUser.emp_id;
                    }
                    _entities.SaveChanges();
                    return new Confirmation { output = "success", msg = "Profile update successfully" };
                }
                else
                {
                    return new Confirmation { output = "error", msg = "Now a valid user/password(old)" };
                }
            }
            else
            {
                return new Confirmation { output = "error", msg = "Now a valid user/password(old)!" };
            }
        }


        public object GetAllPartyUsers()
        {
            var partyUsers = (from us in _entities.users
                         join ro in _entities.roles on us.role_id equals ro.role_id into temRo
                         from ro in temRo.DefaultIfEmpty()
                         //join br in _entities.branches on us.branch_id equals br.branch_id into temBr
                         //from br in temBr.DefaultIfEmpty()
                         join pa in _entities.parties on us.party_id equals pa.party_id into temPa
                         from pa in temPa.DefaultIfEmpty()
                         join pt in _entities.party_type on pa.party_type_id equals pt.party_type_id into temPt
                         from pt in temPt.DefaultIfEmpty()
                         where pt.party_type_id != 1
                         select new
                         {

                             user_id = us.user_id,
                             full_name = us.full_name,
                             password = "password",
                             company_id = us.company_id,
                             login_name = us.login_name,
                             role_id = us.role_id,
                             role_name = ro.role_name,
                             branch_id = us.branch_id,
                             //branch_name = br.branch_name,
                             party_id = us.party_id,
                             party_name = pa.party_name,
                             party_type_id = pa.party_type_id,
                             party_type_name = pt.party_type_name

                         }).OrderByDescending(e => e.user_id).ToList();

            return partyUsers;
        }
    }
}