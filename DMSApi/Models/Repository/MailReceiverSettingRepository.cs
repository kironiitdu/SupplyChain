using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMSApi.Models.IRepository;

namespace DMSApi.Models.Repository
{
    public class MailReceiverSettingRepository : IMailReceiverSettingRepository
    {
        private DMSEntities _entities;

        public MailReceiverSettingRepository()
        {
            this._entities = new DMSEntities();
        }

        public object GetMailReceiverSettings()
        {
            try
            {
                var data = (from mrs in _entities.mail_receiver_setting
                            join spm in _entities.software_process_module on mrs.process_code_id equals spm.process_code_id
                            select new
                            {
                                mail_receiver_setting_id = mrs.mail_receiver_setting_id,
                                process_code_name = spm.process_code_name,
                                process_code_id = spm.process_code_id,
                                receiver_name = mrs.receiver_name,
                                receiver_email = mrs.receiver_email,
                                is_active = mrs.is_active,
                                is_deleted = mrs.is_deleted,
                                created_by = mrs.created_by,
                                created_date = mrs.created_date,
                                updated_by = mrs.updated_by,
                                updated_date = mrs.updated_date
                            }).Where(c => c.is_deleted != true).OrderBy(c => c.receiver_name).ToList();

                return data;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public object GetAllActiveMailReceiverSettings()
        {
            try
            {
                var data = (from mrs in _entities.mail_receiver_setting
                            join spm in _entities.software_process_module on mrs.process_code_id equals spm.process_code_id
                            select new
                            {
                                mail_receiver_setting_id = mrs.mail_receiver_setting_id,
                                process_code_name = spm.process_code_name,
                                process_code_id = spm.process_code_id,
                                receiver_name = mrs.receiver_name,
                                receiver_email = mrs.receiver_email,
                                is_active = mrs.is_active,
                                is_deleted = mrs.is_deleted,
                                created_by = mrs.created_by,
                                created_date = mrs.created_date,
                                updated_by = mrs.updated_by,
                                updated_date = mrs.updated_date
                            }).Where(c =>c.is_active==true && c.is_deleted != true).OrderByDescending(c => c.mail_receiver_setting_id).ToList();

                return data;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public mail_receiver_setting GetMailReceiverSettingByID(long mail_receiver_setting_id)
        {
            throw new NotImplementedException();
        }

        public bool InsertMailReceiverSetting(mail_receiver_setting oMailReceiverSetting)
        {
            try
            {
                mail_receiver_setting insertMailReceiverSetting = new mail_receiver_setting
                {
                    process_code_id = oMailReceiverSetting.process_code_id,
                    receiver_name = oMailReceiverSetting.receiver_name,
                    receiver_email = oMailReceiverSetting.receiver_email,
                    created_by = oMailReceiverSetting.created_by,
                    created_date = oMailReceiverSetting.created_date,
                    updated_by = oMailReceiverSetting.updated_by,
                    updated_date = oMailReceiverSetting.updated_date,
                    is_active = oMailReceiverSetting.is_active,
                    is_deleted = oMailReceiverSetting.is_deleted
                };
                _entities.mail_receiver_setting.Add(insertMailReceiverSetting);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteMailReceiverSetting(long mail_receiver_setting_id)
        {
            try
            {

                mail_receiver_setting oMailReceiverSetting = _entities.mail_receiver_setting.FirstOrDefault(st => st.mail_receiver_setting_id == mail_receiver_setting_id);
                oMailReceiverSetting.is_deleted = true;
                _entities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateMailReceiverSetting(mail_receiver_setting oMailReceiverSetting)
        {
            try
            {
                mail_receiver_setting con = _entities.mail_receiver_setting.Find(oMailReceiverSetting.mail_receiver_setting_id);
                con.process_code_id = oMailReceiverSetting.process_code_id;
                con.receiver_name = oMailReceiverSetting.receiver_name;
                con.receiver_email = oMailReceiverSetting.receiver_email;
                con.updated_date = DateTime.Now;
                con.is_active = oMailReceiverSetting.is_active;
                _entities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool CheckDuplicateRecieverEmail(string recieverEmail)
        {
            var checkDuplicateRecieverEmail = _entities.mail_receiver_setting.FirstOrDefault(c => c.receiver_email == recieverEmail);
            bool return_type = checkDuplicateRecieverEmail == null ? false : true;
            return return_type;
        }
    }
}