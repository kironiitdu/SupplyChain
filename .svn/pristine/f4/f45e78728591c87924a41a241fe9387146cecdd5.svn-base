using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSApi.Models.IRepository
{
    public interface IInventoryAdjustmentRepository
    {
        object GetSystemQuantityForAccessories(long warehouse_id, long product_id);
        long PostAccessories(inventory_adjustment inventory_adjustment);
        bool PutAccessories(inventory_adjustment inventory_adjustment);
        object GetInventoryAdjustmentListForApprove();
        object GetAllInventoryAdjustment();
        inventory_adjustment GetInventoryAdjustmentById(long inventory_adjustment_id);
        bool DeleteInventoryAdjustment(long inventory_adjustment_id);
        bool ApproveInventoryAdjustment(long inventory_adjustment_id, long user_id);
    }
}
