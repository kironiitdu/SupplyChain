using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMSApi.Models.StronglyType;

namespace DMSApi.Models.IRepository
{
    public interface IInternalEmiRequisitionRepository
    {
        long AddInternalEmiRequisition(InternalEmiRequisitionModel internalEmiRequisitionModel);
        object GetAllInternalEmiRequisitionRfd();
        object GetInternalEmiDeliveryReportById(long delivery_master_id, long user_id);
        object GetInternalEmiInvoiceReport(long internal_requisition_master_id, long user_id);
        object GetAllInternalEmiDeliveryList();
        bool ConfirmDelivery(long delivery_master_id, long user_id);
        bool CancelDelivery(long delivery_master_id, long user_id);
    }
}
