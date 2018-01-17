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
    public class CiPlController : ApiController
    {
        private ICiPlRepository ciPlRepository;

        public CiPlController()
        {
            this.ciPlRepository = new CiPlRepository();
        }

        public CiPlController(ICiPlRepository ciPlRepository)
        {
            this.ciPlRepository = ciPlRepository;
        }

        [HttpGet, ActionName("GetAllCiPl")]
        public HttpResponseMessage GetAllCiPl()
        {
            var data = ciPlRepository.GetAllCiPl();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, data, formatter);
        }

        [HttpGet, ActionName("GetNewCiPl")]
        public HttpResponseMessage GetNewCiPl()
        {
            var data = ciPlRepository.GetNewCiPl();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, data, formatter);
        }

        [ActionName("GetCiPlById")]
        [HttpPost]
        public HttpResponseMessage GetCiPlById([FromBody]Models.ci_pl_master ci_pl_master)
        {
            var ci_pl_master_id = ci_pl_master.ci_pl_master_id;

            var data = ciPlRepository.GetCiPlById(ci_pl_master_id);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, data);
            return response;
        }

        [ActionName("GetCiPlByIdGet")]
        [HttpGet]
        public HttpResponseMessage GetCiPlById(long ci_pl_master_id)
        {
            var data = ciPlRepository.GetCiPlById(ci_pl_master_id);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, data);
            return response;
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody] CiPlModel ciPlModel)
        {
            try
            {
                if (string.IsNullOrEmpty(ciPlModel.CiPlMasterData.supplier_id.ToString()))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Please Select Supplier !!" }, formatter);
                }
                if (string.IsNullOrEmpty(ciPlModel.CiPlMasterData.purchase_order_master_id.ToString()))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Please Select PI !!" }, formatter);
                }
                if (string.IsNullOrEmpty(ciPlModel.CiPlMasterData.ci_no.ToString()))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Please Select PI !!" }, formatter);
                }
                if (string.IsNullOrEmpty(ciPlModel.CiPlMasterData.ci_date.ToString()))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "CI date is empty !!" }, formatter);
                }
                if (string.IsNullOrEmpty(ciPlModel.CiPlMasterData.issue_date.ToString()))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Issue date empty !!" }, formatter);
                }
                if (ciPlRepository.CheckDuplicateCiNo(ciPlModel.CiPlMasterData.ci_no))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "CI NO Already Exists" }, formatter);
                }
                else
                {
                    ciPlRepository.AddCiPl(ciPlModel);
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Information save successfully" }, formatter);
                }

            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [System.Web.Http.HttpPut]
        public HttpResponseMessage Put([FromBody]CiPlModel ciPlModel)
        {
            try
            {
                if (string.IsNullOrEmpty(ciPlModel.CiPlMasterData.supplier_id.ToString()))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Please Select Supplier !!" }, formatter);
                }
                if (string.IsNullOrEmpty(ciPlModel.CiPlMasterData.purchase_order_master_id.ToString()))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Please Select PI !!" }, formatter);
                }
                if (string.IsNullOrEmpty(ciPlModel.CiPlMasterData.ci_no.ToString()))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Please Select PI !!" }, formatter);
                }
                if (string.IsNullOrEmpty(ciPlModel.CiPlMasterData.ci_date.ToString()))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "CI date is empty !!" }, formatter);
                }
                if (string.IsNullOrEmpty(ciPlModel.CiPlMasterData.issue_date.ToString()))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Issue date empty !!" }, formatter);
                }
                else
                {
                    ciPlRepository.EditCiPl(ciPlModel);
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "CiPl Update successfully" }, formatter);
                }

            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [ActionName("DeleteCiPlDetailsById")]
        [HttpDelete]
        public HttpResponseMessage DeleteCiPlDetailsById(long ci_pl_details_id)
        {
            try
            {
                bool deleteCiPl = ciPlRepository.DeleteCiPlDetailsById(ci_pl_details_id);

                if (deleteCiPl == true)
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "CiPl Details Delete Successfully." }, formatter);
                }
                else
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = "Error in delete CiPl!!" }, formatter);
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