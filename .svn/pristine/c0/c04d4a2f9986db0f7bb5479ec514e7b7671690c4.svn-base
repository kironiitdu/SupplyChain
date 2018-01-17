using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using DMSApi.Models.crystal_models;
using DMSApi.Models.IRepository;
using DMSApi.Models.StronglyType;
using Microsoft.Ajax.Utilities;
using WebGrease.Css.Extensions;

namespace DMSApi.Models.Repository
{
    public class DeliveryRepository : IDeliveryRepository
    {
        private readonly DMSEntities _entities;

        public DeliveryRepository()
        {
            _entities = new DMSEntities();
        }

        public object GetAllDelivery()
        {
            var data = (from dm in _entities.delivery_master
                        join p in _entities.parties on dm.party_id equals p.party_id
                        join r in _entities.requisition_master on dm.requisition_master_id equals r.requisition_master_id
                        select new
                        {
                            delivery_master_id = dm.delivery_master_id,
                            delivery_no = dm.delivery_no,
                            delivery_date = dm.delivery_date,
                            party_id = dm.party_id,
                            party_name = p.party_name,
                            total_amount = dm.total_amount,
                            status = dm.status,
                            courier_slip_no = dm.courier_slip_no,
                            requisition_code = r.requisition_code,
                            created_date = r.created_date

                        }).OrderByDescending(e => e.delivery_master_id).ToList();

            return data;
        }

        public object GetAllDeliveryByPartyId(long party_id)
        {
            if (party_id == 1)
            {
                //delivery list view for admin ----------------------------

                var data = (from dm in _entities.delivery_master
                            join p in _entities.parties on dm.party_id equals p.party_id
                            join r in _entities.requisition_master on dm.requisition_master_id equals r.requisition_master_id
                            join inv in _entities.invoice_master on dm.requisition_master_id equals inv.requisition_master_id
                            where dm.status == "RFD" && (inv.party_type_id == 4 || inv.party_type_id == 5) && (dm.party_id != 78) // dealer or b2b
                            select new
                            {

                                delivery_master_id = dm.delivery_master_id,
                                delivery_no = dm.delivery_no,
                                delivery_date = dm.delivery_date,
                                invoice_master_id = inv.invoice_master_id,
                                party_id = dm.party_id,
                                party_name = p.party_name,
                                total_amount = dm.total_amount,
                                status = dm.status,
                                courier_slip_no = dm.courier_slip_no,
                                requisition_code = r.requisition_code,
                                created_date = r.created_date,
                                receive_date = dm.receive_date

                            }).OrderByDescending(e => e.delivery_master_id).ToList();

                return data;
            }
            //else
            //{
            //    //delivery list view for MD/DBIS/Retailer ----------------------------
            //    var data = (from dm in _entities.delivery_master
            //                join p in _entities.parties on dm.party_id equals p.party_id
            //                join r in _entities.requisition_master on dm.requisition_master_id equals r.requisition_master_id
            //                where dm.party_id == party_id && dm.status=="RFD"
            //                select new
            //                {
            //                    delivery_master_id = dm.delivery_master_id,
            //                    delivery_no = dm.delivery_no,
            //                    delivery_date = dm.delivery_date,
            //                    party_id = dm.party_id,
            //                    party_name = p.party_name,
            //                    total_amount = dm.total_amount,
            //                    status = dm.status,
            //                    courier_slip_no = dm.courier_slip_no,
            //                    requisition_code = r.requisition_code,
            //                    created_date = r.created_date,
            //                    receive_date = dm.receive_date

            //                }).OrderByDescending(e => e.delivery_master_id).ToList();

            //    return data;
            //}
            return null;
        }

        public object GetDeliveryReportById(long delivery_master_id)
        {
            try
            {
                // string query = "select distinct dm.delivery_no,dm.lot_no,dm.delivery_date,usr.full_name,rm.requisition_code,par.party_name, whf.warehouse_name as from_warehouse_name,wht.warehouse_name as to_warehouse_name,pro.product_name, pc.product_category_name,bd.brand_name,col.color_name, string_agg(imei_no, ', ')as imei_no , dm.remarks, dd.delivered_quantity,(dd.requisition_quantity-dd.delivered_quantity) as deliverable_quantity, dd.unit_price,dm.total_amount,dd.line_total FROM delivery_master dm inner join delivery_details dd on dm.delivery_master_id =dd.delivery_master_id inner join receive_serial_no_details rsnd on dd.product_id =rsnd.product_id and dd.color_id =rsnd.color_id and dd.delivery_master_id =rsnd.deliver_master_id inner join product pro on dd.product_id = pro.product_id inner join color col on dd.color_id= col.color_id inner join warehouse whf on dm.from_warehouse_id= whf.warehouse_id inner join warehouse wht on dm.to_warehouse_id= wht.warehouse_id inner join party par on dm.party_id= par.party_id inner join requisition_master rm on dm.requisition_master_id= rm.requisition_master_id inner join brand bd on pro.brand_id= bd.brand_id inner join product_category pc on pro.product_category_id= pc.product_category_id left join users usr on dm.delivered_by = usr.user_id where dm.delivery_master_id='" + delivery_master_id + "' group by dm.delivery_no,dm.delivery_date,usr.full_name,rm.requisition_code,par.party_name,from_warehouse_name,to_warehouse_name,pro.product_name,col.color_name , dm.remarks, dd.delivered_quantity,deliverable_quantity, dd.unit_price, dm.total_amount ,bd.brand_name,pc.product_category_name, dd.line_total,dm.lot_no";
                // Maruf: 11.Dec.2016: seperate rows for live dummy, sales gift, promotional gift
                //string query = "select distinct dm.delivery_no ,company.company_name,dm.lot_no ,dm.delivery_date ,usr.full_name ,rm.requisition_code ,par.party_name , whf.warehouse_name as from_warehouse_name , wht.warehouse_name as to_warehouse_name ,pro.product_name , pc.product_category_name , bd.brand_name ,col.color_name , string_agg(imei_no, ', ')as imei_no , dm.remarks, dd.delivered_quantity , (dd.requisition_quantity-dd.delivered_quantity) as deliverable_quantity, dd.unit_price,dm.total_amount,dd.line_total ,dd.is_gift ,dd.delivery_details_id ,dd.is_live_dummy , COALESCE(dd.gift_type, '') as gift_type FROM delivery_master dm inner join delivery_details dd on dm.delivery_master_id =dd.delivery_master_id inner join receive_serial_no_details rsnd on dd.product_id =rsnd.product_id and dd.color_id =rsnd.color_id and dd.delivery_master_id = rsnd.deliver_master_id inner join product pro on dd.product_id = pro.product_id inner join color col on dd.color_id= col.color_id inner join warehouse whf on dm.from_warehouse_id= whf.warehouse_id inner join warehouse wht on dm.to_warehouse_id= wht.warehouse_id inner join party par on dm.party_id= par.party_id inner join requisition_master rm on dm.requisition_master_id= rm.requisition_master_id inner join company  on rm.company_id=company.company_id inner join brand bd on pro.brand_id= bd.brand_id inner join product_category pc on pro.product_category_id= pc.product_category_id left join users usr on dm.delivered_by = usr.user_id where dm.delivery_master_id='" + delivery_master_id + "' and dd.is_gift=rsnd.is_gift and dd.is_live_dummy=rsnd.is_live_dummy AND COALESCE(dd.gift_type, '') = COALESCE(rsnd.gift_type, '') group by dm.delivery_no,company.company_name,dm.delivery_date,usr.full_name,rm.requisition_code,par.party_name,from_warehouse_name,to_warehouse_name, pro.product_name, col.color_name , dm.remarks, dd.delivered_quantity,deliverable_quantity, dd.unit_price, dm.total_amount , bd.brand_name,pc.product_category_name, dd.line_total,dm.lot_no, dd.gift_type,dd.gift_type ,dd.is_live_dummy,dd.is_gift ,dd.delivery_details_id order by delivery_details_id asc";

                string query = "select distinct  par.address,par.mobile,par.email,dm.delivery_no ,reg.region_name ,are.area_name ,ter.territory_name ,company.company_name ,dm.lot_no ,dm.delivery_date ,usr.full_name ,rm.requisition_code ,par.party_name ,whf.warehouse_name as from_warehouse_name ,wht.warehouse_name as to_warehouse_name ,pro.product_name ,pc.product_category_name ,bd.brand_name ,col.color_name ,(SELECT STUFF((SELECT ' ' + imei_no FROM receive_serial_no_details where dd.product_id=receive_serial_no_details.product_id and dd.color_id=receive_serial_no_details.color_id and dd.product_version_id=receive_serial_no_details.product_version_id and receive_serial_no_details.deliver_master_id=26 FOR XML PATH('')) ,1,1,'') AS Txt)as imei_no ,dm.remarks ,dd.delivered_quantity ,(dd.requisition_quantity-dd.delivered_quantity) as deliverable_quantity , dd.unit_price,dm.total_amount,dd.line_total , dd.delivery_details_id FROM delivery_master dm inner join delivery_details dd on dm.delivery_master_id =dd.delivery_master_id left join receive_serial_no_details rsnd on dd.product_id =rsnd.product_id and dd.color_id =rsnd.color_id and dd.delivery_master_id = rsnd.deliver_master_id inner join product pro on dd.product_id = pro.product_id left join color col on dd.color_id= col.color_id left join warehouse whf on dm.from_warehouse_id= whf.warehouse_id left join warehouse wht on dm.to_warehouse_id= wht.warehouse_id inner join party par on dm.party_id= par.party_id inner join region reg on par.region_id = reg.region_id inner join area are on par.area_id = are.area_id inner join territory ter on par.territory_id = ter.territory_id inner join requisition_master rm on dm.requisition_master_id= rm.requisition_master_id inner join company on rm.company_id=company.company_id inner join brand bd on pro.brand_id= bd.brand_id inner join product_category pc on pro.product_category_id= pc.product_category_id left join users usr on dm.delivered_by = usr.user_id where dm.delivery_master_id='" + delivery_master_id + "' group by par.address, dm.delivery_no ,reg.region_name ,are.area_name ,ter.territory_name ,company.company_name ,dm.delivery_date ,usr.full_name ,rm.requisition_code ,par.party_name , whf.warehouse_name ,wht.warehouse_name , pro.product_name , col.color_name , dm.remarks , dd.delivered_quantity ,dd.product_id,dd.color_id ,dd.product_version_id , dd.requisition_quantity , dd.unit_price , dm.total_amount , bd.brand_name ,pc.product_category_name , dd.line_total,dm.lot_no , dd.delivery_details_id,par.mobile,par.email order by delivery_details_id asc";
                var poData = _entities.Database.SqlQuery<DeliveryReportModel>(query).ToList();

                return poData;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public object GetDeliveryExcelReportByDeliveryMasterId(long delivery_master_id)
        {
            try
            {
                string query = "select pro.product_name,c.color_name,rsnd.imei_no from receive_serial_no_details rsnd inner join product pro on rsnd.product_id = pro.product_id inner join color c on c.color_id =rsnd.color_id where rsnd.deliver_master_id='" + delivery_master_id + "'";
                var poData = _entities.Database.SqlQuery<DeliveryReportModel>(query).ToList();

                return poData;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public long AddDelivery(DeliveryModel deliveryModel)
        {
            try
            {
                var deliveryMaster = deliveryModel.DeliveryMasterData;
                var deliveryDetailsList = deliveryModel.DeliveryDetailsList;
                var receiveSerialNoDetails = deliveryModel.ReceiveSerialNoDetails;
                long counter = 0;

                //Get Party type Prefix by party Id :Kiron:27/10/2016

                var partyTypePrefix = (from ptype in _entities.party_type
                                       join par in _entities.parties on ptype.party_type_id equals par.party_type_id
                                       where par.party_id == deliveryMaster.party_id
                                       select new
                                       {
                                           party_prefix = ptype.party_prefix

                                       }).FirstOrDefault();

                // IMEI Validity Check
                foreach (var item in receiveSerialNoDetails)
                {
                    long imeiForDuplicateSearch = item.imei_no??0;
                    var xxx = _entities.receive_serial_no_details.FirstOrDefault(r => r.imei_no == imeiForDuplicateSearch || r.imei_no2 == imeiForDuplicateSearch);

                    var versionId = deliveryDetailsList.FirstOrDefault(d => d.product_id == xxx.product_id && d.color_id == xxx.color_id).product_version_id;
                    // check if imei valid
                    if (xxx == null)
                    {
                        return 2;
                    }
                    if (xxx.product_id != item.product_id || xxx.color_id != item.color_id || xxx.product_version_id != versionId)
                    {
                        return 3;
                    }
                    // check if imei already delivered
                    if (xxx.deliver_master_id != 0 && xxx.deliver_master_id != null)
                    {
                        counter++;
                    }

                }

                if (counter == 0)
                {
                    // generate order_no
                    long deliverySerial = _entities.delivery_master.Max(po => (long?)po.delivery_master_id) ?? 0;

                    deliverySerial++;
                    var deliveryStr = deliverySerial.ToString().PadLeft(7, '0');

                    //string orderNo = "ORD/" + DateTime.Now.Year + "/" + poSerial;
                    string deliveryNo = "DN-" + partyTypePrefix.party_prefix + "-" + deliveryStr;
                    deliveryMaster.delivery_no = deliveryNo;
                    deliveryMaster.delivery_date = deliveryModel.DeliveryMasterData.delivery_date;
                    deliveryMaster.party_id = deliveryModel.DeliveryMasterData.party_id;
                    deliveryMaster.requisition_master_id = deliveryModel.DeliveryMasterData.requisition_master_id;
                    deliveryMaster.courier_id = deliveryModel.DeliveryMasterData.courier_id;
                    deliveryMaster.company_id = deliveryModel.DeliveryMasterData.company_id;
                    deliveryMaster.courier_slip_no = deliveryModel.DeliveryMasterData.courier_slip_no;
                    deliveryMaster.delivery_address = deliveryModel.DeliveryMasterData.delivery_address;
                    deliveryMaster.delivered_by = deliveryModel.DeliveryMasterData.delivered_by;
                    deliveryMaster.from_warehouse_id = deliveryModel.DeliveryMasterData.from_warehouse_id;
                    deliveryMaster.to_warehouse_id = deliveryModel.DeliveryMasterData.to_warehouse_id;
                    deliveryMaster.status = "RFD";
                    deliveryMaster.remarks = deliveryModel.DeliveryMasterData.remarks;
                    deliveryMaster.total_amount = deliveryModel.DeliveryMasterData.total_amount;
                    deliveryMaster.lot_no = deliveryModel.DeliveryMasterData.lot_no;
                    deliveryMaster.vehicle_no = deliveryModel.DeliveryMasterData.vehicle_no;
                    deliveryMaster.truck_driver_name = deliveryModel.DeliveryMasterData.truck_driver_name;
                    deliveryMaster.truck_driver_mobile = deliveryModel.DeliveryMasterData.truck_driver_mobile;

                    _entities.delivery_master.Add(deliveryMaster);
                    _entities.SaveChanges();
                    long deliveryMasterId = deliveryMaster.delivery_master_id;
                    var requisitionTotalQty = 0;
                    var deliveryTotalQty = 0;


                    foreach (var item in deliveryDetailsList)
                    {
                        int liftinQuantity = 0;
                        var deliveryDetails = new delivery_details();

                        if (item.is_gift == false || item.is_gift == null)
                        {
                            deliveryDetails.delivery_master_id = deliveryMasterId;
                            deliveryDetails.party_id = item.party_id;
                            deliveryDetails.product_id = item.product_id;
                            deliveryDetails.product_version_id = item.product_version_id;
                            deliveryDetails.requisition_quantity = item.requisition_quantity;
                            deliveryDetails.unit_id = item.unit_id;
                            deliveryDetails.unit_price = item.unit_price;
                            deliveryDetails.color_id = item.color_id;
                            deliveryDetails.delivered_quantity = item.delivered_quantity;
                            deliveryDetails.line_total = item.line_total;
                            deliveryDetails.is_gift = false;
                            deliveryDetails.gift_type = "";
                            deliveryDetails.is_live_dummy = item.is_live_dummy;
                            liftinQuantity += item.delivered_quantity ?? 0;
                        }
                        else
                        {
                            deliveryDetails.delivery_master_id = deliveryMasterId;
                            deliveryDetails.party_id = item.party_id;
                            deliveryDetails.product_id = item.product_id;
                            deliveryDetails.product_version_id = item.product_version_id;
                            deliveryDetails.requisition_quantity = item.requisition_quantity;
                            deliveryDetails.unit_id = item.unit_id;
                            deliveryDetails.unit_price = 0;
                            deliveryDetails.color_id = item.color_id;
                            deliveryDetails.delivered_quantity = item.delivered_quantity;
                            deliveryDetails.line_total = 0;
                            deliveryDetails.is_gift = item.is_gift;
                            deliveryDetails.gift_type = item.gift_type;
                            deliveryDetails.is_live_dummy = false;
                            liftinQuantity += item.delivered_quantity ?? 0;
                        }

                        if (deliveryDetails.delivered_quantity > 0) //maruf
                        {
                            _entities.delivery_details.Add(deliveryDetails);
                        }


                        long saved = _entities.SaveChanges();


                        //liftingQuantity add----
                        var invLog = _entities.invoice_log.FirstOrDefault(xxx => xxx.requisition_master_id == deliveryMaster.requisition_master_id);
                        invLog.lifting_quantity += liftinQuantity;
                        _entities.SaveChanges();

                        if (saved > 0)
                        {
                            // update inventory
                            var updateInventoty = new InventoryRepository();

                            //'39' virtual in-transit warehouse-----------------------------
                            updateInventoty.UpdateInventory("RFD", deliveryMaster.delivery_no, deliveryMaster.from_warehouse_id ?? 0, 39, deliveryDetails.product_id ?? 0, deliveryDetails.color_id ?? 0, deliveryDetails.product_version_id ?? 0, deliveryDetails.unit_id ?? 0,
                                deliveryDetails.delivered_quantity ?? 0, deliveryMaster.delivered_by ?? 0);
                        }
                        long requisitionDetailsId = item.requisition_details_id;

                        var requisitionDetails = _entities.requisition_details.Find(requisitionDetailsId);

                        // Maruf: updating paramenters to check delivery status
                        requisitionDetails.delivered_quantity += item.delivered_quantity;
                        requisitionTotalQty += requisitionDetails.quantity ?? 0;
                        deliveryTotalQty += requisitionDetails.delivered_quantity ?? 0;
                        _entities.SaveChanges();
                    }
                    // maruf: updating delivery status 19.Oct.2016
                    var requisitionMaster = _entities.requisition_master.FirstOrDefault(r => r.requisition_master_id == deliveryModel.DeliveryMasterData.requisition_master_id);
                    requisitionMaster.delivery_status = (deliveryTotalQty >= requisitionTotalQty) ? "Delivered" : "Partially Delivered";
                    _entities.SaveChanges();

                    //long counter = 0;

                    foreach (var item in receiveSerialNoDetails)
                    {
                        receive_serial_no_details receiveSerial = _entities.receive_serial_no_details.FirstOrDefault(r => r.imei_no == item.imei_no || r.imei_no2 == item.imei_no);
                        //receiveSerial.product_id = receive_serial_no_details.product_id;
                        //receiveSerial.brand_id = receive_serial_no_details.brand_id;
                        //receiveSerial.color_id = receive_serial_no_details.color_id;
                        //receiveSerial.imei_no = receive_serial_no_details.imei_no;
                        //receiveSerial.received_warehouse_id = receive_serial_no_details.received_warehouse_id;
                        //receiveSerial.received_date = receive_serial_no_details.received_date;
                        //receiveSerial.grn_master_id = receive_serial_no_details.grn_master_id;
                        //receiveSerial.sales_status = receive_serial_no_details.sales_status;
                        //receiveSerial.current_warehouse_id = deliveryModel.DeliveryMasterData.to_warehouse_id;
                        //maruf: 07.Dec.2016: setting in-transit warehouse instead of party warehouse. It will be updated by party warehouse while it will be received by party


                        //receiveSerial.current_warehouse_id = deliveryMaster.to_warehouse_id;//comments on 22.05.2017
                        receiveSerial.current_warehouse_id = 39;// 22.05.2017//when deliver need to go product in Intransit warehouse.
                        receiveSerial.party_id = deliveryMaster.party_id;
                        receiveSerial.deliver_date = deliveryMaster.delivery_date;
                        receiveSerial.requisition_id = deliveryMaster.requisition_master_id;
                        receiveSerial.deliver_master_id = deliveryMaster.delivery_master_id;
                        receiveSerial.is_gift = item.is_gift;
                        receiveSerial.is_live_dummy = item.is_live_dummy;
                        _entities.SaveChanges();


                    }
                    return 1;
                }
                return 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }



        // update delivery status=Received when party receives the delivery
        //public bool UpdateApproveStatus(long delivery_master_id, long userid)
        //{
        //    try
        //    {
        //        delivery_master deMaster = _entities.delivery_master.Find(delivery_master_id);
        //        deMaster.status = "Received";
        //        //deMaster.receive_date = DateTime.Now.ToString();
        //        deMaster.receive_date = DateTime.Now;
        //        deMaster.receive_by = userid;
        //        _entities.SaveChanges();

        //        //retrieve data from delivery_master
        //        string delivery_no = deMaster.delivery_no;
        //        long delivery_to = deMaster.to_warehouse_id ?? 0;
        //        long delivered_by = deMaster.delivered_by ?? 0;

        //        long product_id = 0;
        //        long color_id = 0;
        //        long unit_id = 0;
        //        int delivery_quantity = 0;
        //        long product_version_id = 0;

        //        //retrieve data from delivery_details
        //        var deliveryDetailsList = _entities.delivery_details.Where(d => d.delivery_master_id == delivery_master_id).ToList();
        //        foreach (var d in deliveryDetailsList)
        //        {

        //            product_id = d.product_id ?? 0;
        //            color_id = d.color_id ?? 0;
        //            unit_id = d.unit_id ?? 0;
        //            product_version_id = d.product_version_id ?? 0;
        //            delivery_quantity = d.delivered_quantity ?? 0;


        //            InventoryRepository updateInventoty = new InventoryRepository();
        //            updateInventoty.UpdateInventory("RECEIVE", delivery_no, 39,
        //                delivery_to, product_id, color_id, product_version_id, unit_id,
        //                delivery_quantity, userid);
        //        }

        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //}

        public delivery_master GetCourierInformation(long delivery_master_id)
        {
            var loadCourierInformationByDeliveryMasterId = _entities.delivery_master.Find(delivery_master_id);
            return loadCourierInformationByDeliveryMasterId;
        }

        public DeliveryModel GetDeliveryByIdForConfirmation(long delivery_master_id)
        {
            var deliveryModel = new DeliveryModel();
            try
            {
                deliveryModel.DeliveryMasterData = _entities.delivery_master.Find(delivery_master_id);
                var partyInfo = _entities.parties.Find(deliveryModel.DeliveryMasterData.party_id);
                var deliveryDetailsList = _entities.delivery_details.Where(d => d.delivery_master_id == delivery_master_id)
                    .Join(_entities.products, jp => jp.product_id, p => p.product_id, (jp, p) => new { jp, p })
                    .GroupJoin(_entities.colors, jc => jc.jp.color_id, c => c.color_id, (jc, c) => new { jc, nc = c.FirstOrDefault() })
                    //.Join(_entities.units, ju => ju.jc.jp.unit_id, u => u.unit_id, (ju, u) => new { ju, u })
                    .GroupJoin(_entities.units, ju => ju.jc.jp.unit_id, u => u.unit_id, (ju, u) => new { ju, uu = u.FirstOrDefault() })
                    .GroupJoin(_entities.product_version, jv => jv.ju.jc.jp.product_version_id, v => v.product_version_id, (jv, v) => new { jv, nv = v.FirstOrDefault() })
                    .ToList();
                var deliveryDetailsModels = new List<DeliveryDetailsModel>();
                deliveryModel.region_id = partyInfo.region_id;
                deliveryModel.area_id = partyInfo.area_id;
                deliveryModel.territory_id = partyInfo.territory_id;
                deliveryModel.party_type_id = partyInfo.party_type_id;
                foreach (var d in deliveryDetailsList)
                {
                    var deliveryDetailsModel = new DeliveryDetailsModel();
                    deliveryDetailsModel.delivery_details_id = d.jv.ju.jc.jp.delivery_details_id;
                    deliveryDetailsModel.gift_type = d.jv.ju.jc.jp.gift_type;
                    deliveryDetailsModel.delivery_master_id = d.jv.ju.jc.jp.delivery_master_id;
                    deliveryDetailsModel.product_id = d.jv.ju.jc.jp.product_id;
                    deliveryDetailsModel.product_name = d.jv.ju.jc.p.product_name;
                    deliveryDetailsModel.color_id = d.jv.ju.jc.jp.color_id;
                    //deliveryDetailsModel.color_name = d.jv.ju.nc.color_name;
                    deliveryDetailsModel.color_name = (d.jv.ju.nc == null) ? "" : d.jv.ju.nc.color_name;
                    //deliveryDetailsModel.unit_id = d.jv.ju.jc.jp.unit_id;
                    //deliveryDetailsModel.unit_name = d.jv.u.unit_name;
                    deliveryDetailsModel.product_version_id = d.jv.ju.jc.jp.product_version_id;
                    //deliveryDetailsModel.product_version_name = d.nv.product_version_name;
                    deliveryDetailsModel.product_version_name = (d.nv == null) ? "" : d.nv.product_version_name;
                    deliveryDetailsModel.requisition_quantity = d.jv.ju.jc.jp.requisition_quantity;
                    deliveryDetailsModel.delivered_quantity = d.jv.ju.jc.jp.delivered_quantity;
                    deliveryDetailsModel.unit_price = d.jv.ju.jc.jp.unit_price;
                    deliveryDetailsModel.line_total = d.jv.ju.jc.jp.line_total;
                    deliveryDetailsModels.Add(deliveryDetailsModel);
                }
                deliveryModel.DeliveryDetailsList = deliveryDetailsModels;
                return deliveryModel;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public object GetAllDeliveredByPartyId(long party_id)
        {
            if (party_id == 1)
            {
                //delivery list view for admin ----------------------------

                var data = (from dm in _entities.delivery_master
                            join p in _entities.parties on dm.party_id equals p.party_id

                            join r in _entities.requisition_master on dm.requisition_master_id equals r.requisition_master_id
                            join inv in _entities.invoice_master on dm.requisition_master_id equals inv.requisition_master_id

                            where dm.status == "Delivered"
                            select new
                            {

                                delivery_master_id = dm.delivery_master_id,
                                delivery_no = dm.delivery_no,
                                delivery_date = dm.delivery_date,
                                party_id = dm.party_id,
                                party_name = p.party_name,
                                //party_prefix = pt.party_prefix,
                                invoice_master_id = inv.invoice_master_id,
                                total_amount = dm.total_amount,
                                status = dm.status,
                                delivery_method_id = dm.delivery_method_id,
                                courier_slip_no = dm.courier_slip_no,
                                requisition_code = r.requisition_code,
                                created_date = r.created_date,
                                receive_date = dm.receive_date

                            }).OrderByDescending(e => e.delivery_master_id).ToList();

                return data;
            }
            else
            {
                //delivery list view for MD/DBIS/Retailer ----------------------------
                var data = (from dm in _entities.delivery_master
                            join p in _entities.parties on dm.party_id equals p.party_id
                            join r in _entities.requisition_master on dm.requisition_master_id equals r.requisition_master_id
                            where dm.party_id == party_id && dm.status == "Delivered"
                            select new
                            {
                                delivery_master_id = dm.delivery_master_id,
                                delivery_no = dm.delivery_no,
                                delivery_date = dm.delivery_date,
                                party_id = dm.party_id,
                                party_name = p.party_name,
                                total_amount = dm.total_amount,
                                status = dm.status,
                                courier_slip_no = dm.courier_slip_no,
                                requisition_code = r.requisition_code,
                                created_date = r.created_date,
                                receive_date = dm.receive_date

                            }).OrderByDescending(e => e.delivery_master_id).ToList();

                return data;
            }
        }

        public bool UpdateCourierInfo(delivery_master objUpdateCourier)
        {
            try
            {
                delivery_master courierInfo = _entities.delivery_master.Find(objUpdateCourier.delivery_master_id);
                courierInfo.courier_slip_no = objUpdateCourier.courier_slip_no;
                courierInfo.receive_date = DateTime.Now;
                _entities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UploadDeliveryChallan()
        {
            try
            {
                HttpRequest rsk = HttpContext.Current.Request;

                var postedFile = rsk.Files["challanFile"];
                long deliveryMasterId = Convert.ToInt32(rsk.Form["delivery_master_id"]);

                if (postedFile == null && rsk.Form["purchase_order_master_id"].ToString() == "")
                {
                    return false;//no file upload
                }
                else
                {
                    if (postedFile != null)
                    {
                        string actualFileName = postedFile.FileName;
                        var arrFileName = actualFileName.Split('.');
                        var ext = arrFileName[arrFileName.Length - 1];
                        var fileName = "DCLN_" + deliveryMasterId + "." + ext;
                        var deliveryMaster = _entities.delivery_master.Find(deliveryMasterId);
                        var channelId = deliveryMaster.party_id;
                        //var clientDir = HttpContext.Current.Server.MapPath("~/App_Data/Delivery_Challans/");
                        var clientDir = HttpContext.Current.Server.MapPath("~/App_Data/Delivery_Challans/" + "Channel_" + channelId);
                        var fileNameDb = "Channel_" + channelId + "/" + fileName;
                        bool exists = System.IO.Directory.Exists(clientDir);
                        if (!exists)
                            System.IO.Directory.CreateDirectory(clientDir);

                        var fileSavePath = Path.Combine(clientDir, fileName);


                        if (!string.IsNullOrEmpty(deliveryMaster.challan_copy))
                        {
                            var oldAttachment = deliveryMaster.challan_copy;
                            var fileDeletePath = Path.Combine(clientDir, oldAttachment);
                            if (File.Exists(fileDeletePath))
                            {
                                File.Delete(fileDeletePath);
                            }
                        }
                        postedFile.SaveAs(fileSavePath);
                        deliveryMaster.challan_copy = fileNameDb;
                        deliveryMaster.receive_date = DateTime.Now;
                        _entities.SaveChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool CancelDelivery(delivery_master deliveryMaster)
        {
            try
            {
                var deliveryMaster1 = _entities.delivery_master.Find(deliveryMaster.delivery_master_id);
                var deliveryDetailsList =
                    _entities.delivery_details.Where(dd => dd.delivery_master_id == deliveryMaster.delivery_master_id).ToList();
                var receiveSerialNoDetails =
                    _entities.receive_serial_no_details.Where(r => r.deliver_master_id == deliveryMaster.delivery_master_id).ToList();
                long counter = 0;

                // Delivery Master
                deliveryMaster1.status = "Cancelled";
                deliveryMaster1.is_active = false;
                deliveryMaster1.is_deleted = true;
                deliveryMaster1.updated_by = deliveryMaster.updated_by;
                deliveryMaster1.updated_date = DateTime.Now;
                _entities.SaveChanges();

                // receive_serialno_details
                foreach (var serialItem in receiveSerialNoDetails)
                {
                    var receiveSerial = _entities.receive_serial_no_details.Find(serialItem.receive_serial_no_details_id);
                    if (receiveSerial != null)
                    {
                        receiveSerial.current_warehouse_id = deliveryMaster1.from_warehouse_id;
                        receiveSerial.party_id = null;
                        receiveSerial.deliver_date = null;
                        receiveSerial.requisition_id = 0;
                        receiveSerial.deliver_master_id = 0;
                        receiveSerial.is_gift = false;
                        receiveSerial.is_live_dummy = false;
                        _entities.SaveChanges();
                    }

                }

                var deliveryTotalQty = 0;
                var requisitionTotalQty = 0;
                // Delivery Details
                foreach (var deliveryDetails in deliveryDetailsList)
                {
                    var productId = deliveryDetails.product_id ?? 0;
                    var colorId = deliveryDetails.color_id ?? 0;
                    var versionId = deliveryDetails.product_version_id ?? 0;
                    var unitId = deliveryDetails.unit_id ?? 0;
                    var isGift = deliveryDetails.is_gift ?? false;
                    var deliveredQty = deliveryDetails.delivered_quantity ?? 0;

                    // update inventory 
                    var inventoryObject = new InventoryRepository();
                    inventoryObject.UpdateInventory("CANCELLED", deliveryMaster1.delivery_no, 39, deliveryMaster1.from_warehouse_id ?? 0, productId, colorId, versionId, unitId,
                                deliveredQty, deliveryMaster.delivered_by ?? 0);

                    // update requisition details
                    var requisitionDetails =
                        _entities.requisition_details.FirstOrDefault(rd => rd.requisition_master_id == deliveryMaster1.requisition_master_id &&
                            rd.product_id == productId && rd.color_id == colorId && rd.product_version_id == versionId && rd.is_gift == isGift);
                    if (requisitionDetails != null)
                    {
                        requisitionDetails.delivered_quantity -= deliveredQty;
                        requisitionTotalQty += requisitionDetails.quantity ?? 0;
                        deliveryTotalQty += requisitionDetails.delivered_quantity ?? 0;
                        _entities.SaveChanges();
                    }
                }

                // requisition_master //RequisitionMaster.delivery_status = "Not Delivered";
                var requisitionMaster = _entities.requisition_master.FirstOrDefault(r => r.requisition_master_id == deliveryMaster1.requisition_master_id);
                var deliveryStatus = "";
                if (deliveryTotalQty < 1)
                {
                    deliveryStatus = "Not Delivered";
                }
                else if (deliveryTotalQty != -0 && deliveryTotalQty >= requisitionTotalQty)
                {
                    deliveryStatus = "Delivered";
                }
                else
                {
                    deliveryStatus = "Partially Delivered";
                }
                requisitionMaster.delivery_status = deliveryStatus;
                _entities.SaveChanges();


                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        //Update Courier Information
        public bool UpdateDeliveryMethod(delivery_master objUpdateCourier)
        {
            try
            {
                var courierInfo = _entities.delivery_master.Find(objUpdateCourier.delivery_master_id);
                courierInfo.delivery_method_id = objUpdateCourier.delivery_method_id;
                courierInfo.courier_id = objUpdateCourier.courier_id;
                courierInfo.delivery_person_name = objUpdateCourier.delivery_person_name;
                courierInfo.delivery_date = DateTime.Now;
                courierInfo.status = "Delivered";
                _entities.SaveChanges();

                //retrieve data from delivery_details
                var deliveryDetailsList = _entities.delivery_details.Where(d => d.delivery_master_id == objUpdateCourier.delivery_master_id).ToList();
                foreach (var d in deliveryDetailsList)
                {

                    var productId = d.product_id ?? 0;
                    var colorId = d.color_id ?? 0;
                    var unitId = d.unit_id ?? 0;
                    var productVersionId = d.product_version_id ?? 0;
                    var deliveryQty = d.delivered_quantity ?? 0;
                    var toWarehouse = courierInfo.to_warehouse_id ?? 0;
                    var userId = objUpdateCourier.updated_by ?? 0;
                    var updateInventoty = new InventoryRepository();
                    updateInventoty.UpdateInventory("DELIVERED", courierInfo.delivery_no, 39, toWarehouse, productId, colorId, productVersionId, unitId,
                        deliveryQty, userId);
                }

                var receiveSerialNoDetails=_entities.receive_serial_no_details.Where(
                    d => d.deliver_master_id == objUpdateCourier.delivery_master_id).ToList();

                foreach (var item in receiveSerialNoDetails)
                {
                    receive_serial_no_details receiveSerial = _entities.receive_serial_no_details.FirstOrDefault(r => r.imei_no == item.imei_no || r.imei_no2 == item.imei_no);
                    receiveSerial.current_warehouse_id = courierInfo.to_warehouse_id;
                    _entities.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}