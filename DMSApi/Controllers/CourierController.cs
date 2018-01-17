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

namespace DMSApi.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]

    public class CourierController : ApiController
    {
        private ICourierRepository courierRepository;
        public CourierController()
        {
            this.courierRepository = new CourierRepository();
        }
        public HttpResponseMessage GetAllCouriers()
        {
            var couriers = courierRepository.GetAllCouriers();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, couriers, formatter);
        }
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Post([FromBody] Models.courier courier)
        {

            try
            {
                if (string.IsNullOrEmpty(courier.courier_name))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Courier is Empty" }, formatter);

                }
                else
                {
                    if (courierRepository.CheckDuplicateCouriers(courier.courier_name))
                    {
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Courier Already Exists" }, formatter);
                    }
                    else
                    {
                        courier insert_brand = new courier
                        {
                            courier_name = courier.courier_name,
                            cell = courier.cell

                        };

                        courierRepository.AddCourier(insert_brand);
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Courier saved successfully" }, formatter);
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
        public HttpResponseMessage Put([FromBody] Models.courier courier)
        {
            try
            {
                if (courierRepository.CheckDuplicateCouriers(courier.courier_id,courier.courier_name))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Courier Already Exists" }, formatter);
                }
                else
                {
                    Models.courier updateCourier = new Models.courier
                    {
                        courier_id = courier.courier_id,
                        courier_name = courier.courier_name,
                        cell = courier.cell

                    };

                    courierRepository.Editcourier(updateCourier);
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Courier updated successfully" }, formatter);
                }

            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [System.Web.Http.HttpDelete]
        public HttpResponseMessage Delete([FromBody]Models.courier courier)
        {
            try
            {
                bool updatCourier = courierRepository.Deletecourier(courier.courier_id);

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Courier Deleted Successfully." }, formatter);
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }

        }
        [ActionName("GetcourierById")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage GetcourierById([FromBody]Models.courier courier)
        {
            var courierId = courier.courier_id;

            var cour = courierRepository.GetcourierById(courierId);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, cour);
            return response;
        }
    }
}