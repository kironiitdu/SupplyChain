using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSApi.Models.IRepository
{
    public interface INotifierMailAccountRepository
    {
        List<notifier_mail_account> GetNotifierMailAccount();
        object GetActiveNotifierMailAccount();
        notifier_mail_account GetActiveNotifierMailAccountByID(long notifier_mail_account_id);
        bool InsertNotifierMailAccount(notifier_mail_account oNotifierMailAccount);
        bool DeleteNotifierMailAccount(long notifier_mail_account_id);
        bool UpdateNotifierMailAccount(notifier_mail_account oNotifierMailAccount);
        bool CheckDuplicateNotifierMailAccount(string AccountTitle);
    }
}
