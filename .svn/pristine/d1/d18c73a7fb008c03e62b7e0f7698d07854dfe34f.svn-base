using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using DMSApi.Models;
using DMSApi.Models.IRepository;
using DMSApi.Models.Repository;

namespace DMSApi.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class MailReceiverSettingController : ApiController
    {
        private IMailReceiverSettingRepository mailReceiverSettingRepository;

        public MailReceiverSettingController()
        {
            this.mailReceiverSettingRepository = new MailReceiverSettingRepository();
        }

        public MailReceiverSettingController(IMailReceiverSettingRepository mailReceiverSettingRepository)
        {
            this.mailReceiverSettingRepository = mailReceiverSettingRepository;
        }

        [HttpGet, ActionName("GetMailReceiverSettings")]
        public HttpResponseMessage GetMailReceiverSettings()
        {
            var data = mailReceiverSettingRepository.GetMailReceiverSettings();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, data, formatter);
        }

        [HttpGet, ActionName("GetAllActiveMailReceiverSettings")]
        public HttpResponseMessage GetAllActiveMailReceiverSettings()
        {
            var data = mailReceiverSettingRepository.GetAllActiveMailReceiverSettings();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, data, formatter);
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody]Models.mail_receiver_setting mail_receiver_setting)
        {
            try
            {
                if (string.IsNullOrEmpty(mail_receiver_setting.process_code_id.ToString()))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Process Code is Empty" }, formatter);
                }
                if (string.IsNullOrEmpty(mail_receiver_setting.receiver_name))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Receiver Name is Empty" }, formatter);
                }
                if (string.IsNullOrEmpty(mail_receiver_setting.receiver_email))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Receiver Email is Empty" }, formatter);
                }
                //if (mailReceiverSettingRepository.CheckDuplicateRecieverEmail(mail_receiver_setting.receiver_email))
                //{
                //    var formatter = RequestFormat.JsonFormaterString();
                //    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Email Already Exists" }, formatter);
                //}
                else
                {
                    Models.mail_receiver_setting insertMailReceiverSetting = new Models.mail_receiver_setting
                    {

                        process_code_id = mail_receiver_setting.process_code_id,
                        receiver_name = mail_receiver_setting.receiver_name,
                        receiver_email = mail_receiver_setting.receiver_email,
                        is_active = true,
                        is_deleted = false,
                        created_by = mail_receiver_setting.created_by,
                        created_date = DateTime.Now,
                        updated_by = mail_receiver_setting.updated_by,
                        updated_date = DateTime.Now
                    };
                    bool save = mailReceiverSettingRepository.InsertMailReceiverSetting(insertMailReceiverSetting);

                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Save successfully" }, formatter);
                }

            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [HttpPut]
        public HttpResponseMessage Put([FromBody]Models.mail_receiver_setting mail_receiver_setting)
        {
            try
            {
                if (string.IsNullOrEmpty(mail_receiver_setting.process_code_id.ToString()))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Process Code is Empty" }, formatter);
                }
                if (string.IsNullOrEmpty(mail_receiver_setting.receiver_name))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Receiver Name is Empty" }, formatter);
                }
                if (string.IsNullOrEmpty(mail_receiver_setting.receiver_email))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Receiver Email is Empty" }, formatter);
                }
                else
                {
                    Models.mail_receiver_setting updateMailReceiverSetting = new Models.mail_receiver_setting

                    {
                        mail_receiver_setting_id = mail_receiver_setting.mail_receiver_setting_id,
                        process_code_id = mail_receiver_setting.process_code_id,
                        receiver_name = mail_receiver_setting.receiver_name,
                        receiver_email = mail_receiver_setting.receiver_email,
                        is_active = mail_receiver_setting.is_active,
                        updated_by = mail_receiver_setting.updated_by,
                        updated_date = DateTime.Now

                    };

                    bool irepoUpdate = mailReceiverSettingRepository.UpdateMailReceiverSetting(updateMailReceiverSetting);

                    if (irepoUpdate == true)
                    {
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Update successfully" }, formatter);
                    }
                    else
                    {
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Update Failed" }, formatter);
                    }
                }
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [HttpDelete]
        public HttpResponseMessage Delete([FromBody]Models.mail_receiver_setting mail_receiver_setting)
        {
            try
            {
                bool updateMailReceiverSetting = mailReceiverSettingRepository.DeleteMailReceiverSetting(mail_receiver_setting.mail_receiver_setting_id);

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Delete Successfully." }, formatter);
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }
    }
}