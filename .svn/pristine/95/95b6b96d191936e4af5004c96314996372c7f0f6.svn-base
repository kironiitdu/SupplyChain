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
    public class ProductColorMappingController : ApiController
    {
        private readonly IProductColorMappingRepository _mappingRepository;

        public ProductColorMappingController()
        {
            this._mappingRepository = new ProductColorRepository();
        }

        public ProductColorMappingController(ProductColorRepository mappingRepository)
        {
            this._mappingRepository = mappingRepository;
        }


        [HttpGet, ActionName("GetAllColorMapping")]
        public HttpResponseMessage GetAllColorMapping()
        {
            try
            {
                var mapping = _mappingRepository.GetAllColorMapping();
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, mapping, formatter);
            }
            catch (Exception ex)
            {

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [HttpGet, ActionName("GetAllColorMappingForTransection")]
        public HttpResponseMessage GetAllColorMappingForTransection()
        {
            try
            {
                var mapping = _mappingRepository.GetAllColorMappingForTransection();
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, mapping, formatter);
            }
            catch (Exception ex)
            {

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [HttpGet, ActionName("GetProductwiseColor")]
        public HttpResponseMessage GetProductwiseColor()
        {
            try
            {
                var mapping = _mappingRepository.GetProductwiseColor();
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, mapping, formatter);
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [HttpGet, ActionName("GetColorByProductId")]
        public HttpResponseMessage GetColorByProductId(long productId)
        {
            try
            {
                var mapping = _mappingRepository.GetColorByProductId(productId);
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
        public HttpResponseMessage Post([FromBody] Models.product_color_mapping productColor, long create_by)
        {

            try
            {
                if (productColor.product_id == null || productColor.product_id == 0)
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Select Product Name" }, formatter);

                }
                if (productColor.color_id == null || productColor.color_id == 0)
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Select Color Name" }, formatter);

                }


                if (!_mappingRepository.CheckDuplicateMapping(productColor))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Same Product And Color Mapping Already Exists! " }, formatter);
                }
                else
                {

                    _mappingRepository.AddColorMapping(productColor, create_by);
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
        public HttpResponseMessage Put([FromBody] Models.product_color_mapping productColor, long update_by)
        {
            try
            {
                if (!_mappingRepository.CheckDuplicateMappingForUpdate(productColor))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Same Product And Color Mapping Already Exists " }, formatter);
                }
                else
                {
                    _mappingRepository.EditColorMapping(productColor, update_by);

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
        public HttpResponseMessage Delete([FromBody]Models.product_color_mapping productColor)
        {
            try
            {
                bool deleteArea = _mappingRepository.DeleteColorMapping(productColor.product_color_mapping_id);

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