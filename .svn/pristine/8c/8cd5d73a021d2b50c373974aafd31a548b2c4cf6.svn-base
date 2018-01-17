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
    public class LocalPurchaseOrderController : ApiController
    {
        private ILocalPurchaseOrderRepository localPurchaseOrderRepository;

        public LocalPurchaseOrderController()
        {
            this.localPurchaseOrderRepository = new LocalPurchaseOrderRepository();
        }

        public LocalPurchaseOrderController(ILocalPurchaseOrderRepository localPurchaseOrderRepository)
        {
            this.localPurchaseOrderRepository = localPurchaseOrderRepository;
        }

        [HttpGet, ActionName("GetAllLocalPurchaseOrders")]
        public HttpResponseMessage GetAllLocalPurchaseOrders()
        {
            var data = localPurchaseOrderRepository.GetAllLocalPurchaseOrders();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, data, formatter);
        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage Post([FromBody] LocalPurchaseOrderModel localPurchaseOrderModel)
        {
            try
            {

                var details = localPurchaseOrderModel.LocalPoDetailsList;

                if (string.IsNullOrEmpty(localPurchaseOrderModel.LocalPoMasterData.supplier_id.ToString()))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = "Please Select Supplier !!" }, formatter);
                }
                if (string.IsNullOrEmpty(localPurchaseOrderModel.LocalPoMasterData.company_id.ToString()))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = "Please Select Company !!" }, formatter);
                }
                if (string.IsNullOrEmpty(localPurchaseOrderModel.LocalPoMasterData.currency_id.ToString()))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = "Please Select Currency !!" }, formatter);
                }
                if (details.Count<1)
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = "Please give the product list information !!" }, formatter);
                }
                else
                {
                    localPurchaseOrderRepository.AddLocalPurchaseOrder(localPurchaseOrderModel);
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Save successfully" }, formatter);
                }

            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = ex.ToString() }, formatter);
            }
        }
    }
}