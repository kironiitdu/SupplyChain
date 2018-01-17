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
    public class DealerTypeController : ApiController
    {
       
        private IDealerTypeRepository dealerTypeRepository;

        public DealerTypeController()
        {
            this.dealerTypeRepository = new DealerTypeRepository();
        }

        public DealerTypeController(IDealerTypeRepository dealerTypeRepository)
        {
            this.dealerTypeRepository = dealerTypeRepository;
        }
        [HttpGet, ActionName("GetAllDealerType")]
        public HttpResponseMessage GetAllDealerType()
        {
            var dealerType = dealerTypeRepository.GetAllDealerType();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, dealerType, formatter);
        }
        public HttpResponseMessage Post([FromBody]Models.dealer_type objDealerType, long created_by)
        {
            try
            {
                if (string.IsNullOrEmpty(objDealerType.dealer_type_name.Trim()))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Dealer Type is Empty" }, formatter);
                }
                if (string.IsNullOrEmpty(objDealerType.dealer_type_prefix.Trim()))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = "Dealer Type Prefix is Empty" }, formatter);
                }
                if (string.IsNullOrEmpty(objDealerType.credit_limit.ToString()))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = "Credit Limit is Empty" }, formatter);
                }
                else
                {
                    if (string.IsNullOrEmpty(objDealerType.dealer_type_name.Trim()))
                    {
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Dealer Type is Empty" }, formatter);
                    }
                    if (string.IsNullOrEmpty(objDealerType.dealer_type_prefix.Trim()))
                    {
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = "Dealer Type Prefix is Empty" }, formatter);
                    }
                    else
                    {

                        Models.dealer_type insertDealerType = new Models.dealer_type
                        {
                            dealer_type_name = objDealerType.dealer_type_name.Trim(),
                            dealer_type_prefix = objDealerType.dealer_type_prefix.ToUpper().Trim(),
                            credit_limit=objDealerType.credit_limit,
                            created_by = created_by,
                            created_date = DateTime.Now,
                            is_deleted = false,
                            is_active = true
                        };
                        bool saveDealerType = dealerTypeRepository.InsertDealerType(insertDealerType, created_by);

                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Dealer Type save successfully" }, formatter);
                    }
                }
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }
        public HttpResponseMessage Put([FromBody]Models.dealer_type objDealerType, long updated_by)
        {
            try
            {
                if (string.IsNullOrEmpty(objDealerType.dealer_type_name.Trim()))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = "Dealer Type is Empty" }, formatter);
                }
                if (string.IsNullOrEmpty(objDealerType.dealer_type_prefix.Trim()))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = "Dealer Type Prefix is Empty" }, formatter);
                }
                if (string.IsNullOrEmpty(objDealerType.credit_limit.ToString()))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = "Credit Limit is Empty" }, formatter);
                }
                else
                {
                  
                 Models.dealer_type updateDealerType = new Models.dealer_type

                        {
                            dealer_type_id = objDealerType.dealer_type_id,
                            dealer_type_name = objDealerType.dealer_type_name.Trim(),
                            dealer_type_prefix = objDealerType.dealer_type_prefix.ToUpper().Trim(),
                            credit_limit = objDealerType.credit_limit,
                            updated_by = updated_by,
                            updated_date = DateTime.Now,
                            is_active = true
                        };

                        bool irepoUpdate = dealerTypeRepository.UpdateDealerType(updateDealerType, updated_by);

                        if (irepoUpdate == true)
                        {
                            var formatter = RequestFormat.JsonFormaterString();
                            return Request.CreateResponse(HttpStatusCode.OK,
                                new Confirmation { output = "success", msg = "Dealer Type Updated successfully" }, formatter);
                        }

                        else
                        {
                            var formatter = RequestFormat.JsonFormaterString();
                            return Request.CreateResponse(HttpStatusCode.OK,
                                new Confirmation { output = "success", msg = "Update Failed" }, formatter);
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
        public HttpResponseMessage Delete([FromBody]Models.dealer_type objDealerType, long updated_by)
        {
            try
            {              
                bool updatPartyType = dealerTypeRepository.DeleteDealerType(objDealerType.dealer_type_id, updated_by);

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Dealer Type Delete Successfully." }, formatter);
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }
        [HttpGet, ActionName("GetDealerTypeWiseCreditLimit")]
        public HttpResponseMessage GetDealerTypeWiseCreditLimit(long dealer_type_id)
        {
            var creditLimit = dealerTypeRepository.GetDealerTypeWiseCreditLimit(dealer_type_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, creditLimit, formatter);
        }
    }
}