using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMSApi.Models.crystal_models;
using DMSApi.Models.IRepository;
using DMSApi.Models.StronglyType;

namespace DMSApi.Models.Repository
{
    public class ToDeliveryRepository : IToDeliveryRepository
    {
        private DMSEntities _entities;

        public ToDeliveryRepository()
        {
            this._entities = new DMSEntities();
        }

        public object GetAllToDelivery()
        {
            var data = (from tdm in _entities.to_delivery_master
                        join tom in _entities.transfer_order_master on tdm.transfer_order_master_id equals tom.transfer_order_master_id
                        join frw in _entities.warehouses on tdm.from_warehouse_id equals frw.warehouse_id
                        join tow in _entities.warehouses on tdm.to_warehouse_id equals tow.warehouse_id
                        join usr in _entities.users on tdm.created_by equals usr.user_id
                        select new
                        {
                            to_delivery_master_id = tdm.to_delivery_master_id,
                            to_delivery_no = tdm.to_delivery_no,
                            to_delivery_date = tdm.to_delivery_date,
                            transfer_order_master_id = tdm.transfer_order_master_id,
                            order_no = tom.order_no,
                            from_warehouse_id = tdm.from_warehouse_id,
                            from_warehouse_name = frw.warehouse_name,
                            to_warehouse_id = tdm.from_warehouse_id,
                            to_warehouse_name = tow.warehouse_name,
                            status = tdm.status,
                            created_by = tdm.created_by,
                            full_name = usr.full_name,
                            created_date = tdm.created_date,
                            updated_by = tdm.updated_by,
                            updated_date = tdm.updated_date,
                            is_active = tdm.is_active,
                            is_deleted = tdm.is_deleted

                        }).Where(e => e.is_active == true).OrderByDescending(e => e.to_delivery_master_id).ToList();

            return data;
        }

        public object GetAllToDeliveryForRfd()
        {
            var data = (from tdm in _entities.to_delivery_master
                        join tom in _entities.transfer_order_master on tdm.transfer_order_master_id equals tom.transfer_order_master_id
                        join frw in _entities.warehouses on tdm.from_warehouse_id equals frw.warehouse_id
                        join tow in _entities.warehouses on tdm.to_warehouse_id equals tow.warehouse_id
                        join usr in _entities.users on tdm.created_by equals usr.user_id
                        select new
                        {
                            to_delivery_master_id = tdm.to_delivery_master_id,
                            to_delivery_no = tdm.to_delivery_no,
                            to_delivery_date = tdm.to_delivery_date,
                            transfer_order_master_id = tdm.transfer_order_master_id,
                            order_no = tom.order_no,
                            from_warehouse_id = tdm.from_warehouse_id,
                            from_warehouse_name = frw.warehouse_name,
                            to_warehouse_id = tdm.from_warehouse_id,
                            to_warehouse_name = tow.warehouse_name,
                            status = tdm.status,
                            created_by = tdm.created_by,
                            full_name = usr.full_name,
                            created_date = tdm.created_date,
                            updated_by = tdm.updated_by,
                            updated_date = tdm.updated_date,
                            is_active = tdm.is_active,
                            is_deleted = tdm.is_deleted

                        }).Where(e => e.is_active == true && e.status == "RFD").OrderByDescending(e => e.to_delivery_master_id).ToList();

            return data;
        }

        public ToDeliveryModel GetToDeliveryById(long to_delivery_master_id)
        {
            ToDeliveryModel toDeliveryModel = new ToDeliveryModel();

            toDeliveryModel.ToDeliveryMasterData = _entities.to_delivery_master.Find(to_delivery_master_id);

            toDeliveryModel.ToDeliveryDetailsList =
                _entities.to_delivery_details
                    .Join(_entities.products, jp => jp.product_id, p => p.product_id, (jp, p) => new { jp, p })
                     .GroupJoin(_entities.colors, jc => jc.jp.color_id, c => c.color_id, (jc, c) => new { jc, nc = c.FirstOrDefault() })
                    .Join(_entities.units, ju => ju.jc.jp.unit_id, u => u.unit_id, (ju, u) => new { ju, u })
                     .GroupJoin(_entities.product_version, jpv => jpv.ju.jc.jp.product_version_id, pv => pv.product_version_id, (jpv, pv) => new { jpv, vc = pv.FirstOrDefault() })
                    .Where(k => k.jpv.ju.jc.jp.to_delivery_master_id == to_delivery_master_id)
                    .Select(i => new ToDeliveryDetailsModel
                    {
                        to_delivery_master_id = i.jpv.ju.jc.jp.to_delivery_master_id,
                        product_id = i.jpv.ju.jc.jp.product_id,
                        product_name = i.jpv.ju.jc.p.product_name,
                        color_id = i.jpv.ju.nc.color_id,
                        product_version_id = i.vc.product_version_id,
                        color_name = string.IsNullOrEmpty(i.jpv.ju.nc.color_name) ? "" : i.jpv.ju.nc.color_name,
                        product_version_name = string.IsNullOrEmpty(i.vc.product_version_name) ? "" : i.vc.product_version_name,
                        to_quantity = i.jpv.ju.jc.jp.to_quantity,
                        delivered_quantity = i.jpv.ju.jc.jp.delivered_quantity,
                        to_delivery_details_id = i.jpv.ju.jc.jp.to_delivery_details_id,
                        unit_id = i.jpv.u.unit_id,
                        unit_name = i.jpv.u.unit_name
                    }).OrderByDescending(p => p.to_delivery_master_id).ToList();

            toDeliveryModel.ReceiveSerialNoDetails = (from rsnd in _entities.receive_serial_no_details
                                                      join pro in _entities.products on rsnd.product_id equals pro.product_id
                                                      join col in _entities.colors on rsnd.color_id equals col.color_id
                                                      join ver in _entities.product_version on rsnd.product_version_id equals ver.product_version_id
                                                      select new ReceiveSerialNoDetailsModel
                                                      {
                                                          receive_serial_no_details_id = rsnd.receive_serial_no_details_id,
                                                          to_deliver_master_id = rsnd.to_deliver_master_id,
                                                          product_id = pro.product_id,
                                                          product_name = pro.product_name,
                                                          color_id = col.color_id,
                                                          color_name = col.color_name,
                                                          product_version_id = ver.product_version_id,
                                                          product_version_name = ver.product_version_name,
                                                          imei_no = rsnd.imei_no,
                                                          imei_no2 = rsnd.imei_no2

                                                      }).Where(r => r.to_deliver_master_id == to_delivery_master_id).OrderByDescending(e => e.receive_serial_no_details_id).ToList();

            return toDeliveryModel;
        }

        public object GetMonthlyTransferReport(DateTime from_date, DateTime to_date, long from_warehouse_id)
        {
            DateTime toDate = to_date.AddDays(1);

            try
            {
                string query = "select tdm.to_delivery_master_id,tdm.to_delivery_date,tdm.to_delivery_no,tw.warehouse_name as to_warehouse_name,pro.product_name,col.color_name,ver.product_version_name, tdd.delivered_quantity,us.full_name,tdm.delivery_method,tdm.to_logistics_delivered_by,cou.courier_name, '"+from_date+"' as from_date, '"+to_date+"' as to_date from to_delivery_master tdm left join warehouse tw on tdm.to_warehouse_id = tw.warehouse_id left join to_delivery_details tdd on tdm.to_delivery_master_id = tdd.to_delivery_master_id left join product pro on tdd.product_id = pro.product_id left join color col on tdd.color_id = col.color_id left join product_version ver on tdd.product_version_id = ver.product_version_id left join users us on tdm.created_by = us.user_id left join courier cou on tdm.courier_id = cou.courier_id where tdm.status='Delivered' and tdm.from_warehouse_id='" + from_warehouse_id + "' and tdm.to_delivery_date >= '" + from_date + "' and tdm.to_delivery_date <= '" + toDate + "'";

                var data = _entities.Database.SqlQuery<MonthlyTransferReportModel>(query).ToList();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object GetToDeliveryReportById(long to_delivery_master_id)
        {
            try
            {
                string query = "select distinct tdm.to_delivery_no ,tdm.to_delivery_date ,usr.full_name ,tom.order_no ,fwn.warehouse_name as from_warehouse_name ,twn.warehouse_name as to_warehouse_name ,pro.product_name ,pc.product_category_name ,bd.brand_name ,col.color_name ,pv.product_version_name ,(SELECT STUFF((SELECT ' ' + imei_no FROM receive_serial_no_details where tdd.product_id=receive_serial_no_details.product_id and tdd.color_id=receive_serial_no_details.color_id and tdd.product_version_id=receive_serial_no_details.product_version_id and receive_serial_no_details.to_deliver_master_id='" + to_delivery_master_id + "' FOR XML PATH('')) ,1,1,'') AS Txt)as imei_no ,(SELECT STUFF((SELECT ' ' + imei_no2 FROM receive_serial_no_details where tdd.product_id=receive_serial_no_details.product_id and tdd.color_id=receive_serial_no_details.color_id and tdd.product_version_id=receive_serial_no_details.product_version_id and receive_serial_no_details.to_deliver_master_id='" + to_delivery_master_id + "' FOR XML PATH('')) ,1,1,'') AS Txt)as imei_no2 ,tdm.remarks ,tdd.delivered_quantity ,(tdd.to_quantity-tdd.delivered_quantity) as deliverable_quantity FROM to_delivery_master tdm inner join to_delivery_details tdd on tdm.to_delivery_master_id = tdd.to_delivery_master_id left join receive_serial_no_details rsnd on tdd.product_id =rsnd.product_id and tdd.color_id =rsnd.color_id and tdd.product_version_id=rsnd.product_version_id and tdd.to_delivery_master_id = rsnd.to_deliver_master_id inner join product pro on tdd.product_id = pro.product_id left join color col on tdd.color_id= col.color_id left join product_version pv on tdd.product_version_id= pv.product_version_id inner join warehouse fwn on tdm.from_warehouse_id= fwn.warehouse_id inner join warehouse twn on tdm.to_warehouse_id= twn.warehouse_id inner join transfer_order_master tom on tdm.transfer_order_master_id = tom.transfer_order_master_id inner join brand bd on pro.brand_id= bd.brand_id inner join product_category pc on pro.product_category_id= pc.product_category_id left join users usr on tdm.created_by = usr.user_id where tdm.to_delivery_master_id='" + to_delivery_master_id + "' group by tdm.to_delivery_no ,tdm.to_delivery_date ,usr.full_name ,tom.order_no ,fwn.warehouse_name ,twn.warehouse_name ,pro.product_name ,col.color_name ,tdm.remarks ,tdd.delivered_quantity ,bd.brand_name ,pc.product_category_name ,tdd.to_quantity-tdd.delivered_quantity ,tdd.product_id ,tdd.color_id ,tdd.product_version_id ,tdd.to_delivery_master_id ,pv.product_version_name";

                var poData = _entities.Database.SqlQuery<ToDeliveryReportModel>(query).ToList();
                return poData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long AddToDelivery(ToDeliveryModel toDeliveryModel)
        {
            try
            {
                var toDeliveryMaster = toDeliveryModel.ToDeliveryMasterData;
                var toDeliveryDetailsList = toDeliveryModel.ToDeliveryDetailsList;
                var receiveSerialNoDetails = toDeliveryModel.ReceiveSerialNoDetails;

                // generate order_no
                long toDeliverySerial = _entities.to_delivery_master.Max(po => (long?)po.to_delivery_master_id) ?? 0;

                toDeliverySerial++;
                var toDeliveryStr = toDeliverySerial.ToString().PadLeft(7, '0');

                string toDeliveryNo = "TO-DN" + "-" + toDeliveryStr;
                toDeliveryMaster.to_delivery_no = toDeliveryNo;
                toDeliveryMaster.to_delivery_date = DateTime.Now;
                toDeliveryMaster.transfer_order_master_id = toDeliveryModel.ToDeliveryMasterData.transfer_order_master_id;
                toDeliveryMaster.courier_id = toDeliveryModel.ToDeliveryMasterData.courier_id;
                toDeliveryMaster.courier_slip_no = toDeliveryModel.ToDeliveryMasterData.courier_slip_no;
                toDeliveryMaster.from_warehouse_id = toDeliveryModel.ToDeliveryMasterData.from_warehouse_id;
                toDeliveryMaster.to_warehouse_id = toDeliveryModel.ToDeliveryMasterData.to_warehouse_id;
                toDeliveryMaster.remarks = toDeliveryModel.ToDeliveryMasterData.remarks;
                toDeliveryMaster.status = "RFD";
                toDeliveryMaster.created_by = toDeliveryModel.ToDeliveryMasterData.created_by;
                toDeliveryMaster.created_date = DateTime.Now;
                toDeliveryMaster.updated_by = toDeliveryModel.ToDeliveryMasterData.updated_by;
                toDeliveryMaster.updated_date = DateTime.Now;
                toDeliveryMaster.is_active = true;
                toDeliveryMaster.is_deleted = false;

                _entities.to_delivery_master.Add(toDeliveryMaster);
                _entities.SaveChanges();

                long toDeliveryMasterId = toDeliveryMaster.to_delivery_master_id;

                var transferOrderTotalQty = 0;
                var deliveredTotalQty = 0;

                foreach (var item in toDeliveryDetailsList)
                {
                    var toDeliveryDetails = new to_delivery_details();

                    toDeliveryDetails.to_delivery_master_id = toDeliveryMasterId;
                    toDeliveryDetails.product_id = item.product_id;
                    toDeliveryDetails.color_id = item.color_id;
                    toDeliveryDetails.product_version_id = item.product_version_id;
                    toDeliveryDetails.unit_id = item.unit_id;
                    toDeliveryDetails.to_quantity = item.to_quantity;
                    toDeliveryDetails.delivered_quantity = item.delivered_quantity;


                    if (toDeliveryDetails.delivered_quantity > 0) //maruf
                    {
                        _entities.to_delivery_details.Add(toDeliveryDetails);
                    }

                    long saved = _entities.SaveChanges();

                    if (saved > 0)
                    {
                        // update inventory
                        InventoryRepository updateInventoty = new InventoryRepository();

                        //'39' virtual in-transit warehouse-----------------------------
                        updateInventoty.UpdateInventory("TO RFD", toDeliveryMaster.to_delivery_no, toDeliveryMaster.from_warehouse_id ?? 0, 39, toDeliveryDetails.product_id ?? 0, toDeliveryDetails.color_id ?? 0, toDeliveryDetails.product_version_id ?? 0, toDeliveryDetails.unit_id ?? 0,
                            toDeliveryDetails.delivered_quantity ?? 0, toDeliveryMaster.created_by ?? 0);
                    }
                    long transferOrderDetailsId = item.transfer_order_details_id;

                    transfer_order_details transferOrderDetails = _entities.transfer_order_details.Find(transferOrderDetailsId);

                    // Maruf: updating paramenters to check delivery status
                    transferOrderDetails.transfered_quantity += item.delivered_quantity;
                    transferOrderTotalQty += toDeliveryDetails.to_quantity ?? 0;
                    deliveredTotalQty += toDeliveryDetails.delivered_quantity ?? 0;
                    _entities.SaveChanges();
                }
                // maruf: updating delivery status 19.Oct.2016
                var transferOrderMaster = _entities.transfer_order_master.FirstOrDefault(r => r.transfer_order_master_id == toDeliveryModel.ToDeliveryMasterData.transfer_order_master_id);
                transferOrderMaster.status = (deliveredTotalQty >= transferOrderTotalQty) ? "Delivered" : "Partially Delivered";
                _entities.SaveChanges();


                foreach (var item in receiveSerialNoDetails)
                {
                    receive_serial_no_details receiveSerial = _entities.receive_serial_no_details.FirstOrDefault(r => r.imei_no == item.imei_no || r.imei_no2 == item.imei_no);

                    receiveSerial.current_warehouse_id = 39;
                    receiveSerial.deliver_date = DateTime.Now;
                    receiveSerial.to_deliver_master_id = toDeliveryMaster.to_delivery_master_id;
                    _entities.SaveChanges();
                }
                return 1;

            }
            catch (Exception)
            {
                return 0;
            }
        }

        public long RfdDelivery(ToDeliveryModel toDeliveryModel)
        {
            try
            {
                var toDeliveryMaster = toDeliveryModel.ToDeliveryMasterData;
                var toDeliveryDetailsList = toDeliveryModel.ToDeliveryDetailsList;
                var receiveSerialNoDetails = toDeliveryModel.ReceiveSerialNoDetails;

                var toDeliveryObject = _entities.to_delivery_master.Find(toDeliveryMaster.to_delivery_master_id);
                toDeliveryObject.status = "Delivered";
                toDeliveryObject.delivery_method = toDeliveryMaster.delivery_method;
                toDeliveryObject.courier_id = toDeliveryMaster.courier_id;
                toDeliveryObject.to_logistics_delivered_by = toDeliveryMaster.to_logistics_delivered_by;
                _entities.SaveChanges();

                foreach (var item in toDeliveryDetailsList)
                {
                        InventoryRepository updateInventoty = new InventoryRepository();

                        //'39' virtual in-transit warehouse-----------------------------
                        updateInventoty.UpdateInventory("TO DELIVERED ", toDeliveryMaster.to_delivery_no, 39, toDeliveryMaster.to_warehouse_id ?? 0, item.product_id ?? 0, item.color_id ?? 0, item.product_version_id ?? 0, item.unit_id ?? 0,
                            item.delivered_quantity ?? 0, toDeliveryMaster.created_by ?? 0);
                }


                foreach (var item in receiveSerialNoDetails)
                {
                    receive_serial_no_details receiveSerial = _entities.receive_serial_no_details.FirstOrDefault(r => r.imei_no == item.imei_no || r.imei_no2 == item.imei_no);

                    receiveSerial.current_warehouse_id = toDeliveryMaster.to_warehouse_id;
                    receiveSerial.deliver_date = DateTime.Now;
                    _entities.SaveChanges();
                }
                return 1;

            }
            catch (Exception)
            {
                return 0;
            }
        }

        public long CancelToDelivery(ToDeliveryModel toDeliveryModel)
        {
            try
            {
                var toDeliveryMaster = toDeliveryModel.ToDeliveryMasterData;
                var toDeliveryDetailsList = toDeliveryModel.ToDeliveryDetailsList;
                var receiveSerialNoDetails = toDeliveryModel.ReceiveSerialNoDetails;

                var toDeliveryObject = _entities.to_delivery_master.Find(toDeliveryMaster.to_delivery_master_id);
                toDeliveryObject.status = "Canceled";
                toDeliveryObject.is_active = false;
                toDeliveryObject.is_deleted = true;
                _entities.SaveChanges();

                foreach (var item in toDeliveryDetailsList)
                {
                    var toDeliveryDetails = new to_delivery_details();
                    toDeliveryDetails.delivered_quantity = item.delivered_quantity;

                    var transferOrderDetailsData = _entities.transfer_order_details.Where(t => t.transfer_order_master_id == toDeliveryObject.transfer_order_master_id).ToList();

                    var fullReturn = true;

                    foreach (var todItem in transferOrderDetailsData)
                    {
                        if (todItem.product_id == item.product_id && todItem.color_id == item.color_id && todItem.product_version_id == item.product_version_id)
                        {
                            todItem.transfered_quantity -= item.delivered_quantity;
                            _entities.SaveChanges();
                            if (todItem.transfered_quantity != 0)
                            {
                                fullReturn = false;
                            }
                        }
                    }
                    if (fullReturn)
                    {
                        var xxx = _entities.transfer_order_master.Find(toDeliveryObject.transfer_order_master_id);
                        xxx.status = "Created";
                        _entities.SaveChanges();
                        InventoryRepository updateInventoty = new InventoryRepository();

                        //'39' virtual in-transit warehouse-----------------------------
                        updateInventoty.UpdateInventory("TO DELIVERY CANCEL", toDeliveryMaster.to_delivery_no, 39, toDeliveryMaster.from_warehouse_id ?? 0, item.product_id ?? 0, item.color_id ?? 0, item.product_version_id ?? 0, item.unit_id ?? 0,
                            item.delivered_quantity ?? 0, toDeliveryMaster.created_by ?? 0);
                    }
                    else
                    {
                        var xxx = _entities.transfer_order_master.Find(toDeliveryObject.transfer_order_master_id);
                        xxx.status = "Partially Delivered";
                        _entities.SaveChanges();
                        InventoryRepository updateInventoty = new InventoryRepository();

                        //'39' virtual in-transit warehouse-----------------------------
                        updateInventoty.UpdateInventory("TO DELIVERY CANCEL", toDeliveryMaster.to_delivery_no, 39, toDeliveryMaster.from_warehouse_id ?? 0, item.product_id ?? 0, item.color_id ?? 0, item.product_version_id ?? 0, item.unit_id ?? 0,
                            item.delivered_quantity ?? 0, toDeliveryMaster.created_by ?? 0);
                    }


                }


                foreach (var item in receiveSerialNoDetails)
                {
                    receive_serial_no_details receiveSerial = _entities.receive_serial_no_details.FirstOrDefault(r => r.imei_no == item.imei_no || r.imei_no2 == item.imei_no);

                    receiveSerial.current_warehouse_id = toDeliveryMaster.from_warehouse_id;
                    receiveSerial.deliver_date = null;
                    receiveSerial.to_deliver_master_id = null;
                    _entities.SaveChanges();
                }
                return 1;

            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}