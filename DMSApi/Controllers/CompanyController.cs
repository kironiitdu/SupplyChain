using Newtonsoft.Json;
using DMSApi.Models;
using DMSApi.Models.IRepository;
using DMSApi.Models.Repository;
using DMSApi.Models.StronglyType;

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Script.Serialization;

namespace DMSApi.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CompanyController : ApiController
    {
        private ICompanyRepository companyRepository;

        public CompanyController()
        {
            this.companyRepository = new CompanyRepository();
        }

        public CompanyController(ICompanyRepository companyRepository)
        {
            this.companyRepository = companyRepository;
        }

        [HttpGet, ActionName("GetAllCompany")]
        public HttpResponseMessage GetAllCompany()
        {
            var company = companyRepository.GetAllCompany();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, company, formatter);
        }

        [HttpGet, ActionName("GetCompanyById")]
        public HttpResponseMessage GetCompanyById(long company_id)
        {
            try
            {
                var company = companyRepository.GetCompanyById(company_id);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, company);
                return response;
            }
            catch (Exception ex)
            {

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Post([FromBody] Models.company objCompany)
        {

            try
            {
                if (companyRepository.CheckDuplicateCompany(objCompany.company_name))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = "Company Name Already Exists" }, formatter);
                }
                if (string.IsNullOrEmpty(objCompany.company_name))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = "Company Name is Empty" }, formatter);

                }
                if (string.IsNullOrEmpty(objCompany.company_code))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = "Company Code is Empty" }, formatter);

                }
                if (string.IsNullOrEmpty(objCompany.address))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = "Address is Empty" }, formatter);

                }
                if (string.IsNullOrEmpty(objCompany.mobile))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = "Mobile is Empty" }, formatter);

                }
                if (string.IsNullOrEmpty(objCompany.zip_code))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = "Zip Code is Empty" }, formatter);

                }
                if (string.IsNullOrEmpty(objCompany.email))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = "Email is Empty" }, formatter);

                }
                if (string.IsNullOrEmpty(objCompany.country_id.ToString()))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = "Country is Empty" }, formatter);

                }
                if (string.IsNullOrEmpty(objCompany.city_id.ToString()))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = "City is Empty" }, formatter);

                }
                else
                {
                    company insertCountry = new company
                    {
                       
                        company_name = objCompany.company_name,
                        company_code = objCompany.company_code,
                        address = objCompany.address,
                        mobile = objCompany.mobile,
                        email = objCompany.email,
                        zip_code=objCompany.zip_code,
                        country_id = objCompany.country_id,
                        city_id = objCompany.city_id,
                        created_by = objCompany.created_by,
                        created_date = DateTime.Now,
                        is_active =true,
                        is_deleted = false
                       

                    };

                    companyRepository.AddCompany(insertCountry);
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Company saved successfully" }, formatter);
                }


            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }
        [System.Web.Http.HttpPut]
        public HttpResponseMessage Put([FromBody] Models.company objCompany)
        {
            try
            {
                if (string.IsNullOrEmpty(objCompany.company_name))
                {
                    var format = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = "Company Name is Empty" }, format);

                }
                if (string.IsNullOrEmpty(objCompany.company_code))
                {
                    var format = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = "Company Code is Empty" }, format);

                }
                if (string.IsNullOrEmpty(objCompany.address))
                {
                    var format = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = "Address is Empty" }, format);

                }
                if (string.IsNullOrEmpty(objCompany.mobile))
                {
                    var format = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = "Mobile is Empty" }, format);

                }
                if (string.IsNullOrEmpty(objCompany.zip_code))
                {
                    var format = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = "Zip Code is Empty" }, format);

                }
                if (string.IsNullOrEmpty(objCompany.email))
                {
                    var format = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = "Email is Empty" }, format);

                }
                if (string.IsNullOrEmpty(objCompany.country_id.ToString()))
                {
                    var format = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = "Country is Empty" }, format);

                }
                if (string.IsNullOrEmpty(objCompany.city_id.ToString()))
                {
                    var format = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = "City is Empty" }, format);

                }
                company updateCountry = new company
                {
                    company_id = objCompany.company_id,
                    company_name = objCompany.company_name,
                    company_code = objCompany.company_code,
                    address = objCompany.address,
                    mobile = objCompany.mobile,
                    email = objCompany.email,
                    zip_code = objCompany.zip_code,
                    country_id = objCompany.country_id,
                    city_id = objCompany.city_id,
                    updated_by = objCompany.updated_by,
                    updated_date = DateTime.Now,
                    is_active = objCompany.is_active,
                    is_deleted = false


                };


                companyRepository.EditCompany(updateCountry);
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Company updated successfully" }, formatter);
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [System.Web.Http.HttpDelete]
        public HttpResponseMessage Delete([FromBody]Models.company objCompany)
        {
            try
            {

                bool deleteCompany = companyRepository.DeleteCompany(objCompany.company_id);

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Company Delete Successfully." }, formatter);
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }

        }
        [Route("Company/GetCompanyCode?company_id={company_id}")]
        public HttpResponseMessage GetCompanyCode(long company_id)
        {
            var companies = companyRepository.GetCompanyCode(company_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, companies, formatter);
        }

        [Route("Company/GetCompanyName?company_id={company_id}")]
        public HttpResponseMessage GetCompanyName(long company_id)
        {
            var companies = companyRepository.GetCompanyName(company_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, companies, formatter);
        }

        //[Route("Company/GetCompanyFlagLogo?company_id={company_id}")]
        //public HttpResponseMessage GetCompanyFlagLogo(long company_id)
        //{
        //    var companies = companyRepository.GetCompanyFlagLogo(company_id);
        //    var formatter = RequestFormat.JsonFormaterString();
        //    return Request.CreateResponse(HttpStatusCode.OK, companies, formatter);
        //}


    }
}