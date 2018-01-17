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

namespace DMSApi.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ProductVersionMappingController : ApiController
    {
        private readonly IProductVersionMappingRepository _mappingRepository;

        public ProductVersionMappingController()
        {
            this._mappingRepository = new ProductVersionMappingRepository();
        }

        public ProductVersionMappingController(ProductVersionMappingRepository mappingRepository)
        {
            this._mappingRepository = mappingRepository;
        }

         [HttpGet, ActionName("GetAllVersionMapping")]
        public HttpResponseMessage GetAllVersionMapping()
        {
            try
            {
                var mapping = _mappingRepository.GetAllVersionMapping();
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, mapping, formatter);
            }
            catch (Exception ex)
            {

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [HttpGet, ActionName("GetProductwiseVersion")]
         public HttpResponseMessage GetProductwiseVersion()
        {
            try
            {
                var mapping = _mappingRepository.GetProductwiseVersion();
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, mapping, formatter);
            }
            catch (Exception ex)
            {

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage Post([FromBody] Models.product_version_mapping productColor, long create_by)
        {

            try
            {
                if (productColor.product_id == null || productColor.product_id == 0)
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Select Product Name" }, formatter);

                }
                if (productColor.product_version_id == null || productColor.product_version_id == 0)
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Select Version" }, formatter);

                }


                if (!_mappingRepository.CheckDuplicateMapping(productColor))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Same Product And Version Mapping Already Exists! " }, formatter);
                }
                else
                {

                    _mappingRepository.AddVersionMapping(productColor, create_by);
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Mapping Saved" }, formatter);
                }
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [System.Web.Http.HttpPut]
        public HttpResponseMessage Put([FromBody] Models.product_version_mapping productColor, long update_by)
        {
            try
            {
                if (!_mappingRepository.CheckDuplicateMappingForUpdate(productColor))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Same Product And Version Mapping Already Exists " }, formatter);
                }
                else
                {
                    _mappingRepository.EditVersionrMapping(productColor, update_by);

                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Mapping Update Successfully" }, formatter);
                }

            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [System.Web.Http.HttpDelete]
        public HttpResponseMessage Delete([FromBody]Models.product_version_mapping productColor)
        {
            try
            {
                bool deleteArea = _mappingRepository.DeleteVersionMapping(productColor.product_version_mapping_id);

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Mapping Deleted Successfully." }, formatter);
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }

        }
    }
}