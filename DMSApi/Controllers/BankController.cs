using DMSApi.Models;
using DMSApi.Models.IRepository;
using DMSApi.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DMSApi.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BankController : ApiController
    {
        private IBankRepository bankRepository;

        public BankController()
        {
            this.bankRepository = new BankRepository();
        }

        public BankController(IBankRepository bankRepository)
        {
            this.bankRepository = bankRepository;
        }
        public HttpResponseMessage GetAllBank()
        {
            var countries = bankRepository.GetAllBank();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, countries, formatter);
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody]Models.bank oBank, long created_by)
        {
            try
            {
                Models.bank insertBank = new Models.bank
                {
                    bank_name = oBank.bank_name,
                    is_active = oBank.is_active,
                    is_deleted = false,
                    created_by = created_by,
                    created_date = DateTime.Now
                };
                if (bankRepository.DuplicateCheck(oBank.bank_name))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = "Bank name is duplicate" }, formatter);
                }
                else
                {
                    bool save_bank_branch = bankRepository.InsertBank(insertBank);
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Bank save successfully" }, formatter);
                }

            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [HttpPut]
        public HttpResponseMessage Put([FromBody]Models.bank oBank, long updated_by)
        {
            try
            {

                Models.bank updatebankbranch = new Models.bank

                {
                    bank_id = oBank.bank_id,
                    bank_name = oBank.bank_name,
                    is_active = oBank.is_active,
                    is_deleted = false,
                    updated_by = updated_by,
                    updated_date = DateTime.Now
                };
                if (bankRepository.DuplicateCheckEdit(oBank.bank_id, oBank.bank_name))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = "Bank name is duplicate" }, formatter);
                }
                else
                {
                    bool irepoUpdate = bankRepository.UpdateBank(updatebankbranch);

                    if (irepoUpdate == true)
                    {
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Bank Update successfully" }, formatter);
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
        public HttpResponseMessage Delete([FromBody]Models.bank oBank, long updated_by)
        {
            try
            {
                //int con_id = int.Parse(country_id);
                bool updatBankbranch = bankRepository.DeleteBank(oBank.bank_id, updated_by);

                if (updatBankbranch)
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Bank Delete Successfully." }, formatter);                    
                }
                else
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Bank can not be delete." }, formatter);
                }
                
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

    }
}