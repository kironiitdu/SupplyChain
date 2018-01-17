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
    public class ShippingMethodController : ApiController
    {
        private IShippingMethodRepository shippingMethodRepository;

        public ShippingMethodController()
        {
            this.shippingMethodRepository = new ShippingMethodRepository();
        }

        public ShippingMethodController(IShippingMethodRepository shippingMethodRepository)
        {
            this.shippingMethodRepository = shippingMethodRepository;
        }

        public HttpResponseMessage GetShippingMethods()
        {
            var data = shippingMethodRepository.GetShippingMethods();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, data, formatter);
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody]Models.shipping_method shipping_method)
        {
            try
            {
                if (string.IsNullOrEmpty(shipping_method.shipping_method_name))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Shipping Method is Empty" }, formatter);
                }
                if (shippingMethodRepository.CheckDuplicateShippingMethods(shipping_method.shipping_method_name))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Shipping Method Already Exists" }, formatter);
                }
                else
                {
                    Models.shipping_method insertShippingMethods = new Models.shipping_method
                    {

                        shipping_method_name = shipping_method.shipping_method_name,
                        is_active = true,
                        is_deleted = false,
                        created_by = shipping_method.created_by,
                        created_date = DateTime.Now,
                        updated_by = shipping_method.updated_by,
                        updated_date = DateTime.Now
                    };
                    bool save = shippingMethodRepository.InsertShippingMethods(insertShippingMethods);

                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Shipping Method save successfully" }, formatter);
                }

            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [HttpPut]
        public HttpResponseMessage Put([FromBody]Models.shipping_method shipping_method)
        {
            try
            {
                if (string.IsNullOrEmpty(shipping_method.shipping_method_name))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Shipping Method is Empty" }, formatter);
                }
                else
                {
                    Models.shipping_method updateShippingMethod = new Models.shipping_method

                    {
                        shipping_method_id = shipping_method.shipping_method_id,
                        shipping_method_name = shipping_method.shipping_method_name,
                        is_active = shipping_method.is_active,
                        updated_by = shipping_method.updated_by,
                        updated_date = DateTime.Now

                    };

                    bool irepoUpdate = shippingMethodRepository.UpdateShippingMethods(updateShippingMethod);

                    if (irepoUpdate == true)
                    {
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Shipping Method Update successfully" }, formatter);
                    }
                    else
                    {
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Update Failed" }, formatter);
                    }
                }
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [HttpDelete]
        public HttpResponseMessage Delete([FromBody]Models.shipping_method shipping_method)
        {
            try
            {
                bool updatShippingMethod = shippingMethodRepository.DeleteShippingMethods(shipping_method.shipping_method_id);

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Shipping Method Delete Successfully." }, formatter);
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }
    }
}