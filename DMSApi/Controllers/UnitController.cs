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
    public class UnitController : ApiController
    {
        private IUnitRepository unitRepository;

        public UnitController()
        {
            this.unitRepository = new UnitRepository();
        }

        public UnitController(IUnitRepository unitRepository)
        {
            this.unitRepository = unitRepository;
        }

        public HttpResponseMessage GetAllUnits()
        {
            var countries = unitRepository.GetAllUnits();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, countries, formatter);
        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage Post([FromBody] Models.unit unit)
        {

            try
            {
                if (string.IsNullOrEmpty(unit.unit_name))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Unit is Empty" }, formatter);

                }
                else
                {
                    unit insert_unit = new unit
                    {
                        unit_name = unit.unit_name

                    };


                    /* user table entry by rabby*/
                    unitRepository.AddUnit(insert_unit);
                    /* user table entry by rabby*/

                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Unit save successfully" }, formatter);
                }


            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [System.Web.Http.HttpPut]
        public HttpResponseMessage Put([FromBody] Models.unit unit)
        {
            try
            {
                Models.unit updateUnit = new Models.unit
                {
                    unit_id = unit.unit_id,
                    unit_name = unit.unit_name

                };
                /* user table entry by rabby*/

                /* user table entry by rabby*/
                unitRepository.EditUnit(updateUnit);


                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Unit update successfully" }, formatter);
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [System.Web.Http.HttpDelete]
        public HttpResponseMessage Delete([FromBody]Models.unit unit)
        {
            try
            {
                //long con_id = long.Parse(country_id);
                bool updatCountry = unitRepository.DeleteUnit(unit.unit_id);

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Unit Delete Successfully." }, formatter);
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }

        }

        [ActionName("GetUnitById")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage GetUnitById([FromBody]Models.unit unit)
        {
            var unitId = unit.unit_id;

            var employee = unitRepository.GetUnitById(unitId);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, employee);
            return response;
        }
    }
}