using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DMSApi.Models.StronglyType;

namespace DMSApi.Models.IRepository
{
    public interface ITransferOrderRepository
    {
        object GetAllTransferOrder();
        object GetAllDeliverableTransferOrder();
        object GetTransferOrderReportById(long transfer_order_master_id);
        long AddTransferOrder(TransferOrderModel transferOrderModel);
        TransferOrderModel GetTransferOrderById(long transfer_order_master_id);
        bool EditTransferOrder(TransferOrderModel transferOrderModel);
        bool DeleteTransferOrder(long transfer_order_master_id);
        bool DeleteTransferOrderDetailsById(long transfer_order_details_id);
    }
}
