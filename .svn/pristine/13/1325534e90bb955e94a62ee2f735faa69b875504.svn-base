using System;
using System.Collections.Generic;
using System.IO;
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
    public class EshopRequisitionController : ApiController
    {
        private readonly IEshopRequisitionRepository _eshopRequisitionRepository;

        public EshopRequisitionController()
        {
            this._eshopRequisitionRepository = new EshopRequisitionRepository();
        }

        public EshopRequisitionController(EshopRequisitionRepository eshopRequisitionRepository)
        {
            this._eshopRequisitionRepository = eshopRequisitionRepository;
        }

        [HttpGet]
        public HttpResponseMessage GetShippingMethod()
        {
            var imeiOb = _eshopRequisitionRepository.GetShippingMethod();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, imeiOb, formatter);
        }
        [HttpGet]
        public HttpResponseMessage GetPaymentMethod()
        {
            var imeiOb = _eshopRequisitionRepository.GetPaymentMethod();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, imeiOb, formatter);
        }
        [HttpGet]
        public HttpResponseMessage GetEshopRfdList()
        {
            var imeiOb = _eshopRequisitionRepository.GetEshopRfdList();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, imeiOb, formatter);
        }

        [HttpGet]
        public HttpResponseMessage EshopInvoiceReport(long requisition_master_id)
        {
            var imeiOb = _eshopRequisitionRepository.EshopInvoiceReport(requisition_master_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, imeiOb, formatter);
        }

        [HttpGet]
        public HttpResponseMessage EshopDeliveryChallanReport(long delivery_master_id)
        {
            var imeiOb = _eshopRequisitionRepository.EshopDeliveryChallanReport(delivery_master_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, imeiOb, formatter);
        }

        [HttpGet]
        public HttpResponseMessage GetEshopReadyForDelivery(long delivery_master_id)
        {
            var imeiOb = _eshopRequisitionRepository.GetEshopReadyForDelivery(delivery_master_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, imeiOb, formatter);
        }

        [HttpGet]
        public HttpResponseMessage GetProductDetailsForDelivery(long delivery_master_id)
        {
            var imeiOb = _eshopRequisitionRepository.GetProductDetailsForDelivery(delivery_master_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, imeiOb, formatter);
        }

        [HttpGet]
        public HttpResponseMessage GetEshopRfdConfirmList()
        {
            var imeiOb = _eshopRequisitionRepository.GetEshopRfdConfirmList();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, imeiOb, formatter);
        }

        [HttpGet]
        public HttpResponseMessage GetProductForReturn(long delivery_master_id)
        {
            var imeiOb = _eshopRequisitionRepository.GetProductForReturn(delivery_master_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, imeiOb, formatter);
        }

        [HttpGet]
        public HttpResponseMessage GetIMEIForReturn(long imei)
        {
            var imeiOb = _eshopRequisitionRepository.GetIMEIForReturn(imei);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, imeiOb, formatter);
        }

        [HttpGet]
        public HttpResponseMessage CancelEshopRequisition(long delivery_master_id)
        {
            var imeiOb = _eshopRequisitionRepository.CancelEshopRequisition(delivery_master_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, imeiOb, formatter);
        }

        [HttpGet]
        public HttpResponseMessage GetInvoiceForEshoPayment(long delivery_master_id)
        {
            var imeiOb = _eshopRequisitionRepository.GetInvoiceForEshoPayment(delivery_master_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, imeiOb, formatter);
        }

        [HttpPost]
        public HttpResponseMessage EshopReturnInsert(EshopReturnModel eshopReturnModel)
        {
            var imeiOb = _eshopRequisitionRepository.EshopReturnInsert(eshopReturnModel);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, imeiOb, formatter);
        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage Post([FromBody] EshopRequisitionModel onlineRequisitionModel)
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

                var value = _eshopRequisitionRepository.AddEshopRequisition(onlineRequisitionModel);
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, value, formatter);
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [System.Web.Http.HttpPut]
        public HttpResponseMessage Put()
        {
            try
            {
                HttpRequest rsk = HttpContext.Current.Request;

                var postedFile = rsk.Files["UploadedImage"];

                string delivery_method_id = rsk.Form["delivery_method_id"].ToString();
                string deliverd_master_id = rsk.Form["deliverd_master_id"].ToString();
                string delivery_person_name = rsk.Form["delivery_person_name"].ToString();
                string courier_slip_no = rsk.Form["courier_slip_no"].ToString();
                string courier_id = rsk.Form["courier_id"].ToString();
                string created_by = rsk.Form["created_by"].ToString();

                string actualFileName = "";
                string filePathForDb = "";
                if (courier_id == "")
                {
                    courier_id = "0";
                }

                if (postedFile == null)
                {
                    filePathForDb = "";
                }
                else
                {
                    actualFileName = postedFile.FileName;
                    var clientDir = HttpContext.Current.Server.MapPath("~/App_Data/Delivery_Challan/");
                    bool exists = System.IO.Directory.Exists(clientDir);
                    if (!exists)
                        System.IO.Directory.CreateDirectory(clientDir);
                    var fileSavePath = Path.Combine(clientDir, actualFileName);
                    bool checkFileSave = false;
                    try
                    {
                        postedFile.SaveAs(fileSavePath);
                        checkFileSave = true;
                    }
                    catch
                    {
                        checkFileSave = false;
                    }
                }

                var deliveryMaster = new delivery_master();
                deliveryMaster.delivery_master_id = Convert.ToInt64(deliverd_master_id);
                deliveryMaster.courier_id = Convert.ToInt64(courier_id);
                deliveryMaster.delivery_method_id = Convert.ToInt32(delivery_method_id);
                deliveryMaster.courier_slip_no = courier_slip_no;
                deliveryMaster.challan_copy = actualFileName;
                deliveryMaster.delivery_person_name = delivery_person_name;
                deliveryMaster.updated_by = Convert.ToInt64(created_by);

                var value = _eshopRequisitionRepository.UpdateEshopDelivery(deliveryMaster);
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, value, formatter);
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [HttpPost]
        public HttpResponseMessage InsertPaymentReceive()
        {
            var imeiOb = _eshopRequisitionRepository.InsertPaymentReceive();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, imeiOb, formatter);
        }
    }
}