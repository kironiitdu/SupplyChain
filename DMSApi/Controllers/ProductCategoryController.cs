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
    public class ProductCategoryController : ApiController
    {
        private IProductCategoryRepository productCategoryRepository;

        public ProductCategoryController()
        {
            this.productCategoryRepository = new ProductCategoryRepository();
        }

        public ProductCategoryController(IProductCategoryRepository productCategoryRepository)
        {
            this.productCategoryRepository = productCategoryRepository;
        }

        public HttpResponseMessage GetAllProductCategories()
        {
            var countries = productCategoryRepository.GetAllProductCategories();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, countries, formatter);
        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage Post([FromBody] Models.product_category product_category)
        {

            try
            {
                if (string.IsNullOrEmpty(product_category.product_category_name))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Name is Empty" }, formatter);

                }
                else
                {
                    product_category insert_product_category = new product_category
                    {
                        product_category_name = product_category.product_category_name,
                        product_category_code = product_category.product_category_code

                    };


                
                    productCategoryRepository.AddProductCategory(insert_product_category);
                

                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Product Category save successfully" }, formatter);
                }


            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [System.Web.Http.HttpPut]
        public HttpResponseMessage Put([FromBody] Models.product_category product_category)
        {
            try
            {
                Models.product_category updateProductCategory= new Models.product_category
                {
                    product_category_id = product_category.product_category_id,
                    product_category_name = product_category.product_category_name,
                    product_category_code = product_category.product_category_code

                };
            
                productCategoryRepository.EditProductCategory(updateProductCategory);


                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Product Category update successfully" }, formatter);
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [System.Web.Http.HttpDelete]
        public HttpResponseMessage Delete([FromBody]Models.product_category product_category)
        {
            try
            {
                //long con_id = long.Parse(country_id);
                bool updatCountry = productCategoryRepository.DeleteProductCategory(product_category.product_category_id);

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Product Category Delete Successfully." }, formatter);
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }

        }

       
        [HttpGet, ActionName("GetProductCategoryByProductId")]
        public HttpResponseMessage GetProductCategoryByProductId(long product_id)
        {

            var productCategory = productCategoryRepository.GetProductCategoryByProductId(product_id);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, productCategory);
            return response;
        }
        [ActionName("GetProductCategoryById")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage GetProductCategoryById([FromBody]Models.product_category product_category)
        {
            var productCategoryId = product_category.product_category_id;

            var employee = productCategoryRepository.GetProductCategoryById(productCategoryId);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, employee);
            return response;
        }
    }

}