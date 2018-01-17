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
    public class DesignationController : ApiController
    {
        private IDesignationRepository designationRepository;

        public DesignationController()
        {
            this.designationRepository = new DesignationRepository();
        }

        public DesignationController(IDesignationRepository designationRepository)
        {
            this.designationRepository = designationRepository;
        }

        [HttpGet, ActionName("GetAllDesignation")]
        public HttpResponseMessage GetAllDesignation()
        {
            var countries = designationRepository.GetAllDesignation();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, countries, formatter);
        }

        [ActionName("GetDesignationById")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage GetDesignationById([FromBody]Models.designation designation)
        {
            var salesDesignationId = designation.sales_designation_id;
            var data = designationRepository.GetDesignationById(salesDesignationId);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, data);
            return response;
        }

        [HttpGet, ActionName("GetAllDesignationForGrid")]
        public HttpResponseMessage GetAllDesignationForGrid()
        {
            var countries = designationRepository.GetAllDesignationForGrid();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, countries, formatter);
        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage Post([FromBody] Models.designation designation)
        {

            try
            {
                if (string.IsNullOrEmpty(designation.sales_designation))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Designation Name is Empty" }, formatter);
                }
                if (string.IsNullOrEmpty(designation.sales_person_type_code))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Code is Empty" }, formatter);
                }
                else
                {
                    designation insertDesignation = new designation
                    {
                        sales_designation = designation.sales_designation,
                        sales_person_type_code = designation.sales_person_type_code,
                        parent_designation_id = designation.parent_designation_id,
                        sales_type_id = designation.sales_type_id,
                        created_by = designation.created_by,
                        updated_by = designation.updated_by,
                        created_date = DateTime.Now,
                        updated_date = DateTime.Now
                    };

                    designationRepository.AddDesignation(insertDesignation);
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Save successfully" }, formatter);
                }
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [System.Web.Http.HttpPut]
        public HttpResponseMessage Put([FromBody] Models.designation designation)
        {
            try
            {
                if (string.IsNullOrEmpty(designation.sales_designation))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Designation Name is Empty" }, formatter);
                }
                if (string.IsNullOrEmpty(designation.sales_person_type_code))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Code is Empty" }, formatter);
                }
                else
                {
                    designation updateDesignation = new designation
                    {
                        sales_designation_id = designation.sales_designation_id,
                        sales_designation = designation.sales_designation,
                        sales_person_type_code = designation.sales_person_type_code,
                        parent_designation_id = designation.parent_designation_id,
                        sales_type_id = designation.sales_type_id,
                        updated_by = designation.updated_by,
                        updated_date = DateTime.Now
                    };

                    designationRepository.EditDesignation(updateDesignation);
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Updated Successfully" }, formatter);
                }
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [System.Web.Http.HttpDelete]
        public HttpResponseMessage Delete([FromBody]Models.designation designation)
        {
            try
            {
                //int con_id = int.Parse(country_id);
                bool data = designationRepository.DeleteDesignation(designation.sales_designation_id);

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Delete Successfully." }, formatter);
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }

        }
    }
}