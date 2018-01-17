using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMSApi.Models.crystal_models;
using DMSApi.Models.IRepository;
using DMSApi.Models.StronglyType;
using OnlineInvoiceModel = DMSApi.Models.crystal_models.OnlineInvoiceModel;


namespace DMSApi.Models.Repository
{
    public class OnlineRequisitionRepository : IOnlineRequisitionRepository
    {
        private DMSEntities _entities;

        public OnlineRequisitionRepository()
        {
            _entities = new DMSEntities();
        }

        public object GetImeiForOnlineRequisitionDelivery(long imei1, int warehouseId)
        {
            try
            {
                var imei =
                    _entities.receive_serial_no_details.SingleOrDefault(
                        a => (a.imei_no == imei1 || a.imei_no2 == imei1) && a.current_warehouse_id == warehouseId && a.deliver_master_id == 0);
                return imei;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public long AddOnlineREquisition(StronglyType.OnlineRequisitionModel onlineRequisitionModel)
        {
            try
            {
                long deliveryMasterId = 0;
                int save = 0;
                var requisitionModel = onlineRequisitionModel.RequisitionMaster;
                var requitionDetails = onlineRequisitionModel.RequisitionDetailses;
                var serialNumber = onlineRequisitionModel.ReceiveSerialNoDetails;
                var deliveryMaster = new delivery_master();
                var deliveryDetails = new delivery_details();


                var partyTypePrefix = (from ptype in _entities.party_type
                                       join par in _entities.parties on ptype.party_type_id equals par.party_type_id
                                       where par.party_id == requisitionModel.party_id
                                       select new
                                       {
                                           party_prefix = ptype.party_prefix

                                       }).FirstOrDefault();

                int RequisitionSerial = _entities.requisition_master.Max(rq => (int?)rq.requisition_master_id) ?? 0;
                RequisitionSerial++;

                var rqStr = RequisitionSerial.ToString().PadLeft(7, '0');
                string requisitionNo = "REQ-" + partyTypePrefix.party_prefix + "-" + rqStr;

                requisitionModel.requisition_code = requisitionNo;
                requisitionModel.requisition_date = onlineRequisitionModel.RequisitionMaster.requisition_date;
                requisitionModel.expected_receiving_date = DateTime.Now;
                requisitionModel.warehouse_from = onlineRequisitionModel.RequisitionMaster.warehouse_from;
                requisitionModel.remarks = onlineRequisitionModel.RequisitionMaster.remarks;
                requisitionModel.amount = onlineRequisitionModel.RequisitionMaster.amount;
                requisitionModel.party_type_id = onlineRequisitionModel.RequisitionMaster.party_type_id;
                requisitionModel.party_id = onlineRequisitionModel.RequisitionMaster.party_id;
                requisitionModel.created_by = onlineRequisitionModel.RequisitionMaster.created_by;
                requisitionModel.created_date = DateTime.Now;
                requisitionModel.updated_by = onlineRequisitionModel.RequisitionMaster.created_by;
                requisitionModel.updated_date = DateTime.Now;
                requisitionModel.is_invoice_created = false;
                requisitionModel.status = "Approved";
                requisitionModel.delivery_status = "Delivered";
                requisitionModel.edit_count = 0;
                requisitionModel.region_id = 0;
                requisitionModel.area_id = 0;
                requisitionModel.credit_limit = 0;
                requisitionModel.credit_term = onlineRequisitionModel.RequisitionMaster.credit_term;
                requisitionModel.contact_person_mobile = onlineRequisitionModel.RequisitionMaster.contact_person_mobile;
                requisitionModel.address = onlineRequisitionModel.RequisitionMaster.address;

                requisitionModel.is_demo = "";

                requisitionModel.discount_percentage = 0;
                requisitionModel.discount_amount = 0;

                requisitionModel.incentive_percentage = 0;
                requisitionModel.incentive_amount = 0;
                requisitionModel.finance_status = "Not Approved";
                requisitionModel.is_deleted = false;
                requisitionModel.territory_id = 0;
                requisitionModel.price_type = onlineRequisitionModel.RequisitionMaster.price_type;
                requisitionModel.requisition_type = onlineRequisitionModel.RequisitionMaster.requisition_type;
                requisitionModel.reference_no = onlineRequisitionModel.RequisitionMaster.reference_no;
                requisitionModel.is_requisition_closed = false;
                requisitionModel.return_quantity = 0;
                requisitionModel.invoicable_quantity = 0;


                _entities.requisition_master.Add(requisitionModel);
                _entities.SaveChanges();

                Int64 RequisitionMasterId = requisitionModel.requisition_master_id;

                //save requisition details

                foreach (var item in requitionDetails)
                {
                    var ooo = _entities.products.SingleOrDefault(a => a.product_id == item.product_id);
                    var requisitionDetails = new requisition_details();

                    requisitionDetails.requisition_master_id = RequisitionMasterId;
                    requisitionDetails.product_id = item.product_id;
                    requisitionDetails.color_id = item.color_id;
                    requisitionDetails.brand_id = item.brand_id;
                    requisitionDetails.product_category_id = ooo.product_category_id;
                    requisitionDetails.unit_id = ooo.unit_id;
                    requisitionDetails.price = item.price;
                    requisitionDetails.delivered_quantity = item.quantity;                    
                    requisitionDetails.quantity = item.quantity;
                    requisitionDetails.line_total = item.line_total;
                    requisitionDetails.product_version_id = item.product_version_id;
                    requisitionDetails.discount_amount = item.discount_amount;
                    requisitionDetails.discount = item.discount;
                    requisitionDetails.price_amount = item.price_amount;
                    requisitionDetails.return_quantity = 0;
                    requisitionDetails.invoice_quantity = 0;
                    if (item.is_gift == true)
                    {
                        requisitionDetails.is_gift = true;
                        requisitionDetails.gift_type = "Promotional Gift";
                    }
                    if (item.promotion_master_id != null)
                    {
                        requisitionDetails.promotion_master_id = item.promotion_master_id;
                    }

                    _entities.requisition_details.Add(requisitionDetails);
                    save = _entities.SaveChanges();
                }

                if (save > 0)
                {

                    var ToWarehouse =
                        _entities.warehouses.SingleOrDefault(
                            a => a.party_id == onlineRequisitionModel.RequisitionMaster.party_id);

                    long deliverySerial = _entities.delivery_master.Max(po => (long?)po.delivery_master_id) ?? 0;
                    deliverySerial++;

                    var deliveryStr = deliverySerial.ToString().PadLeft(7, '0');

                    string deliveryNo = "DN-" + partyTypePrefix.party_prefix + "-" + deliveryStr;
                    deliveryMaster.delivery_no = deliveryNo;
                    deliveryMaster.delivery_date = DateTime.Now;
                    deliveryMaster.party_id = onlineRequisitionModel.RequisitionMaster.party_id;
                    deliveryMaster.requisition_master_id = RequisitionMasterId;
                    deliveryMaster.courier_id = 0;
                    deliveryMaster.courier_slip_no = "";
                    deliveryMaster.delivery_address = onlineRequisitionModel.RequisitionMaster.address;
                    deliveryMaster.delivered_by = onlineRequisitionModel.RequisitionMaster.created_by;
                    deliveryMaster.from_warehouse_id = onlineRequisitionModel.RequisitionMaster.warehouse_from;
                    deliveryMaster.to_warehouse_id = ToWarehouse.warehouse_id;
                    deliveryMaster.status = "Deliverd";
                    deliveryMaster.remarks = "";
                    deliveryMaster.total_amount = onlineRequisitionModel.RequisitionMaster.amount;
                    deliveryMaster.lot_no = "";
                    deliveryMaster.vehicle_no = "";
                    deliveryMaster.truck_driver_name = "";
                    deliveryMaster.truck_driver_mobile = "";

                    _entities.delivery_master.Add(deliveryMaster);
                    _entities.SaveChanges();
                    deliveryMasterId = deliveryMaster.delivery_master_id;

                    //delivery details 

                    foreach (var item in requitionDetails)
                    {
                        var ooo = _entities.products.SingleOrDefault(a => a.product_id == item.product_id);
                        var deliveryDetails1 = new delivery_details();

                        deliveryDetails1.delivery_master_id = deliveryMasterId;
                        deliveryDetails1.product_id = item.product_id;
                        deliveryDetails1.color_id = item.color_id;
                        deliveryDetails1.unit_id = ooo.unit_id;
                        deliveryDetails1.unit_price = item.price;
                        deliveryDetails1.delivered_quantity = item.quantity;
                        if (item.is_gift == true)
                        {
                            deliveryDetails1.is_gift = true;
                            deliveryDetails1.gift_type = "Promotion";
                        }
                        else
                        {
                            deliveryDetails1.is_gift = false;
                            deliveryDetails1.gift_type = "";
                        }
                        
                        deliveryDetails1.requisition_quantity = item.quantity;
                        deliveryDetails1.line_total = item.line_total;
                        deliveryDetails1.product_version_id = item.product_version_id;
                        deliveryDetails1.party_id = onlineRequisitionModel.RequisitionMaster.party_id;
                        
                        deliveryDetails1.is_live_dummy = false;

                        _entities.delivery_details.Add(deliveryDetails1);
                        int saved = _entities.SaveChanges();

                        if (saved > 0)
                        {
                            // update inventory
                            InventoryRepository updateInventoty = new InventoryRepository();

                            var masterDelivery = _entities.delivery_master.Find(deliveryMasterId);

                            //'39' virtual in-transit warehouse-----------------------------
                            updateInventoty.UpdateInventory("DELIVERY", masterDelivery.delivery_no, masterDelivery.from_warehouse_id ?? 0, masterDelivery.to_warehouse_id ?? 0, item.product_id ?? 0, item.color_id ?? 0, item.product_version_id ?? 0, ooo.unit_id ?? 0,
                                item.quantity ?? 0, onlineRequisitionModel.RequisitionMaster.created_by ?? 0);
                        }


                    }

                    foreach (var item in serialNumber)
                    {
                        receive_serial_no_details receiveSerial = _entities.receive_serial_no_details.FirstOrDefault(r => r.imei_no == item.imei_no || r.imei_no2 == item.imei_no);
                        receiveSerial.current_warehouse_id = ToWarehouse.warehouse_id;
                        receiveSerial.party_id = onlineRequisitionModel.RequisitionMaster.party_id;
                        receiveSerial.deliver_date = DateTime.Now;
                        receiveSerial.requisition_id = RequisitionMasterId;
                        receiveSerial.deliver_master_id = deliveryMasterId;
                        receiveSerial.is_gift = item.is_gift;
                        receiveSerial.is_live_dummy = false;
                        _entities.SaveChanges();

                    }

                }

                return deliveryMasterId;

            }
            catch (Exception)
            {

                throw;
            }
        }


        public object GetOnlineDeliveryChallanReport(long deliveryMasterId)
        {
            try
            {
                try
                {
                    //string query = "select distinct gm.delivery_no,gm.delivery_date,usr.full_name,pom.requisition_code,wh.warehouse_name as from_warehouse_name,pro.product_name, pc.product_category_name,bd.brand_name,col.color_name,(SELECT STUFF((SELECT ' ' + imei_no FROM receive_serial_no_details where gd.product_id=receive_serial_no_details.product_id and gd.color_id=receive_serial_no_details.color_id and gd.product_version_id=receive_serial_no_details.product_version_id and receive_serial_no_details.deliver_master_id = " + deliveryMasterId + " FOR XML PATH('')) ,1,1,'')AS Txt)as imei_no,(SELECT STUFF((SELECT ' ' + imei_no2 FROM receive_serial_no_details where gd.product_id=receive_serial_no_details.product_id and gd.color_id=receive_serial_no_details.color_id and gd.product_version_id=receive_serial_no_details.product_version_id and receive_serial_no_details.deliver_master_id = " + deliveryMasterId + " FOR XML PATH('')) ,1,1,'') AS Txt)as imei_no2,gd.delivered_quantity,gm.remarks,twh.warehouse_name as to_warehouse_name,par.party_name FROM delivery_master gm inner join delivery_details gd on gm.delivery_master_id =gd.delivery_master_id inner join receive_serial_no_details rsnd on gd.product_id =rsnd.product_id and gd.color_id =rsnd.color_id and gd.delivery_master_id =rsnd.deliver_master_id inner join product pro on gd.product_id = pro.product_id inner join color col on gd.color_id= col.color_id inner join warehouse wh on gm.from_warehouse_id = wh.warehouse_id inner join warehouse twh on gm.to_warehouse_id = twh.warehouse_id inner join requisition_master pom on gm.requisition_master_id= pom.requisition_master_id inner join brand bd on pro.brand_id= bd.brand_id inner join product_category pc on pro.product_category_id= pc.product_category_id left join users usr on gm.created_by = usr.user_id inner join party par on gm.party_id = par.party_id where gm.delivery_master_id=" + deliveryMasterId + " group by gm.delivery_no,gm.delivery_date,usr.full_name,pom.requisition_code,wh.warehouse_name,pro.product_name,col.color_name,gm.remarks,bd.brand_name, pc.product_category_name,gd.product_id,gd.color_id,gd.product_version_id,twh.warehouse_name,gd.delivered_quantity,par.party_name";
                    string query = "select distinct gm.delivery_no,gm.delivery_date,usr.full_name,pom.requisition_code,wh.warehouse_name as from_warehouse_name,pro.product_name, pc.product_category_name,bd.brand_name,col.color_name ,gd.delivered_quantity,gm.remarks,twh.warehouse_name as to_warehouse_name,par.party_name,gd.line_total,ver.product_version_name FROM delivery_master gm inner join delivery_details gd on gm.delivery_master_id =gd.delivery_master_id inner join product pro on gd.product_id = pro.product_id left join color col on gd.color_id= col.color_id left join product_version ver on gd.product_version_id= ver.product_version_id inner join warehouse wh on gm.from_warehouse_id = wh.warehouse_id inner join warehouse twh on gm.to_warehouse_id = twh.warehouse_id inner join requisition_master pom on gm.requisition_master_id= pom.requisition_master_id inner join brand bd on pro.brand_id= bd.brand_id inner join product_category pc on pro.product_category_id= pc.product_category_id left join users usr on gm.created_by = usr.user_id inner join party par on gm.party_id = par.party_id where gm.delivery_master_id=" + deliveryMasterId + " and pom.requisition_type='Online Requisition' group by gm.delivery_no,gm.delivery_date,usr.full_name,pom.requisition_code ,wh.warehouse_name,pro.product_name,col.color_name,gm.remarks,bd.brand_name, pc.product_category_name,gd.product_id,gd.color_id,gd.product_version_id,twh.warehouse_name,gd.delivered_quantity, par.party_name,gd.line_total,ver.product_version_name";
                    var poData = _entities.Database.SqlQuery<OnlineDeliveryChallanModel>(query).ToList();
                    return poData;
                }
                catch (Exception ex)
                {
                    return ex;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        public object GetOnlineRequisitionAndDeliveryList()
        {
            try
            {
                var list = (from e in _entities.delivery_master

                            join r in _entities.requisition_master on e.requisition_master_id equals r.requisition_master_id
                            join p in _entities.parties on e.party_id equals p.party_id
                            where r.requisition_type == "Online Requisition"
                            select new
                            {
                                delivery_master_id = e.delivery_master_id,
                                delivery_no = e.delivery_no,
                                delivery_date = e.delivery_date,
                                reference_no = r.reference_no,
                                requisition_code = r.requisition_code,
                                party_name = p.party_name,
                                quantity = _entities.delivery_details.Where(a => a.delivery_master_id == e.delivery_master_id).Sum(a => a.delivered_quantity)
                            }).ToList();
                return list.OrderByDescending(a => a.delivery_master_id);
            }
            catch (Exception)
            {

                return null;
            }
        }


        public object GetPartyForPaymentCollect()
        {
            try
            {
                var list = _entities.parties.Where(a => a.party_type_id == 9).ToList();
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public object GetPaymentCollectGridForInvoice(int partyId, DateTime reqFrom, DateTime reqTo)
        {
            try
            {
                var list = (from e in _entities.requisition_master
                            join r in _entities.delivery_master on e.requisition_master_id equals r.requisition_master_id
                            join pro in _entities.parties on e.party_id equals pro.party_id
                            where e.party_id == partyId && (e.requisition_date >= reqFrom && e.requisition_date <= reqTo) && e.is_requisition_closed == false
                            select new
                            {
                                requisition_master_id = e.requisition_master_id,
                                requisition_code = e.requisition_code,
                                requisition_date = e.requisition_date,
                                quantity = _entities.requisition_details.Where(a => a.requisition_master_id == e.requisition_master_id).Sum(a => a.quantity) - (_entities.requisition_details.Where(a => a.requisition_master_id == e.requisition_master_id).Sum(a => a.invoice_quantity) + _entities.requisition_details.Where(a => a.requisition_master_id == e.requisition_master_id).Sum(a => a.return_quantity)),
                                invoicable_qauntity = e.invoicable_quantity,
                                delivery_no = r.delivery_no,
                                line_total = e.amount
                            }).ToList();
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public object GetImeiForOnlineRequisitionPaymentCollect(long imei, int partyId)
        {
            try
            {
                var warehouse = _entities.warehouses.SingleOrDefault(a => a.party_id == partyId);
                var imeiObject = (from e in _entities.receive_serial_no_details
                                  join req in _entities.requisition_master on e.requisition_id equals req.requisition_master_id
                                  join pro in _entities.products on e.product_id equals pro.product_id
                                  join col in _entities.colors on e.color_id equals col.color_id
                                  join ver in _entities.product_version on e.product_version_id equals ver.product_version_id
                                  where
                                      (e.imei_no == imei || e.imei_no2 == imei) && e.party_id == partyId &&
                                      req.is_requisition_closed == false && e.current_warehouse_id == warehouse.warehouse_id
                                  select new
                                  {
                                      e.product_id,
                                      e.color_id,
                                      e.requisition_id,
                                      e.imei_no,
                                      e.imei_no2,
                                      e.product_version_id,
                                      e.receive_serial_no_details_id,
                                      req.requisition_master_id,
                                      pro.product_name,
                                      col.color_name,
                                      ver.product_version_name

                                  }).SingleOrDefault();
                return imeiObject;
            }
            catch (Exception)
            {

                throw;
            }
        }




        public object GetProductForPaymentAndInvoiceGenerate(List<requisition_details> requisitionDetailses)
        {
            try
            {
                var listOfProduct = new List<OnlinePaymentProductModel>();
                foreach (var requisitionDetailse in requisitionDetailses)
                {
                    var list = (from e in _entities.requisition_details
                                join pro in _entities.products on e.product_id equals pro.product_id
                                join col in _entities.colors on e.color_id equals col.color_id into tempCol
                                from col in tempCol.DefaultIfEmpty()
                                join ver in _entities.product_version on e.product_version_id equals ver.product_version_id into tempVer
                                from ver in tempVer.DefaultIfEmpty()
                                where (e.requisition_master_id == requisitionDetailse.requisition_master_id && e.quantity != e.invoice_quantity)
                                select new OnlinePaymentProductModel()
                                {
                                    product_id = pro.product_id,
                                    product_name = pro.product_name,
                                    color_id = col.color_id,
                                    color_name = col.color_name,
                                    product_version_id = ver.product_version_id,
                                    version_name = ver.product_version_name,
                                    requisition_details_id = e.requisition_details_id,
                                    requisition_master_id = requisitionDetailse.requisition_master_id,
                                    requisition_quantity = e.quantity,
                                    invoiceable_quantity = e.quantity - (e.invoice_quantity + e.return_quantity),
                                    invoice_quantity = 0,
                                    price = e.price,
                                    line_total = e.line_total,
                                    discount_price = e.discount_amount,
                                    price_amount = e.price_amount,
                                    promotion_master_id = e.promotion_master_id,
                                    is_gift = e.is_gift,
                                    gift_type = e.gift_type
                                }).ToList();

                    foreach (var onlinePaymentProductModel in list)
                    {
                        var o = new OnlinePaymentProductModel();
                        o.product_id = onlinePaymentProductModel.product_id;
                        o.product_name = onlinePaymentProductModel.product_name;
                        o.color_id = onlinePaymentProductModel.color_id;
                        o.color_name = onlinePaymentProductModel.color_name;
                        o.product_version_id = onlinePaymentProductModel.product_version_id;
                        o.version_name = onlinePaymentProductModel.version_name;
                        o.requisition_details_id = onlinePaymentProductModel.requisition_details_id;
                        o.requisition_master_id = onlinePaymentProductModel.requisition_master_id;
                        o.requisition_quantity = onlinePaymentProductModel.requisition_quantity;
                        o.invoiceable_quantity = onlinePaymentProductModel.invoiceable_quantity;
                        o.invoice_quantity = 0;
                        o.price = onlinePaymentProductModel.price;
                        o.line_total = onlinePaymentProductModel.line_total;
                        o.discount_price = onlinePaymentProductModel.discount_price;
                        o.price_amount = onlinePaymentProductModel.price_amount;
                        o.promotion_master_id = onlinePaymentProductModel.promotion_master_id;
                        o.is_gift = onlinePaymentProductModel.is_gift;
                        o.gift_type = onlinePaymentProductModel.gift_type;

                        listOfProduct.Add(o);
                    }

                }
                return listOfProduct;
            }
            catch (Exception ex)
            {
                throw;
            }
        }



        public object GetOnlineInvoiceReport(long online_invoice_master_id)
        {

            try
            {
                //string query = "select distinct oim.online_invoice_no ,oim.online_invoice_date ,users.full_name ,party.party_name ,party.party_code ,party_type.party_type_name ,product.product_name ,product_category.product_category_name ,color.color_name ,unit.unit_name ,brand.brand_name ,oim.item_total ,oid.price ,oid.line_total ,oid.discount ,oid.discount_amount ,oid.price_amount ,requisition_master.requisition_code ,oirm.online_invoice_master_id from online_invoice_master oim inner join online_invoice_details oid on oim.online_invoice_master_id=oid.online_invoice_master_id inner join party on oim.party_id=party.party_id inner join party_type on party.party_type_id=party_type.party_type_id inner join product on oid.product_id=product.product_id inner join product_category on product.product_category_id=product_category.product_category_id left join product_version on oid.product_version_id=product_version.product_version_id left join color on oid.color_id=color.color_id left join unit on oid.unit_id=unit.unit_id left join brand on oid.brand_id=brand.brand_id left join users on oim.created_by=users.user_id inner join online_invoice_requisition_mapping oirm on oim.online_invoice_master_id=oirm.online_invoice_master_id inner join requisition_master on oirm.requisition_master_id=requisition_master.requisition_master_id where oim.online_invoice_master_id='" + online_invoice_master_id + "' group by requisition_master.requisition_code, oim.online_invoice_no,oim.online_invoice_date,users.full_name,party.party_name,party.party_code,party_type.party_type_name ,product.product_name,product_category.product_category_name,color.color_name ,unit.unit_name,brand.brand_name,oim.item_total,oid.price,oid.line_total,oid.discount ,oid.discount_amount,oid.price_amount,oirm.online_invoice_master_id";
                string query =
                    "select distinct oim.online_invoice_no ,oim.online_invoice_date ,users.full_name ,party.party_name ,party.party_code ,party_type.party_type_name ,product.product_name ,product_category.product_category_name ,color.color_name ,unit.unit_name ,brand.brand_name ,oid.quantity as item_total ,oid.price ,oid.line_total ,oid.discount ,oid.discount_amount ,oid.price_amount from online_invoice_master oim inner join online_invoice_details oid on oim.online_invoice_master_id=oid.online_invoice_master_id inner join party on oim.party_id=party.party_id inner join party_type on party.party_type_id=party_type.party_type_id inner join product on oid.product_id=product.product_id inner join product_category on product.product_category_id=product_category.product_category_id left join product_version on oid.product_version_id=product_version.product_version_id left join color on oid.color_id=color.color_id left join unit on oid.unit_id=unit.unit_id left join brand on oid.brand_id=brand.brand_id left join users on oim.created_by=users.user_id where oim.online_invoice_master_id=" + online_invoice_master_id;

                var invoieData = _entities.Database.SqlQuery<OnlineInvoiceModel>(query).ToList();
                return invoieData;
            }
            catch (Exception ex)
            {
                return ex;
            }

        }



        public object GetAllProductWithoutAss()
        {
            //12121212
            try
            {
                var products = (from prod in _entities.products
                                join uni in _entities.units on prod.unit_id equals uni.unit_id
                                join bra in _entities.brands on prod.brand_id equals bra.brand_id
                                join pc in _entities.product_category on prod.product_category_id equals pc.product_category_id
                                where prod.product_category_id != 3
                                select new
                                {

                                    product_id = prod.product_id,
                                    product_name = prod.product_name,
                                    product_code = prod.product_code,
                                    unit_id = prod.unit_id,
                                    unit_name = uni.unit_name,
                                    brand_id = prod.brand_id,
                                    brand_name = bra.brand_name,
                                    product_category_id = prod.product_category_id,
                                    product_category_name = pc.product_category_name,
                                    current_balance = prod.current_balance,

                                    // batch_number = prod.batch_number,

                                    product_image_url = prod.product_image_url,
                                    has_serial = prod.has_serial,
                                    has_warrenty = prod.has_warrenty,
                                    warrenty_type = prod.warrenty_type,
                                    warrenty_value = prod.warrenty_value,
                                    vat_percentage = prod.vat_percentage,
                                    tax_percentage = prod.tax_percentage,
                                    rp_price = prod.rp_price,
                                    md_price = prod.md_price,
                                    mrp_price = prod.mrp_price,
                                    bs_price = prod.bs_price,

                                }).OrderByDescending(e => e.product_id).ToList();

                return products;
            }
            catch (Exception)
            {

                throw;
            }
        }



        public object GetReturnRequisitionList(int partyId, DateTime reqFrom, DateTime reqTo)
        {
            try
            {
                var list = (from e in _entities.requisition_master
                            join r in _entities.delivery_master on e.requisition_master_id equals r.requisition_master_id
                            join pro in _entities.parties on e.party_id equals pro.party_id
                            where e.party_id == partyId && (e.requisition_date >= reqFrom && e.requisition_date <= reqTo) && e.is_requisition_closed == false
                            select new
                            {
                                requisition_master_id = e.requisition_master_id,
                                requisition_code = e.requisition_code,
                                requisition_date = e.requisition_date,
                                customer_po = e.reference_no,
                                quantity = _entities.requisition_details.Where(a => a.requisition_master_id == e.requisition_master_id).Sum(a => a.quantity) - (_entities.requisition_details.Where(a => a.requisition_master_id == e.requisition_master_id).Sum(a => a.invoice_quantity) + _entities.requisition_details.Where(a => a.requisition_master_id == e.requisition_master_id).Sum(a => a.return_quantity)),
                                return_quntity = 0,
                                delivery_no = r.delivery_no

                            }).ToList();
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //function
        

    }
}