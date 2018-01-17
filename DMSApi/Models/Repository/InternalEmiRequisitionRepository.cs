using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMSApi.Models.crystal_models;
using DMSApi.Models.IRepository;
using DMSApi.Models.StronglyType;

namespace DMSApi.Models.Repository
{
    public class InternalEmiRequisitionRepository : IInternalEmiRequisitionRepository
    {
        private DMSEntities _entities;
        private PartyJournalRepository partyJournalRepository;

        public InternalEmiRequisitionRepository()
        {
            _entities = new DMSEntities();
            this.partyJournalRepository = new PartyJournalRepository();
        }
        public long AddInternalEmiRequisition(InternalEmiRequisitionModel internalEmiRequisitionModel)
        {
            try
            {
                var internalRequisitionMasterData = internalEmiRequisitionModel.InternalRequisitionMaster;
                var internalRequisitionDetailsList = internalEmiRequisitionModel.InternalRequisitionDetails;
                var receiveSerialNoDetailsList = internalEmiRequisitionModel.ReceiveSerialNoDetails;
                var promotionDetailsList = internalEmiRequisitionModel.PromotionDetails;
                var installmentDetails = internalEmiRequisitionModel.InstallmentDetails;

                int save = 0;

                //Auto Number Creator
                long internalRequisitionSerial = _entities.internal_requisition_master.Max(re => (long?)re.internal_requisition_master_id) ?? 0;
                internalRequisitionSerial++;
                var internalRequisitionSerialNo = internalRequisitionSerial.ToString().PadLeft(7, '0');
                string intReqNo = "INT-EMI-REQ-" + internalRequisitionSerialNo;

                //Master Table
                internalRequisitionMasterData.internal_requisition_no = intReqNo;
                internalRequisitionMasterData.from_warehouse_id = internalEmiRequisitionModel.InternalRequisitionMaster.from_warehouse_id;
                internalRequisitionMasterData.to_department = internalEmiRequisitionModel.InternalRequisitionMaster.to_department;
                internalRequisitionMasterData.customar_name = internalEmiRequisitionModel.InternalRequisitionMaster.customar_name;
                internalRequisitionMasterData.mobile_no = internalEmiRequisitionModel.InternalRequisitionMaster.mobile_no;
                internalRequisitionMasterData.requisition_date = DateTime.Now;
                internalRequisitionMasterData.payment_type = "EMI";
                internalRequisitionMasterData.pricing = internalEmiRequisitionModel.InternalRequisitionMaster.pricing;
                internalRequisitionMasterData.remarks = internalEmiRequisitionModel.InternalRequisitionMaster.remarks;
                internalRequisitionMasterData.requisition_status = "Created";
                internalRequisitionMasterData.total_incentive_amount = internalRequisitionDetailsList.Sum(tin => tin.incentive_amount) ?? 0;
                internalRequisitionMasterData.total_amount = internalEmiRequisitionModel.InternalRequisitionMaster.total_amount;
                internalRequisitionMasterData.created_by = internalEmiRequisitionModel.InternalRequisitionMaster.created_by;
                internalRequisitionMasterData.created_date = DateTime.Now;
                _entities.internal_requisition_master.Add(internalRequisitionMasterData);
                _entities.SaveChanges();

                //Details Table
                long internalRequisitionMasterId = internalRequisitionMasterData.internal_requisition_master_id;
                foreach (var item in internalRequisitionDetailsList)
                {
                    var internalRequisitionDetails = new internal_requisition_details
                    {
                        internal_requisition_master_id = internalRequisitionMasterId,
                        product_id = item.product_id,
                        color_id = item.color_id,
                        product_version_id = item.product_version_id,
                        quantity = item.quantity,
                        price = item.price,
                        amount = item.amount,
                        is_gift = false,
                        incentive_amount = item.incentive_amount,
                        promotion_master_id = item.promotion_master_id,
                    };
                    _entities.internal_requisition_details.Add(internalRequisitionDetails);
                    save = _entities.SaveChanges();
                }

                //for promotion List---
                foreach (var item in promotionDetailsList)
                {
                    var internalRequisitionDetails = new internal_requisition_details
                    {
                        internal_requisition_master_id = internalRequisitionMasterId,
                        product_id = item.product_id,
                        color_id = item.color_id,
                        product_version_id = item.product_version_id,
                        quantity = item.quantity,
                        price = item.price,
                        amount = item.amount,
                        is_gift = true,
                        gift_type = "Promotional",
                        incentive_amount = item.incentive_amount,
                        promotion_master_id = item.promotion_master_id,
                    };
                    _entities.internal_requisition_details.Add(internalRequisitionDetails);
                    save = _entities.SaveChanges();
                }

                if (save > 0)
                {
                    var deliveryMaster = new delivery_master();


                    //Get To Warehouse Id
                    var toWarehouseId =
                      _entities.warehouses.SingleOrDefault(
                          a => a.warehouse_name == "Internal EMI Warehouse").warehouse_id;
                    //Get Delivery Party Id
                    var partyId = _entities.warehouses.SingleOrDefault(
                          a => a.warehouse_name == "Internal EMI Warehouse").party_id;
                    //Get Party Address
                    var partyAddress = _entities.parties.SingleOrDefault(
                          a => a.party_name == "Internal EMI").address;
                    long deliverySerial = _entities.delivery_master.Max(po => (long?)po.delivery_master_id) ?? 0;
                    deliverySerial++;

                    var deliveryStr = deliverySerial.ToString().PadLeft(7, '0');
                    string deliveryNo = "DN-INT-EMI-REQ" + "-" + deliveryStr;
                    deliveryMaster.delivery_no = deliveryNo;
                    deliveryMaster.delivery_date = DateTime.Now;
                    deliveryMaster.party_id = partyId;
                    deliveryMaster.requisition_master_id = internalRequisitionMasterId;
                    deliveryMaster.courier_id = 0;
                    deliveryMaster.courier_slip_no = "";
                    deliveryMaster.delivery_address = partyAddress;
                    deliveryMaster.created_by = internalEmiRequisitionModel.InternalRequisitionMaster.created_by;
                    deliveryMaster.created_date = DateTime.Now;
                    deliveryMaster.from_warehouse_id = internalEmiRequisitionModel.InternalRequisitionMaster.from_warehouse_id;
                    deliveryMaster.to_warehouse_id = toWarehouseId;
                    deliveryMaster.status = "EMI-RFD";
                    deliveryMaster.remarks = "DN-INT-EMI-REQ";
                    deliveryMaster.total_amount = internalEmiRequisitionModel.InternalRequisitionMaster.total_amount;
                    deliveryMaster.lot_no = "";
                    deliveryMaster.vehicle_no = "DN-INT-EMI-REQ";
                    deliveryMaster.truck_driver_name = "DN-INT-EMI-REQ";
                    deliveryMaster.truck_driver_mobile = "DN-INT-EMI-REQ";

                    _entities.delivery_master.Add(deliveryMaster);
                    _entities.SaveChanges();
                    long deliveryMasterId = deliveryMaster.delivery_master_id;

                    // Delivery Details Table
                    foreach (var item in internalRequisitionDetailsList)
                    {

                        var deliveryDetails = new delivery_details()
                        {
                            delivery_master_id = deliveryMasterId,
                            product_id = item.product_id,
                            color_id = item.color_id,
                            product_version_id = item.product_version_id,
                            delivered_quantity = item.quantity,
                            is_gift = item.is_gift,
                            requisition_quantity = item.quantity,
                            unit_price = item.product_id,
                            line_total = item.amount,
                            party_id = partyId,
                            gift_type = item.gift_type,
                            is_live_dummy = false

                        };
                        _entities.delivery_details.Add(deliveryDetails);
                        int saved = _entities.SaveChanges();



                        if (saved > 0)
                        {
                            // update inventory
                            InventoryRepository updateInventoty = new InventoryRepository();
                            var intransitWarehouseId =
                                _entities.warehouses.SingleOrDefault(k => k.warehouse_name == "In Transit").warehouse_id;
                            var masterDelivery = _entities.delivery_master.Find(deliveryMasterId);

                            //'39' virtual in-transit warehouse
                            updateInventoty.UpdateInventory("INT-EMI-REQ-DELIVERY", masterDelivery.delivery_no, masterDelivery.from_warehouse_id ?? 0, intransitWarehouseId, item.product_id ?? 0, item.color_id ?? 0, item.product_version_id ?? 0, 1,
                                item.quantity ?? 0, internalEmiRequisitionModel.InternalRequisitionMaster.created_by ?? 0);
                        }

                    }

                    foreach (var item in promotionDetailsList)
                    {

                        var deliveryDetails = new delivery_details()
                        {
                            delivery_master_id = deliveryMasterId,
                            product_id = item.product_id,
                            color_id = item.color_id,
                            product_version_id = item.product_version_id,
                            delivered_quantity = item.quantity,
                            is_gift = item.is_gift,
                            requisition_quantity = item.quantity,
                            unit_price = item.product_id,
                            line_total = item.amount,
                            party_id = partyId,
                            gift_type = item.gift_type,
                            is_live_dummy = false

                        };
                        _entities.delivery_details.Add(deliveryDetails);
                        int saved = _entities.SaveChanges();



                        if (saved > 0)
                        {
                            // update inventory
                            InventoryRepository updateInventoty = new InventoryRepository();
                            var intransitWarehouseId =
                                _entities.warehouses.SingleOrDefault(k => k.warehouse_name == "In Transit").warehouse_id;
                            var masterDelivery = _entities.delivery_master.Find(deliveryMasterId);

                            //'39' virtual in-transit warehouse
                            updateInventoty.UpdateInventory("INT-EMI-REQ-DELIVERY", masterDelivery.delivery_no, masterDelivery.from_warehouse_id ?? 0, intransitWarehouseId, item.product_id ?? 0, item.color_id ?? 0, item.product_version_id ?? 0, 1,
                                item.quantity ?? 0, internalEmiRequisitionModel.InternalRequisitionMaster.created_by ?? 0);
                        }

                    }
                    //For Party Journal

                    ////Get Party Account Balance
                    //var partyJournal =
                    //    _entities.party_journal.Where(pj => pj.party_id == partyId)
                    //        .OrderByDescending(p => p.party_journal_id)
                    //        .FirstOrDefault();
                    //decimal partyAccountBalance = 0;
                    //if (partyJournal != null)
                    //{
                    //    partyAccountBalance = partyJournal.closing_balance ?? 0;
                    //}

                    ////Find Invoice Total
                    //decimal invoiceTotal = 0;
                    //invoiceTotal = internalEmiRequisitionModel.InternalRequisitionMaster.total_amount ?? 0; //insert in both invoice master and party journal table

                    ////Account Balance
                    //decimal accountBalance = 0;
                    //accountBalance = invoiceTotal + partyAccountBalance;



                    //Invoice Master Table

                    long InvoiceSerial = _entities.invoice_master.Max(po => (long?)po.invoice_master_id) ?? 0;
                    InvoiceSerial++;
                    var invStr = InvoiceSerial.ToString().PadLeft(7, '0');
                    string invoiceNo = "INV-" + "INT-EMI-REQ" + "-" + invStr;

                    invoice_master insert_invoice = new invoice_master
                    {
                        invoice_no = invoiceNo,
                        invoice_date = System.DateTime.Now,
                        party_id = partyId,
                        requisition_master_id = internalRequisitionMasterId,
                        company_id = 0,
                        remarks = internalEmiRequisitionModel.InternalRequisitionMaster.remarks,
                        created_by = internalEmiRequisitionModel.InternalRequisitionMaster.created_by,
                        created_date = DateTime.Now,
                        incentive_amount = internalRequisitionDetailsList.Sum(tin => tin.incentive_amount),
                        item_total = internalEmiRequisitionModel.InternalRequisitionMaster.total_amount,
                        party_type_id = 11,
                        rebate_total = 0,
                        invoice_total = 0,
                        account_balance = 0,
                    };

                    _entities.invoice_master.Add(insert_invoice);
                    _entities.SaveChanges();
                    long InvoiceMasterId = insert_invoice.invoice_master_id;


                    //Invoice Details Table
                    foreach (var item in internalRequisitionDetailsList)
                    {
                        var getProductCategory =
                            _entities.products.SingleOrDefault(p => p.product_id == item.product_id).product_category_id;
                        var getProductUnit =
                            _entities.products.SingleOrDefault(p => p.product_id == item.product_id).unit_id;
                        var invoiceDetails_insert = new invoice_details
                        {

                            invoice_master_id = InvoiceMasterId,
                            product_category_id = getProductCategory,
                            product_id = item.product_id,
                            color_id = item.color_id,
                            product_version_id = item.product_version_id,
                            unit_id = getProductUnit,
                            price = item.price,
                            quantity = item.quantity,
                            line_total = item.amount,
                            is_gift = false,
                            is_live_dummy = false

                        };

                        _entities.invoice_details.Add(invoiceDetails_insert);
                        _entities.SaveChanges();
                    }

                    //Invoice Details Table
                    foreach (var item in promotionDetailsList)
                    {
                        var getProductCategory =
                            _entities.products.SingleOrDefault(p => p.product_id == item.product_id).product_category_id;
                        var getProductUnit =
                            _entities.products.SingleOrDefault(p => p.product_id == item.product_id).unit_id;
                        var invoiceDetails_insert = new invoice_details
                        {

                            invoice_master_id = InvoiceMasterId,
                            product_category_id = getProductCategory,
                            product_id = item.product_id,
                            color_id = item.color_id,
                            product_version_id = item.product_version_id,
                            unit_id = getProductUnit,
                            price = item.price,
                            quantity = item.quantity,
                            line_total = item.amount,
                            is_gift = true,
                            is_live_dummy = false

                        };

                        _entities.invoice_details.Add(invoiceDetails_insert);
                        _entities.SaveChanges();
                    }


                    //Party Journal
                   // partyJournalRepository.PartyJournalEntry("INVOICE", partyId ?? 0, 0, "Invoice", internalEmiRequisitionModel.InternalRequisitionMaster.created_by ?? 0, invoiceNo);


                    //Receive Serial Table
                    
                    var intransitWarehouse =
                             _entities.warehouses.SingleOrDefault(k => k.warehouse_name == "In Transit").warehouse_id;
                    //Get Delivery Party Id

                    var partyIdForDelivery = _entities.warehouses.SingleOrDefault(
                          a => a.warehouse_name == "Internal EMI Warehouse").party_id;


                    foreach (var item in receiveSerialNoDetailsList)
                    {

                        receive_serial_no_details receiveSerialNoDetails = _entities.receive_serial_no_details.FirstOrDefault(r => r.imei_no == item.imei_no || r.imei_no2 == item.imei_no2);
                        receiveSerialNoDetails.current_warehouse_id = intransitWarehouse;
                        receiveSerialNoDetails.party_id = partyIdForDelivery;
                        receiveSerialNoDetails.deliver_date = DateTime.Now;
                        receiveSerialNoDetails.requisition_id = internalRequisitionMasterId;
                        receiveSerialNoDetails.deliver_master_id = deliveryMasterId;
                        receiveSerialNoDetails.is_gift = item.is_gift;
                        receiveSerialNoDetails.is_live_dummy = false;
                        _entities.SaveChanges();

                    }

                    foreach (var item in installmentDetails)
                    {
                        var installmentDetailsList = new installment_details
                        {
                            internal_requisition_master_id = internalRequisitionMasterId,
                            installment_no = item.installment_no,
                            installment_date = item.installment_date,
                            installment_amount = item.installment_amount
                        };
                        _entities.installment_details.Add(installmentDetailsList);
                        save = _entities.SaveChanges();
                    }
                }
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object GetAllInternalEmiRequisitionRfd()
        {
            try
            {
                var data = (from dm in _entities.delivery_master
                            join p in _entities.parties on dm.party_id equals p.party_id into tempP
                            from p in tempP.DefaultIfEmpty()
                            join r in _entities.internal_requisition_master on dm.requisition_master_id equals r.internal_requisition_master_id into tempr
                            from r in tempr.DefaultIfEmpty()
                            where dm.status == "EMI-RFD"
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
                                requisition_code = r.internal_requisition_no,
                                internal_requisition_master_id = r.internal_requisition_master_id,
                                created_date = r.created_date,
                                receive_date = dm.receive_date //== null ? "Pending" : dm.receive_date.ToString()


                            }).OrderByDescending(e => e.delivery_master_id).ToList();

                return data;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public object GetInternalEmiDeliveryReportById(long delivery_master_id, long user_id)
        {
            try
            {

                string query = "select distinct dm.delivery_no ,reg.region_name ,are.area_name ,ter.territory_name ,dm.lot_no ,dm.delivery_date ,usr.full_name ,rm.internal_requisition_no as requisition_code ,par.party_name ,whf.warehouse_name as from_warehouse_name ,wht.warehouse_name as to_warehouse_name ,pro.product_name ,pc.product_category_name ,bd.brand_name ,col.color_name ,(SELECT STUFF((SELECT ' ' + imei_no FROM receive_serial_no_details where dd.product_id=receive_serial_no_details.product_id and dd.color_id=receive_serial_no_details.color_id and dd.product_version_id=receive_serial_no_details.product_version_id and receive_serial_no_details.deliver_master_id=" + delivery_master_id + " FOR XML PATH('')) ,1,1,'') AS Txt)as imei_no ,dm.remarks ,dd.delivered_quantity ,(dd.requisition_quantity-dd.delivered_quantity) as deliverable_quantity , dd.unit_price,dm.total_amount,dd.line_total , dd.delivery_details_id FROM delivery_master dm inner join delivery_details dd on dm.delivery_master_id =dd.delivery_master_id left join receive_serial_no_details rsnd on dd.product_id =rsnd.product_id and dd.color_id =rsnd.color_id and dd.delivery_master_id = rsnd.deliver_master_id inner join product pro on dd.product_id = pro.product_id left join color col on dd.color_id= col.color_id left join warehouse whf on dm.from_warehouse_id= whf.warehouse_id left join warehouse wht on dm.to_warehouse_id= wht.warehouse_id inner join party par on dm.party_id= par.party_id inner join region reg on par.region_id = reg.region_id inner join area are on par.area_id = are.area_id inner join territory ter on par.territory_id = ter.territory_id inner join internal_requisition_master rm on dm.requisition_master_id= rm.internal_requisition_master_id inner join brand bd on pro.brand_id= bd.brand_id inner join product_category pc on pro.product_category_id= pc.product_category_id left join users usr on dm.delivered_by = usr.user_id where dm.delivery_master_id=" + delivery_master_id + " group by dm.delivery_no ,reg.region_name ,are.area_name ,ter.territory_name ,dm.delivery_date ,usr.full_name ,rm.internal_requisition_no ,par.party_name , whf.warehouse_name ,wht.warehouse_name , pro.product_name , col.color_name , dm.remarks , dd.delivered_quantity ,dd.product_id,dd.color_id ,dd.product_version_id , dd.requisition_quantity , dd.unit_price , dm.total_amount , bd.brand_name ,pc.product_category_name , dd.line_total,dm.lot_no , dd.delivery_details_id order by delivery_details_id asc";
                var reportData = _entities.Database.SqlQuery<DeliveryReportModel>(query).ToList();

                return reportData;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public object GetInternalEmiInvoiceReport(long internal_requisition_master_id, long user_id)
        {
            try
            {
                string query1 =
                    "select distinct oim.invoice_no ,oid.is_gift ,oim.invoice_date ,users.full_name ,cus.customar_name ,cus.mobile_no ,cus.internal_requisition_no ,cus.requisition_date as requisition_code ,cus.payment_type ,sbu.sbu_name as department_name ,product.product_name ,product_category.product_category_name ,color.color_name ,unit.unit_name ,brand.brand_name ,oid.quantity ,oid.price ,oid.line_total from invoice_master oim inner join invoice_details oid on oim.invoice_master_id=oid.invoice_master_id inner join internal_requisition_master cus on oim.requisition_master_id=cus.internal_requisition_master_id inner join product on oid.product_id=product.product_id inner join product_category on product.product_category_id=product_category.product_category_id left join product_version on oid.product_version_id=product_version.product_version_id left join color on oid.color_id=color.color_id left join unit on oid.unit_id=unit.unit_id left join brand on oid.brand_id=brand.brand_id inner join sbu on cus.to_department=sbu.sbu_id left join users on oim.created_by=users.user_id where oim.requisition_master_id=" + internal_requisition_master_id;

                var invoieData = _entities.Database.SqlQuery<InternalInvoiceReportModel>(query1).ToList();
                var list1 = new List<InternalInvoiceReportModel>(invoieData);

                string query2 = "select * from installment_details where installment_details.internal_requisition_master_id=" + internal_requisition_master_id + "";
                var installmentData = _entities.Database.SqlQuery<InstallmentDetailsModel>(query2);
                var list2 = new List<InstallmentDetailsModel>(installmentData);

                var internalEmiCombined = new InternalEmiCombinedInvoiceReportModel();
                internalEmiCombined.InternalInvoiceReportModel = list1;
                internalEmiCombined.InstallmentDetailsModel = list2;
                return internalEmiCombined;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public object GetAllInternalEmiDeliveryList()
        {
            try
            {
                var data = (from dm in _entities.delivery_master
                            join p in _entities.parties on dm.party_id equals p.party_id into tempP
                            from p in tempP.DefaultIfEmpty()
                            join r in _entities.internal_requisition_master on dm.requisition_master_id equals r.internal_requisition_master_id into tempr
                            from r in tempr.DefaultIfEmpty()
                            where dm.status == "EMI-Delivered"
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
                                requisition_code = r.internal_requisition_no,
                                internal_requisition_master_id = r.internal_requisition_master_id,
                                created_date = r.created_date,
                                receive_date = dm.receive_date //== null ? "Pending" : dm.receive_date.ToString()


                            }).OrderByDescending(e => e.delivery_master_id).ToList();

                return data;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public bool ConfirmDelivery(long delivery_master_id, long user_id)
        {
            try
            {
                //Update Delivery Master
                var deliveryMaster =
                    _entities.delivery_master.SingleOrDefault(dm => dm.delivery_master_id == delivery_master_id);
                deliveryMaster.status = "EMI-Delivered";
                deliveryMaster.delivered_by = user_id;
                deliveryMaster.delivery_date = DateTime.Now;
                deliveryMaster.receive_by = user_id;
                deliveryMaster.receive_date = DateTime.Now;
                _entities.SaveChanges();
                //Get Delivery Master Id In receive Serial table
                var receiveSerialList =
                    _entities.receive_serial_no_details.Where(rsd => rsd.deliver_master_id == delivery_master_id)
                        .ToList();

                var detailsList = _entities.delivery_details.Where(d => d.delivery_master_id == delivery_master_id).ToList();

                InventoryRepository updateInventoty = new InventoryRepository();

                var intransitWarehouseId =
                    _entities.warehouses.SingleOrDefault(k => k.warehouse_name == "In Transit").warehouse_id;

                //Get To Warehouse Id
                var toWarehouseId =
                    _entities.warehouses.SingleOrDefault(
                        a => a.warehouse_name == "Internal EMI Warehouse").warehouse_id;



                foreach (var item in detailsList)

                    //'39' virtual in-transit warehouse-----------------------------
                    updateInventoty.UpdateInventory("INT-EMI-REQ-CONFIRM", deliveryMaster.delivery_no, intransitWarehouseId,
                        toWarehouseId, item.product_id ?? 0, item.color_id ?? 0, item.product_version_id ?? 0,
                        item.unit_id ?? 0,
                        item.delivered_quantity ?? 0, user_id);

                // Update Receive Serial
                foreach (var item in receiveSerialList)
                {
                    receive_serial_no_details receiveSerial = _entities.receive_serial_no_details.FirstOrDefault(rsd => rsd.receive_serial_no_details_id == item.receive_serial_no_details_id);

                    receiveSerial.current_warehouse_id = deliveryMaster.to_warehouse_id;
                    receiveSerial.deliver_date = DateTime.Now;
                    receiveSerial.sales_status = true;
                    receiveSerial.sales_date = DateTime.Now;
                    _entities.SaveChanges();

                }
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CancelDelivery(long delivery_master_id, long user_id)
        {
            try
            {


                //Cancel Delivery Master
                var deliveryMaster =
                    _entities.delivery_master.SingleOrDefault(dm => dm.delivery_master_id == delivery_master_id);

                deliveryMaster.status = "Cancel";
                deliveryMaster.updated_by = user_id;
                deliveryMaster.updated_date = DateTime.Now;
                _entities.SaveChanges();

                //Cancel Internal Requisition Master

                var intReqMaster =
                    _entities.internal_requisition_master.SingleOrDefault(
                        inrm => inrm.internal_requisition_master_id == deliveryMaster.requisition_master_id);
                intReqMaster.requisition_status = "Cancel";
                _entities.SaveChanges();

                var intransitWarehouseId =
                    _entities.warehouses.SingleOrDefault(k => k.warehouse_name == "In Transit").warehouse_id;

                //Cancel Inventory
                var details =
                     _entities.delivery_details.Where(a => a.delivery_master_id == delivery_master_id).ToList();
                foreach (var deliveryDetails in details)
                {
                    var updateInventoty = new InventoryRepository();

                    //From warehouse== Internal Requisition To warehouse=Central
                    updateInventoty.UpdateInventory("INT-EMI-REQ-CANCEL", deliveryMaster.delivery_no, intransitWarehouseId, deliveryMaster.from_warehouse_id ?? 0, deliveryDetails.product_id ?? 0, deliveryDetails.color_id ?? 0, deliveryDetails.product_version_id ?? 0, 1,
                        deliveryDetails.delivered_quantity ?? 0, user_id);
                }



                //Cancel Receive Serial Details
                var serialNo =
                       _entities.receive_serial_no_details.Where(a => a.deliver_master_id == deliveryMaster.delivery_master_id)
                           .ToList();
                //Get Central warehouse
                var centralWarehouseId =
                              _entities.warehouses.SingleOrDefault(k => k.warehouse_name == "WE Central Warehouse").warehouse_id;

                foreach (var receiveSerialNo in serialNo)
                {
                    receive_serial_no_details receiveSerial = _entities.receive_serial_no_details.FirstOrDefault(r => r.receive_serial_no_details_id == receiveSerialNo.receive_serial_no_details_id);

                    receiveSerial.current_warehouse_id = centralWarehouseId;
                    receiveSerial.party_id = 0;
                    receiveSerial.deliver_date = null;
                    receiveSerial.sales_status = false;
                    receiveSerial.sales_date = null;
                    receiveSerial.is_gift = false;
                    receiveSerial.gift_type = "";
                    receiveSerial.deliver_master_id = 0;
                    receiveSerial.requisition_id = 0;

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