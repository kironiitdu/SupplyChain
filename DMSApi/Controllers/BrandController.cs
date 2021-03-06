﻿using System;
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
    public class BrandController : ApiController
    {
        private IBrandRepository brandRepository;

        public BrandController()
        {
            this.brandRepository = new BrandRepository();
        }

        public BrandController(IBrandRepository brandRepository)
        {
            this.brandRepository = brandRepository;
        }

        public HttpResponseMessage GetAllBrands()
        {
            var countries = brandRepository.GetAllBrands();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, countries, formatter);
        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage Post([FromBody] Models.brand brand)
        {

            try
            {
                if (string.IsNullOrEmpty(brand.brand_name))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Brand is Empty" }, formatter);

                }
                else
                {
                    if (brandRepository.CheckDuplicateBrands(brand.brand_name))
                    {
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Brand Already Exists" }, formatter);
                    }
                    else
                    {
                        brand insert_brand = new brand
                        {
                            brand_name = brand.brand_name

                        };

                        brandRepository.Addbrand(insert_brand);
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Brand save successfully" }, formatter);
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
        public HttpResponseMessage Put([FromBody] Models.brand brand)
        {
            try
            {
                if (brandRepository.CheckDuplicateBrands(brand.brand_name))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Brand Already Exists" }, formatter);
                }
                else
                {
                    Models.brand updateBrand = new Models.brand
                    {
                        brand_id = brand.brand_id,
                        brand_name = brand.brand_name

                    };

                    brandRepository.Editbrand(updateBrand);
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Brand update successfully" }, formatter);
                }
               
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [System.Web.Http.HttpDelete]
        public HttpResponseMessage Delete([FromBody]Models.brand brand)
        {
            try
            {
                //int con_id = int.Parse(country_id);
                bool updatCountry = brandRepository.Deletebrand(brand.brand_id);

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Brand Delete Successfully." }, formatter);
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }

        }

        [ActionName("GetbrandById")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage GetbrandById([FromBody]Models.brand brand)
        {
            var brandId = brand.brand_id;

            var employee = brandRepository.GetbrandById(brandId);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, employee);
            return response;
        }
    }
}