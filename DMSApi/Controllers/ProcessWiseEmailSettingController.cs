﻿using System;
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
    public class ProcessWiseEmailSettingController : ApiController
    {
        private IProcessWiseEmailSettingRepository processWiseEmailSettingRepository;

        public ProcessWiseEmailSettingController()
        {
            this.processWiseEmailSettingRepository = new ProcessWiseEmailSettingRepository();
        }

        public ProcessWiseEmailSettingController(IProcessWiseEmailSettingRepository processWiseEmailSettingRepository)
        {
            this.processWiseEmailSettingRepository = processWiseEmailSettingRepository;
        }



        [HttpGet, ActionName("GetAllProcessWiseEmailSetting")]
        public HttpResponseMessage GetAllProcessWiseEmailSetting()
        {
            var processWiseEmailSetting = processWiseEmailSettingRepository.GetAllProcessWiseEmailSetting();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, processWiseEmailSetting, formatter);
        }
        [HttpGet, ActionName("GetAllProcessCodeForGrid")]
        public HttpResponseMessage GetAllProcessCodeForGrid()
        {
            var processWiseEmailSetting = processWiseEmailSettingRepository.GetAllProcessCodeForGrid();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, processWiseEmailSetting, formatter);
        }
        [HttpGet, ActionName("GetAllProcessCode")]
        public HttpResponseMessage GetAllProcessCode()
        {
            var processCode = processWiseEmailSettingRepository.GetAllProcessCode();
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, processCode);
            return response;
        }
        [HttpGet, ActionName("GetProcessCodeById")]
        public HttpResponseMessage GetProcessCodeById(long process_wise_mail_setting_id)
        {
            var processCode = processWiseEmailSettingRepository.GetProcessCodeById(process_wise_mail_setting_id);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, processCode);
            return response;
        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage Post([FromBody] Models.process_wise_mail_setting objProcessWiseEmailSetting)
        {

            try
            {

                if (string.IsNullOrEmpty(objProcessWiseEmailSetting.created_by.ToString()))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = "Please Login First" }, formatter);

                }
                if (processWiseEmailSettingRepository.CheckDuplicateProcessWiseEmailSetting((long) objProcessWiseEmailSetting.process_code_id))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = "Process Code Already Exists" }, formatter);
                }
                if (string.IsNullOrEmpty(objProcessWiseEmailSetting.process_code_id.ToString()))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = "Process Code Is Empty" }, formatter);

                }
                if (string.IsNullOrEmpty(objProcessWiseEmailSetting.email_subject))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = "Email Subject Is Empty" }, formatter);

                }
                if (string.IsNullOrEmpty(objProcessWiseEmailSetting.email_body))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = "Email Body Is Empty" }, formatter);

                }

                else
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

                        processWiseEmailSettingRepository.AddProcessWiseEmailSetting(insert);
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

        [System.Web.Http.HttpPut]
        public HttpResponseMessage Put([FromBody] Models.process_wise_mail_setting objProcessWiseEmailSetting)
        {
            try
            {
                if (string.IsNullOrEmpty(objProcessWiseEmailSetting.updated_by.ToString()))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = "Please Login First" }, formatter);

                }
                if (string.IsNullOrEmpty(objProcessWiseEmailSetting.process_code_id.ToString()))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = "Process Code Is Empty" }, formatter);

                }
                if (string.IsNullOrEmpty(objProcessWiseEmailSetting.email_subject))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = "Email Subject Is Empty" }, formatter);

                }
                if (string.IsNullOrEmpty(objProcessWiseEmailSetting.email_body))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = "Email Body Is Empty" }, formatter);

                }
                else
                {

                    processWiseEmailSettingRepository.EditProcessWiseEmailSetting(objProcessWiseEmailSetting);
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Updated successfully" }, formatter);
                }

            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }
        [System.Web.Http.HttpDelete]
        public HttpResponseMessage Delete([FromBody]Models.process_wise_mail_setting objProcessWiseEmailSetting, long updatedBy)
        {
            try
            {

                bool deleteAC = processWiseEmailSettingRepository.DeleteProcessWiseEmailSetting(objProcessWiseEmailSetting.process_wise_mail_setting_id, updatedBy);

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