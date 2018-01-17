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
    public class DeliveryController : ApiController
    {

        private IDeliveryRepository deliveryRepository;

        public DeliveryController()
        {
            this.deliveryRepository = new DeliveryRepository();
        }

        public DeliveryController(IDeliveryRepository deliveryRepository)
        {
            this.deliveryRepository = deliveryRepository;
        }

        public HttpResponseMessage GetAllDelivery()
        {
            var countries = deliveryRepository.GetAllDelivery();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, countries, formatter);
        }
        [HttpGet, ActionName("GetAllDeliveryByPartyId")]
        public HttpResponseMessage GetAllDeliveryByPartyId(long party_id)
        {
            var data = deliveryRepository.GetAllDeliveryByPartyId(party_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, data, formatter);
        }
        [HttpGet, ActionName("GetAllDeliveredByPartyId")]
        public HttpResponseMessage GetAllDeliveredByPartyId(long party_id)
        {
            var data = deliveryRepository.GetAllDeliveredByPartyId(party_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, data, formatter);
        }
        public object GetDeliveryReportById(long delivery_master_id)
        {
            var countries = deliveryRepository.GetDeliveryReportById(delivery_master_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, countries, formatter);
        }

        public object GetDeliveryExcelReportByDeliveryMasterId(long delivery_master_id)
        {
            var countries = deliveryRepository.GetDeliveryExcelReportByDeliveryMasterId(delivery_master_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, countries, formatter);
        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage Post([FromBody] DeliveryModel deliveryModel)
        {

            try
            {
                //if (string.IsNullOrEmpty(deliveryModel.DeliveryMasterData.delivery_date))
                //{
                //    var formatter = RequestFormat.JsonFormaterString();
                //    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Delivery Date Empty !!" }, formatter);

                //}

                if (string.IsNullOrEmpty(deliveryModel.DeliveryMasterData.party_id.ToString()))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Please Select Party !!" }, formatter);

                }
                if (string.IsNullOrEmpty(deliveryModel.DeliveryMasterData.requisition_master_id.ToString()))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Please Select Requisition No !!" }, formatter);

                }
                if (string.IsNullOrEmpty(deliveryModel.DeliveryMasterData.from_warehouse_id.ToString()))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Please Select From Warehouse !!" }, formatter);

                }
                if (string.IsNullOrEmpty(deliveryModel.DeliveryMasterData.to_warehouse_id.ToString()))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Please Select To Warehouse !!" }, formatter);

                }
                //if (string.IsNullOrEmpty(deliveryModel.DeliveryMasterData.lot_no))
                //{
                //    var formatter = RequestFormat.JsonFormaterString();
                //    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Lot No Empty !!" }, formatter);

                //}
                else
                {

                    var x = deliveryRepository.AddDelivery(deliveryModel);
                    if (x == 1)
                    {
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Delivery save successfully" }, formatter);
                    }
                    else if(x==0)
                    {
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "IMEI Already Delivered !!!" }, formatter);
                    }
                    else if (x == 3)
                    {
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "IMEI Scan For Wrong Product Or Color !!!" }, formatter);
                    } 
                    else
                    {
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "IMEI Not Found !!!" }, formatter);
                    }
                }
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }
        [System.Web.Http.HttpPut]
        public HttpResponseMessage Put([FromBody] Models.delivery_master updateCourierInformation)
        {
            try
            {
                if (string.IsNullOrEmpty(updateCourierInformation.delivery_method_id.ToString()))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation {output = "warning", msg = "Please Select Delivery Method"}, formatter);

                }
                else
                {
                    if (updateCourierInformation.delivery_method_id == 1 && string.IsNullOrEmpty(updateCourierInformation.courier_id.ToString()))
                    {
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = "Please Select Courier Name" }, formatter);
                    }
                    else if (updateCourierInformation.delivery_method_id == 2 && string.IsNullOrEmpty(updateCourierInformation.delivery_person_name))
                    {
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = "Please Input Delivery Person Name" }, formatter);
                    }
                    
                    else
                    {
                        var objUpdateCourier = new delivery_master
                        {
                            delivery_master_id = updateCourierInformation.delivery_master_id,
                            delivery_method_id = updateCourierInformation.delivery_method_id,
                            updated_by = updateCourierInformation.updated_by
                        };
                        if (updateCourierInformation.delivery_method_id == 1)
                        {
                            objUpdateCourier.courier_id = updateCourierInformation.courier_id;
                        }
                        else if (updateCourierInformation.delivery_method_id == 2)
                        {
                            objUpdateCourier.delivery_person_name = updateCourierInformation.delivery_person_name;
                        }
                        
                        bool success = deliveryRepository.UpdateDeliveryMethod(objUpdateCourier);

                        if (success)
                        {
                            var formatter = RequestFormat.JsonFormaterString();
                            return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Delivery Information Updated Successfully" }, formatter);
                        }
                        else
                        {
                            var formatter = RequestFormat.JsonFormaterString();
                            return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Delivery Information Updated Failed" }, formatter);
                        }
                        
                    }
                }
                
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        // UpdateCourierInfo
        [System.Web.Http.HttpPut]
        public HttpResponseMessage UpdateCourierInfo(Models.delivery_master updateCourierInformation)
        {
            try
            {
                if (string.IsNullOrEmpty(updateCourierInformation.delivery_method_id.ToString()))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "warning", msg = "Please Select Delivery Method" }, formatter);

                }
                else
                {
                    if (updateCourierInformation.delivery_method_id == 1 && string.IsNullOrEmpty(updateCourierInformation.courier_slip_no))
                    {
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = "Please Input Courier Slip No." }, formatter);
                    }
                    else if (updateCourierInformation.delivery_method_id == 2)
                    {
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = "Wrong Delivery Process" }, formatter);
                    }

                    else
                    {
                        var objUpdateCourier = new delivery_master
                        {
                            delivery_master_id = updateCourierInformation.delivery_master_id,
                            courier_slip_no = updateCourierInformation.courier_slip_no,
                            updated_by = updateCourierInformation.updated_by
                        };
                        bool success = deliveryRepository.UpdateCourierInfo(objUpdateCourier);

                        if (success)
                        {
                            var formatter = RequestFormat.JsonFormaterString();
                            return Request.CreateResponse(HttpStatusCode.OK, new Confirmation {output = "success", msg = "Courier Information Updated Successfully"},
                                formatter);
                        }
                        else
                        {
                            var formatter = RequestFormat.JsonFormaterString();
                            return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Courier Information Updated Failed" },
                                formatter);
                        }


                    }
                }

            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [HttpPost, ActionName("UploadDeliveryChallan")]
        public HttpResponseMessage UploadPiAttachment()
        {
            bool data = deliveryRepository.UploadDeliveryChallan();
            if (data == true)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Delivery Challan Uploaded Successfully" }, formatter);
            }
            else
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Upload Failed !!" }, formatter);
            }
        }
        //[HttpGet, ActionName("UpdateApproveStatus")]
        ////public HttpResponseMessage UpdateApproveStatus(long delivery_master_id)
        //public HttpResponseMessage UpdateApproveStatus(long delivery_master_id, long userid)
        //{
        //    try
        //    {
        //        if (delivery_master_id > 0)
        //        {
        //            //deliveryRepository.UpdateApproveStatus(delivery_master_id);
        //            deliveryRepository.UpdateApproveStatus(delivery_master_id, userid);
        //            var formatter = RequestFormat.JsonFormaterString();
        //            return Request.CreateResponse(HttpStatusCode.OK,
        //                new Confirmation { output = "success", msg = "Received successfully" }, formatter);
        //        }
        //        else
        //        {
        //            var formatter = RequestFormat.JsonFormaterString();
        //            return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Invalid Requisition" }, formatter);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        var formatter = RequestFormat.JsonFormaterString();
        //        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
        //    }
        //}


        [HttpGet, ActionName("GetCourierInformation")]
        public HttpResponseMessage GetCourierInformation(long delivery_master_id)
        {
            var courierInformation = deliveryRepository.GetCourierInformation(delivery_master_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, courierInformation, formatter);
        }

        [HttpGet, ActionName("GetDeliveryByIdForConfirmation")]
        public HttpResponseMessage GetDeliveryByIdForConfirmation(long delivery_master_id)
        {
            var deliveryInfo = deliveryRepository.GetDeliveryByIdForConfirmation(delivery_master_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, deliveryInfo, formatter);
        }

        [System.Web.Http.HttpPut]
        public HttpResponseMessage CancelDelivery(Models.delivery_master deliveryMaster)
        {
            try
            {
                bool success = deliveryRepository.CancelDelivery(deliveryMaster);

                if (success)
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Courier Information Updated Successfully" },
                        formatter);
                }
                else
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Courier Information Updated Failed" },
                        formatter);
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