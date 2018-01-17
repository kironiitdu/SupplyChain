using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DMSApi.Models.StronglyType;

namespace DMSApi.Models.IRepository
{
    public interface IPurchaseOrderRepository
    {
        object GetAllPurchaseOrders();
        object GetAllPurchaseOrdersForVerify();
        object GetAllVerifiedPurchaseOrders();
        object GetAllApprovedPurchaseOrders();
        object GetAllApprovedPurchaseOrdersPiNo();
        object GetAllApprovedPurchaseOrdersForDropDown();
        bool UploadPiAttachment();
        HttpResponseMessage GetPiAttachment(long purchase_order_master_id);
        bool VerifyPurchaseOrder(long purchase_order_master_id,long user_id);
        bool ApprovePurchaseOrder(long purchase_order_master_id, long user_id);
        object GetPurchaseOrdersReportById(long purchase_order_master_id);
        long AddPurchaseOrder(PurchaseOrderModel purchaseOrderModel);
        PurchaseOrderModel GetPurchaseOrderById(long purchase_order_master_id);
        object GetPurchaseOrderExcelData(string from_date, string to_date);
        bool EditPurchaseOrder(PurchaseOrderModel purchaseOrderModel);
        bool UpdatePiInfoOnPo(PurchaseOrderModel purchaseOrderModel);
        bool UpdateLcNoOnPo(PurchaseOrderModel purchaseOrderModel);
        bool DeletePurchaseOrder(long purchase_order_master_id);
        bool DeletePurchaseOrderDetailsById(long purchase_order_details_id);
    }
}
