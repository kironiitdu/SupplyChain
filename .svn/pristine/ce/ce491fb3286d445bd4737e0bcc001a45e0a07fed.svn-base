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
    public class InternalEmiRequisitionController : ApiController
    {
        private IInternalEmiRequisitionRepository internalEmiRequisitionRepository;

        public InternalEmiRequisitionController()
        {
            this.internalEmiRequisitionRepository = new InternalEmiRequisitionRepository();
        }

        public InternalEmiRequisitionController(IInternalEmiRequisitionRepository internalEmiRequisitionRepository)
        {
            this.internalEmiRequisitionRepository = internalEmiRequisitionRepository;
        }

        [HttpPost, ActionName("AddInternalEmiRequisition")]
        public HttpResponseMessage AddInternalEmiRequisition([FromBody] InternalEmiRequisitionModel internalEmiRequisitionModel)
        {
            try
            {
                if (string.IsNullOrEmpty(internalEmiRequisitionModel.InternalRequisitionMaster.from_warehouse_id.ToString()))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Please Select From Warehouse!!" }, formatter);

                }
                if (string.IsNullOrEmpty(internalEmiRequisitionModel.InternalRequisitionMaster.to_department.ToString()))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Please Select To Department!!" }, formatter);

                }

                else
                {
                    internalEmiRequisitionRepository.AddInternalEmiRequisition(internalEmiRequisitionModel);
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Requisition save successfully" }, formatter);

                }

            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Sorry Internal EMI Requisition Failed!" }, formatter);
            }
        }

        [HttpGet, ActionName("GetAllInternalEmiRequisitionRfd")]
        public HttpResponseMessage GetAllInternalEmiRequisitionRfd()
        {
            try
            {
                var data = internalEmiRequisitionRepository.GetAllInternalEmiRequisitionRfd();
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, data, formatter);
            }
            catch (Exception)
            {

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Error Occurred!" }, formatter);
            }
        }
        
        [HttpGet, ActionName("GetInternalEmiDeliveryReportById")]
        public HttpResponseMessage GetInternalEmiDeliveryReportById(long delivery_master_id, long user_id)
        {
            try
            {
                var data = internalEmiRequisitionRepository.GetInternalEmiDeliveryReportById(delivery_master_id, user_id);
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, data, formatter);
            }
            catch (Exception)
            {

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Error Occurred!" }, formatter);
            }
        }

        [HttpGet, ActionName("GetInternalEmiInvoiceReport")]
        public HttpResponseMessage GetInternalEmiInvoiceReport(long internal_requisition_master_id, long user_id)
        {
            try
            {
                var data = internalEmiRequisitionRepository.GetInternalEmiInvoiceReport(internal_requisition_master_id, user_id);
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, data, formatter);
            }
            catch (Exception)
            {

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Error Occurred!" }, formatter);
            }
        }

        [HttpGet, ActionName("GetAllInternalEmiDeliveryList")]
        public HttpResponseMessage GetAllInternalEmiDeliveryList()
        {
            try
            {
                var data = internalEmiRequisitionRepository.GetAllInternalEmiDeliveryList();
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
                var delivered = internalEmiRequisitionRepository.ConfirmDelivery(delivery_master_id, user_id);
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

                var cancel = internalEmiRequisitionRepository.CancelDelivery(delivery_master_id, user_id);
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
    }
}