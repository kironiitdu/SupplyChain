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
    public class ControlController : ApiController
    {
        private IControlRepository controlRepository;

        public ControlController()
        {
            this.controlRepository = new ControlRepository();
        }

        public ControlController(IControlRepository controlRepository)
        {
            this.controlRepository = controlRepository;
        }

        public HttpResponseMessage GetAllControls()
        {
            var control = controlRepository.GetAllControls();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, control, formatter);
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody]Models.control control)
        {
            try
            {
                if (string.IsNullOrEmpty(control.control_name))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Control Name is Empty" }, formatter);
                }
                else if (control.control_sort < 0)
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Control Sort can not be Negative" }, formatter);
                }
                else
                {
                    if (controlRepository.CheckControlForDuplicateByName(control.control_name))
                    {
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Control Name already exist" }, formatter);
                    }
                    else
                    {
                        long level = 0;
                        long parent_id = 0;
                        if (control.control_type_id == 1 && control.control_parent_id.ToString() == string.Empty)
                        {
                            level = 1;
                        }
                        else if (control.control_type_id == 1 && control.control_parent_id.ToString() != string.Empty)
                        {
                            level = 2;
                        }
                        else
                        {
                            level = 3;
                        }
                        parent_id = control.control_parent_id == null ? 0 : long.Parse(control.control_parent_id.ToString());
                        Models.control insertControl = new Models.control
                        {
                            control_name = control.control_name,
                            control_parent_id = parent_id,
                            control_type_id = 1,
                            control_sort = control.control_sort,
                            control_alias = control.control_alias,
                            control_controller = control.control_controller,
                            control_action = control.control_action,
                            is_active = control.is_active,
                            created_by = 1,
                            //created_date = DateTime.Now.ToString(),
                            updated_by = 1,
                            //updated_date = DateTime.Now.ToString(),
                            company_id = control.company_id,
                            Level = level,
                            icon = control.icon
                        };
                        bool save_control = controlRepository.InsertControl(insertControl);
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Control Name Save Successfully" }, formatter);
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
        public HttpResponseMessage Put([FromBody]Models.control control)
        {
            try
            {
                if (string.IsNullOrEmpty(control.control_name))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Control Name is Empty" }, formatter);
                }
                else
                {
                    long level = 0;
                    if (control.control_type_id == 1 && control.control_parent_id.ToString() == string.Empty)
                    {
                        level = 1;
                    }
                    else if (control.control_type_id == 1 && control.control_parent_id.ToString() != string.Empty)
                    {
                        level = 2;
                    }
                    else
                    {
                        level = 3;
                    }
                    Models.control updateControl = new Models.control
                    {
                        control_id = control.control_id,
                        control_name = control.control_name,
                        control_parent_id = control.control_parent_id,
                        control_type_id = 1,
                        control_sort = control.control_sort,
                        control_alias = control.control_alias,
                        control_controller = control.control_controller,
                        control_action = control.control_action,
                        is_active = control.is_active,
                        updated_by = 1,
                        //updated_date = DateTime.Now.ToString(),
                        company_id = control.company_id,
                        Level = level,
                        icon = control.icon
                    };

                    bool irepopdate = controlRepository.UpdateControl(updateControl);
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Control update succesfully" }, formatter);
                }
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [HttpDelete]
        public HttpResponseMessage Delete([FromBody]Models.control control)
        {
            try
            {
                bool updateControl = controlRepository.DeleteControl(control.control_id);
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Control name Delete Successfully." }, formatter);
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }
    }
}