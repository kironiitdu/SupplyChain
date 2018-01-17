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
    public class OnlineRequisitionController : ApiController
    {
        private IOnlineRequisitionRepository _onlineRequisition;

        public OnlineRequisitionController()
        {
            _onlineRequisition = new OnlineRequisitionRepository();
        }

        public OnlineRequisitionController(IOnlineRequisitionRepository _onlineRequisition)
        {
            this._onlineRequisition = _onlineRequisition;
        }

        [HttpGet]
        public HttpResponseMessage GetImeiForOnlineRequisitionDelivery(long imei, int warehouseId)
        {
            var imeiOb = _onlineRequisition.GetImeiForOnlineRequisitionDelivery(imei,warehouseId);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, imeiOb, formatter);
        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage Post([FromBody] OnlineRequisitionModel onlineRequisitionModel)
        {
            try
            {                
                if (string.IsNullOrEmpty(onlineRequisitionModel.RequisitionMaster.warehouse_from.ToString()))
                {
                    var partyFormatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = "Please Select Warehouse" }, partyFormatter);
                }
                if (string.IsNullOrEmpty(onlineRequisitionModel.RequisitionMaster.party_id.ToString()))
                {
                    var partyFormatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = "Please Select Sales Channel" }, partyFormatter);
                }
                if (string.IsNullOrEmpty(onlineRequisitionModel.RequisitionMaster.credit_term.ToString()))
                {
                    var partyFormatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = "Please Select Credit Term" }, partyFormatter);
                }                
                if (onlineRequisitionModel.RequisitionDetailses.Count < 1)
                {
                    var partyFormatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = "Please Select Products" }, partyFormatter);
                }

                //if (onlineRequisitionModel.RequisitionDetailses.Count > 0)
                //{
                //    foreach (var qt in onlineRequisitionModel.RequisitionDetailses)
                //    {
                //        var Q = qt.quantity;
                //        var pId = qt.color_id;
                //        var colId = qt.color_id;
                //        if (Q < 1 || Q == null || colId == null || pId == null)
                //        {
                //            var partyFormatter = RequestFormat.JsonFormaterString();
                //            return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = "Check Quantity, Product or Color" }, partyFormatter);
                //            break;
                //        }
                //    }
                //}

                var value = _onlineRequisition.AddOnlineREquisition(onlineRequisitionModel);
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, value, formatter);
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [ActionName("GetOnlineDeliveryChallanReport")]
        [HttpGet]
        public HttpResponseMessage GetOnlineDeliveryChallanReport(long deliveryMasterId)
        {
            //sdfsdfdfsdf
            var imeiOb = _onlineRequisition.GetOnlineDeliveryChallanReport(deliveryMasterId);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, imeiOb, formatter);
        }
        
    
        [HttpGet]
        public HttpResponseMessage GetOnlineRequisitionAndDeliveryList()
        {
            var imeiOb = _onlineRequisition.GetOnlineRequisitionAndDeliveryList();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, imeiOb, formatter);
        }

        [HttpGet]
        public HttpResponseMessage GetPartyForPaymentCollect()
        {
            var imeiOb = _onlineRequisition.GetPartyForPaymentCollect();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, imeiOb, formatter);
        }

        [HttpGet]
        public HttpResponseMessage GetPaymentCollectGridForInvoice(int partyId,DateTime reqFrom,DateTime reqTo)
        {
            var imeiOb = _onlineRequisition.GetPaymentCollectGridForInvoice(partyId,reqFrom,reqTo);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, imeiOb, formatter);
        }

        [HttpGet]
        public HttpResponseMessage GetImeiForOnlineRequisitionPaymentCollect(long imei,int partyId)
        {
            var imeiOb = _onlineRequisition.GetImeiForOnlineRequisitionPaymentCollect(imei,partyId);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, imeiOb, formatter);
        }

        [HttpPost]
        public HttpResponseMessage GetProductForPaymentAndInvoiceGenerate([FromBody] OnlineRequisitionModel onlineRequisitionModel)
        {
            var imeiOb = _onlineRequisition.GetProductForPaymentAndInvoiceGenerate(onlineRequisitionModel.RequisitionDetailses);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, imeiOb, formatter);
        }
        [HttpGet, ActionName("GetOnlineInvoiceReport")]
        public HttpResponseMessage GetOnlineInvoiceReport(long online_invoice_master_id)
        {
            var dealerType = _onlineRequisition.GetOnlineInvoiceReport(online_invoice_master_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, dealerType, formatter);
        }

        [HttpGet]
        public HttpResponseMessage GetAllProductWithoutAss()
        {
            var imeiOb = _onlineRequisition.GetAllProductWithoutAss();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, imeiOb, formatter);
        }

        

        [HttpGet]
        public HttpResponseMessage GetReturnRequisitionList(int partyId, DateTime reqFrom, DateTime reqTo)
        {
            var imeiOb = _onlineRequisition.GetReturnRequisitionList(partyId,reqFrom,reqTo);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, imeiOb, formatter);
        }

    }
}