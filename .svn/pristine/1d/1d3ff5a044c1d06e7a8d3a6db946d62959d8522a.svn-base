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
    public class ShippingTermsController : ApiController
    {
        private IShippingTermsRepository shippingTermsRepository;

        public ShippingTermsController()
        {
            this.shippingTermsRepository = new ShippingTermsRepository();
        }

        public ShippingTermsController(IShippingTermsRepository shippingTermsRepository)
        {
            this.shippingTermsRepository = shippingTermsRepository;
        }

        public HttpResponseMessage GetAllShippingTerms()
        {
            var data = shippingTermsRepository.GetAllShippingTerms();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, data, formatter);
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody]Models.shipping_terms shipping_terms)
        {
            try
            {
                if (string.IsNullOrEmpty(shipping_terms.shipping_terms_name))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Shipping Terms is Empty" }, formatter);
                }
                if (shippingTermsRepository.CheckDuplicateShippingTerms(shipping_terms.shipping_terms_name))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Shipping Terms Already Exists" }, formatter);
                }
                else
                {
                    Models.shipping_terms insertShippingTerms = new Models.shipping_terms
                    {

                        shipping_terms_name = shipping_terms.shipping_terms_name,
                        is_active = true,
                        is_deleted = false,
                        created_by = shipping_terms.created_by,
                        created_date = DateTime.Now,
                        updated_by = shipping_terms.updated_by,
                        updated_date = DateTime.Now
                    };
                    bool save = shippingTermsRepository.InsertShippingTerms(insertShippingTerms);

                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Shipping Terms save successfully" }, formatter);
                }

            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [HttpPut]
        public HttpResponseMessage Put([FromBody]Models.shipping_terms shipping_terms)
        {
            try
            {
                if (string.IsNullOrEmpty(shipping_terms.shipping_terms_name))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Shipping Terms is Empty" }, formatter);
                }
                else
                {
                    Models.shipping_terms updateShippingTerms = new Models.shipping_terms

                    {
                        shipping_terms_id = shipping_terms.shipping_terms_id,
                        shipping_terms_name = shipping_terms.shipping_terms_name,
                        is_active = shipping_terms.is_active,
                        updated_by = shipping_terms.updated_by,
                        updated_date = DateTime.Now

                    };

                    bool irepoUpdate = shippingTermsRepository.UpdateShippingTerms(updateShippingTerms);

                    if (irepoUpdate == true)
                    {
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Shipping Terms Update successfully" }, formatter);
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
        public HttpResponseMessage Delete([FromBody]Models.shipping_terms shipping_terms)
        {
            try
            {
                bool updatShippingTerms = shippingTermsRepository.DeleteShippingTerms(shipping_terms.shipping_terms_id);

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Shipping Terms Delete Successfully." }, formatter);
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }
    }
}