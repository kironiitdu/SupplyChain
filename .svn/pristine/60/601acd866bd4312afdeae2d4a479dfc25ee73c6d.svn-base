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
    public class OnlineInvoiceAndPaymentController : ApiController
    {
        private IOnlineInvoiceAndPaymentRepository _onlineRequisition;

        public OnlineInvoiceAndPaymentController()
        {
            _onlineRequisition = new OnlineInvoiceAndPaymentRepository();
        }

        public OnlineInvoiceAndPaymentController(IOnlineInvoiceAndPaymentRepository _onlineRequisition)
        {
            this._onlineRequisition = _onlineRequisition;
        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage Post([FromBody] OnlineInvoiceModel onlineInvoiceModel)
        {
            try
            {
                if (string.IsNullOrEmpty(onlineInvoiceModel.OnlineInvoiceMaster.party_id.ToString()))
                {
                    var partyFormatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = "Please Select Party" }, partyFormatter);
                }

                var value = _onlineRequisition.AddOnlineInvoice(onlineInvoiceModel);
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
        public HttpResponseMessage GetAllOnlineInvoiceAndPayment()
        {
            var imeiOb = _onlineRequisition.GetAllOnlineInvoiceAndPayment();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, imeiOb, formatter);
        }

        [HttpGet]
        public HttpResponseMessage GetInvoiceNo(long masterId)
        {
            var imeiOb = _onlineRequisition.GetInvoiceNo(masterId);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, imeiOb, formatter);
        }
        [HttpGet]
        public HttpResponseMessage GetAmount(long masterId)
        {
            var imeiOb = _onlineRequisition.GetAmount(masterId);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, imeiOb, formatter);
        }

        [HttpPost]
        public HttpResponseMessage InsertOnlinePaymentReceive()
        {
            var imeiOb = _onlineRequisition.InsertOnlinePaymentReceive();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, imeiOb, formatter);
        }

    }
}