using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMSApi.Models.StronglyType;

namespace DMSApi.Models.IRepository
{
    public interface IToDeliveryRepository
    {
        object GetAllToDelivery();
        object GetAllToDeliveryForRfd();
        ToDeliveryModel GetToDeliveryById(long to_delivery_master_id);
        object GetMonthlyTransferReport(DateTime from_date, DateTime to_date,long from_warehouse_id);
        //object GetAllDeliveryByPartyId(long party_id);
        object GetToDeliveryReportById(long to_delivery_master_id);
        //object GetDeliveryExcelReportByDeliveryMasterId(long delivery_master_id);
        long AddToDelivery(ToDeliveryModel toDeliveryModel);
        long RfdDelivery(ToDeliveryModel toDeliveryModel);
        long CancelToDelivery(ToDeliveryModel toDeliveryModel);
        //bool UpdateCourierInformation(delivery_master objUpdateCourier);
        //bool UpdateApproveStatus(long requisition_master_id, long userid);
        //delivery_master GetCourierInformation(long delivery_master_id);
    }
}
