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
    public class DamageTypeController : ApiController
    {
        private IDamageTypeRepositoty damageTypeRepository;

        public DamageTypeController()
        {
            this.damageTypeRepository = new DamageTypeRepositoty();
        }

        public DamageTypeController(IDamageTypeRepositoty damageTypeRepository)
        {
            this.damageTypeRepository = damageTypeRepository;
        }

        public HttpResponseMessage GetAllDamageTypes()
        {
            var damageType = damageTypeRepository.GetAllDamageTypes();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, damageType, formatter);
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody]Models.damage_type objDamageType)
        {
            try
            {
                if (string.IsNullOrEmpty(objDamageType.damage_type_name))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Damage Type  Name is Empty" }, formatter);
                }

                else
                {
                    if (damageTypeRepository.CheckDuplicateDamageTypeName(objDamageType.damage_type_name) == true)
                    {
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Damage Type  Already Exists" }, formatter);
                    }
                    else
                    {
                        damage_type insertDamageType = new damage_type
                        {
                            damage_type_name = objDamageType.damage_type_name,
                            created_by = objDamageType.created_by,
                            created_date = DateTime.Now,
                            is_active = true,
                            updated_by = objDamageType.updated_by,
                            updated_date = objDamageType.updated_date,
                        };
                        bool save = damageTypeRepository.InsertDamageType(insertDamageType);
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Damage Type save successfully" }, formatter);
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
        public HttpResponseMessage Put([FromBody]Models.damage_type objDamageType)
        {
            try
            {
                if (string.IsNullOrEmpty(objDamageType.damage_type_name))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "DamageType Name is Empty" }, formatter);
                }

                else
                {
                    damage_type insertDamageType = new damage_type

                    {
                        damage_type_id = objDamageType.damage_type_id,
                        damage_type_name = objDamageType.damage_type_name,
                        is_active = objDamageType.is_active,
                        updated_by = objDamageType.updated_by,
                        updated_date = DateTime.Now

                    };

                    bool irepoUpdate = damageTypeRepository.UpdateDamageType(insertDamageType);

                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Damage Type Update successfully" }, formatter);
                }
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [HttpDelete]
        public HttpResponseMessage Delete([FromBody]Models.damage_type objDamageType)
        {
            try
            {
                bool updateDamage = damageTypeRepository.DeleteDamageType(objDamageType.damage_type_id);
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Damage Type Delete Successfully." }, formatter);
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }
    }
}