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
    public class TermsAndConditionController : ApiController
    {
        private readonly ITermsAndConditionRepository _termsAndConditionRepository;

        public TermsAndConditionController()
        {
            this._termsAndConditionRepository = new TermsAndConditionRepository();
        }

        public TermsAndConditionController(TermsAndConditionRepository termsAndConditionRepository)
        {
            this._termsAndConditionRepository = termsAndConditionRepository;
        }
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetAllTermsAndCondition()
        {
            try
            {
                var termAndCondition = _termsAndConditionRepository.GetAllTermsAndCondition();
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, termAndCondition, formatter);
            }
            catch (Exception ex)
            {

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [HttpGet, ActionName("GetTermsAndConditionByheader")]
        public HttpResponseMessage GetTermsAndConditionByheader(string terms_condition_header)
        {
            var data = _termsAndConditionRepository.GetTermsAndConditionByheader(terms_condition_header);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, data, formatter);
        }

        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetTermsAndConditionById(long terms_and_condition_id)
        {
            try
            {
                var termAndCondition = _termsAndConditionRepository.GetTermsAndConditionById(terms_and_condition_id);
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, termAndCondition, formatter);
            }
            catch (Exception ex)
            {

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage Post([FromBody] Models.terms_and_condition objTermsAndCondition)
        {

            try
            {
                if (string.IsNullOrEmpty(objTermsAndCondition.terms_and_condition_description))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Terms And Condition is Empty" }, formatter);

                }
                if (string.IsNullOrEmpty(objTermsAndCondition.terms_condition_header))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Terms And Condition Header is Empty" }, formatter);

                }



                if (_termsAndConditionRepository.CheckDuplicatetermsAndCondition(objTermsAndCondition.terms_and_condition_description))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Already Exists! " }, formatter);
                }
                else
                {
                    terms_and_condition objTAndC = new terms_and_condition
                    {
                        terms_and_condition_description = objTermsAndCondition.terms_and_condition_description,
                        terms_condition_header = objTermsAndCondition.terms_condition_header,
                        is_active = objTermsAndCondition.is_active = true,
                        created_by = objTermsAndCondition.created_by,
                        created_date = DateTime.Now
                    };



                    _termsAndConditionRepository.AddTermsAndCondition(objTAndC);


                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Terms And Condition saved successfully" }, formatter);
                }




            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [System.Web.Http.HttpPut]
        public HttpResponseMessage Put([FromBody] Models.terms_and_condition objTermsAndCondition)
        {
            try
            {
                if (string.IsNullOrEmpty(objTermsAndCondition.terms_and_condition_description))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Terms And Condition is Empty" }, formatter);

                }
                if (string.IsNullOrEmpty(objTermsAndCondition.terms_condition_header))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Terms And Condition Header is Empty" }, formatter);

                }
                else
                {
                    terms_and_condition objTAndC = new terms_and_condition
                    {
                        terms_and_condition_id = objTermsAndCondition.terms_and_condition_id,
                        terms_and_condition_description = objTermsAndCondition.terms_and_condition_description,
                        terms_condition_header = objTermsAndCondition.terms_condition_header,
                        is_active = objTermsAndCondition.is_active,
                        updated_by = objTermsAndCondition.updated_by,
                        update_date = DateTime.Now
                    };



                    _termsAndConditionRepository.EditTermsAndCondition(objTAndC);


                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Terms And Condition updated successfully" }, formatter);
                }




            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }
        [System.Web.Http.HttpDelete]
        public HttpResponseMessage Delete([FromBody]Models.terms_and_condition objTermsAndCondition)
        {
            try
            {

                bool deleteArea = _termsAndConditionRepository.DeleteTermsAndCondition(objTermsAndCondition.terms_and_condition_id);

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Deleted Successfully." }, formatter);
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }

        }
    }
}