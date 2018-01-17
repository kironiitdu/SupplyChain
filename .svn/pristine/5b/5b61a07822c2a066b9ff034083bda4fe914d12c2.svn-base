using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text;
using DMSApi.Models.crystal_models;
using DMSApi.Models.IRepository;
using DMSApi.Models.StronglyType;

namespace DMSApi.Models.Repository
{
    public class MrrRepository : IMrrRepository
    {
        private DMSEntities _entities;
        private IMailRepository mailRepository;
        private INotifierMailAccountRepository notifierMailAccountRepository;

        public MrrRepository()
        {
            _entities = new DMSEntities();
            this.mailRepository = new MailRepository();
            this.notifierMailAccountRepository = new NotifierMailAccountRepository();
        }

        public object GetGrnByGrnMasterId(long grnMasterId)
        {
            try
            {
                var mrrModel = new MrrModel();

                mrrModel.GrnMasterData = _entities.grn_master.Find(grnMasterId);

                mrrModel.GrnDetailsList =
                    _entities.grn_details
                        .Join(_entities.products, jp => jp.product_id, p => p.product_id, (jp, p) => new { jp, p })
                        .Join(_entities.brands, jb => jb.jp.brand_id, b => b.brand_id, (jb, b) => new { jb, b })
                         .GroupJoin(_entities.colors, jc => jc.jb.jp.color_id, c => c.color_id, (jc, c) => new { jc, nc = c.FirstOrDefault() })
                        .Join(_entities.units, ju => ju.jc.jb.jp.unit_id, u => u.unit_id, (ju, u) => new { ju, u })
                         .GroupJoin(_entities.product_version, jpv => jpv.ju.jc.jb.jp.product_version_id, pv => pv.product_version_id, (jpv, pv) => new { jpv, vc = pv.FirstOrDefault() })
                          .Join(_entities.grn_master, jgrn => jgrn.jpv.ju.jc.jb.jp.grn_master_id, grn => grn.grn_master_id, (jgrn, grn) => new { jgrn, grn })
                        .Where(k => k.jgrn.jpv.ju.jc.jb.jp.grn_master_id == grnMasterId )
                        //.Where(k => k.jgrn.jpv.ju.jc.jb.jp.grn_master_id == grnMasterId && k.jgrn.jpv.ju.jc.jb.p.has_serial == true)
                        .Select(i => new GrnDetailsModel
                        {
                            grn_details_id = i.jgrn.jpv.ju.jc.jb.jp.grn_details_id,
                            product_id = i.jgrn.jpv.ju.jc.jb.jp.product_id,
                            product_name = i.jgrn.jpv.ju.jc.jb.p.product_name,
                            has_serial = (bool)i.jgrn.jpv.ju.jc.jb.p.has_serial,
                            brand_id = i.jgrn.jpv.ju.jc.jb.jp.brand_id,
                            brand_name = i.jgrn.jpv.ju.jc.b.brand_name,
                            color_id = i.jgrn.jpv.ju.nc.color_id,
                            color_name = string.IsNullOrEmpty(i.jgrn.jpv.ju.nc.color_name) ? "" : i.jgrn.jpv.ju.nc.color_name,
                            receive_quantity = i.jgrn.jpv.ju.jc.jb.jp.receive_quantity,
                            pi_quantity = i.jgrn.jpv.ju.jc.jb.jp.pi_quantity,
                            product_version_id = i.jgrn.vc.product_version_id,
                            product_version_name = string.IsNullOrEmpty(i.jgrn.vc.product_version_name) ? "" : i.jgrn.vc.product_version_name,
                            unit_price = i.jgrn.jpv.ju.jc.jb.jp.unit_price,
                            line_total = i.jgrn.jpv.ju.jc.jb.jp.line_total,
                            grn_master_id = i.jgrn.jpv.ju.jc.jb.jp.grn_master_id,
                            unit_id = i.jgrn.jpv.u.unit_id,
                            unit_name = i.jgrn.jpv.u.unit_name,
                            vat_pcnt = i.jgrn.jpv.ju.jc.jb.jp.vat_pcnt,
                            tax_pcnt = i.jgrn.jpv.ju.jc.jb.jp.tax_pcnt,
                            vat_amount = i.jgrn.jpv.ju.jc.jb.jp.vat_amount,
                            tax_amount = i.jgrn.jpv.ju.jc.jb.jp.tax_amount,
                            amount = i.jgrn.jpv.ju.jc.jb.jp.amount,
                            company_id = i.grn.company_id ?? 0
                        }).OrderByDescending(p => p.grn_details_id).ToList();


                return mrrModel;

            }
            catch (Exception ex)
            {

                return ex;
            }
        }


        public object GetAllGrnNo()
        {
            try
            {
                var data = (from grnMaster in _entities.grn_master
                            join sup in _entities.suppliers on grnMaster.supplier_id equals sup.supplier_id into tempSup
                            from sup in tempSup.DefaultIfEmpty()
                            join pom in _entities.purchase_order_master on grnMaster.purchase_order_master_id equals pom.purchase_order_master_id into tempPom
                            from pom in tempPom.DefaultIfEmpty()
                            join com in _entities.companies on grnMaster.company_id equals com.company_id
                            where grnMaster.mrr_status == false
                            select new
                            {
                                grnMaster.grn_master_id,
                                grnMaster.grn_no,
                                grnMaster.mrr_status,
                                com.company_id,
                                sup.supplier_id
                            }).OrderByDescending(e => e.grn_master_id).ToList();

                return data;
            }
            catch (Exception ex)
            {

                return ex;
            }
        }


        public long CreateMrr(MrrModel objMrrModel)
        {
            try
            {
                var mrrMaster = objMrrModel.MrrMasterData;
                var mrrDetailsList = objMrrModel.MrrDetailsList;

                var fromWarehouseId = mrrMaster.from_warehouse_id;
                long saleableWarehouseId = 1;
                long mrrSerial = _entities.mrr_master.Max(mr => (long?)mr.mrr_master_id) ?? 0;
                mrrSerial++;
                var mrrStr = mrrSerial.ToString(CultureInfo.InvariantCulture).PadLeft(7, '0');

                //Insert Mrr Master Table
                string mrrNo = "MRR-" + mrrStr;
                mrrMaster.mrr_no = mrrNo;
                mrrMaster.grn_master_id = objMrrModel.MrrMasterData.grn_master_id;
                mrrMaster.lost_comment = objMrrModel.MrrMasterData.lost_comment;
                mrrMaster.created_by = objMrrModel.MrrMasterData.created_by;
                mrrMaster.created_date = DateTime.Now;
                _entities.mrr_master.Add(mrrMaster);
                _entities.SaveChanges();

                long mrrMasterId = mrrMaster.mrr_master_id;



                // at first updating all imeis a saleable
                //var receiveSerialNoDetails =
                //    _entities.receive_serial_no_details.Where(m => m.grn_master_id == mrrMaster.grn_master_id).ToList();
                //foreach (var serialObj in receiveSerialNoDetails)
                //{
                //    serialObj.received_warehouse_id = saleableWarehouseId;
                //    serialObj.current_warehouse_id = saleableWarehouseId;
                //    serialObj.mrr_status = true;
                //    serialObj.mrr_saleable = true;
                //    serialObj.mrr_box_damage = false;
                //    serialObj.mrr_physical_damage = false;
                //    serialObj.customs_lost = false;
                //    _entities.SaveChanges();
                //}

                int updatingReceiveSerials = _entities.Database.ExecuteSqlCommand("UPDATE receive_serial_no_details SET mrr_status=1 , mrr_saleable=1,mrr_box_damage=0 ,mrr_physical_damage=0,customs_lost=0, received_warehouse_id='" + saleableWarehouseId + "', current_warehouse_id='" + saleableWarehouseId + "' WHERE grn_master_id='" + mrrMaster.grn_master_id + "'");
                _entities.SaveChanges();
                // Insert Mrr Details Table with damaged/lost products
                foreach (var item in mrrDetailsList)
                {
                    var mrrDetail = new mrr_details
                    {
                        mrr_master_id = mrrMasterId,
                        product_id = item.product_id,
                        color_id = item.color_id,
                        product_version_id = item.product_version_id,
                        grn_quantity = item.grn_quantity,
                        saleable_quantity = item.saleable_quantity,
                        physical_damaged_quantity = item.physical_damaged_quantity,
                        box_damage_quantity = item.box_damage_quantity,
                        customs_lost_quantity = item.customs_lost_quantity,
                        total_received_quantity = item.total_received_quantity
                    };
                    _entities.mrr_details.Add(mrrDetail);
                    int save = _entities.SaveChanges();

                    //updateInventoty.UpdateInventory()
                    if (save > 0)
                    {
                        var updateInventoty = new InventoryRepository();

                        if (item.saleable_quantity > 0)
                        {
                            updateInventoty.UpdateInventory("MRR-Saleable", mrrMaster.mrr_no, fromWarehouseId??0,
                                saleableWarehouseId, item.product_id ?? 0, item.color_id ?? 0, item.product_version_id ?? 0, 1,
                                item.saleable_quantity ?? 0, mrrMaster.created_by ?? 0);
                        }

                        if (item.physical_damaged_quantity > 0)
                        {
                            var physicalwarehouseId = _entities.warehouses.SingleOrDefault(w => w.warehouse_name == "Physical Damage Warehouse")
                                .warehouse_id;
                            updateInventoty.UpdateInventory("MRR-Physical Damage", mrrMaster.mrr_no, fromWarehouseId ?? 0,
                                physicalwarehouseId, item.product_id ?? 0, item.color_id ?? 0, item.product_version_id ?? 0, 1,
                                item.physical_damaged_quantity ?? 0, mrrMaster.created_by ?? 0);
                        }
                        
                        if (item.box_damage_quantity > 0)
                        {
                            var boxDamagewarehouseId = _entities.warehouses.SingleOrDefault(w => w.warehouse_name == "Box Damage Warehouse")
                                .warehouse_id;
                            updateInventoty.UpdateInventory("MRR-Box Damage", mrrMaster.mrr_no, fromWarehouseId??0,
                                boxDamagewarehouseId, item.product_id ?? 0, item.color_id ?? 0, item.product_version_id ?? 0, 1,
                                item.box_damage_quantity ?? 0, mrrMaster.created_by ?? 0);
                        }
                          
                        if (item.customs_lost_quantity > 0)
                        {
                            var customLostwarehouseId = _entities.warehouses.SingleOrDefault(w => w.warehouse_name == "Customs Lost")
                            .warehouse_id;
                            updateInventoty.UpdateInventory("MRR-Customs Lost", mrrMaster.mrr_no, fromWarehouseId??0,
                                customLostwarehouseId, item.product_id ?? 0, item.color_id ?? 0, item.product_version_id ?? 0, 1,
                                item.customs_lost_quantity ?? 0, mrrMaster.created_by ?? 0);
                        }
                    }

                }

                //Update Receive Serial No Details
                var receiveSerialDetails = objMrrModel.ReceiveSerialNoDetails;
                

                foreach (var item in receiveSerialDetails)
                {
                    var receiveSerialNo =
                        _entities.receive_serial_no_details.SingleOrDefault(mrr => mrr.receive_serial_no_details_id == item.receive_serial_no_details_id);
                    if (item.damage_type_name == "Box Damage")
                    {
                        var singleOrDefault = _entities.warehouses.SingleOrDefault(w => w.warehouse_name == "Box Damage Warehouse");
                        if (singleOrDefault != null)
                        {
                            var warehouseId = singleOrDefault
                                .warehouse_id;
                            receiveSerialNo.mrr_box_damage = true;
                            receiveSerialNo.mrr_status = true;
                            receiveSerialNo.mrr_saleable = false;
                            receiveSerialNo.current_warehouse_id = warehouseId;
                            _entities.SaveChanges();
                        }
                    }
                    if (item.damage_type_name == "Physical Damage")
                    {
                        var warehouseId = _entities.warehouses.SingleOrDefault(w => w.warehouse_name == "Physical Damage Warehouse")
                          .warehouse_id;
                        receiveSerialNo.mrr_physical_damage = true;
                        receiveSerialNo.mrr_status = true;
                        receiveSerialNo.mrr_saleable = false;
                        receiveSerialNo.current_warehouse_id = warehouseId;
                        _entities.SaveChanges();

                    }
                    if (item.damage_type_name == "Customs Lost")
                    {
                        var warehouseId = _entities.warehouses.SingleOrDefault(w => w.warehouse_name == "Customs Lost")
                          .warehouse_id;
                        receiveSerialNo.customs_lost = true;
                        receiveSerialNo.mrr_status = true;
                        receiveSerialNo.mrr_saleable = false;
                        receiveSerialNo.current_warehouse_id = warehouseId;
                        _entities.SaveChanges();
                    }
                }
                //Update Mrr Status in Grn Master
                var mrrStatus = _entities.grn_master.Find(mrrMaster.grn_master_id);
                mrrStatus.mrr_status = true;

                //Get Mail Data
                int counter = 0;
                var rEmail = "";

                 var dataSmtp = _entities.notifier_mail_account.FirstOrDefault(s => s.is_active == true);
                
                var dataReceiver = (from mrs in _entities.mail_receiver_setting
                            join spm in _entities.software_process_module on mrs.process_code_id equals spm.process_code_id
                            select new
                            {
                                mail_receiver_setting_id = mrs.mail_receiver_setting_id,
                                process_code_name = spm.process_code_name,
                                process_code_id = spm.process_code_id,
                                receiver_name = mrs.receiver_name,
                                receiver_email = mrs.receiver_email,
                                is_active = mrs.is_active,
                                is_deleted = mrs.is_deleted,
                                created_by = mrs.created_by,
                                created_date = mrs.created_date,
                                updated_by = mrs.updated_by,
                                updated_date = mrs.updated_date
                            }).Where(c => c.is_deleted != true && c.process_code_name=="MRR").OrderByDescending(c => c.mail_receiver_setting_id).ToList();

                var dataProcess = (from mrs in _entities.process_wise_mail_setting
                            join spm in _entities.software_process_module on mrs.process_code_id equals spm.process_code_id
                            select new
                            {
                                process_wise_mail_setting_id = mrs.process_wise_mail_setting_id,
                                process_code_name = spm.process_code_name,
                                process_code_id = spm.process_code_id,
                                email_body = mrs.email_body,
                                email_subject = mrs.email_subject,
                                is_active = mrs.is_active,
                                is_deleted = mrs.is_deleted,
                                created_by = mrs.created_by,
                                created_date = mrs.created_date,
                                updated_by = mrs.updated_by,
                                updated_date = mrs.updated_date
                            }).FirstOrDefault(c => c.is_deleted != true && c.process_code_name=="MRR");



                //get current Mrr information------------------------
                var mrrInformation = this.GetMrrInformationById(mrrMasterId);

                StringBuilder sb = new StringBuilder();
                sb.Append("<h4 style='color: red'>" + dataProcess.email_body + "</h4>");
                sb.Append("<table>");
                sb.Append("<tr>");
                sb.Append("<td><b>MRR No</b></td>");
                sb.Append("<td><b>: <span style='color: red'>" + mrrInformation[0].mrr_no + "</span></b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td><b>GRN No</b></td>");
                sb.Append("<td><b>: " + mrrInformation[0].grn_no + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td><b>MRR Date</b></td>");
                sb.Append("<td><b>: " + mrrInformation[0].created_date + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td><b>Created By</b></td>");
                sb.Append("<td><b>: " + mrrInformation[0].created_by + "</b></td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("<br/>");
                sb.Append("<table border='1px' cellpadding='7'>");
                sb.Append("<tr>");
                sb.Append("<th>Product Name</th>");
                sb.Append("<th>Color</th>");
                sb.Append("<th>Version</th>");
                sb.Append("<th>GRN Qty</th>");
                sb.Append("<th>Saleable</th>");
                sb.Append("<th>Custom Lost</th>");
                sb.Append("<th>Box Damage</th>");
                sb.Append("<th>Physical Damage</th>");
                sb.Append("<th>Total Received</th>");
                sb.Append("</tr>");
                foreach (var item in mrrInformation)
                {
                    sb.Append("<tr align='center'>");
                    sb.Append("<td>" + item.product_name + "</td>");
                    sb.Append("<td>" + item.color_name + "</td>");
                    sb.Append("<td>" + item.product_version_name + "</td>");
                    sb.Append("<td>" + item.grn_quantity + "</td>");
                    sb.Append("<td>" + item.saleable_quantity + "</td>");
                    sb.Append("<td>" + item.customs_lost_quantity + "</td>");
                    sb.Append("<td>" + item.box_damage_quantity + "</td>");
                    sb.Append("<td>" + item.physical_damaged_quantity + "</td>");
                    sb.Append("<td>" + item.total_received_quantity + "</td>");
                    sb.Append("</tr>");
                }
                sb.Append("</table>");



                string mrrNumberEmailBody = sb.ToString();

                foreach (var item in dataReceiver)
                {
                    if (counter==0)
                    {
                        rEmail=item.receiver_email;
                    }
                    rEmail+=","+item.receiver_email;
                    counter++;
                }



                //Send Confirmation Mail
                mailRepository.SendMail(dataSmtp.account_email, rEmail, dataProcess.email_subject,
                    mrrNumberEmailBody, dataSmtp.account_email, dataSmtp.accoutn_password, "");
                
                _entities.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }


        public object GetImeiForMrr(long imei1, int warehouseId)
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


        public object GetAllMrr()
        {
            try
            {
                var mrrData = _entities.mrr_master
                    .GroupJoin(_entities.mrr_details, jmrrd => jmrrd.mrr_master_id, mrrd => mrrd.mrr_master_id,
                        (jmrrd, mrrd) => new { jmrrd, jmrd = mrrd.FirstOrDefault() })
                    .GroupJoin(_entities.grn_master, jgrnm => jgrnm.jmrrd.grn_master_id, grnm => grnm.grn_master_id,
                        (jgrnm, grnm) => new { jgrnm, grn = grnm.FirstOrDefault() })
                    .GroupJoin(_entities.users, jusr => jusr.jgrnm.jmrrd.created_by, usr => usr.user_id,
                        (jusr, usr) => new { jusr, us = usr.FirstOrDefault() })

                    .Select(m => new MrrDataModelForGrid
                    {
                        mrr_master_id = m.jusr.jgrnm.jmrd.mrr_master_id ?? 0,
                        mrr_no = m.jusr.jgrnm.jmrrd.mrr_no,
                        grn_no = m.jusr.grn.grn_no,
                        grn_date = (DateTime)m.jusr.grn.grn_date,
                        grn_quantity = (int)_entities.mrr_details.Sum(sm=>sm.grn_quantity),
                        saleable_quantity = (int)_entities.mrr_details.Sum(sm=>sm.saleable_quantity),
                        total_received_quantity = (int)_entities.mrr_details.Sum(sm=>sm.total_received_quantity),
                        box_damage_quantity = (int)_entities.mrr_details.Sum(sm => sm.box_damage_quantity),
                        physical_damaged_quantity = m.jusr.jgrnm.jmrd.physical_damaged_quantity,
                        customs_lost_quantity =   (int)_entities.mrr_details.Sum(sm => sm.customs_lost_quantity),
                        created_date = (DateTime)m.jusr.jgrnm.jmrrd.created_date,
                        created_by = m.us.full_name

                    }).OrderByDescending(mrr => mrr.mrr_master_id).ToList();
                return mrrData;


            }
            catch (Exception ex)
            {

                return ex;
            }
        }


        public object GetMrrReportById(long mrrMasterId)
        {
            try
            {

                string query = "select mrr_master.mrr_no,grn_master.grn_no,grn_master.grn_date,mrr_master.grn_master_id,mrr_master.lost_comment,mrr_master.created_date,users.full_name as created_by, product.product_name,color.color_name,product_version.product_version_name, mrr_details.grn_quantity,mrr_details.saleable_quantity,mrr_details.box_damage_quantity,mrr_details.physical_damaged_quantity, mrr_details.customs_lost_quantity,mrr_details.total_received_quantity from mrr_master inner join mrr_details on mrr_master.mrr_master_id = mrr_details.mrr_master_id inner join grn_master on mrr_master.grn_master_id=grn_master.grn_master_id left join product on mrr_details.product_id=product.product_id left join color on mrr_details.color_id=color.color_id left join product_version on mrr_details.product_version_id=product_version.product_version_id left join users on mrr_master.mrr_master_id=users.user_id where mrr_master.mrr_master_id='" + mrrMasterId + "'";
                var mrrDeta = _entities.Database.SqlQuery<MRRReportModel>(query).ToList();

                return mrrDeta;
                    //var mrrData = _entities.mrr_master
                    //.GroupJoin(_entities.mrr_details, jmrrd => jmrrd.mrr_master_id, mrrd => mrrd.mrr_master_id,
                    //     (jmrrd, mrrd) => new { jmrrd, jmrd = mrrd.FirstOrDefault() })
                    //.GroupJoin(_entities.grn_master, jgrnm => jgrnm.jmrrd.grn_master_id, grnm => grnm.grn_master_id,
                    //     (jgrnm, grnm) => new { jgrnm, grn = grnm.FirstOrDefault() })
                    //.GroupJoin(_entities.users, jusr => jusr.jgrnm.jmrrd.created_by, usr => usr.user_id,
                    //    (jusr, usr) => new { jusr, us = usr.FirstOrDefault() })
                    //.GroupJoin(_entities.products, jpro => jpro.jusr.jgrnm.jmrrd.created_by, pro => pro.product_id,
                    //    (jpro, pro) => new { jpro, p = pro.FirstOrDefault() })
                    //.GroupJoin(_entities.colors, jcolo => jcolo.jpro.jusr.jgrnm.jmrrd.created_by, col => col.color_id,
                    //    (jcolo, col) => new { jcolo, co = col.FirstOrDefault() })
                    //.GroupJoin(_entities.product_version, jver => jver.jcolo.jpro.jusr.jgrnm.jmrrd.created_by, ver => ver.product_version_id,
                    //    (jver, ver) => new { jver, vr = ver.FirstOrDefault() })
                    //.Where(mrr => mrr.jver.jcolo.jpro.jusr.jgrnm.jmrrd.mrr_master_id == mrr_master_id)
                    //.Select(m => new MRRReportModel
                    //{
                    //        mrr_master_id = m.jver.jcolo.jpro.jusr.jgrnm.jmrd.mrr_master_id ?? 0,
                    //        mrr_no = m.jver.jcolo.jpro.jusr.jgrnm.jmrrd.mrr_no,
                    //        grn_no = m.jver.jcolo.jpro.jusr.grn.grn_no,
                    //        grn_date = (DateTime)m.jver.jcolo.jpro.jusr.grn.grn_date,
                    //        grn_quantity = (int)m.jver.jcolo.jpro.jusr.jgrnm.jmrd.grn_quantity,
                    //        saleable_quantity = (int)m.jver.jcolo.jpro.jusr.jgrnm.jmrd.saleable_quantity,
                    //        total_received_quantity = (int)m.jver.jcolo.jpro.jusr.jgrnm.jmrd.total_received_quantity,
                    //        box_damage_quantity = m.jver.jcolo.jpro.jusr.jgrnm.jmrd.box_damage_quantity??0,
                    //        physical_damaged_quantity = m.jver.jcolo.jpro.jusr.jgrnm.jmrd.physical_damaged_quantity??0,
                    //        customs_lost_quantity = m.jver.jcolo.jpro.jusr.jgrnm.jmrd.customs_lost_quantity??0,
                    //        created_date = (DateTime)m.jver.jcolo.jpro.jusr.jgrnm.jmrrd.created_date,
                    //        created_by = m.jver.jcolo.jpro.us.full_name,                           
                    //        product_name=string.IsNullOrEmpty(m.jver.jcolo.p.product_name) ? "" : m.jver.jcolo.p.product_name,
                    //        color_name = string.IsNullOrEmpty(m.jver.co.color_name) ? "" : m.jver.co.color_name,
                    //        product_version_name = string.IsNullOrEmpty(m.vr.product_version_name) ? "" : m.vr.product_version_name

                    //}).OrderByDescending(mrr => mrr.mrr_master_id).ToList();
                    //        return mrrData;


            }
            catch (Exception ex)
            {

                return ex;
            }
        }

        public List<MRRReportModel> GetMrrInformationById(long mrrMasterId)
        {
            string query = "select mrr_master.mrr_no,grn_master.grn_no,grn_master.grn_date,mrr_master.grn_master_id,mrr_master.lost_comment,mrr_master.created_date,users.full_name as created_by, product.product_name,color.color_name,product_version.product_version_name, mrr_details.grn_quantity,mrr_details.saleable_quantity,mrr_details.box_damage_quantity,mrr_details.physical_damaged_quantity, mrr_details.customs_lost_quantity,mrr_details.total_received_quantity from mrr_master inner join mrr_details on mrr_master.mrr_master_id = mrr_details.mrr_master_id inner join grn_master on mrr_master.grn_master_id=grn_master.grn_master_id left join product on mrr_details.product_id=product.product_id left join color on mrr_details.color_id=color.color_id left join product_version on mrr_details.product_version_id=product_version.product_version_id left join users on mrr_master.created_by=users.user_id where mrr_master.mrr_master_id='" + mrrMasterId + "'";
            var mrrDeta = _entities.Database.SqlQuery<MRRReportModel>(query).ToList();

            return mrrDeta;
        }


        public object GetAllEmail()
        {

            try
            {
                var allEmail = (from emp in _entities.employees
                    select new
                    {
                        email_address=emp.email_address
                    }).Where(e=>e.email_address !=null).ToList();
                return allEmail;

            }
            catch (Exception)
            {

                return null;
            }
        }


        public object GetLoginUserMail(long userId)
        {
            try
            {

                var loginUserMail = (from usr in _entities.users
                                     join emp in _entities.employees on usr.emp_id equals emp.employee_id into temEmp
                                     from emp in temEmp.DefaultIfEmpty()
                                     select new
                                     {
                                         email_address = emp.email_address ?? "Sorry Email Not set yet!"
                                     }).FirstOrDefault();
                return loginUserMail;
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}