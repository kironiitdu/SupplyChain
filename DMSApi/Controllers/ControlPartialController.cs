using DMSApi.Models;
using DMSApi.Models.IRepository;
using DMSApi.Models.Repository;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DMSApi.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ControlPartialController : ApiController
    {
        private IControlRepository controlRepository;

        public ControlPartialController()
        {
            this.controlRepository = new ControlRepository();
        }

        public ControlPartialController(IControlRepository controlRepository)
        {
            this.controlRepository = controlRepository;
        }

        public HttpResponseMessage GetAllControlsOnly()
        {
            var control = controlRepository.GetAllControlsOnly();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, control, formatter);
        }

        [ActionName("GetControlById")]
        [HttpPost]
        public HttpResponseMessage GetControlById([FromBody]Models.control oControl)
        {
            var control = controlRepository.GetControlById(oControl.control_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, control, formatter);
        }
    }
}