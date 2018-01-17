using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using DMSApi.Controllers;
using DMSApi.Models;
using DMSApi.Models.IRepository;
using DMSApi.Models.Repository;

namespace DMSApi.Controllers
{

    //[EnableCors(origins: "*", headers: "*", methods: "*")]

    public class RegionController : ApiController
    {
        private readonly IRegionRepository _regionRepository;
         public RegionController()
         {
            this._regionRepository = new RegionRepository();
         }
        public RegionController(IRegionRepository regionRepository)
        {
           this._regionRepository = regionRepository;
        }

        public HttpResponseMessage GetAllRegions()
        {
            var region = _regionRepository.GetAllRegions();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, region, formatter);
        }
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Post([FromBody] Models.region mRegion)
        {

            try
            {
                if (string.IsNullOrEmpty(mRegion.region_name))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Region is Empty" }, formatter);

                }
                if (mRegion.region_code.Length!=3)
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Region code must be 3 character" }, formatter);

                }
                else
                {
                    if (_regionRepository.CheckDuplicateRegions(mRegion))
                    {
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Region Already Exists" }, formatter);
                    }
                    else
                    {
                        region insert_region = new region
                        {
                            region_name = mRegion.region_name,
                            region_code = mRegion.region_code,
                            is_active = mRegion.is_active,
                            created_by = mRegion.created_by,
                            updated_by = mRegion.updated_by
                        };

                        _regionRepository.AddRegion(insert_region);
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Region save successfully" }, formatter);
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
        public HttpResponseMessage Put([FromBody] Models.region mRegion)
        {
            try
            {
                if (mRegion.region_code.Length != 3)
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Region code must be 3 character" }, formatter);

                }
                //if (_regionRepository.CheckDuplicateRegions(mRegion))
                //{
                //    var formatter = RequestFormat.JsonFormaterString();
                //    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Region already exists" }, formatter);
                //}
                //else
                {
                    Models.region updateRegion = new Models.region
                    {
                        region_id = mRegion.region_id,
                        region_name = mRegion.region_name,
                        region_code = mRegion.region_code,
                        is_active = mRegion.is_active,
                        updated_by = mRegion.updated_by
                    };

                    _regionRepository.EditRegion(updateRegion);
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Region update successfully" }, formatter);
                }

            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }
        [System.Web.Http.HttpDelete]
        public HttpResponseMessage Delete([FromBody]Models.region region)
        {
            try
            {
                bool updateRegion = _regionRepository.DeleteRegion(region.region_id);

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Region Deleted Successfully." }, formatter);
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }

        }
        [ActionName("GetRegionById")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage GetUserById([FromBody]Models.region region)
        {
            var regionId = region.region_id;

            var rg = _regionRepository.GetRegionById(regionId);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, region);
            return response;
        }
       
    }
}