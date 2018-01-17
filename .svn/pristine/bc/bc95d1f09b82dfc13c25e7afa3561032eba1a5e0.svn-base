using DMSApi.Models;
using DMSApi.Models.IRepository;
using DMSApi.Models.Repository;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DMSApi.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CountryController : ApiController
    {
        private ICountryRepository countryRepository;

        public CountryController()
        {
            this.countryRepository = new CountryRepository();
        }

        public CountryController(ICountryRepository countryRepository)
        {
            this.countryRepository = countryRepository;
        }

        public HttpResponseMessage GetAllCountries()
        {
            var countries = countryRepository.GetAllCountries();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, countries, formatter);
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody]Models.country country)
        {
            try
            {
                if (string.IsNullOrEmpty(country.country_name))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Country Name is Empty" }, formatter);
                }
                if ((country.country_code.Length > 3))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Country Code and Length is invalid" }, formatter);
                }
                if (string.IsNullOrEmpty(country.country_code.ToUpper()))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Country Code is Empty" }, formatter);
                }
                else
                {
                    if (countryRepository.CheckDuplicateCountry(country.country_name))
                    {
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Country Already Exists" }, formatter);
                    }
                    else
                    {
                        Models.country insertCountry = new Models.country
                        {
                          
                            country_name = country.country_name,
                            country_code=country.country_code.ToUpper().Trim(),
                            is_active = true,
                            is_deleted = false,
                            created_by = country.created_by,
                            created_date = DateTime.Now,
                            updated_by =country.updated_by,
                            updated_date = DateTime.Now                           
                        };
                        bool save_country = countryRepository.InsertCountry(insertCountry);

                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Country save successfully" }, formatter);
                    }
                }
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [HttpPut]
        public HttpResponseMessage Put([FromBody]Models.country country)
        {
            try
            {
                if (string.IsNullOrEmpty(country.country_name))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Country Name is Empty" }, formatter);
                }
                if ((country.country_code.Length>3))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Country Code and Length is invalid" }, formatter);
                }
                if (string.IsNullOrEmpty(country.country_code.ToUpper()))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Country Code is Empty" }, formatter);
                }
                else
                {
                    Models.country updateCountry = new Models.country

                    {
                        country_id = country.country_id,
                        country_name = country.country_name,
                        country_code = country.country_code.ToUpper().Trim(),
                        is_active = country.is_active,
                        updated_by = country.updated_by,
                        updated_date = DateTime.Now

                    };

                    bool irepoUpdate = countryRepository.UpdateCountry(updateCountry);

                    if (irepoUpdate == true)
                    {
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Country Update successfully" }, formatter);
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
        public HttpResponseMessage Delete([FromBody]Models.country country)
        {
            try
            {
                //int con_id = int.Parse(country_id);
                bool updatCountry = countryRepository.DeleteCountry(country.country_id);

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Country Delete Successfully." }, formatter);
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }
    }
}