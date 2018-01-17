using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSApi.Models.IRepository
{
   public interface IProcessWiseEmailSettingRepository
    {
       List<process_wise_mail_setting> GetAllProcessWiseEmailSetting();
       object GetAllProcessCodeForGrid();
       process_wise_mail_setting GetProcessCodeById(long process_wise_mail_setting_id);
       List<software_process_module> GetAllProcessCode();
       bool CheckDuplicateProcessWiseEmailSetting(long processCodeId);
       long AddProcessWiseEmailSetting(process_wise_mail_setting objProcessWiseEmailSetting);
       process_wise_mail_setting GetProcessWiseEmailSettingById(long processWiseEmailSettingId);
       bool EditProcessWiseEmailSetting(process_wise_mail_setting objProcessWiseEmailSetting);
       bool DeleteProcessWiseEmailSetting(long processWiseMailSettingId, long updatedBy);
    }
}
