using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSApi.Models.IRepository
{
    public interface IMailReceiverSettingRepository
    {
        object GetMailReceiverSettings();
        object GetAllActiveMailReceiverSettings();
        mail_receiver_setting GetMailReceiverSettingByID(long mail_receiver_setting_id);
        bool InsertMailReceiverSetting(mail_receiver_setting oMailReceiverSetting);
        bool DeleteMailReceiverSetting(long mail_receiver_setting_id);
        bool UpdateMailReceiverSetting(mail_receiver_setting oMailReceiverSetting);
        bool CheckDuplicateRecieverEmail(string recieverEmail);
    }
}
