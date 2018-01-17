using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMSApi.Models.StronglyType;

namespace DMSApi.Models.IRepository
{
    public interface IOnlineRequisitionRepository
    {
        object GetImeiForOnlineRequisitionDelivery(long imei,int warehouseId);
        long AddOnlineREquisition(OnlineRequisitionModel onlineRequisitionModel);
        object GetOnlineDeliveryChallanReport(long deliveryMasterId);
        object GetOnlineRequisitionAndDeliveryList();
        object GetPartyForPaymentCollect();
        object GetPaymentCollectGridForInvoice(int partyId,DateTime reqFrom,DateTime reqTo);
        object GetImeiForOnlineRequisitionPaymentCollect(long imei,int partyId);
        object GetProductForPaymentAndInvoiceGenerate(List<requisition_details> requisitionDetailses);

        object GetOnlineInvoiceReport(long online_invoice_master_id);

        object GetAllProductWithoutAss();

        object GetReturnRequisitionList(int partyId, DateTime reqFrom, DateTime reqTo);
    }
}