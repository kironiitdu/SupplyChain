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
    public class ColorController : ApiController
    {
        private IColorRepository colorRepository;

        public ColorController()
        {
            this.colorRepository = new ColorRepository();
        }

        public ColorController(IColorRepository colorRepository)
        {
            this.colorRepository = colorRepository;
        }

        public HttpResponseMessage GetAllColors()
        {
            var countries = colorRepository.GetAllColors();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, countries, formatter);
        }

        [HttpGet, ActionName("GetAllColorsForGrnExport")]
        public HttpResponseMessage GetAllColorsForGrnExport()
        {
            var countries = colorRepository.GetAllColorsForGrnExport();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, countries, formatter);
        }
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Post([FromBody] Models.color color)
        {

            try
            {
                if (string.IsNullOrEmpty(color.color_name))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Color is Empty" }, formatter);

                }
                else
                {
                    if (colorRepository.CheckDuplicateColors(color.color_name))
                    {
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Color Already Exists" }, formatter);
                    }
                    else
                    {
                        color insert_color = new color
                        {
                            color_name = color.color_name

                        };

                        colorRepository.AddColor(insert_color);
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Color save successfully" }, formatter);
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
        public HttpResponseMessage Put([FromBody] Models.color color)
        {
            try
            {
               
                    Models.color updateBrand = new Models.color
                    {
                        color_id = color.color_id,
                        color_name = color.color_name

                    };

                    colorRepository.EditColor(updateBrand);
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Color update successfully" }, formatter);
                
               
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [System.Web.Http.HttpDelete]
        public HttpResponseMessage Delete([FromBody]Models.color color)
        {
            try
            {
                //long con_id = long.Parse(country_id);
                bool updatCountry = colorRepository.DeleteColor(color.color_id);

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Color Delete Successfully." }, formatter);
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }

        }

        [ActionName("GetColorById")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage GetColorById([FromBody]Models.color color)
        {
            var colorId = color.color_id;

            var employee = colorRepository.GetColorById(colorId);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, employee);
            return response;
        }
      
    }
}