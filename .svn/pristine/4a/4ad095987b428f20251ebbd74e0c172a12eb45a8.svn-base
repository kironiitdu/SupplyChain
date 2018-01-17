using DMSApi.Models.StronglyType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSApi.Models.IRepository
{
   public interface IInventoryStockRepository
    {
       List<InventoryStockModel> GetAllInventoryStock(long warehouse_id, long product_id, long uom_id, long location_id);

       Object GetInventoryStock(long companyid, long branchid, long warehouseid, long productid);

        bool InsertInventoryStock(inventory oInventory);

        bool UpdateInventoryStock(inventory oInventory);

        bool DeleteInventoryStock(long inventory_id);

    }
}
