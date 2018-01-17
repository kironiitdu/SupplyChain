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
    public class CityController : ApiController
    {
        private ICityRepository cityRepository;

        public CityController()
        {
            this.cityRepository = new CityRepository();
        }

        public CityController(ICityRepository cityRepository)
        {
            this.cityRepository = cityRepository;
        }

        public HttpResponseMessage GetAllCities()
        {
            var cities = cityRepository.GetAllCities();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, cities, formatter);
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody]Models.city city)
        {
            try
            {
                if (string.IsNullOrEmpty(city.city_name))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "City Name is Empty" }, formatter);
                }
               
                else
                {
                    if (cityRepository.CheckCityForDuplicateByName(city.city_name) == true)
                    {
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "City Already Exists" }, formatter);
                    }
                    else
                    {
                        Models.city insertCity = new Models.city()
                        {
                            city_name = city.city_name,                          
                            created_by = city.created_by,
                            created_date = DateTime.Now,
                            is_active = true,
                            updated_by = city.updated_by,
                            updated_date =city.updated_date,                          
                        };
                        bool save_city = cityRepository.InsertCity(insertCity);
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "City save successfully" }, formatter);
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
        public HttpResponseMessage Put([FromBody]Models.city city)
        {
            try
            {
                if (string.IsNullOrEmpty(city.city_name))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "City Name is Empty" }, formatter);
                }
           
                else
                {
                    Models.city updateCity = new Models.city

                    {
                        city_id = city.city_id,
                        city_name = city.city_name,
                        is_active = city.is_active,
                        updated_by = city.updated_by,
                        updated_date = DateTime.Now

                    };

                    bool irepoUpdate = cityRepository.UpdateCity(updateCity);

                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "City Update successfully" }, formatter);
                }
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [HttpDelete]
        public HttpResponseMessage Delete([FromBody]Models.city city)
        {
            try
            {
                bool updateCity = cityRepository.DeleteCity(city.city_id);
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "City Delete Successfully." }, formatter);
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }
    }
}