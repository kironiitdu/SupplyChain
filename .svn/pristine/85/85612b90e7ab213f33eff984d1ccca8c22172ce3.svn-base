using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMSApi.Models.IRepository;

namespace DMSApi.Models.Repository
{
    public class ProcessWiseEmailSettingRepository : IProcessWiseEmailSettingRepository
    {
          private DMSEntities _entities;

          public ProcessWiseEmailSettingRepository()
        {
            this._entities = new DMSEntities();
        }

         

          public bool CheckDuplicateProcessWiseEmailSetting(long processCodeId)
          {
              var checkDuplicateCode = _entities.process_wise_mail_setting.FirstOrDefault(c => c.process_code_id == processCodeId);
              bool return_type = checkDuplicateCode == null ? false : true;
              return return_type;
          }

          public long AddProcessWiseEmailSetting(process_wise_mail_setting objProcessWiseEmailSetting)
          {
              try
              {
                  process_wise_mail_setting insert = new process_wise_mail_setting
                  {
                      process_code_id = objProcessWiseEmailSetting.process_code_id,
                      email_subject = objProcessWiseEmailSetting.email_subject,
                      email_body = objProcessWiseEmailSetting.email_body,
                      created_by = objProcessWiseEmailSetting.created_by,
                      created_date = DateTime.Now,
                      is_active = true,
                      is_deleted = false
                  };
                  _entities.process_wise_mail_setting.Add(insert);
                  _entities.SaveChanges();
                  long last_insert_id = insert.process_wise_mail_setting_id;
                  return last_insert_id;

              }
              catch (Exception)
              {

                  return 0;
              }
          }

          public process_wise_mail_setting GetProcessWiseEmailSettingById(long processWiseEmailSettingId)
          {
              throw new NotImplementedException();
          }

          public bool EditProcessWiseEmailSetting(process_wise_mail_setting objProcessWiseEmailSetting)
          {
              try
              {
                  process_wise_mail_setting objProcessEmail = _entities.process_wise_mail_setting.Find(objProcessWiseEmailSetting.process_wise_mail_setting_id);
                  objProcessEmail.process_code_id = objProcessWiseEmailSetting.process_code_id;
                  objProcessEmail.email_subject = objProcessWiseEmailSetting.email_subject;
                  objProcessEmail.email_body = objProcessWiseEmailSetting.email_body;
                  objProcessEmail.updated_by = objProcessWiseEmailSetting.updated_by;
                  objProcessEmail.updated_date = DateTime.Now;
                  objProcessEmail.is_active = objProcessWiseEmailSetting.is_active;
                  objProcessEmail.is_deleted = false;
                  _entities.SaveChanges();

                  return true;
              }
              catch (Exception)
              {

                  return false;
              }
          }

          public bool DeleteProcessWiseEmailSetting(long processWiseMailSettingId,long updatedBy)
          {
              try
              {
                  process_wise_mail_setting objProcessWiseMailSetting = _entities.process_wise_mail_setting.FirstOrDefault(c => c.process_wise_mail_setting_id == processWiseMailSettingId);
                  if (objProcessWiseMailSetting != null)
                  {
                      objProcessWiseMailSetting.is_active = false;
                      objProcessWiseMailSetting.is_deleted = true;
                      objProcessWiseMailSetting.updated_by = updatedBy;
                  }
                  _entities.SaveChanges();
                  return true;
              }
              catch (Exception)
              {

                  return false;
              }
          }


          public List<software_process_module> GetAllProcessCode()
          {
              var pro = _entities.software_process_module.OrderByDescending(p => p.process_code_id).ToList();
              return pro;
          }


        public List<process_wise_mail_setting> GetAllProcessWiseEmailSetting()
        {
            throw new NotImplementedException();
        }

        public object GetAllProcessCodeForGrid()
          {
              try
              {
                  var data = (from ms in _entities.process_wise_mail_setting
                              join pc in _entities.software_process_module on ms.process_code_id equals pc.process_code_id
                              join usr in _entities.users on ms.created_by equals usr.user_id into tempUsr
                              from usr in tempUsr.DefaultIfEmpty()
                              where ms.is_deleted==false
                              select new
                              {
                                  process_wise_mail_setting_id = ms.process_wise_mail_setting_id,
                                  process_code_name = pc.process_code_name,
                                  email_subject = ms.email_subject,
                                  email_body = ms.email_body,
                                  created_by = usr.full_name,
                                  created_date = ms.created_date


                              }).OrderBy(p => p.process_code_name).ToList();

                  return data;

              }
              catch (Exception)
              {

                  return null;
              }
          }



          public process_wise_mail_setting GetProcessCodeById(long process_wise_mail_setting_id)
          {
              var processCode = _entities.process_wise_mail_setting.Find(process_wise_mail_setting_id);
              return processCode;
          }
    }
}