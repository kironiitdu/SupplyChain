using DMSApi.Models;
using DMSApi.Models.IRepository;
using DMSApi.Models.Repository;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DMSApi.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RoleController : ApiController
    {
        private IRoleRepository roleRepository;

        public RoleController()
        {
            this.roleRepository = new RoleRepository();
        }

        public RoleController(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        [ActionName("GetAllRoles")]
        [HttpGet]
        public HttpResponseMessage GetAllRoles()
        {
            var roles = roleRepository.GetAllRoles();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, roles, formatter);
        }

        [ActionName("GetAllEmployeRoles")]
        [HttpGet]
        public HttpResponseMessage GetAllEmployeRoles(long companyId)
        {
            var roles = roleRepository.GetEmployeeRoleTypeBySource(companyId);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, roles, formatter);
        }

        [ActionName("GetAllRolesForEmpUser")]
        [HttpGet]
        public HttpResponseMessage GetAllRolesForEmpUser(long company_id)
        {
            var roles = roleRepository.GetAllRolesForEmpUser(company_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, roles, formatter);
        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage Post([FromBody]Models.role role)
        {
          
            try
            {
                if (string.IsNullOrEmpty(role.role_name))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Role Name is Empty" }, formatter);
                }
                else
                {
                    if (roleRepository.CheckRoleForDuplicateByname(role.role_name))
                    {
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Role Already Exists" }, formatter);
                    }
                    else
                    {

                        //long companyId = long.Parse(Request.GetQueryNameValuePairs().SingleOrDefault(com=>com.Key =="company_id").Value);
                    
                        Models.role insertRole = new Models.role
                        {
                            role_name = role.role_name,
                            is_active = role.is_active,
                            created_by = role.created_by,
                            created_date = DateTime.Now,
                            //updated_by = role.updated_by,
                            //updated_date = DateTime.Now.ToString(),
                            company_id = role.company_id,
                            is_fixed = role.is_fixed,
                            role_type_id = role.role_type_id
                        };
                        bool save_role = roleRepository.InsertRole(insertRole);

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
        public HttpResponseMessage Put([FromBody]Models.role role)
        {
            try
            {
                if (string.IsNullOrEmpty(role.role_name))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Role Name is Empty" }, formatter);
                }
                else
                {
                    Models.role updateRole = new Models.role
                    {
                        role_id = role.role_id,
                        role_name = role.role_name,
                        is_active = role.is_active,
                        updated_by = role.updated_by,
                        //updated_date = DateTime.Now.ToString(),
                        is_fixed = role.is_fixed,
                        role_type_id = role.role_type_id
                    };

                    bool roleUpdate = roleRepository.UpdateRole(updateRole);

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
        public HttpResponseMessage Delete([FromBody]Models.role role)
        {
            try
            {
                bool deleteRole = roleRepository.DeleteRole(role.role_id);

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