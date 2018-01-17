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
    public class RoleTypeController : ApiController
    {
        //
        // GET: /RoleType/
        private IRoleTypeRepository roleTypeRepository;

        public RoleTypeController()
        {
            this.roleTypeRepository = new RoleTypeRepository();
        }

        public RoleTypeController(IRoleTypeRepository roleTypeRepository)
        {
            this.roleTypeRepository = roleTypeRepository;
        }

        public HttpResponseMessage GetAllRoles()
        {
            var roleType = roleTypeRepository.GetAllRolesOnly();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, roleType, formatter);
        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage Post([FromBody]Models.role_type roleType)
        {
            try
            {
                if (string.IsNullOrEmpty(roleType.role_type_name))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Role Name is Empty" }, formatter);
                }
                else
                {
                    if (roleTypeRepository.CheckRoleForDuplicateByname(roleType.role_type_name))
                    {
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Role Already Exists" }, formatter);
                    }
                    else
                    {
                        Models.role_type insertRole = new Models.role_type()
                        {
                            role_type_name = roleType.role_type_name,
                            description = roleType.description
                        };
                        bool save_role = roleTypeRepository.InsertRoleType(insertRole);

                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Role save successfully" }, formatter);
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
        public HttpResponseMessage Put([FromBody]Models.role_type role)
        {
            try
            {
                if (string.IsNullOrEmpty(role.role_type_name))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Role Name is Empty" }, formatter);
                }
                else
                {
                    Models.role_type updateRole = new Models.role_type
                    {
                        role_type_id = role.role_type_id,
                        role_type_name = role.role_type_name,
                        description = role.description,
                    };

                    bool roleUpdate = roleTypeRepository.UpdateRoleType(updateRole);

                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Role Update successfully" }, formatter);
                }
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [System.Web.Http.HttpDelete]
        public HttpResponseMessage Delete([FromBody]Models.role_type roletype)
        {
            try
            {
                bool deleteRole = roleTypeRepository.DeleteRoleType(roletype.role_type_id);

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Role Delete Successfully." }, formatter);
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }
    }
}