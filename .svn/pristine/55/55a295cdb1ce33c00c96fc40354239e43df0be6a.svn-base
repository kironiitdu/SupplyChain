using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMSApi.Models.IRepository;
using DMSApi.Models.StronglyType;

namespace DMSApi.Models.Repository
{
    public class DirectTransferRepository : IDirectTransferRepository
    {
        private DMSEntities _entities;

        public DirectTransferRepository()
        {
            this._entities = new DMSEntities();
        }

        public int AddDirectTransfer(DirectTransferModel directTransferModel)
        {
            try
            {
                #region "1# DirectTransfer Master and DirectTransfer Details Add"

                var directTransferMaster = directTransferModel.DirectTransferMasterData;
                var directTransferDetailsList = directTransferModel.DirectTransferDetailsList;
                var imeiList = directTransferModel.ReceiveSerialNoDetails;


                // generate Requisition No
                int dtSerial = _entities.direct_transfer_master.Max(rq => (int?)rq.direct_transfer_master_id) ?? 0;

                if (dtSerial != 0)
                {
                    dtSerial++;

                }
                else
                {
                    dtSerial++;
                }
                var dtStr = dtSerial.ToString().PadLeft(7, '0');
                string dtCode = "TRN-" + dtStr;
                directTransferMaster.direct_transfer_code = dtCode;
                directTransferMaster.from_warehouse_id = directTransferModel.DirectTransferMasterData.from_warehouse_id;
                directTransferMaster.to_warehouse_id = directTransferModel.DirectTransferMasterData.to_warehouse_id;
                directTransferMaster.transfer_date = directTransferModel.DirectTransferMasterData.transfer_date;
                directTransferMaster.remarks = directTransferModel.DirectTransferMasterData.remarks;
                directTransferMaster.created_by = directTransferModel.DirectTransferMasterData.created_by;

                _entities.direct_transfer_master.Add(directTransferMaster);
                _entities.SaveChanges();
                long directTransferMasterId = directTransferMaster.direct_transfer_master_id;


                foreach (var item in directTransferDetailsList)
                {
                    var directTransferDetails = new direct_transfer_details
                    {
                        direct_transfer_master_id = directTransferMasterId,
                        product_id = item.product_id,
                        color_id = item.color_id,
                        product_version_id = item.product_version_id,
                        quantity = item.quantity

                    };
                    _entities.direct_transfer_details.Add(directTransferDetails);
                    _entities.SaveChanges();
                }

                #endregion

                #region "2# Update current_warehouse_id in receive_serial_no_details table"

                foreach (var item in imeiList)
                {
                    var imeiNo = item.imei_no;

                    var imaiDetails = _entities.receive_serial_no_details.FirstOrDefault(r => r.imei_no == imeiNo || r.imei_no2 == imeiNo);
                    if (imaiDetails != null)
                    {
                        imaiDetails.current_warehouse_id = directTransferModel.DirectTransferMasterData.to_warehouse_id;
                    }
                    _entities.SaveChanges();
                }

                #endregion

                #region "3# Update inventory"

                // update inventory
                InventoryRepository updateInventoty = new InventoryRepository();

                foreach (var item in directTransferDetailsList)
                {
                    updateInventoty.UpdateInventory("TRANSFER", dtCode, (int)directTransferMaster.from_warehouse_id,
                    directTransferMaster.to_warehouse_id ?? 0, item.product_id ?? 0, item.color_id ?? 0, item.product_version_id ?? 0, 3,
                    (int)item.quantity, directTransferMaster.created_by ?? 0);
                }

                #endregion


                return 1;
            }
            catch (Exception ex)
            {

                return 0;
            }
        }
    }
}