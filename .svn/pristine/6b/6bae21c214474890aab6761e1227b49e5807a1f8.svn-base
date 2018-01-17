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
    public class InventoryAdjustmentController : ApiController
    {
        private IInventoryAdjustmentRepository inventoryAdjustmentRepository;

        public InventoryAdjustmentController()
        {
            this.inventoryAdjustmentRepository = new InventoryAdjustmentRepository();
        }

        public InventoryAdjustmentController(IInventoryAdjustmentRepository inventoryAdjustmentRepository)
        {
            this.inventoryAdjustmentRepository = inventoryAdjustmentRepository;
        }

        [HttpGet, ActionName("GetSystemQuantityForAccessories")]
        public HttpResponseMessage GetSystemQuantityForAccessories(long warehouse_id, long product_id)
        {
            var countries = inventoryAdjustmentRepository.GetSystemQuantityForAccessories(warehouse_id, product_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, countries, formatter);
        }

        [HttpGet, ActionName("GetAllInventoryAdjustment")]
        public HttpResponseMessage GetAllInventoryAdjustment()
        {
            var countries = inventoryAdjustmentRepository.GetAllInventoryAdjustment();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, countries, formatter);
        }

        [HttpGet, ActionName("GetInventoryAdjustmentListForApprove")]
        public HttpResponseMessage GetInventoryAdjustmentListForApprove()
        {
            var countries = inventoryAdjustmentRepository.GetInventoryAdjustmentListForApprove();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, countries, formatter);
        }

        [HttpGet, ActionName("ApproveInventoryAdjustment")]
        public HttpResponseMessage ApproveInventoryAdjustment(long inventory_adjustment_id, long user_id)
        {
            var data = inventoryAdjustmentRepository.ApproveInventoryAdjustment(inventory_adjustment_id, user_id);

            if (data)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Approved successfully" }, formatter); 
            }
            else
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Approved Failed !!" }, formatter); 
            }
            
        }

        [ActionName("GetInventoryAdjustmentById")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage GetInventoryAdjustmentById([FromBody]Models.inventory_adjustment inventory_adjustment)
        {
            var inventoryAdjustmentId = inventory_adjustment.inventory_adjustment_id;

            var employee = inventoryAdjustmentRepository.GetInventoryAdjustmentById(inventoryAdjustmentId);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, employee);
            return response;
        }

        [HttpPost, ActionName("PostAccessories")]
        public HttpResponseMessage PostAccessories([FromBody] Models.inventory_adjustment inventory_adjustment)
        {

            try
            {
                if (string.IsNullOrEmpty(inventory_adjustment.warehouse_id.ToString()))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Select Warehouse" }, formatter);

                }
                if (string.IsNullOrEmpty(inventory_adjustment.contra_warehouse_id.ToString()))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Select Contra Warehouse" }, formatter);

                }
                if (string.IsNullOrEmpty(inventory_adjustment.product_id.ToString()))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Select Product" }, formatter);

                }
                if (string.IsNullOrEmpty(inventory_adjustment.physical_quantity.ToString()))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Physical Count is empty !!" }, formatter);

                }
                else
                {
                    inventory_adjustment insertInventoryAdjustment = new inventory_adjustment
                    {
                        adjustment_type = "Accessories",
                        warehouse_id = inventory_adjustment.warehouse_id,
                        contra_warehouse_id = inventory_adjustment.contra_warehouse_id,
                        product_id = inventory_adjustment.product_id,
                        system_quantity = inventory_adjustment.system_quantity,
                        physical_quantity = inventory_adjustment.physical_quantity,
                        adjustment_quantity = inventory_adjustment.adjustment_quantity,
                        status = "Created",
                        created_by = inventory_adjustment.created_by,
                        created_date = DateTime.Now,
                        updated_by = inventory_adjustment.created_by,
                        updated_date = DateTime.Now
                    };

                    var x = inventoryAdjustmentRepository.PostAccessories(insertInventoryAdjustment);

                    if (x == 0)
                    {
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Error !!" }, formatter);
                    }
                    else
                    {
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Saved successfully" }, formatter);
                    }
                }


            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [HttpPut, ActionName("PutAccessories")]
        public HttpResponseMessage PutAccessories([FromBody] Models.inventory_adjustment inventory_adjustment)
        {

            try
            {
                if (string.IsNullOrEmpty(inventory_adjustment.warehouse_id.ToString()))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Select Warehouse" }, formatter);

                }
                if (string.IsNullOrEmpty(inventory_adjustment.contra_warehouse_id.ToString()))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Select Contra Warehouse" }, formatter);

                }
                if (string.IsNullOrEmpty(inventory_adjustment.product_id.ToString()))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Select Product" }, formatter);

                }
                if (string.IsNullOrEmpty(inventory_adjustment.physical_quantity.ToString()))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Physical Count is empty !!" }, formatter);

                }
                else
                {
                    inventory_adjustment insertInventoryAdjustment = new inventory_adjustment
                    {
                        inventory_adjustment_id = inventory_adjustment.inventory_adjustment_id,
                        warehouse_id = inventory_adjustment.warehouse_id,
                        contra_warehouse_id = inventory_adjustment.contra_warehouse_id,
                        product_id = inventory_adjustment.product_id,
                        system_quantity = inventory_adjustment.system_quantity,
                        physical_quantity = inventory_adjustment.physical_quantity,
                        adjustment_quantity = inventory_adjustment.adjustment_quantity,
                        status = "Updated",
                        updated_by = inventory_adjustment.created_by,
                        updated_date = DateTime.Now
                    };

                    var x = inventoryAdjustmentRepository.PutAccessories(insertInventoryAdjustment);

                    if (x)
                    {
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Updated successfully" }, formatter);
                    }
                    else
                    {
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Error !!" }, formatter);
                    }

                }


            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [System.Web.Http.HttpDelete]
        public HttpResponseMessage Delete([FromBody]Models.inventory_adjustment inventory_adjustment)
        {
            try
            {

                bool data = inventoryAdjustmentRepository.DeleteInventoryAdjustment(inventory_adjustment.inventory_adjustment_id);

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Delete Successfully." }, formatter);
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }

        }
    }
}