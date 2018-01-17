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
    public class InternalRequisitionController : ApiController
    {
        private IInternalRequisitionRepository internalRequisitionRepository;

        public InternalRequisitionController()
        {
            this.internalRequisitionRepository = new InternalRequisitionRepository();
        }

        public InternalRequisitionController(IInternalRequisitionRepository internalRequisitionRepository)
        {
            this.internalRequisitionRepository = internalRequisitionRepository;
        }

        [HttpPost, ActionName("AddInternalRequisition")]
        public HttpResponseMessage AddInternalRequisition([FromBody] InternalRequisitionModel objInternalRequisitionModel)
        {
            try
            {



                if (string.IsNullOrEmpty(objInternalRequisitionModel.InternalRequisitionMaster.from_warehouse_id.ToString()))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Please Select From Warehouse!!" }, formatter);

                }
                if (string.IsNullOrEmpty(objInternalRequisitionModel.InternalRequisitionMaster.to_department.ToString()))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Please Select To Department!!" }, formatter);

                }

                else
                {
                    internalRequisitionRepository.AddInternalRequisition(objInternalRequisitionModel);
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Requisition save successfully" }, formatter);

                }

            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Sorry Internal Requisition Failed!" }, formatter);
            }
        }

        [HttpGet, ActionName("GetAllInternalRequisitionRfd")]
        public HttpResponseMessage GetAllInternalRequisitionRfd()
        {
            try
            {
                var data = internalRequisitionRepository.GetAllInternalRequisitionRfd();
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, data, formatter);
            }
            catch (Exception)
            {

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Error Occurred!" }, formatter);
            }
        }
          [HttpGet, ActionName("GetAllInternalDeliveryList")]
        public HttpResponseMessage GetAllInternalDeliveryList()
        {
            try
            {
                var data = internalRequisitionRepository.GetAllInternalDeliveryList();
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, data, formatter);
            }
            catch (Exception)
            {

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Error Occurred!" }, formatter);
            }
        }
        
        [HttpGet, ActionName("ConfirmDelivery")]
        public HttpResponseMessage ConfirmDelivery(long delivery_master_id, long user_id)
        {
            try
            {

                var delivered = internalRequisitionRepository.ConfirmDelivery(delivery_master_id, user_id);
                if (delivered == true)
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Delivered Successfully" }, formatter);
                }
                else
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = "Delivered Failed" }, formatter);
                }


            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }


        }
        [HttpGet, ActionName("CancelDelivery")]
        public HttpResponseMessage CancelDelivery(long delivery_master_id, long user_id)
        {
            try
            {

                var cancel = internalRequisitionRepository.CancelDelivery(delivery_master_id, user_id);
                if (cancel == true)
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Delivery Canceled!" }, formatter);
                }
                else
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = "Cancel Failed" }, formatter);
                }


            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }


        }
        [HttpGet, ActionName("GetInternalDeliveryReportById")]
        public HttpResponseMessage GetInternalDeliveryReportById(long delivery_master_id, long user_id)
        {
            try
            {
                var data = internalRequisitionRepository.GetInternalDeliveryReportById(delivery_master_id, user_id);
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, data, formatter);
            }
            catch (Exception)
            {

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Error Occurred!" }, formatter);
            }
        }
        [HttpGet, ActionName("InternalInvoiceReport")]
        public HttpResponseMessage InternalInvoiceReport(long internal_requisition_master_id, long user_id)
        {
            try
            {
                var data = internalRequisitionRepository.GetInternalInvoiceReport(internal_requisition_master_id, user_id);
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, data, formatter);
            }
            catch (Exception)
            {

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Error Occurred!" }, formatter);
            }
        }
    }
}