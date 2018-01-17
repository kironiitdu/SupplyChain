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
    public class InventoryController : ApiController
    {


        private IInventoryRepository inventoryRepository;

        public InventoryController()
        {
            this.inventoryRepository = new InventoryRepository();
        }

        public InventoryController(IInventoryRepository inventoryRepository)
        {
            this.inventoryRepository = inventoryRepository;
        }
        //Added by Kiron 03/09/2016
        [HttpGet, ActionName("GetInventoryReport")]
        public HttpResponseMessage GetInventoryReport(long warehouse_id, long product_id, long color_id, string from_date, string to_date, long user_id)
        {

            var inventoryData = inventoryRepository.GetInventoryReport(warehouse_id, product_id, color_id, from_date, to_date, user_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, inventoryData, formatter);
        }


        //For Get ADA Product Inventory Excel Details with IMEI-----------------------------------------GetPartyWiseInventoryDetailsExcel
        public object GetAdaProductInventoryDetailsExcel(long product_id, long color_id, long warehouse_id)
        {
            var data = inventoryRepository.GetAdaProductInventoryDetailsExcel(product_id,color_id,warehouse_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, data, formatter);
        }
        //For Get Ada Product Inventory All Excel Details with All IMEI-----------------------------------------
        public object GetAdaProductInventoryAllExcel()
        {
            var data = inventoryRepository.GetAdaProductInventoryAllExcel();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, data, formatter);
        }
      

          [HttpGet, ActionName("GetPartyWiseInventoryDetailsExcel")]
        public object GetPartyWiseInventoryDetailsExcel(long product_id, long color_id, long warehouse_id, long party_id)
          {
              var data = inventoryRepository.GetPartyWiseInventoryDetailsExcel(product_id, color_id, warehouse_id, party_id);
              var formatter = RequestFormat.JsonFormaterString();
              return Request.CreateResponse(HttpStatusCode.OK, data, formatter);
          }

        //For Product Inventory Excel-----------------------------------------
        [HttpGet, ActionName("GetProductInventoryExcelData")]
        public HttpResponseMessage GetProductInventoryExcelData(string from_date, string to_date, string product_id, string color_id)
        {
            var data = inventoryRepository.GetProductInventoryExcelData(from_date, to_date, product_id, color_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, data, formatter);
        }

        //Added by Kiron 26/09/2016
        [HttpGet, ActionName("GetInventoryStockReport")]
        public HttpResponseMessage GetInventoryStockReport(long warehouse_id, long product_id, long color_id, long user_id)
        {

            var stockReport = inventoryRepository.GetInventoryStockReport(warehouse_id, product_id, color_id, user_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, stockReport, formatter);
        }
        //Added by Kiron 31/10/2016
        [HttpGet, ActionName("LoadAllInventoryStock")]
        public HttpResponseMessage LoadAllInventoryStock(long warehouse_id, long product_id, long color_id, long product_version_id)
        {
            var InventoryStockGridData = inventoryRepository.LoadAllInventoryStock(warehouse_id, product_id, color_id, product_version_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, InventoryStockGridData, formatter);
        }
        //Added by Kiron 01/10/2016
        [HttpGet, ActionName("DailySalesTransactionReport")]
        public HttpResponseMessage DailySalesTransactionReport(long user_id, string from_date, string to_date)
        {
            var dailySales = inventoryRepository.DailySalesTransaction(user_id, from_date, to_date);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, dailySales, formatter);
        }
        [HttpGet, ActionName("DailySalesTransaction")]
        public HttpResponseMessage DailySalesTransaction()
        {
            var dailySales = inventoryRepository.DailySalesTransaction();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, dailySales, formatter);
        }

        [HttpGet, ActionName("TraceIMEINo")]
        public HttpResponseMessage TraceIMEINo(string imei_no)
        {
            try
            {
                if (string.IsNullOrEmpty(imei_no))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = "Please Enter IMEI No" }, formatter);

                }
                var traceIMEINo = inventoryRepository.TraceIMEI(imei_no.Trim());
                var newFormatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, traceIMEINo, newFormatter);
            }
            catch (Exception ex)
            {

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = "No IMEI found!" }, formatter);
            }
        }
        [HttpGet, ActionName("PartyWiseStock")]
        public HttpResponseMessage PartyWiseStock(long role_id, long party_id)
        {
            try
            {
                var partyWiseStock = inventoryRepository.PartyWiseStock(role_id, party_id);
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, partyWiseStock, formatter);
            }
            catch (Exception)
            {

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Sorry!Try Again...." }, formatter);
            }
        }

        [HttpGet, ActionName("PartyWiseStockReport")]
        public HttpResponseMessage PartyWiseStockReport(long party_id, long user_id)
        {
            try
            {
                var partyWiseStockReportData = inventoryRepository.PartyWiseStockReport(party_id, user_id);
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, partyWiseStockReportData, formatter);
            }
            catch (Exception)
            {

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Sorry!Try Again...." }, formatter);
            }
        }
        //By Kiron: IMEI Movement 29/10/2016
        [HttpGet, ActionName("ImeiMovementCentralToParty")]
        public HttpResponseMessage ImeiMovementCentralToParty(string from_date, string to_date)
        {
            try
            {
                var imeiMovementCentralToPartyData = inventoryRepository.ImeiMovementCentralToParty(from_date, to_date);
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, imeiMovementCentralToPartyData, formatter);
            }
            catch (Exception)
            {

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Sorry!Try Again...." }, formatter);
            }
        }
        //By Kiron
        [HttpGet, ActionName("ImeiMovementCentralToParty")]
        public HttpResponseMessage ImeiMovementCentralToParty()
        {
            try
            {
                var imeiMovementCentralToParty = inventoryRepository.ImeiMovementCentralToParty();
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, imeiMovementCentralToParty, formatter);
            }
            catch (Exception)
            {

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Sorry!Try Again...." }, formatter);
            }
        }
        //01-11-2016 By Kiron 
        [HttpGet, ActionName("CustomerWisePSI")]
        public HttpResponseMessage CustomerWisePSI(string product_id, string color_id, string from_date, string to_date)
        {
            try
            {
                var customerWisePSI = inventoryRepository.CustomerWisePSI(product_id, color_id, from_date, to_date);
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, customerWisePSI, formatter);
            }
            catch (Exception)
            {

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = "Sorry!Try Again...." }, formatter);
            }
        }
        //01-11-2016 By Kiron 
        [HttpGet, ActionName("DeliverySummaryReportADAToMDDBIS")]
        public HttpResponseMessage DeliverySummaryReportADAToMDDBIS(string party_type_id, string product_id, string color_id, string from_date, string to_date)
        {
            try
            {
                var deliverySummary = inventoryRepository.DeliverySummaryReportADAToMDDBIS(party_type_id, product_id, color_id, from_date, to_date);
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, deliverySummary, formatter);
            }
            catch (Exception)
            {

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = "Sorry!Try Again...." }, formatter);
            }
        }
        //06-11-2016 By Kiron
        [HttpGet, ActionName("SellThroughBack")]
        public HttpResponseMessage SellThroughBack(string from_date, string to_date)
        {
            try
            {
                var sellBack = inventoryRepository.SellThroughBack(from_date, to_date);
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, sellBack, formatter);
            }
            catch (Exception)
            {

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "warning", msg = "Sorry!Try Again...." }, formatter);
            }
        }
        //20-11-2016 All Delivered IMEI By Kiron
        [HttpGet, ActionName("GetAllDeliveredIMEIExcel")]
        public object GetAllDeliveredIMEIExcel()
        {
            var data = inventoryRepository.GetAllDeliveredIMEIExcel();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, data, formatter);
        }
        [HttpGet, ActionName("PSIDetails")]
        public object PSIDetails(string product_id, string color_id, string from_date, string to_date)
        {
            var data = inventoryRepository.PSIDetails(product_id, color_id, from_date, to_date);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, data, formatter);
        }
        //Added by Kiron 31/10/2016
        [HttpGet, ActionName("GetAllInventoryStockExcel")]
        public HttpResponseMessage GetAllInventoryStockExcel(long product_id, long color_id, long product_version_id, long warehouse_id)
        {
            var InventoryStockGridData = inventoryRepository.InventoryStockExcel( product_id, color_id, product_version_id,warehouse_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, InventoryStockGridData, formatter);
        }

        [HttpGet, ActionName("GetAllInventoryStockPDF")]
        public HttpResponseMessage GetAllInventoryStockPDF(long product_id, long color_id, long product_version_id, long warehouse_id, long user_id)
        {
            var InventoryStockGridData = inventoryRepository.GetAllInventoryStockPDF(product_id, color_id, product_version_id, warehouse_id, user_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, InventoryStockGridData, formatter);
        }
     
        
    }
}