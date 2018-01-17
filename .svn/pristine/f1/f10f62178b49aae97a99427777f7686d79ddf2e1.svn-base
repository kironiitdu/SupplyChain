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
    // [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SupplierTypeController : ApiController
    {
        private readonly ISupplierTypeRepository _supplierTypeRepository;

            public SupplierTypeController()
            {
                this._supplierTypeRepository = new SupplierTypeRepository();
            }

            public SupplierTypeController(SupplierTypeRepository supplierTypeRepository)
            {
                this._supplierTypeRepository = supplierTypeRepository;
            }
            //By kiron (18/01/2017)
            [HttpGet, ActionName("GetAllSupplierTypeList")]
            public HttpResponseMessage GetAllSupplierTypeList()
            {
                var countries = _supplierTypeRepository.GetSupplierTypeListForGrid();
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, countries, formatter);
            }
            [HttpGet, ActionName("GetSupplierTypeListForDropDown")]
            public HttpResponseMessage GetSupplierTypeListForDropDown()
            {
                var countries = _supplierTypeRepository.GetSupplierTypeListForDropDown();
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, countries, formatter);
            }
            //By kiron (18/01/2017)
            [HttpPost]
            public HttpResponseMessage Post([FromBody]Models.supplier_type objSupplierType, long? created_by)
            {
                try
                {
                    if (string.IsNullOrEmpty(objSupplierType.supplier_type_name))
                    {
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Supplier Type  is Empty" }, formatter);
                    }
                   
                    else
                    {
                        if (_supplierTypeRepository.CheckDuplicateSupplierType(objSupplierType.supplier_type_name))
                        {
                            var formatter = RequestFormat.JsonFormaterString();
                            return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = "Supplier Type Already Exists" }, formatter);
                        }
                     
                        else
                        {

                            supplier_type insertSupplierType = new supplier_type
                            {
                                supplier_type_name = objSupplierType.supplier_type_name,
                                created_by = objSupplierType.created_by,
                                created_date = DateTime.Now,
                                is_deleted = objSupplierType.is_deleted = false,
                                is_active = objSupplierType.is_active = true
                            };

                            bool saveSupplierType = _supplierTypeRepository.AddSupplierType(insertSupplierType, created_by);

                            var formatter = RequestFormat.JsonFormaterString();
                            return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Supplier Type save successfully" }, formatter);
                        }
                    }
                }
                catch (Exception ex)
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
                }
            }
            //Updated By kiron (18/01/2017)
            [HttpPut]
            public HttpResponseMessage Put([FromBody]Models.supplier_type objSupplierType, long? updated_by)
            {
                try
                {
                    if (string.IsNullOrEmpty(objSupplierType.supplier_type_name))
                    {
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Supplier Type  is Empty" }, formatter);
                    }

                    else
                    {
                        //if (_supplierTypeRepository.CheckDuplicateSupplierType(objSupplierType.supplier_type_name))
                        //{
                        //    var formatter = RequestFormat.JsonFormaterString();
                        //    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = "Supplier Type Already Exists" }, formatter);
                        //}
                        //else
                        //{
                            supplier_type editSupplierType = new supplier_type
                            {
                                supplier_type_id = objSupplierType.supplier_type_id,
                                supplier_type_name = objSupplierType.supplier_type_name,
                                updated_by = objSupplierType.updated_by,
                                updated_date = DateTime.Now,
                                is_deleted = objSupplierType.is_deleted = false,
                                is_active = objSupplierType.is_active
                            };

                            bool supplierTypeUpdate = _supplierTypeRepository.EditSupplierType(editSupplierType, updated_by);

                            if (supplierTypeUpdate == true)
                            {
                                var formatter = RequestFormat.JsonFormaterString();
                                return Request.CreateResponse(HttpStatusCode.OK,
                                    new Confirmation { output = "success", msg = "Supplier Type Updated successfully" }, formatter);
                            }

                            else
                            {
                                var formatter = RequestFormat.JsonFormaterString();
                                return Request.CreateResponse(HttpStatusCode.OK,
                                    new Confirmation { output = "success", msg = "Update Failed" }, formatter);
                            }
                        //}
                    }
                }
                catch (Exception ex)
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
                }
            }

            [HttpDelete]
            public HttpResponseMessage Delete([FromBody]Models.supplier_type objSupplierType, long? updated_by)
            {
                try
                {
                 
                    bool objSupplier = _supplierTypeRepository.DeleteSupplierType(objSupplierType.supplier_type_id, updated_by);

                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Supplier Type Delete Successfully." }, formatter);
                }
                catch (Exception ex)
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
                }
            }
    }
}