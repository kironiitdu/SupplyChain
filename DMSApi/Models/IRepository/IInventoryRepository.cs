using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSApi.Models.IRepository
{
  public interface IInventoryRepository
  {
      bool UpdateInventory(string transactionType, string documentCode, long fromWarehouse, long toWarehouse, long productId, long colorId, long productVersionId, long uomId, int transactionQuantity, long userId);
      long GetWarehouseClosingStockByProductId(long warehouseId, long productId, long colorId, long productVersionId);
      object GetProductInventoryExcelData(string from_date, string to_date, string product_id, string color_id);
      object GetAdaProductInventoryDetailsExcel(long product_id, long color_id, long warehouse_id);
      object InventoryStockExcel(long product_id, long color_id, long product_version_id,long warehouse_id);
      object GetAdaProductInventoryAllExcel();
      object GetInventoryReport(long warehouse_id, long product_id, long color_id, string from_date, string to_date, long user_id);
      object GetInventoryStockReport(long warehouse_id, long product_id, long color_id, long user_id);
      object LoadAllInventoryStock(long warehouse_id, long product_id, long color_id, long product_version_id);
      object DailySalesTransaction();
      object DailySalesTransaction(long user_id, string from_date, string to_date);
      object TraceIMEI(string imei_no);
      object ImeiMovementCentralToParty(string from_date, string to_date);
      object PartyWiseStock(long role_id, long party_id);
      object PartyWiseStockReport( long party_id, long user_id);
      object ImeiMovementCentralToParty();
      object CustomerWisePSI(string product_id, string color_id, string from_date, string to_date);
      object DeliverySummaryReportADAToMDDBIS(string party_type_id, string product_id, string color_id, string from_date, string to_date);
      object SellThroughBack(string from_date, string to_date);
      object GetPartyWiseInventoryDetailsExcel(long product_id, long color_id, long warehouse_id, long party_id);
      object GetAllDeliveredIMEIExcel();
      object PSIDetails(string product_id, string color_id, string from_date, string to_date);
      object GetAllInventoryStockPDF(long product_id, long color_id, long product_version_id, long warehouse_id,long user_id);
    

  }
}
