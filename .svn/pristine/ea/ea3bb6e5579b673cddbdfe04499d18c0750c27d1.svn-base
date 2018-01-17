using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMSApi.Models.StronglyType;

namespace DMSApi.Models.IRepository
{
   public interface IInternalRequisitionRepository
    {
       long AddInternalRequisition(InternalRequisitionModel objInternalRequisitionModel);
       object GetAllInternalRequisitionRfd();
       bool ConfirmDelivery(long delivery_master_id, long user_id);
       bool CancelDelivery(long delivery_master_id, long user_id);
       object GetAllInternalDeliveryList();
       object GetInternalDeliveryReportById(long delivery_master_id, long user_id);
       object GetInternalInvoiceReport(long internal_requisition_master_id, long user_id);
    }
}
