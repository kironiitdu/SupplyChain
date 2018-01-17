using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMSApi.Models.IRepository;

namespace DMSApi.Models.Repository
{
    public class NotifierMailAccountRepository : INotifierMailAccountRepository
    {
        private DMSEntities _entities;

        public NotifierMailAccountRepository()
        {
            this._entities = new DMSEntities();
        }

        public List<notifier_mail_account> GetNotifierMailAccount()
        {
            List<notifier_mail_account> data = _entities.notifier_mail_account.Where(c => c.is_deleted != true).OrderByDescending(c => c.notifier_mail_account_id).ToList();
            return data;
        }

        public object GetActiveNotifierMailAccount()
        {
            var data = _entities.notifier_mail_account.FirstOrDefault(s => s.is_active == true);
            return data;
        }

        public notifier_mail_account GetActiveNotifierMailAccountByID(long notifier_mail_account_id)
        {
            throw new NotImplementedException();
        }

        public bool InsertNotifierMailAccount(notifier_mail_account oNotifierMailAccount)
        {
            try
            {
                var data = _entities.notifier_mail_account.ToList();
                if (data.Count > 0)
                {
                    foreach (var item in data)
                    {
                        notifier_mail_account con = _entities.notifier_mail_account.Find(item.notifier_mail_account_id);
                        con.is_active = false;
                        _entities.SaveChanges();
                    }
                }

                notifier_mail_account insertNotifierMailAccount = new notifier_mail_account
                {
                    account_title = oNotifierMailAccount.account_title,
                    account_email = oNotifierMailAccount.account_email,
                    accoutn_password = oNotifierMailAccount.accoutn_password,
                    created_by = oNotifierMailAccount.created_by,
                    created_date = oNotifierMailAccount.created_date,
                    updated_by = oNotifierMailAccount.updated_by,
                    updated_date = oNotifierMailAccount.updated_date,
                    is_active = oNotifierMailAccount.is_active,
                    is_deleted = oNotifierMailAccount.is_deleted
                };
                _entities.notifier_mail_account.Add(insertNotifierMailAccount);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteNotifierMailAccount(long notifier_mail_account_id)
        {
            try
            {

                notifier_mail_account oNotifierMailAccount = _entities.notifier_mail_account.FirstOrDefault(st => st.notifier_mail_account_id == notifier_mail_account_id);
                oNotifierMailAccount.is_active = false;
                oNotifierMailAccount.is_deleted = true;
                _entities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateNotifierMailAccount(notifier_mail_account oNotifierMailAccount)
        {
            try
            {
                if (oNotifierMailAccount.is_active == true)
                {
                    var data = _entities.notifier_mail_account.ToList();

                    foreach (var item in data)
                    {
                        notifier_mail_account dd = _entities.notifier_mail_account.Find(item.notifier_mail_account_id);
                        dd.is_active = false;
                        _entities.SaveChanges();
                    }

                }

                notifier_mail_account con = _entities.notifier_mail_account.Find(oNotifierMailAccount.notifier_mail_account_id);
                con.account_title = oNotifierMailAccount.account_title;
                con.account_email = oNotifierMailAccount.account_email;
                con.accoutn_password = oNotifierMailAccount.accoutn_password;
                con.updated_date = DateTime.Now;
                con.is_active = oNotifierMailAccount.is_active;
                _entities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool CheckDuplicateNotifierMailAccount(string AccountTitle)
        {
            throw new NotImplementedException();
        }
    }
}