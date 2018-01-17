using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMSApi.Models.crystal_models;
using DMSApi.Models.IRepository;
using DMSApi.Models.StronglyType;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser.clipper;
using Org.BouncyCastle.Ocsp;
using System.IO;

namespace DMSApi.Models.Repository
{
    public class EshopRequisitionRepository : IEshopRequisitionRepository
    {
        private DMSEntities _entities;
        private PartyJournalRepository partyJournalRepository;

        public EshopRequisitionRepository()
        {
            _entities = new DMSEntities();
            this.partyJournalRepository = new PartyJournalRepository();
        }
        public long AddEshopRequisition(StronglyType.EshopRequisitionModel onlineRequisitionModel)
        {
            try
            {
                long deliveryMasterId = 0;
                int save = 0;
                var requisitionModel = onlineRequisitionModel.RequisitionMaster;
                var requitionDetails = onlineRequisitionModel.RequisitionDetailses;
                var serialNumber = onlineRequisitionModel.ReceiveSerialNoDetails;

                var customerDetaiils = onlineRequisitionModel.EshopCutomerRequisition;

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
                requisitionModel.credit_term = DateTime.Now;
                requisitionModel.contact_person_mobile = onlineRequisitionModel.RequisitionMaster.contact_person_mobile;
                requisitionModel.address = "";

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
                    requisitionDetails.is_gift = item.is_gift;
                    requisitionDetails.quantity = item.quantity;
                    requisitionDetails.promotion_master_id = item.promotion_master_id;
                    requisitionDetails.line_total = item.line_total;
                    requisitionDetails.product_version_id = item.product_version_id;
                    requisitionDetails.discount_amount = item.discount_amount;
                    requisitionDetails.discount = item.discount;
                    requisitionDetails.price_amount = 0;
                    if (item.is_gift == true)
                    {
                        requisitionDetails.gift_type = "Promotional Gift";
                    }
                    requisitionDetails.return_quantity = 0;
                    requisitionDetails.invoice_quantity = item.quantity;

                    _entities.requisition_details.Add(requisitionDetails);
                    save = _entities.SaveChanges();
                }

                if (save > 0)
                {
                    //add customer details

                    customerDetaiils.billing_address = onlineRequisitionModel.EshopCutomerRequisition.billing_address;
                    customerDetaiils.customer_email = onlineRequisitionModel.EshopCutomerRequisition.customer_email;
                    customerDetaiils.customer_name = onlineRequisitionModel.EshopCutomerRequisition.customer_name;
                    customerDetaiils.delivery_date = DateTime.Now;
                    customerDetaiils.mobile_no = onlineRequisitionModel.EshopCutomerRequisition.mobile_no;
                    customerDetaiils.payment_method = onlineRequisitionModel.EshopCutomerRequisition.payment_method;
                    customerDetaiils.requisition_master_id = RequisitionMasterId;
                    customerDetaiils.shipping = onlineRequisitionModel.EshopCutomerRequisition.shipping;
                    customerDetaiils.shipping_address = onlineRequisitionModel.EshopCutomerRequisition.shipping_address;

                    _entities.eshop_cutomer_requisition_mapping.Add(customerDetaiils);
                    _entities.SaveChanges();

                    //end


                    var ToWarehouse =
                        _entities.warehouses.SingleOrDefault(
                            a => a.warehouse_name == "In Transit");

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
                    deliveryMaster.status = "In Transit";
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
                        deliveryDetails1.is_gift = item.is_gift;
                        deliveryDetails1.requisition_quantity = item.quantity;
                        deliveryDetails1.line_total = item.line_total;
                        deliveryDetails1.product_version_id = item.product_version_id;

                        deliveryDetails1.party_id = onlineRequisitionModel.RequisitionMaster.party_id;
                        if (item.is_gift == true)
                        {
                            deliveryDetails1.gift_type = "Promotional Gift";
                        }
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

                    //For Party Journal

                    //GET ACCOUNT BALANCE FROM PARTY JOURNAL
                    var partyJournal =
                        _entities.party_journal.Where(pj => pj.party_id == onlineRequisitionModel.RequisitionMaster.party_id)
                            .OrderByDescending(p => p.party_journal_id)
                            .FirstOrDefault();
                    decimal partyAccountBalance = 0;
                    if (partyJournal != null)
                    {
                        partyAccountBalance = partyJournal.closing_balance ?? 0;
                    }

                    //CALCULATING INVOICE TOTAL
                    decimal invoiceTotal = 0;
                    invoiceTotal = onlineRequisitionModel.RequisitionMaster.amount ?? 0; //insert in both invoice master and party journal table

                    //ACCOUNT BALANCE
                    decimal accountBalance = 0;
                    accountBalance = invoiceTotal + partyAccountBalance;



                    //Invoice save

                    long InvoiceSerial = _entities.invoice_master.Max(po => (long?)po.invoice_master_id) ?? 0;
                    InvoiceSerial++;
                    var invStr = InvoiceSerial.ToString().PadLeft(7, '0');
                    string invoiceNo = "INV-" + partyTypePrefix.party_prefix + "-" + invStr;

                    invoice_master insert_invoice = new invoice_master
                    {
                        invoice_no = invoiceNo,
                        invoice_date = System.DateTime.Now,
                        party_id = onlineRequisitionModel.RequisitionMaster.party_id,
                        party_type_id = 12,
                        requisition_master_id = RequisitionMasterId,
                        company_id = 0,
                        remarks = onlineRequisitionModel.RequisitionMaster.remarks,
                        created_by = onlineRequisitionModel.RequisitionMaster.created_by,
                        item_total = onlineRequisitionModel.RequisitionMaster.amount,
                        rebate_total = 0,
                        invoice_total = invoiceTotal,
                        account_balance = accountBalance,
                    };

                    _entities.invoice_master.Add(insert_invoice);
                    _entities.SaveChanges();
                    long InvoiceMasterId = insert_invoice.invoice_master_id;

                    //INVOICE DETALS
                    foreach (var item1 in requitionDetails)
                    {
                        var invoiceDetails_insert = new invoice_details();

                        invoiceDetails_insert.invoice_master_id = InvoiceMasterId;
                        invoiceDetails_insert.product_category_id = item1.product_category_id;
                        invoiceDetails_insert.product_id = item1.product_id;
                        invoiceDetails_insert.color_id = item1.color_id;
                        invoiceDetails_insert.unit_id = item1.unit_id;
                        invoiceDetails_insert.price = item1.price;
                        invoiceDetails_insert.quantity = item1.quantity;
                        invoiceDetails_insert.line_total = item1.line_total;
                        invoiceDetails_insert.is_gift = item1.is_gift;
                        if (item1.is_gift == true)
                        {
                            invoiceDetails_insert.gift_type = "Promotional Gift";
                        }
                        invoiceDetails_insert.is_live_dummy = false;



                        _entities.invoice_details.Add(invoiceDetails_insert);
                        _entities.SaveChanges();
                    }
                    //End

                    //Party Journal
                    partyJournalRepository.PartyJournalEntry("INVOICE", onlineRequisitionModel.RequisitionMaster.party_id ?? 0, invoiceTotal, "Invoice", onlineRequisitionModel.RequisitionMaster.created_by ?? 0, invoiceNo);


                    foreach (var item in serialNumber)
                    {
                        receive_serial_no_details receiveSerial = _entities.receive_serial_no_details.FirstOrDefault(r => r.imei_no == item.imei_no || r.imei_no2 == item.imei_no);
                        receiveSerial.current_warehouse_id = ToWarehouse.warehouse_id;
                        receiveSerial.party_id = 0;
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


        public object GetShippingMethod()
        {
            try
            {
                var list = _entities.eshop_shipping.ToList();
                return list;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public object GetPaymentMethod()
        {
            try
            {
                var list = _entities.eshop_payment_method.ToList();
                return list;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public object GetEshopRfdList()
        {
            try
            {
                var list = (from e in _entities.delivery_master

                            join r in _entities.requisition_master on e.requisition_master_id equals r.requisition_master_id
                            join c in _entities.eshop_cutomer_requisition_mapping on e.requisition_master_id equals c.requisition_master_id
                            where r.requisition_type == "E-Shop Requisition"
                            select new
                            {
                                delivery_master_id = e.delivery_master_id,
                                requisition_master_id = e.requisition_master_id,
                                delivery_no = e.delivery_no,
                                delivery_date = e.delivery_date,
                                reference_no = r.reference_no,
                                requisition_code = r.requisition_code,
                                mobile_no = c.mobile_no,
                                customer_name = c.customer_name,
                                status = e.status,
                                quantity = _entities.delivery_details.Where(a => a.delivery_master_id == e.delivery_master_id).Sum(a => a.delivered_quantity)
                            }).ToList();
                return list.OrderByDescending(a => a.delivery_master_id);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public object EshopDeliveryChallanReport(long delivery_master_id)
        {

            try
            {
                string query = "select distinct gd.line_total,gm.delivery_no,gm.delivery_date,usr.full_name,pom.requisition_code,wh.warehouse_name as from_warehouse_name ,pro.product_name, pc.product_category_name,bd.brand_name ,col.color_name,pv.product_version_name,SUM(gd.delivered_quantity) as delivered_quantity ,gm.remarks,twh.warehouse_name as to_warehouse_name,cus.customer_name ,cus.mobile_no as moblie_no,cus.billing_address,cus.shipping_address,cus.customer_email,pay.method_name,ship.shipping_name,pom.reference_no FROM delivery_master gm inner join delivery_details gd on gm.delivery_master_id =gd.delivery_master_id inner join product pro on gd.product_id = pro.product_id left join color col on gd.color_id= col.color_id left join product_version pv on gd.product_version_id= pv.product_version_id inner join warehouse wh on gm.from_warehouse_id = wh.warehouse_id inner join warehouse twh on gm.to_warehouse_id = twh.warehouse_id inner join requisition_master pom on gm.requisition_master_id= pom.requisition_master_id inner join brand bd on pro.brand_id= bd.brand_id inner join product_category pc on pro.product_category_id= pc.product_category_id inner join users usr on pom.created_by = usr.user_id inner join eshop_cutomer_requisition_mapping cus on gm.requisition_master_id = cus.requisition_master_id inner join eshop_payment_method pay on cus.payment_method = pay.eshop_payment_method_id inner join eshop_shipping ship on cus.shipping = ship.eshop_shipping_id where gm.delivery_master_id=" + delivery_master_id + " group by pv.product_version_name, gm.delivery_no,gm.delivery_date,usr.full_name,pom.requisition_code, wh.warehouse_name,pro.product_name,col.color_name,gm.remarks,bd.brand_name, pc.product_category_name,gd.product_id,gd.color_id, gd.product_version_id,twh.warehouse_name,gd.delivered_quantity,cus.customer_name, cus.mobile_no,cus.billing_address,cus.shipping_address,cus.customer_email ,pay.method_name,ship.shipping_name,pom.reference_no,gd.is_gift,gd.line_total";

                var poData = _entities.Database.SqlQuery<EshopDeliveryChallanModel>(query).ToList();
                return poData;
            }
            catch (Exception ex)
            {
                return ex;
            }

        }


        public object EshopInvoiceReport(long requisition_master_id)
        {
            try
            {
                //string query =
                //    "select distinct oim.invoice_no ,oim.invoice_date ,users.full_name ,cus.customer_name ,cus.mobile_no,cus.billing_address,cus.customer_email,product.product_name ,product_category.product_category_name ,color.color_name ,unit.unit_name ,brand.brand_name ,oid.quantity ,oid.price ,oid.line_total from invoice_master oim inner join invoice_details oid on oim.invoice_master_id=oid.invoice_master_id inner join eshop_cutomer_requisition_mapping cus on oim.requisition_master_id=cus.requisition_master_id inner join product on oid.product_id=product.product_id inner join product_category on product.product_category_id=product_category.product_category_id left join product_version on oid.product_version_id=product_version.product_version_id left join color on oid.color_id=color.color_id left join unit on oid.unit_id=unit.unit_id left join brand on oid.brand_id=brand.brand_id left join users on oim.created_by=users.user_id where oim.requisition_master_id=" + requisition_master_id;

                string query =
                    "select distinct oim.invoice_no ,oim.invoice_date ,users.full_name ,cus.customer_name ,cus.mobile_no, cus.billing_address,cus.customer_email,product.product_name ,product_category.product_category_name ,color.color_name ,unit.unit_name ,brand.brand_name ,oid.quantity , oid.price ,oid.line_total,re.is_varified from invoice_master oim inner join invoice_details oid on oim.invoice_master_id=oid.invoice_master_id inner join eshop_cutomer_requisition_mapping cus on oim.requisition_master_id=cus.requisition_master_id inner join product on oid.product_id=product.product_id inner join product_category on product.product_category_id=product_category.product_category_id left join product_version on oid.product_version_id=product_version.product_version_id left join color on oid.color_id=color.color_id left join unit on oid.unit_id=unit.unit_id left join brand on oid.brand_id=brand.brand_id left join receive re on oim.invoice_no = re.invoice_no left join users on oim.created_by=users.user_id where oim.requisition_master_id=" + requisition_master_id;

                var invoieData = _entities.Database.SqlQuery<EshopInvoiceModel>(query).ToList();
                return invoieData;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public object GetEshopReadyForDelivery(long delivery_master_id)
        {
            try
            {
                var obj = (from e in _entities.delivery_master
                           join req in _entities.requisition_master on e.requisition_master_id equals req.requisition_master_id
                           join cus in _entities.eshop_cutomer_requisition_mapping on e.requisition_master_id equals
                               cus.requisition_master_id
                           where e.delivery_master_id == delivery_master_id
                           select new
                           {
                               delivery_master_id = e.delivery_master_id,
                               delivery_no = e.delivery_no,
                               requisition_code = req.requisition_code,
                               requisition_date = req.requisition_date,
                               requisition_master_id = req.requisition_master_id,
                               customer_name = cus.customer_name,
                               reference_no = req.reference_no,
                               amount = req.amount,
                               status = e.status,
                               delivery_method_id = e.delivery_method_id,
                               delivery_person_name = e.delivery_person_name,
                               courier_id = e.courier_id,
                           }).SingleOrDefault();

                return obj;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public object GetProductDetailsForDelivery(long delivery_master_id)
        {
            try
            {
                var list = (from e in _entities.delivery_master
                            join d in _entities.delivery_details on e.delivery_master_id equals d.delivery_master_id
                            join m in _entities.requisition_master on e.requisition_master_id equals m.requisition_master_id
                            join p in _entities.products on d.product_id equals p.product_id
                            join c in _entities.colors on d.color_id equals c.color_id into tempCol
                            from c in tempCol.DefaultIfEmpty()
                            join v in _entities.product_version on d.product_version_id equals v.product_version_id into tempVer
                            from v in tempVer.DefaultIfEmpty()
                            where e.delivery_master_id == delivery_master_id
                            select new
                            {
                                delivery_details_id = d.delivery_details_id,
                                delivered_quantity = d.delivered_quantity,
                                unit_price = d.unit_price,
                                line_total = 0,
                                product_name = p.product_name,
                                color_name = c.color_name,
                                product_version_name = v.product_version_name,
                                product_version_id = v.product_version_id == null ? 0 : v.product_version_id,
                                color_id = c.color_id == null ? 0 : c.color_id,
                                product_id = p.product_id,
                                is_gift = d.is_gift,
                                requisition_master_id = e.requisition_master_id
                            }).ToList();

                var finalList = new List<EshopDeliveryModel>();
                foreach (var lll in list)
                {
                    var ob = new EshopDeliveryModel();
                    ob.delivery_details_id = lll.delivery_details_id;
                    ob.unit_price = lll.unit_price;
                    ob.product_name = lll.product_name;
                    ob.color_name = lll.color_name;
                    ob.product_version_name = lll.product_version_name;
                    ob.product_version_id = lll.product_version_id;
                    ob.color_id = lll.color_id;
                    ob.product_id = lll.product_id;
                    ob.is_gift = lll.is_gift;
                    ob.delivered_quantity = lll.delivered_quantity - ReturnQty(lll.requisition_master_id, lll.product_id, lll.color_id, lll.product_version_id, lll.is_gift);
                    ob.line_total = (lll.delivered_quantity - ReturnQty(lll.requisition_master_id, lll.product_id, lll.color_id, lll.product_version_id, lll.is_gift)) * lll.unit_price;

                    finalList.Add(ob);
                }

                return finalList;
            }
            catch (Exception)
            {
                return null;
            }
        }


        private int? ReturnQty(long? reId, long? pId, long? colId, long? verId, bool? gift)
        {
            var product = _entities.products.SingleOrDefault(a => a.product_id == pId);
            if (gift == true)
            {
                if (product.has_serial == true)
                {
                    var answer =
                        _entities.requisition_details.SingleOrDefault(
                            a =>
                                a.requisition_master_id == reId && a.product_id == pId && a.color_id == colId &&
                                a.product_version_id == verId && a.is_gift == true);
                    return answer.return_quantity;
                }
                else
                {
                    var answer =
                        _entities.requisition_details.SingleOrDefault(
                            a =>
                                a.requisition_master_id == reId && a.product_id == pId && a.is_gift == true);
                    return answer.return_quantity;
                }
            }
            else
            {
                if (product.has_serial == true)
                {
                    var answer =
                        _entities.requisition_details.SingleOrDefault(
                            a =>
                                a.requisition_master_id == reId && a.product_id == pId && a.color_id == colId &&
                                a.product_version_id == verId && a.is_gift == null);
                    return answer.return_quantity;
                }
                else
                {
                    var answer =
                        _entities.requisition_details.SingleOrDefault(
                            a =>
                                a.requisition_master_id == reId && a.product_id == pId && a.is_gift == null);
                    return answer.return_quantity;
                }
            }

        }


        public bool UpdateEshopDelivery(delivery_master deliveryMaster)
        {
            try
            {
                bool save = false;
                var master =
                    _entities.delivery_master.SingleOrDefault(
                        a => a.delivery_master_id == deliveryMaster.delivery_master_id);
                if (master.status == "In Transit")
                {
                    master.status = "Delivered";
                    master.delivery_method_id = deliveryMaster.delivery_method_id;
                    master.courier_id = deliveryMaster.courier_id;
                    master.delivery_person_name = deliveryMaster.delivery_person_name;

                    _entities.SaveChanges();
                    save = true;
                }
                else
                {
                    var warehouse = _entities.warehouses.SingleOrDefault(a => a.party_id == master.party_id);

                    var ToWareHouseId = master.to_warehouse_id;

                    master.courier_slip_no = deliveryMaster.courier_slip_no;
                    master.challan_copy = deliveryMaster.challan_copy;
                    master.status = "Confirm Delivered";
                    master.from_warehouse_id = master.to_warehouse_id;
                    master.to_warehouse_id = warehouse.warehouse_id;
                    _entities.SaveChanges();

                    //confirm code
                    var reqList =
                        _entities.requisition_details.Where(a => a.requisition_master_id == master.requisition_master_id)
                            .ToList();

                    var rQty = reqList.Sum(a => a.return_quantity);

                    var reqMaster =
                        _entities.requisition_master.SingleOrDefault(
                            a => a.requisition_master_id == master.requisition_master_id);

                    reqMaster.return_quantity = rQty;
                    reqMaster.is_requisition_closed = true;

                    _entities.SaveChanges();

                    //Update Inventroy

                    foreach (var requisitionDetailse in reqList)
                    {
                        var updateInventoty = new InventoryRepository();
                        int? qty = (requisitionDetailse.quantity - Convert.ToInt32(requisitionDetailse.return_quantity));

                        //'39' virtual in-transit warehouse-----------------------------
                        updateInventoty.UpdateInventory("DELIVERY", master.delivery_no, ToWareHouseId ?? 0, warehouse.warehouse_id, requisitionDetailse.product_id ?? 0, requisitionDetailse.color_id ?? 0, requisitionDetailse.product_version_id ?? 0, requisitionDetailse.unit_id ?? 0,
                            qty ?? 0, master.created_by ?? 0);
                    }

                    //Receive Serial Number Update

                    var serial =
                        _entities.receive_serial_no_details.Where(a => a.deliver_master_id == master.delivery_master_id)
                            .ToList();

                    foreach (var receiveSerialNoDetailse in serial)
                    {
                        receive_serial_no_details receiveSerial = _entities.receive_serial_no_details.FirstOrDefault(r => r.imei_no == receiveSerialNoDetailse.imei_no || r.imei_no2 == receiveSerialNoDetailse.imei_no);

                        receiveSerial.current_warehouse_id = warehouse.warehouse_id;
                        receiveSerial.party_id = master.party_id;
                        receiveSerial.deliver_date = DateTime.Now;
                        receiveSerial.sales_status = true;
                        receiveSerial.sales_date = DateTime.Now;
                        _entities.SaveChanges();
                    }
                    save = true;

                }
                return save;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public object GetEshopRfdConfirmList()
        {
            try
            {
                var list = (from e in _entities.delivery_master

                            join r in _entities.requisition_master on e.requisition_master_id equals r.requisition_master_id
                            join c in _entities.eshop_cutomer_requisition_mapping on e.requisition_master_id equals c.requisition_master_id
                            where r.requisition_type == "E-Shop Requisition" && e.status != "In Transit" && e.status != "Cancel"
                            select new
                            {
                                delivery_master_id = e.delivery_master_id,
                                requisition_master_id = e.requisition_master_id,
                                delivery_no = e.delivery_no,
                                delivery_date = e.delivery_date,
                                reference_no = r.reference_no,
                                requisition_code = r.requisition_code,
                                mobile_no = c.mobile_no,
                                customer_name = c.customer_name,
                                status = e.status,
                                quantity = _entities.delivery_details.Where(a => a.delivery_master_id == e.delivery_master_id).Sum(a => a.delivered_quantity)
                            }).ToList();
                return list.OrderByDescending(a => a.delivery_master_id);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public object GetProductForReturn(long delivery_master_id)
        {
            try
            {
                var list = (from e in _entities.delivery_master
                            join d in _entities.delivery_details on e.delivery_master_id equals d.delivery_master_id
                            join r in _entities.requisition_master on e.requisition_master_id equals r.requisition_master_id
                            join p in _entities.products on d.product_id equals p.product_id
                            join c in _entities.colors on d.color_id equals c.color_id
                            join v in _entities.product_version on d.product_version_id equals v.product_version_id
                            where e.delivery_master_id == delivery_master_id
                            select new
                            {
                                delivery_details_id = d.delivery_details_id,
                                requisition_master_id = r.requisition_master_id,
                                delivered_quantity = d.delivered_quantity,
                                unit_price = d.unit_price,
                                line_total = d.line_total,
                                product_name = p.product_name,
                                color_name = c.color_name,
                                product_version_name = v.product_version_name,
                                return_quantity = 0,
                                is_gift = d.is_gift,
                                product_id = p.product_id,
                                color_id = c.color_id,
                                product_version_id = v.product_version_id,
                                gift_type = d.gift_type,
                                has_serial = p.has_serial
                            }).ToList();

                var finalList = new List<EshopDeliveryModel>();
                foreach (var lll in list)
                {
                    var ob = new EshopDeliveryModel();
                    ob.delivery_details_id = lll.delivery_details_id;
                    ob.unit_price = lll.unit_price;
                    ob.product_name = lll.product_name;
                    ob.color_name = lll.color_name;
                    ob.product_version_name = lll.product_version_name;
                    ob.is_gift = lll.is_gift;
                    ob.gift_type = lll.gift_type;
                    ob.delivered_quantity = lll.delivered_quantity - ReturnQty(lll.requisition_master_id, lll.product_id, lll.color_id, lll.product_version_id, lll.is_gift);
                    ob.return_quantity = lll.return_quantity;
                    ob.requisition_master_id = lll.requisition_master_id;

                    finalList.Add(ob);
                }
                return finalList;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public object GetIMEIForReturn(long imei)
        {
            try
            {
                var warehouse = _entities.warehouses.SingleOrDefault(a => a.warehouse_name == "In Transit");
                var imeiObject = (from e in _entities.receive_serial_no_details
                                  join req in _entities.requisition_master on e.requisition_id equals req.requisition_master_id
                                  join pro in _entities.products on e.product_id equals pro.product_id
                                  join col in _entities.colors on e.color_id equals col.color_id
                                  join ver in _entities.product_version on e.product_version_id equals ver.product_version_id
                                  where
                                      (e.imei_no == imei || e.imei_no2 == imei) &&
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


        public bool CancelEshopRequisition(long delivery_master_id)
        {
            try
            {
                bool save = false;
                var deliveryMaster =
                    _entities.delivery_master.SingleOrDefault(a => a.delivery_master_id == delivery_master_id);

                if (deliveryMaster.status == "In Transit")
                {
                    var reqMaster =
                        _entities.requisition_master.SingleOrDefault(
                            a => a.requisition_master_id == deliveryMaster.requisition_master_id);

                    // Requsition Master

                    reqMaster.status = "Cancel";
                    reqMaster.is_requisition_closed = true;

                    _entities.SaveChanges();

                    //delivery master

                    deliveryMaster.status = "Cancel";
                    _entities.SaveChanges();

                    //Invoice Master

                    var invoice =
                        _entities.invoice_master.SingleOrDefault(
                            a => a.requisition_master_id == deliveryMaster.requisition_master_id);

                    invoice.is_active = false;
                    invoice.is_deleted = true;

                    _entities.SaveChanges();

                    //Update Party Journal

                    //Update Inventory

                    var details =
                        _entities.delivery_details.Where(a => a.delivery_master_id == delivery_master_id).ToList();
                    foreach (var deliveryDetailse in details)
                    {
                        var updateInventoty = new InventoryRepository();

                        //'39' virtual in-transit warehouse-----------------------------
                        updateInventoty.UpdateInventory("DELIVERY CANCEL", deliveryMaster.delivery_no, deliveryMaster.to_warehouse_id ?? 0, deliveryMaster.from_warehouse_id ?? 0, deliveryDetailse.product_id ?? 0, deliveryDetailse.color_id ?? 0, deliveryDetailse.product_version_id ?? 0, deliveryDetailse.unit_id ?? 0,
                            deliveryDetailse.delivered_quantity ?? 0, deliveryMaster.created_by ?? 0);
                    }

                    //Update Serial Number
                    var serial =
                        _entities.receive_serial_no_details.Where(a => a.deliver_master_id == deliveryMaster.delivery_master_id)
                            .ToList();

                    foreach (var receiveSerialNoDetailse in serial)
                    {
                        receive_serial_no_details receiveSerial = _entities.receive_serial_no_details.FirstOrDefault(r => r.imei_no == receiveSerialNoDetailse.imei_no || r.imei_no2 == receiveSerialNoDetailse.imei_no);

                        receiveSerial.current_warehouse_id = deliveryMaster.from_warehouse_id;
                        receiveSerial.party_id = 0;
                        receiveSerial.deliver_date = null;
                        receiveSerial.sales_status = false;
                        receiveSerial.sales_date = null;
                        receiveSerial.deliver_master_id = 0;
                        receiveSerial.requisition_id = 0;
                        receiveSerial.is_gift = null;

                        _entities.SaveChanges();
                    }

                    //Party Journal

                    //GET ACCOUNT BALANCE FROM PARTY JOURNAL
                    var partyJournal =
                        _entities.party_journal.Where(pj => pj.party_id == deliveryMaster.party_id)
                            .OrderByDescending(p => p.party_journal_id)
                            .FirstOrDefault();
                    decimal partyAccountBalance = 0;
                    if (partyJournal != null)
                    {
                        partyAccountBalance = partyJournal.closing_balance ?? 0;
                    }

                    //CALCULATING INVOICE TOTAL
                    //decimal invoiceTotal = 0;

                    //invoiceTotal = onlineRequisitionModel.RequisitionMaster.amount ?? 0; //insert in both invoice master and party journal table

                    decimal? invoiceTotal =
                        _entities.requisition_master.SingleOrDefault(a => a.requisition_master_id == deliveryMaster.requisition_master_id).amount;


                    partyJournalRepository.PartyJournalEntry("RETURN_INVOICE", deliveryMaster.party_id ?? 0, invoiceTotal ?? 0, "Cancel", reqMaster.created_by ?? 0, reqMaster.requisition_code);

                    save = true;
                }
                else
                {
                    save = false;
                }
                return save;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public bool EshopReturnInsert(EshopReturnModel eshopReturnModel)
        {
            //try
            //{
            //    bool saveReturn = false;

            //    var onlineReturnMaster = eshopReturnModel.EshopReturn;
            //    var onlineReturnDetails = eshopReturnModel.EshopReturnDetails;

            //    var reqMaster =
            //        _entities.requisition_master.SingleOrDefault(
            //            a => a.requisition_master_id == eshopReturnModel.EshopReturn.invoice_master_id);

            //    var partyTypePrefix = (from ptype in _entities.party_type
            //                           join par in _entities.parties on ptype.party_type_id equals par.party_type_id
            //                           where par.party_id == reqMaster.party_id
            //                           select new
            //                           {
            //                               party_prefix = ptype.party_prefix

            //                           }).FirstOrDefault();

            //    int ReturnSerial = _entities.eshop_return_master.Max(rq => (int?)rq.eshop_return_master_id) ?? 0;
            //    ReturnSerial++;

            //    var rqStr = ReturnSerial.ToString().PadLeft(7, '0');
            //    string requisitionNo = "RET-" + partyTypePrefix.party_prefix + "-" + rqStr;

            //    onlineReturnMaster.return_date = DateTime.Now;
            //    onlineReturnMaster.return_code = requisitionNo;
            //    onlineReturnMaster.created_by = eshopReturnModel.EshopReturn.created_by;
            //    onlineReturnMaster.created_date = DateTime.Now;
            //    onlineReturnMaster.returned_by = eshopReturnModel.EshopReturn.created_by;
            //    onlineReturnMaster.returned_quantity = eshopReturnModel.EshopReturnDetails.Count;
            //    onlineReturnMaster.updated_by = eshopReturnModel.EshopReturn.created_by;
            //    onlineReturnMaster.updated_date = DateTime.Now;
            //    onlineReturnMaster.invoice_master_id = eshopReturnModel.EshopReturn.invoice_master_id;

            //    _entities.eshop_return_master.Add(onlineReturnMaster);
            //    int save = _entities.SaveChanges();

            //    Int64 onlineReturnMasterId = onlineReturnMaster.eshop_return_master_id;

            //    if (save > 0)
            //    {
            //        foreach (var onlineReturnDetailse in onlineReturnDetails)
            //        {
            //            var reqDetails =
            //                _entities.requisition_details.FirstOrDefault(a => a.product_id == onlineReturnDetailse.product_id && a.color_id == onlineReturnDetailse.color_id && a.product_version_id == onlineReturnDetailse.product_version_id && a.requisition_master_id == eshopReturnModel.EshopReturn.invoice_master_id);
            //            var serial =
            //                _entities.receive_serial_no_details.SingleOrDefault(
            //                    a =>
            //                        a.imei_no == onlineReturnDetailse.imei_no ||
            //                        a.imei_no2 == onlineReturnDetailse.imei_no);
            //            var delivery =
            //                _entities.delivery_master.SingleOrDefault(
            //                    a => a.delivery_master_id == serial.deliver_master_id);
            //            var returnDetails = new eshop_return_details()
            //            {
            //                imei_no = serial.imei_no,
            //                imei_no2 = serial.imei_no2,
            //                brand_id = reqDetails.brand_id,
            //                color_id = reqDetails.color_id,
            //                product_version_id = reqDetails.product_version_id,
            //                product_id = reqDetails.product_id,
            //                price = reqDetails.price,
            //                unit_id = reqDetails.unit_id,
            //                eshop_return_master_id = onlineReturnMasterId,
            //                returned_qty = 1,
            //                verify_status = false,
            //                warehouse_id = delivery.from_warehouse_id,

            //            };
            //            _entities.eshop_return_details.Add(returnDetails);
            //            int sss = _entities.SaveChanges();



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


            //        var returnDetailseQty =
            //            _entities.requisition_details.Where(
            //                a =>
            //                    a.requisition_master_id == eshopReturnModel.EshopReturn.invoice_master_id).ToList();
            //        var requisitionMaster =
            //            _entities.requisition_master.SingleOrDefault(
            //                a => a.requisition_master_id == eshopReturnModel.EshopReturn.invoice_master_id);

            //        var deliveryMaster =
            //            _entities.delivery_master.SingleOrDefault(
            //                a => a.requisition_master_id == eshopReturnModel.EshopReturn.invoice_master_id);

            //        if (requisitionMaster.is_requisition_closed == false)
            //        {
            //            //int? inQty = returnDetailseQty.Sum(a => a.invoice_quantity);
            //            int? reQty = returnDetailseQty.Sum(a => a.return_quantity);
            //            int? qty = returnDetailseQty.Sum(a => a.quantity);
            //            //int? total = reQty + inQty;
            //            if (qty == reQty)
            //            {
            //                requisitionMaster.is_requisition_closed = true;
            //                requisitionMaster.return_quantity = reQty;

            //                _entities.SaveChanges();

            //                deliveryMaster.status = "Item Return";

            //                _entities.SaveChanges();
            //            }
            //            else
            //            {
            //                requisitionMaster.is_requisition_closed = false;
            //                requisitionMaster.return_quantity = reQty;

            //                _entities.SaveChanges();
            //            }


            //        }


            //        //Check This

            //        ////Inventory Update                                                        
            //        Int64 detailsIdReq = 0;

            //        foreach (var onlineReturnDetailse in onlineReturnDetails)
            //        {

            //            var reqDetailsForInventory =
            //                    _entities.requisition_details.SingleOrDefault(
            //                        a =>
            //                            a.requisition_master_id == eshopReturnModel.EshopReturn.invoice_master_id &&
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
            //                        a => a.requisition_master_id == eshopReturnModel.EshopReturn.invoice_master_id);

            //                InventoryRepository updateInventoty = new InventoryRepository();

            //                var onlineMater = _entities.eshop_return_master.Find(onlineReturnMasterId);

            //                //'39' virtual in-transit warehouse-----------------------------
            //                updateInventoty.UpdateInventory("ESHOP RETURN", onlineMater.return_code, delivery.to_warehouse_id ?? 0, delivery.from_warehouse_id ?? 0, reqDetailsForInventory.product_id ?? 0, reqDetailsForInventory.color_id ?? 0, reqDetailsForInventory.product_version_id ?? 0, reqDetailsForInventory.unit_id ?? 0,
            //                    qty ?? 0, eshopReturnModel.EshopReturn.created_by ?? 0);

            //                detailsIdReq = reqDetailsForInventory.requisition_details_id;
            //            }

            //        }

            //        //Party Journal Update

            //        //GET ACCOUNT BALANCE FROM PARTY JOURNAL
            //        var partyJournal =
            //            _entities.party_journal.Where(pj => pj.party_id == deliveryMaster.party_id)
            //                .OrderByDescending(p => p.party_journal_id)
            //                .FirstOrDefault();
            //        decimal partyAccountBalance = 0;
            //        if (partyJournal != null)
            //        {
            //            partyAccountBalance = partyJournal.closing_balance ?? 0;
            //        }

            //        //CALCULATING INVOICE TOTAL
            //        //decimal invoiceTotal = 0;

            //        //invoiceTotal = onlineRequisitionModel.RequisitionMaster.amount ?? 0; //insert in both invoice master and party journal table

            //        decimal? invoiceTotal =
            //            _entities.eshop_return_details.Where(a => a.eshop_return_master_id == onlineReturnMasterId)
            //                .Sum(a => a.price);


            //        partyJournalRepository.PartyJournalEntry("RETURN_INVOICE", deliveryMaster.party_id ?? 0, invoiceTotal ?? 0, "Return", requisitionMaster.created_by ?? 0, requisitionNo);

            //        saveReturn = true;

            //    }

            //    return saveReturn;
            //}
            //catch (Exception)
            //{

            //    throw;
            //}
            return false;
        }


        public object GetInvoiceForEshoPayment(long delivery_master_id)
        {
            try
            {
                var masterData = (from e in _entities.delivery_master
                                  join r in _entities.requisition_master on e.requisition_master_id equals r.requisition_master_id
                                  join i in _entities.invoice_master on r.requisition_master_id equals i.requisition_master_id
                                  join c in _entities.eshop_cutomer_requisition_mapping on r.requisition_master_id equals
                                      c.requisition_master_id
                                  where e.delivery_master_id == delivery_master_id
                                  select new
                                  {
                                      i.invoice_date,
                                      i.invoice_no,
                                      c.payment_method,
                                      i.invoice_master_id
                                  }).FirstOrDefault();
                return masterData;
            }
            catch (Exception)
            {
                return null;

            }
        }


        public bool InsertPaymentReceive()
        {
            try
            {
                HttpRequest rsk = HttpContext.Current.Request;

                bool result = false;
                long bank_id = 0;
                var postedFile = rsk.Files["UploadedImage"];
                long payment_method_id = Int64.Parse(rsk.Form["payment_method_id"].ToString());
                if (payment_method_id != 3)
                {
                    bank_id = 0;
                }
                else
                {
                    bank_id = Int64.Parse(rsk.Form["bank_id"].ToString());
                }

                long bank_branch_id = Convert.ToInt64(rsk.Form["bank_branch_id"].ToString());
                long bank_account_id = Convert.ToInt64(rsk.Form["bank_account_id"].ToString());
                string cheque_no = rsk.Form["cheque_no"].ToString();
                decimal amount = Decimal.Parse(rsk.Form["amount"].ToString());
                long invoice_master_id = Convert.ToInt64(rsk.Form["invoice_master_id"]);

                long created_by = Int64.Parse(rsk.Form["created_by"].ToString());

                if (bank_branch_id == null || bank_branch_id == 0) { bank_branch_id = 0; }
                if (cheque_no == null || cheque_no == "") { cheque_no = "0"; }

                string actualFileName = "";
                long userId = created_by;

                string filePathForDb = "";

                if (postedFile == null)
                {
                    filePathForDb = "";
                    result = false;
                }
                else
                {
                    //Start
                    var invoice =
                        _entities.invoice_master.SingleOrDefault(
                            a => a.invoice_master_id == invoice_master_id);
                    var party = _entities.parties.SingleOrDefault(a => a.party_id == invoice.party_id);

                    actualFileName = postedFile.FileName;
                    var clientDir = HttpContext.Current.Server.MapPath("~/App_Data/Payment_request/" + "Party_" + party.party_id);
                    bool exists = System.IO.Directory.Exists(clientDir);
                    if (!exists)
                        System.IO.Directory.CreateDirectory(clientDir);
                    var fileSavePath = Path.Combine(clientDir, actualFileName);
                    bool checkFileSave = false;
                    try
                    {
                        postedFile.SaveAs(fileSavePath);
                        checkFileSave = true;
                    }
                    catch
                    {
                        checkFileSave = false;
                    }

                    if (checkFileSave)
                    {
                        filePathForDb = "Party_" + party.party_id + "/" + actualFileName;

                        //End

                        //save receive table

                        var partyTypePrefix = (from ptype in _entities.party_type
                                               join par in _entities.parties on ptype.party_type_id equals par.party_type_id
                                               where par.party_id == party.party_id
                                               select new
                                               {
                                                   party_prefix = ptype.party_prefix

                                               }).FirstOrDefault();

                        int InvoiceSerial = _entities.receives.Max(rq => (int?)rq.receive_id) ?? 0;
                        InvoiceSerial++;

                        var rqStr = InvoiceSerial.ToString().PadLeft(7, '0');
                        string invoiceNo = "MRN-" + partyTypePrefix.party_prefix + "-" + rqStr;

                        var insertPayment = new receive
                        {
                            receipt_no = invoiceNo,
                            receive_date = DateTime.Now,
                            party_id = party.party_id,
                            payment_method_id = payment_method_id,
                            cheque_no = cheque_no,
                            bank_id = bank_id,
                            bank_branch_id = bank_branch_id,
                            last_invoice_balance = 0,
                            bank_account_id = bank_account_id,
                            amount = amount,
                            invoice_no = invoice.invoice_no,
                            representative = "",
                            remarks = "",
                            payment_req_id = 0,
                            document_attachment = filePathForDb,
                            bank_charge = 0,
                            is_varified = false,
                            created_date = DateTime.Now,
                            created_by = created_by,
                            is_active = true,
                            is_deleted = false
                        };

                        _entities.receives.Add(insertPayment);
                        int i = _entities.SaveChanges();

                        if (i > 0)
                        {
                            invoice.is_active = true;
                            _entities.SaveChanges();

                            //delivery Master update

                            var masterDeliver =
                                _entities.delivery_master.SingleOrDefault(
                                    a => a.requisition_master_id == invoice.requisition_master_id);

                            masterDeliver.status = "Payment Generate";
                            _entities.SaveChanges();

                        }
                        result = true;

                    }
                    else
                    {
                        result = false;
                    }
                }
                return result;

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}