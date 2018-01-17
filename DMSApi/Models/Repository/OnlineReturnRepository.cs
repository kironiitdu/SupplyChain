using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMSApi.Models.crystal_models;
using DMSApi.Models.IRepository;
using DMSApi.Models.StronglyType;

namespace DMSApi.Models.Repository
{
    public class OnlineReturnRepository : IOnlineReturnRepository
    {
        private DMSEntities _entities;

        public OnlineReturnRepository()
        {
            _entities = new DMSEntities();
        }

        public bool AddOnlineReturn(StronglyType.OnlineReturnModel onlineReturnModel)
        {
            return false;
            //try
            //{
            //    bool saveReturn = false;

            //    var onlineReturnMaster = onlineReturnModel.OnlineReturnMaster;
            //    var onlineReturnDetails = onlineReturnModel.OnlineReturnDetailses;
            //    var receiveSerialNoDetails = onlineReturnModel.ReceiveSerialNoDetails;

            //    var partyTypePrefix = (from ptype in _entities.party_type
            //                           join par in _entities.parties on ptype.party_type_id equals par.party_type_id
            //                           where par.party_id == onlineReturnMaster.party_id
            //                           select new
            //                           {
            //                               party_prefix = ptype.party_prefix

            //                           }).FirstOrDefault();

            //    int ReturnSerial = _entities.online_return_master.Max(rq => (int?)rq.online_return_master_id) ?? 0;
            //    ReturnSerial++;

            //    var rqStr = ReturnSerial.ToString().PadLeft(7, '0');
            //    string requisitionNo = "RET-" + partyTypePrefix.party_prefix + "-" + rqStr;

            //    onlineReturnMaster.return_date = DateTime.Now;
            //    onlineReturnMaster.return_code = requisitionNo;
            //    onlineReturnMaster.created_by = onlineReturnModel.OnlineReturnMaster.created_by;
            //    onlineReturnMaster.created_date = DateTime.Now;
            //    onlineReturnMaster.party_id = onlineReturnModel.OnlineReturnMaster.party_id;
            //    onlineReturnMaster.remarks = onlineReturnModel.OnlineReturnMaster.remarks;
            //    onlineReturnMaster.returned_by = onlineReturnModel.OnlineReturnMaster.party_id;
            //    onlineReturnMaster.returned_quantity = onlineReturnModel.OnlineReturnDetailses.Count;
            //    onlineReturnMaster.updated_by = onlineReturnModel.OnlineReturnMaster.created_by;
            //    onlineReturnMaster.updated_date = DateTime.Now;

            //    _entities.online_return_master.Add(onlineReturnMaster);
            //    int save = _entities.SaveChanges();

            //    Int64 onlineReturnMasterId = onlineReturnMaster.online_return_master_id;

            //    if (save > 0)
            //    {
            //        foreach (var onlineReturnDetailse in onlineReturnDetails)
            //        {

                         
            //            var reqDetails =
            //                _entities.requisition_details.FirstOrDefault(a => a.requisition_master_id == onlineReturnDetailse.invoice_master_id && a.product_id == onlineReturnDetailse.product_id && a.color_id == onlineReturnDetailse.color_id && a.product_version_id == onlineReturnDetailse.product_version_id);
            //            var serial =
            //                _entities.receive_serial_no_details.SingleOrDefault(
            //                    a =>
            //                        a.imei_no == onlineReturnDetailse.imei_no ||
            //                        a.imei_no2 == onlineReturnDetailse.imei_no);
            //            var delivery =
            //                _entities.delivery_master.SingleOrDefault(
            //                    a => a.delivery_master_id == serial.deliver_master_id);
            //            var returnDetails = new online_return_details()
            //            {
            //                imei_no = serial.imei_no,
            //                imei_no2 = serial.imei_no2,
            //                brand_id = reqDetails.brand_id,
            //                color_id = reqDetails.color_id,
            //                product_version_id = reqDetails.product_version_id,
            //                product_id = reqDetails.product_id,
            //                price = reqDetails.price,
            //                unit_id = reqDetails.unit_id,
            //                invoice_master_id = reqDetails.requisition_master_id,
            //                online_return_master_id = onlineReturnMasterId,
            //                returned_qty = 1,
            //                verify_status = false,
            //                warehouse_id = delivery.from_warehouse_id,

            //            };
            //            _entities.online_return_details.Add(returnDetails);
            //            int sss =_entities.SaveChanges();

                         

            //            if (sss > 0)
            //            {
            //                //requisition details update

            //                reqDetails.return_quantity += 1;

            //                _entities.SaveChanges();

            //                //End

            //                //IMEI return

            //                serial.current_warehouse_id = delivery.from_warehouse_id;
            //                serial.party_id = 0;
            //                serial.requisition_id = 0;
            //                serial.deliver_master_id = 0;
            //                serial.is_return = true;

            //                _entities.SaveChanges();

            //            }

            //        }

            //        //Requisition Master Update                    
                    
            //        foreach (var onlineReturnDetailse in onlineReturnDetails)
            //        {
            //            var returnDetailseQty =
            //                _entities.requisition_details.Where(
            //                    a =>
            //                        a.requisition_master_id == onlineReturnDetailse.invoice_master_id).ToList();
            //            var requisitionMaster =
            //                _entities.requisition_master.SingleOrDefault(
            //                    a => a.requisition_master_id == onlineReturnDetailse.invoice_master_id);

            //            if (requisitionMaster.is_requisition_closed == false)
            //            {
            //                int? inQty = returnDetailseQty.Sum(a => a.invoice_quantity);
            //                int? reQty = returnDetailseQty.Sum(a => a.return_quantity);
            //                int? qty = returnDetailseQty.Sum(a => a.quantity);
            //                int? total = reQty + inQty;
            //                if (qty == total)
            //                {
            //                    requisitionMaster.is_requisition_closed = true;
            //                    requisitionMaster.return_quantity = reQty;
            //                }
            //                else
            //                {
            //                    requisitionMaster.is_requisition_closed = false;
            //                    requisitionMaster.return_quantity = reQty;
            //                }

            //                _entities.SaveChanges();                           
            //            }

            //        }

            //        //Inventory Update                                                        
            //        Int64 detailsIdReq = 0;

            //        foreach (var onlineReturnDetailse in onlineReturnDetails)
            //        {

            //            var reqDetailsForInventory =
            //                    _entities.requisition_details.SingleOrDefault(
            //                        a =>
            //                            a.requisition_master_id == onlineReturnDetailse.invoice_master_id &&
            //                            a.product_id == onlineReturnDetailse.product_id &&
            //                            a.color_id == onlineReturnDetailse.color_id &&
            //                            a.product_version_id == onlineReturnDetailse.product_version_id);

            //            if (detailsIdReq != reqDetailsForInventory.requisition_details_id)
            //            {
            //                int? qty =
            //                onlineReturnDetails.Count(a => a.product_id == reqDetailsForInventory.product_id &&
            //                                               a.color_id == reqDetailsForInventory.color_id &&
            //                                               a.color_id == reqDetailsForInventory.color_id);

            //                var delivery =
            //                    _entities.delivery_master.SingleOrDefault(
            //                        a => a.requisition_master_id == onlineReturnDetailse.invoice_master_id);

            //                InventoryRepository updateInventoty = new InventoryRepository();

            //                var onlineMater = _entities.online_return_master.Find(onlineReturnMasterId);

            //                //'39' virtual in-transit warehouse-----------------------------
            //                updateInventoty.UpdateInventory("ONLINE RETURN", onlineMater.return_code, delivery.to_warehouse_id ?? 0, delivery.from_warehouse_id ?? 0, reqDetailsForInventory.product_id ?? 0, reqDetailsForInventory.color_id ?? 0, reqDetailsForInventory.product_version_id ?? 0, reqDetailsForInventory.unit_id ?? 0,
            //                    qty ?? 0, onlineReturnModel.OnlineReturnMaster.created_by ?? 0);

            //                detailsIdReq = reqDetailsForInventory.requisition_details_id;
            //            }
                        
            //        }
            //        saveReturn = true;

            //    }

            //    return saveReturn;
            //}
            //catch (Exception)
            //{
                
            //    throw;
            //}
        }


        public object GetOnlineReturnList()
        {
            try
            {
                var list = (from e in _entities.online_return_master
                    join p in _entities.parties on e.party_id equals p.party_id
                    select new
                    {
                        e.return_code,
                        e.return_date,
                        e.online_return_master_id,
                        e.returned_quantity,
                        p.party_name
                    }).ToList();
                return list.OrderByDescending(a=>a.online_return_master_id);
            }
            catch (Exception)
            {
                
                throw;
            }
        }


        public object GetReturnChallanReport(long returnMasterId)
        {
            try
            {
                string query = "select distinct gm.return_code,gm.return_date,usr.full_name,wh.warehouse_name as from_warehouse_name,pro.product_name, pc.product_category_name,bd.brand_name,col.color_name,(SELECT STUFF((SELECT ' ' + imei_no FROM online_return_details where gd.product_id=online_return_details.product_id and gd.color_id=online_return_details.color_id and gd.product_version_id=online_return_details.product_version_id and online_return_details.online_return_master_id = "+returnMasterId+" FOR XML PATH('')) ,1,1,'')AS Txt)as imei_no,(SELECT STUFF((SELECT ' ' + imei_no2 FROM online_return_details where gd.product_id=online_return_details.product_id and gd.color_id=online_return_details.color_id and gd.product_version_id=online_return_details.product_version_id and online_return_details.online_return_master_id = "+returnMasterId+" FOR XML PATH('')) ,1,1,'') AS Txt)as imei_no2,gd.returned_qty,gm.remarks,par.party_name FROM online_return_master gm   inner join online_return_details gd on gm.online_return_master_id =gd.online_return_master_id  inner join product pro on gd.product_id = pro.product_id   inner join color col on gd.color_id= col.color_id inner join warehouse wh on gd.warehouse_id = wh.warehouse_id  inner join brand bd on pro.brand_id= bd.brand_id   inner join product_category pc on pro.product_category_id= pc.product_category_id  left join users usr on gm.created_by = usr.user_id   inner join party par on gm.party_id = par.party_id   where gm.online_return_master_id="+returnMasterId+"  group by gm.return_code,gm.return_date,usr.full_name,  wh.warehouse_name,pro.product_name,col.color_name,gm.remarks,bd.brand_name,  pc.product_category_name,gd.product_id,gd.color_id,gd.product_version_id,gd.returned_qty,par.party_name";

                var poData = _entities.Database.SqlQuery<OnlineReturnChallanModel>(query).ToList();
                return poData;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public bool CheckQuantity(long returnMasterId,long pId,long colId,long verId, int qty)
        {
            try
            {
                var kkk = _entities.requisition_details.FirstOrDefault(a => a.requisition_master_id == returnMasterId && a.product_id == pId && a.color_id == colId && a.product_version_id == verId);

                var quant = kkk.quantity - (kkk.invoice_quantity - kkk.return_quantity);

                if (qty >= quant)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
            catch (Exception)
            {
                return false;
            }
        }

        public object GetReturnRequisitionDetailsList(int partyId, DateTime reqFrom, DateTime reqTo)
        {
            try
            {
                var list = (from e in _entities.requisition_master
                            join r in _entities.requisition_details on e.requisition_master_id equals r.requisition_master_id
                            join p in _entities.products on r.product_id equals p.product_id
                            join c in _entities.colors on r.color_id equals c.color_id
                            join v in _entities.product_version on r.product_version_id equals v.product_version_id
                            join pro in _entities.parties on e.party_id equals pro.party_id
                            where e.party_id == partyId && (e.requisition_date >= reqFrom && e.requisition_date <= reqTo) && e.is_requisition_closed == false
                            select new
                            {
                                quantity = r.quantity - (r.invoice_quantity + r.return_quantity),
                                return_quantity = 0,
                                product_id = r.product_id,
                                color_id = r.color_id,
                                product_version_id = r.product_version_id,
                                product_name = p.product_name,
                                color_name = c.color_name,
                                product_version_name = v.product_version_name,
                                requisition_details_id = r.requisition_details_id,
                                requisition_master_id = r.requisition_master_id,
                                requisition_code = e.requisition_code,
                                gift_type = r.gift_type

                            }).ToList();
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}