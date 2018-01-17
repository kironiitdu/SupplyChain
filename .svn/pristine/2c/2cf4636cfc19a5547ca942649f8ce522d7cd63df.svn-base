using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMSApi.Models.StronglyType;

namespace DMSApi.Models.IRepository
{
    public interface IDeliveryRepository
    {
        object GetAllDelivery();
        object GetAllDeliveryByPartyId(long party_id);  
        object GetDeliveryReportById(long delivery_master_id);
        object GetDeliveryExcelReportByDeliveryMasterId(long delivery_master_id);
        long AddDelivery(DeliveryModel deliveryModel);
        bool UpdateDeliveryMethod(delivery_master objUpdateCourier);
        //bool UpdateApproveStatus(long requisition_master_id, long userid);
        delivery_master GetCourierInformation(long delivery_master_id);

        DeliveryModel GetDeliveryByIdForConfirmation(long delivery_master_id);

        object GetAllDeliveredByPartyId(long party_id);

        bool UpdateCourierInfo(delivery_master objUpdateCourier);

        bool UploadDeliveryChallan();

        bool CancelDelivery(delivery_master deliveryMaster);
    }
}
