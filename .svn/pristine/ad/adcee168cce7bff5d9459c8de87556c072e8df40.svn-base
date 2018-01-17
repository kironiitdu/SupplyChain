using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSApi.Models.IRepository
{
    public interface IReceiveSerialNoDetailsRepository
    {
        object GetProductInformation(long imei_no,int party_id);
        object GetProductInformationForCentral(long imei_no);
        object GetProductInformationForDirectTransfer(long imei_no, int from_warehouse_id);
        object GetProductInformationForScan(long imei_no, int from_warehouse_id);
        //List<receive_serial_no_details> Getimeino();
        object Getimeino();
        object GetImeiPartyNProductwise( int party_id, int product_id);

        object GetImeiListByCartonNoFromWarehouse(string cartonNo, int from_warehouse_id);
        object GetProductInfoByImeiFromWarehouse(long imei_no, int from_warehouse_id);
    }
}
