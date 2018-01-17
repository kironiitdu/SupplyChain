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
using DMSApi.Models.StronglyType;

namespace DMSApi.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class MrrController : ApiController
    {
           private IMrrRepository mrrRepository;

        public MrrController()
        {
            this.mrrRepository = new MrrRepository();
        }

        public MrrController(IMrrRepository mrrRepository)
        {
            this.mrrRepository = mrrRepository;
        }
        [ActionName("GetGrnByGrnMasterId")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage GetGrnByGrnMasterId(long grn_master_id)
        {
            var mrr = mrrRepository.GetGrnByGrnMasterId(grn_master_id);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, mrr);
            return response;
        }

      
        [HttpGet, ActionName("GetAllGrnNo")]
        public HttpResponseMessage GetAllGrnNo()
        {
            var mrr = mrrRepository.GetAllGrnNo();
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, mrr);
            return response;
        }
        [HttpGet, ActionName("GetImeiInfoForMrr")]
        public HttpResponseMessage GetImeiInfoForMrr(long imei, int warehouseId)
        {
            var imeiOb = mrrRepository.GetImeiForMrr(imei, warehouseId);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, imeiOb, formatter);
        }
       
        [HttpPost, ActionName("CreateMrr")]
        public HttpResponseMessage CreateMrr([FromBody] MrrModel objMrrModel)
        {
            try
            {



                if (string.IsNullOrEmpty(objMrrModel.MrrMasterData.grn_master_id.ToString()))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Please Select Grn Master No !!" }, formatter);

                }
               
                else
                {
                    mrrRepository.CreateMrr(objMrrModel);
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "MRR save successfully" }, formatter);
                    
                }
              
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Sorry Mrr Failed!" }, formatter);
            }
        }

        [HttpGet, ActionName("GetAllMrr")]
        public HttpResponseMessage GetAllMrr()
        {
            var mrr = mrrRepository.GetAllMrr();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, mrr, formatter);
        }

        [HttpGet, ActionName("GetAllEmail")]
        public HttpResponseMessage GetAllEmail()
        {
            var mrr = mrrRepository.GetAllEmail();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, mrr, formatter);
        }

        [HttpGet, ActionName("GetMrrReportById")]
        public HttpResponseMessage GetMrrReportById(long mrr_master_id)
        {
            var mrr = mrrRepository.GetMrrReportById(mrr_master_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, mrr, formatter);
        }

        [HttpGet, ActionName("GetLoginUserMail")]
        public HttpResponseMessage GetLoginUserMail(long userId)
        {
            var mrr = mrrRepository.GetLoginUserMail(userId);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, mrr, formatter);
        }
    }
}