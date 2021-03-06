﻿using DMSApi.Models;
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
    public class WarehouseController : ApiController
    {
        private IWarehouseRepository warehouseRepository;

        public WarehouseController()
        {
            this.warehouseRepository = new WarehouseRepository();
        }

        public WarehouseController(IWarehouseRepository warehouseRepository)
        {
            this.warehouseRepository = warehouseRepository;
        }


        public HttpResponseMessage GetAllWarehouses()
        {
            var ware = warehouseRepository.GetAllWarehouse();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, ware, formatter);
        }
        [HttpGet, ActionName("GetWarehouseById")]
        public HttpResponseMessage GetWarehouseById(long warehouse_id)
        {
            var wh = warehouseRepository.GetWarehouseById(warehouse_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, wh, formatter);
        }
        [HttpGet, ActionName("GetAllWarehouseForGridLoad")]
        public HttpResponseMessage GetAllWarehouseForGridLoad()
        {
            var wh = warehouseRepository.GetAllWarehouseForGridLoad();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, wh, formatter);
        }
        [HttpGet, ActionName("GetAdaWarehouse")]
        public HttpResponseMessage GetAdaWarehouse()
        {
            var wh = warehouseRepository.GetAdaWarehouse();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, wh, formatter);
        }

        [HttpGet, ActionName("GetWeWarehouse")]
        public HttpResponseMessage GetWeWarehouse()
        {
            var wh = warehouseRepository.GetWeWarehouse();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, wh, formatter);
        }

        [HttpGet, ActionName("GetWarehouseByPartyId")]
        public HttpResponseMessage GetWarehouseByPartyId(long party_id)
        {
            var wh = warehouseRepository.GetWarehouseByPartyId(party_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, wh, formatter);
        }

        [HttpGet, ActionName("GetSalesWarehouseOnly")]
        public HttpResponseMessage GetSalesWarehouseOnly()
        {
            var wh = warehouseRepository.GetSalesWarehouseOnly();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, wh, formatter);
        }

        [HttpGet, ActionName("GetWarehouseForDirectTransfer")]
        public HttpResponseMessage GetWarehouseForDirectTransfer()
        {
            var wh = warehouseRepository.GetWarehouseForDirectTransfer();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, wh, formatter);
        }

        [HttpGet, ActionName("GetWarehouseForTransferOrder")]
        public HttpResponseMessage GetWarehouseForTransferOrder()
        {
            var wh = warehouseRepository.GetWarehouseForTransferOrder();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, wh, formatter);
        }

        [HttpGet, ActionName("GetWarehouseByPartyIdForAll")]
        public HttpResponseMessage GetWarehouseByPartyIdForAll(long party_id)
        {
            var wh = warehouseRepository.GetWarehouseByPartyIdForAll(party_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, wh, formatter);
        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage Post([FromBody] Models.warehouse warehouse)
        {

            try
            {
                if (string.IsNullOrEmpty(warehouse.warehouse_name))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Warehouse Name is Empty" }, formatter);

                }
                if (string.IsNullOrEmpty(warehouse.warehouse_type.ToString()))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Please Select Warehouse Type" }, formatter);

                }
                //if (string.IsNullOrEmpty(warehouse.party_id.ToString()))
                //{
                //    var formatter = RequestFormat.JsonFormaterString();
                //    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Please Select Party" }, formatter);

                //}

                else
                {
                    if (warehouseRepository.CheckDuplicateWarehouse(warehouse.warehouse_name))
                    {
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Warehouse Already Exists" }, formatter);
                    }
                    else
                    {
                        warehouse insert_warehouse = new warehouse
                        {
                            warehouse_name = warehouse.warehouse_name,
                            warehouse_address = warehouse.warehouse_address,
                            warehouse_code = warehouse.warehouse_code,
                            region_id = warehouse.region_id,
                            area_id = warehouse.area_id,
                            territory_id = warehouse.territory_id,
                            is_active = true,
                            is_deleted = false,
                            created_by = warehouse.created_by,
                            warehouse_type = warehouse.warehouse_type

                        };

                        warehouseRepository.AddWarehouse(insert_warehouse);
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Warehouse saved successfully" }, formatter);
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
        public HttpResponseMessage Put([FromBody] Models.warehouse warehouse)
        {
            try
            {
                if (string.IsNullOrEmpty(warehouse.warehouse_name))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Warehouse Name is Empty" }, formatter);

                }
                if (string.IsNullOrEmpty(warehouse.warehouse_type.ToString()))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Please Select Warehouse Type" }, formatter);

                }
                if (string.IsNullOrEmpty(warehouse.region_id.ToString()))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Please Select Region " }, formatter);

                }
                if (string.IsNullOrEmpty(warehouse.area_id.ToString()))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Please Select Area" }, formatter);

                }
                if (string.IsNullOrEmpty(warehouse.territory_id.ToString()))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Please Select Territory" }, formatter);

                }
                else
                {
                    Models.warehouse updatewarehouse = new Models.warehouse
                    {
                        warehouse_id = warehouse.warehouse_id,
                        warehouse_name = warehouse.warehouse_name,
                        warehouse_code = warehouse.warehouse_code,
                        warehouse_address = warehouse.warehouse_address,
                        warehouse_type = warehouse.warehouse_type,
                        region_id = warehouse.region_id,
                        area_id = warehouse.area_id,
                        territory_id = warehouse.territory_id,
                        is_deleted = false,
                        is_active = warehouse.is_active,
                        updated_by = warehouse.updated_by,
                        updated_date = DateTime.Now
                    };

                    warehouseRepository.EditWarehouse(updatewarehouse);
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Warehouse updated successfully" }, formatter);
                }

            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }


        [System.Web.Http.HttpDelete]
        public HttpResponseMessage Delete([FromBody]Models.warehouse warehouse)
        {
            try
            {
                bool deletewarehouse = warehouseRepository.DeleteWarehouse(warehouse.warehouse_id, warehouse.updated_by);

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Warehouse Deleted Successfully." }, formatter);
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }

        }

        //[HttpGet, ActionName("GenerateCode")]
        //public HttpResponseMessage GenerateCode()
        //{
        //    var data = warehouseRepository.GenerateCode();
        //    var formatter = RequestFormat.JsonFormaterString();
        //    return Request.CreateResponse(HttpStatusCode.OK, data, formatter);
        //}

        //[HttpGet, ActionName("WarehouseCategory")]
        //public HttpResponseMessage WarehouseCategory()
        //{
        //    var data = warehouseRepository.WarehouseCategory();
        //    var formatter = RequestFormat.JsonFormaterString();
        //    return Request.CreateResponse(HttpStatusCode.OK, data, formatter);
        //}

        //[HttpPost, ActionName("CreateWarehouse")]
        //public HttpResponseMessage CreateWarehouse(warehouse owarehouse)
        //{
        //    var data = warehouseRepository.AddWarehouse(owarehouse);
        //    var formatter = RequestFormat.JsonFormaterString();
        //    return Request.CreateResponse(HttpStatusCode.OK, data, formatter);
        //}


        public HttpResponseMessage GetWarehouseForEdit(long warehouseid)
        {
            var data = warehouseRepository.GetWarehouseForEdit(warehouseid);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, data, formatter);
        }
    }
}