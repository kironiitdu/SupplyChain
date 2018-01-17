using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using DMSApi.Models;
using DMSApi.Models.IRepository;
using DMSApi.Models.Repository;
using DMSApi.Models.StronglyType;

namespace DMSApi.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class OnlineReturnController : ApiController
    {
         private IOnlineReturnRepository _onlineReturnRepository;

        public OnlineReturnController()
        {
            _onlineReturnRepository = new OnlineReturnRepository();
        }

        public OnlineReturnController(IOnlineReturnRepository _onlineReturnRepository)
        {
            this._onlineReturnRepository = _onlineReturnRepository;
        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage Post([FromBody] OnlineReturnModel onlineReturnModel)
        {
            try
            {

                if (string.IsNullOrEmpty(onlineReturnModel.OnlineReturnMaster.party_id.ToString()))
                {
                    var partyFormatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = "Please Select Sales Channel" }, partyFormatter);
                }                
                if (onlineReturnModel.OnlineReturnDetailses.Count < 1)
                {
                    var partyFormatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = "Please Select Products" }, partyFormatter);
                }                

                var value = _onlineReturnRepository.AddOnlineReturn(onlineReturnModel);
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, value, formatter);
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [HttpGet]
        public HttpResponseMessage GetOnlineReturnList()
        {
            var imeiOb = _onlineReturnRepository.GetOnlineReturnList();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, imeiOb, formatter);
        }

        [HttpGet]
        public HttpResponseMessage GetReturnChallanReport(long returnMasterId)
        {
            var imeiOb = _onlineReturnRepository.GetReturnChallanReport(returnMasterId);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, imeiOb, formatter);
        }

        [HttpGet]
        public HttpResponseMessage CheckQuantity(long returnMasterId,long pId,long colId,long verId,int qty)
        {
            var imeiOb = _onlineReturnRepository.CheckQuantity(returnMasterId,pId,colId,verId,qty);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, imeiOb, formatter);
        }

        [HttpGet]
        public HttpResponseMessage GetReturnRequisitionDetailsList(int partyId, DateTime reqFrom, DateTime reqTo)
        {
            var imeiOb = _onlineReturnRepository.GetReturnRequisitionDetailsList(partyId, reqFrom, reqTo);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, imeiOb, formatter);
        }
    }
}