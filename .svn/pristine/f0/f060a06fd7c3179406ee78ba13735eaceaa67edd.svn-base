using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DMSApi.Models;
using System.Web.Http.Cors;
using DMSApi.Models.IRepository;
using DMSApi.Models.Repository;
using DMSApi.Models.StronglyType;

namespace DMSApi.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]

    public class LoginLogController : ApiController
    {
         private ILoginLogRepository loginLogRepository;

         private LoginLogController()
        {
            this.loginLogRepository=new LoginLogRepository();  
        }

        [HttpGet, ActionName("LogOutInfoEntry")]
        public HttpResponseMessage LogOutInfoEntry(long userId)
        {
            var openingBalance = loginLogRepository.LogOutInfoEntry(userId);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, openingBalance, formatter);
        }
    }
}