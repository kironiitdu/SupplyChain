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
    public class SbuController : ApiController
    {

        private ISbuRepository sbuRepository;

        public SbuController()
        {
            this.sbuRepository = new SbuRepository();
        }

        public SbuController(ISbuRepository bankRepository)
        {
            this.sbuRepository = sbuRepository;
        }
        public HttpResponseMessage GetAllSbu()
        {
            var sbuData = sbuRepository.GetAllSbu();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, sbuData, formatter);
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody]Models.sbu objSbu, long created_by)
        {
            try
            {
                Models.sbu insertSbu = new Models.sbu
                        {
                            sbu_name = objSbu.sbu_name,
                            is_active = true,
                            is_deleted = false,
                            created_by = created_by,
                            created_date = DateTime.Now
                        };
                        bool saveSbu = sbuRepository.InsertSbu(insertSbu);

                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "SBU save successfully" }, formatter);
                  
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [HttpPut]
        public HttpResponseMessage Put([FromBody]Models.sbu objSbu, long updated_by)
        {
            try
            {
              
                    Models.sbu updateSbu = new Models.sbu

                    {
                        sbu_id = objSbu.sbu_id,
                        sbu_name = objSbu.sbu_name,
                        is_active = objSbu.is_active,
                        is_deleted = false,
                        updated_by = updated_by,
                        updated_date = DateTime.Now
                    };

                    bool irepoUpdate = sbuRepository.UpdateSbu(updateSbu);

                    if (irepoUpdate == true)
                    {
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "SBU Update successfully" }, formatter);
                    }
                    else
                    {
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Update Failed" }, formatter);
                    }
               
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [HttpDelete]
        public HttpResponseMessage Delete([FromBody]Models.sbu objSbu, long updated_by)
        {
            try
            {
                //int con_id = int.Parse(country_id);
                bool updatBankbranch = sbuRepository.DeleteSbu(objSbu.sbu_id, updated_by);

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Bank Delete Successfully." }, formatter);
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }


    }
}