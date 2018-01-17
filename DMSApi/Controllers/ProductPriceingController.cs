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
    public class ProductPriceingController : ApiController
    {
        private readonly IProductPriceingReporsitory _mappingRepository;

        public ProductPriceingController()
        {
            this._mappingRepository = new ProductPriceingRepository();
        }

        public ProductPriceingController(ProductPriceingRepository mappingRepository)
        {
            this._mappingRepository = mappingRepository;
        }

        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetAllProductPriceing()
        {
            try
            {
                var mapping = _mappingRepository.GetAllProductPriceing();
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, mapping, formatter);
            }
            catch (Exception ex)
            {

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetProductPriceMappingBtId(long product_price_mapping_id)
        {
            try
            {
                var mapping = _mappingRepository.GetProductPriceMappingBtId(product_price_mapping_id);
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
        public HttpResponseMessage Post([FromBody] Models.product_price_mapping productPrice, long create_by)
        {

            try
            {
                if (productPrice.product_id == null || productPrice.product_id == 0)
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Select Product Name" }, formatter);

                }
                if (productPrice.has_serial == true)
                {
                    if (productPrice.color_id == null || productPrice.color_id == 0)
                    {
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Select Color" }, formatter);

                    }
                }
                
                if (!_mappingRepository.CheckDuplicatePriceing(productPrice))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Same Product And Version Mapping Already Exists! " }, formatter);
                }
                else
                {

                    _mappingRepository.AddProductPriceing(productPrice, create_by);
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
        public HttpResponseMessage Put([FromBody] Models.product_price_mapping productPrice, long update_by)
        {
            try
            {

                _mappingRepository.EditProductPricing(productPrice, update_by);

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Mapping Update Successfully" }, formatter);


            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [System.Web.Http.HttpDelete]
        public HttpResponseMessage Delete([FromBody]Models.product_price_mapping productPrice)
        {
            try
            {
                bool deleteArea = _mappingRepository.DeleteProductPriceing(productPrice.product_price_mapping_id);

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Mapping Deleted Successfully." }, formatter);
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }

        }

        [HttpGet]
        public HttpResponseMessage GetProductVersionByProductId(long product_id)
        {
            try
            {
                var mapping = _mappingRepository.GetProductVersionByProductId(product_id);
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, mapping, formatter);
            }
            catch (Exception ex)
            {

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }
        [HttpGet]
        public HttpResponseMessage GetColorByProductId(long product_id)
        {
            try
            {
                var mapping = _mappingRepository.GetColorByProductId(product_id);
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, mapping, formatter);
            }
            catch (Exception ex)
            {

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetProductwiseColor()
        {
            try
            {
                var colors = _mappingRepository.GetProductwiseColor();
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, colors, formatter);
            }
            catch (Exception ex)
            {

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetPriceingForLogReportPageView(long product_id, long color_id, long product_version_id)
        {
            try
            {
                var colors = _mappingRepository.GetPriceingForLogReportPageView(product_id, color_id, product_version_id);
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, colors, formatter);
            }
            catch (Exception ex)
            {

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

       [HttpGet, ActionName("GetProductColorwiseVersion")]
        public HttpResponseMessage GetProductColorwiseVersion(long aa, long bb)
        //public HttpResponseMessage GetProductColorwiseVersion()
        {
            try
            {
                var versions = _mappingRepository.GetProductColorwiseVersion(aa, bb);
                //var versions = _mappingRepository.GetProductColorwiseVersion();
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, versions, formatter);
            }
            catch (Exception ex)
            {

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

       [HttpGet, ActionName("GetProductColorVersionwisePrice")]
       public HttpResponseMessage GetProductColorVersionwisePrice(long party_type_id,long product_id, long color_id, long product_version_id)
       {
           try
           {
               var pricing = _mappingRepository.GetProductColorVersionwisePrice(party_type_id, product_id, color_id, product_version_id);
               var formatter = RequestFormat.JsonFormaterString();
               return Request.CreateResponse(HttpStatusCode.OK, pricing, formatter);
           }
           catch (Exception ex)
           {

               var formatter = RequestFormat.JsonFormaterString();
               return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
           }
       }
       [HttpGet, ActionName("GetProductColorVersionwiseB2BPrice")]
       public HttpResponseMessage GetProductColorVersionwiseB2BPrice(long party_type_id, long product_id, long color_id, long product_version_id)
       {
           try
           {
               var pricing = _mappingRepository.GetProductColorVersionwiseB2BPrice(party_type_id, product_id, color_id, product_version_id);
               var formatter = RequestFormat.JsonFormaterString();
               return Request.CreateResponse(HttpStatusCode.OK, pricing, formatter);
           }
           catch (Exception ex)
           {

               var formatter = RequestFormat.JsonFormaterString();
               return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
           }
       }
       [HttpGet, ActionName("GetProductColorVersionwiseDealerPrice")]
       public HttpResponseMessage GetProductColorVersionwiseDealerPrice(long party_type_id, long product_id, long color_id, long product_version_id)
       {
           try
           {
               var pricing = _mappingRepository.GetProductColorVersionwiseDealerPrice(party_type_id, product_id, color_id, product_version_id);
               var formatter = RequestFormat.JsonFormaterString();
               return Request.CreateResponse(HttpStatusCode.OK, pricing, formatter);
           }
           catch (Exception ex)
           {

               var formatter = RequestFormat.JsonFormaterString();
               return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
           }
       }

       [HttpGet, ActionName("GetProductColorVersionwiseDealerDemoPrice")]
       public HttpResponseMessage GetProductColorVersionwiseDealerDemoPrice(long party_type_id, long product_id, long color_id, long product_version_id)
       {
           try
           {
               var dealerdemoPeice = _mappingRepository.GetProductColorVersionwiseDealerDemoPrice(party_type_id, product_id, color_id, product_version_id);
               var formatter = RequestFormat.JsonFormaterString();
               return Request.CreateResponse(HttpStatusCode.OK, dealerdemoPeice, formatter);

           }
           catch (Exception ex)
           {
               var formatter = RequestFormat.JsonFormaterString();
               return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
           }
       }
       [HttpGet]
       public HttpResponseMessage GetLastGrn(long product_id, long color_id, long product_version_id)
       {
           try
           {
               var dealerdemoPeice = _mappingRepository.GetLastGrn(product_id, color_id, product_version_id);
               var formatter = RequestFormat.JsonFormaterString();
               return Request.CreateResponse(HttpStatusCode.OK, dealerdemoPeice, formatter);

           }
           catch (Exception ex)
           {
               var formatter = RequestFormat.JsonFormaterString();
               return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
           }
       }
       [HttpGet]
       public HttpResponseMessage GetFilterWiseProductPricing(long catId, long pId, long cId, long vId)
       {
           try
           {
               var dealerdemoPeice = _mappingRepository.GetFilterWiseProductPricing(catId,pId,cId,vId);
               var formatter = RequestFormat.JsonFormaterString();
               return Request.CreateResponse(HttpStatusCode.OK, dealerdemoPeice, formatter);

           }
           catch (Exception ex)
           {
               var formatter = RequestFormat.JsonFormaterString();
               return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
           }
       }

    }
}