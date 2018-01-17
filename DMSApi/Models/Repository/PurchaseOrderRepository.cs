using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using DMSApi.Models.crystal_models;
using DMSApi.Models.IRepository;
using DMSApi.Models.StronglyType;

namespace DMSApi.Models.Repository
{
    public class PurchaseOrderRepository : IPurchaseOrderRepository
    {
        private DMSEntities _entities;

        public PurchaseOrderRepository()
        {
            this._entities = new DMSEntities();
        }

        public object GetAllPurchaseOrders()
        {
            var data = (from pom in _entities.purchase_order_master
                        join sup in _entities.suppliers on pom.supplier_id equals sup.supplier_id
                        select new
                        {
                            purchase_order_master_id = pom.purchase_order_master_id,
                            order_no = pom.order_no,
                            order_date = pom.order_date,
                            supplier_id = pom.supplier_id,
                            pi_number = pom.pi_number,
                            created_date = pom.created_date,
                            supplier_name = sup.supplier_name,
                            vat_total = pom.vat_total,
                            tax_total = pom.tax_total,
                            is_active = pom.is_active,
                            is_deleted = pom.is_deleted,
                            verify_status = pom.verify_status,
                            approve_status = pom.approve_status,
                            total_amount_including_vattax = pom.total_amount_including_vattax,
                            total_amount_without_vattax = pom.total_amount_without_vattax

                        }).Where(e => e.is_active == true).OrderByDescending(e => e.purchase_order_master_id).ToList();

            return data;
        }


        public object GetAllPurchaseOrdersForVerify()
        {
            var data = (from pom in _entities.purchase_order_master
                        join sup in _entities.suppliers on pom.supplier_id equals sup.supplier_id
                        select new
                        {
                            purchase_order_master_id = pom.purchase_order_master_id,
                            order_no = pom.order_no,
                            order_date = pom.order_date,
                            supplier_id = pom.supplier_id,
                            created_date = pom.created_date,
                            supplier_name = sup.supplier_name,
                            vat_total = pom.vat_total,
                            tax_total = pom.tax_total,
                            is_active = pom.is_active,
                            is_deleted = pom.is_deleted,
                            verify_status = pom.verify_status,
                            approve_status = pom.approve_status,
                            total_amount_including_vattax = pom.total_amount_including_vattax,
                            total_amount_without_vattax = pom.total_amount_without_vattax

                        }).Where(e => e.verify_status == "Not Verified" && e.approve_status == "Not Approved" && e.is_active == true).OrderByDescending(e => e.purchase_order_master_id).ToList();

            return data;
        }

        public object GetAllVerifiedPurchaseOrders()
        {
            var data = (from pom in _entities.purchase_order_master
                        join sup in _entities.suppliers on pom.supplier_id equals sup.supplier_id
                        select new
                        {
                            purchase_order_master_id = pom.purchase_order_master_id,
                            order_no = pom.order_no,
                            order_date = pom.order_date,
                            supplier_id = pom.supplier_id,
                            created_date = pom.created_date,
                            supplier_name = sup.supplier_name,
                            vat_total = pom.vat_total,
                            tax_total = pom.tax_total,
                            is_active = pom.is_active,
                            is_deleted = pom.is_deleted,
                            verify_status = pom.verify_status,
                            approve_status = pom.approve_status,
                            total_amount_including_vattax = pom.total_amount_including_vattax,
                            total_amount_without_vattax = pom.total_amount_without_vattax

                        }).Where(e => e.verify_status == "Verified" && e.approve_status == "Not Approved" && e.is_active == true).OrderByDescending(e => e.purchase_order_master_id).ToList();

            return data;
        }

        public object GetAllApprovedPurchaseOrders()
        {
            var data = (from pom in _entities.purchase_order_master
                        join sup in _entities.suppliers on pom.supplier_id equals sup.supplier_id
                        select new
                        {
                            purchase_order_master_id = pom.purchase_order_master_id,
                            order_no = pom.order_no,
                            order_date = pom.order_date,
                            pi_number = pom.pi_number,
                            supplier_id = pom.supplier_id,
                            created_date = pom.created_date,
                            supplier_name = sup.supplier_name,
                            vat_total = pom.vat_total,
                            lc_number = pom.lc_number,
                            tax_total = pom.tax_total,
                            is_active = pom.is_active,
                            is_deleted = pom.is_deleted,
                            verify_status = pom.verify_status,
                            approve_status = pom.approve_status,
                            total_amount_including_vattax = pom.total_amount_including_vattax,
                            total_amount_without_vattax = pom.total_amount_without_vattax

                        }).Where(e => e.verify_status == "Verified" && e.approve_status == "Approved" && e.is_active == true && string.IsNullOrEmpty(e.lc_number)).OrderByDescending(e => e.purchase_order_master_id).ToList();

            return data;
        }

        public object GetAllApprovedPurchaseOrdersPiNo()
        {
            var data = (from pom in _entities.purchase_order_master
                        join sup in _entities.suppliers on pom.supplier_id equals sup.supplier_id
                        select new
                        {
                            purchase_order_master_id = pom.purchase_order_master_id,
                            order_no = pom.order_no,
                            order_date = pom.order_date,
                            pi_number = pom.pi_number,
                            supplier_id = pom.supplier_id,
                            created_date = pom.created_date,
                            supplier_name = sup.supplier_name,
                            vat_total = pom.vat_total,
                            lc_number = pom.lc_number,
                            tax_total = pom.tax_total,
                            is_active = pom.is_active,
                            is_deleted = pom.is_deleted,
                            verify_status = pom.verify_status,
                            approve_status = pom.approve_status,
                            total_amount_including_vattax = pom.total_amount_including_vattax,
                            total_amount_without_vattax = pom.total_amount_without_vattax

                        }).Where(e => e.verify_status == "Verified" && e.approve_status == "Approved" && e.is_active == true).OrderByDescending(e => e.purchase_order_master_id).ToList();

            return data;
        }

        public object GetAllApprovedPurchaseOrdersForDropDown()
        {
            var data = (from pom in _entities.purchase_order_master
                        join sup in _entities.suppliers on pom.supplier_id equals sup.supplier_id
                        select new
                        {
                            purchase_order_master_id = pom.purchase_order_master_id,
                            order_no = pom.order_no,
                            order_date = pom.order_date,
                            supplier_id = pom.supplier_id,
                            created_date = pom.created_date,
                            supplier_name = sup.supplier_name,
                            vat_total = pom.vat_total,
                            lc_number = pom.lc_number,
                            tax_total = pom.tax_total,
                            is_active = pom.is_active,
                            is_deleted = pom.is_deleted,
                            verify_status = pom.verify_status,
                            approve_status = pom.approve_status,
                            total_amount_including_vattax = pom.total_amount_including_vattax,
                            total_amount_without_vattax = pom.total_amount_without_vattax

                        }).Where(e => e.verify_status == "Verified" && e.approve_status == "Approved" && e.is_active == true).OrderByDescending(e => e.purchase_order_master_id).ToList();

            return data;
        }

        public bool UploadPiAttachment()
        {
            try
            {
                HttpRequest rsk = HttpContext.Current.Request;

                var postedFile = rsk.Files["UploadedImage"];
                long purchase_order_master_id = Convert.ToInt32(rsk.Form["purchase_order_master_id"]);

                string actualFileName = "";
                if (postedFile == null && rsk.Form["purchase_order_master_id"].ToString() == "")
                {
                    return false;//no file upload
                }
                else
                {
                    actualFileName = postedFile.FileName;
                    var xx = actualFileName.Split('.');
                    var ext = xx[xx.Length - 1];
                    var fileName = "PI_" + purchase_order_master_id + "." + ext;
                    var clientDir = HttpContext.Current.Server.MapPath("~/App_Data/PI_Attachments/");
                    var fileSavePath = Path.Combine(clientDir, fileName);

                    purchase_order_master oPurchaseOrderMaster = _entities.purchase_order_master.Find(purchase_order_master_id);
                    if (!string.IsNullOrEmpty(oPurchaseOrderMaster.pi_attachment_location))
                    {
                        var oldAttachment = oPurchaseOrderMaster.pi_attachment_location;
                        var fileDeletePath = Path.Combine(clientDir, oldAttachment);
                        if (File.Exists(fileDeletePath))
                        {
                            File.Delete(fileDeletePath);
                        }
                    }

                    postedFile.SaveAs(fileSavePath);

                    oPurchaseOrderMaster.pi_attachment_location = fileName;
                    _entities.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public HttpResponseMessage GetPiAttachment(long purchase_order_master_id)
        {
            try
            {
                // GET FILE NAME
                var document = _entities.purchase_order_master.Find(purchase_order_master_id);

                var fileName = document.pi_attachment_location;
                var clientDir = HttpContext.Current.Server.MapPath("~/App_Data/PI_Attachments/");
                var fileSavePath = Path.Combine(clientDir, fileName);

                //var path = System.Web.HttpContext.Current.Server.MapPath(fileSavePath); ;

                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                var stream = new FileStream(fileSavePath, FileMode.Open);
                result.Content = new StreamContent(stream);
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                result.Content.Headers.ContentDisposition.FileName = Path.GetFileName(fileSavePath);
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                result.Content.Headers.ContentLength = stream.Length;
                return result;  
            }
            catch (Exception)
            {
                return null;
            }
             
        }

        public bool VerifyPurchaseOrder(long purchase_order_master_id, long user_id)
        {
            try
            {
                purchase_order_master oPurchaseOrderMaster = _entities.purchase_order_master.Find(purchase_order_master_id);

                oPurchaseOrderMaster.verify_status = "Verified";
                oPurchaseOrderMaster.updated_by = user_id;
                oPurchaseOrderMaster.updated_date = DateTime.Now;
                _entities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool ApprovePurchaseOrder(long purchase_order_master_id, long user_id)
        {
            try
            {
                purchase_order_master oPurchaseOrderMaster = _entities.purchase_order_master.Find(purchase_order_master_id);

                oPurchaseOrderMaster.approve_status = "Approved";
                oPurchaseOrderMaster.updated_by = user_id;
                oPurchaseOrderMaster.updated_date = DateTime.Now;
                _entities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public object GetPurchaseOrdersReportById(long purchase_order_master_id)
        {
            try
            {
                string query = "select pro.product_name ,pcat.product_category_name ,company.company_name ,col.color_name ,pro_v.product_version_name ,pod.quantity ,pod.pi_quantity ,pom.lc_number ,pom.noc_number , pod.unit_price ,pom.status ,pom.total_amount_including_vattax ,pom.order_date ,pom.shipping_term , pom.vat_total , pom.tax_total ,pom.terms_n_condition ,pom.remarks ,pom.order_no ,usr.full_name ,pod.line_total ,pod.vat_pcnt,pod.tax_pcnt ,pod.vat_amount ,pod.tax_amount ,pod.amount ,pom.verify_status ,pom.pi_number ,sup.supplier_name ,sup.company_address ,sup.contact_person ,supplier_type.supplier_type_name ,currency.currency_name From purchase_order_details pod inner join purchase_order_master pom on pod.purchase_order_master_id =pom.purchase_order_master_id inner join product pro on pod.product_id = pro.product_id left join product_category pcat on pro.product_category_id=pcat.product_category_id left join product_version pro_v on pod.product_version_id = pro_v.product_version_id left join color col on pod.color_id= col.color_id left join users usr on pom.updated_by = usr.user_id left join company on pom.company_id=company.company_id inner join supplier sup on pom.supplier_id=sup.supplier_id inner join supplier_type on sup.supplier_type_id=supplier_type.supplier_type_id left join currency on pom.currency_id=currency.currency_id where pod.purchase_order_master_id=" + purchase_order_master_id + "";

                var poData = _entities.Database.SqlQuery<PoReportModel>(query).ToList();
                return poData;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public long AddPurchaseOrder(PurchaseOrderModel purchaseOrderModel)
        {
            try
            {
                var poMaster = purchaseOrderModel.PoMasterData;
                var poDetailsList = purchaseOrderModel.PoDetailsList;

                // generate order_no
                long poSerial = _entities.purchase_order_master.Max(po => (long?)po.purchase_order_master_id) ?? 0;

                if (poSerial != 0)
                {
                    poSerial++;

                }
                else
                {
                    poSerial++;
                }
                var poStr = poSerial.ToString().PadLeft(7, '0');

                //string orderNo = "ORD/" + DateTime.Now.Year + "/" + poSerial;
                string orderNo = "PO-" + poStr;
                poMaster.purchase_order_reference_number = purchaseOrderModel.PoMasterData.purchase_order_reference_number;
                poMaster.supplier_id = purchaseOrderModel.PoMasterData.supplier_id;
                poMaster.currency_id = purchaseOrderModel.PoMasterData.currency_id;
                poMaster.total_amount_including_vattax = purchaseOrderModel.PoMasterData.total_amount_including_vattax;
                poMaster.status = purchaseOrderModel.PoMasterData.status;
                poMaster.company_id = purchaseOrderModel.PoMasterData.company_id;
                poMaster.order_date = Convert.ToDateTime(purchaseOrderModel.PoMasterData.order_date);
                poMaster.shipping_terms_id = purchaseOrderModel.PoMasterData.shipping_terms_id;
                poMaster.shipping_method_id = purchaseOrderModel.PoMasterData.shipping_method_id;
                poMaster.expected_shipment_date = Convert.ToDateTime(purchaseOrderModel.PoMasterData.expected_shipment_date);
                poMaster.vat_total = purchaseOrderModel.PoMasterData.vat_total;
                poMaster.terms_n_condition = purchaseOrderModel.PoMasterData.terms_n_condition;
                poMaster.created_by = purchaseOrderModel.PoMasterData.created_by;
                poMaster.created_date = DateTime.Now;
                poMaster.updated_by = purchaseOrderModel.PoMasterData.updated_by;
                poMaster.updated_date = DateTime.Now;
                poMaster.remarks = purchaseOrderModel.PoMasterData.remarks;
                poMaster.is_active = true;
                poMaster.noc_number = purchaseOrderModel.PoMasterData.noc_number;
                poMaster.verify_status = "Not Verified";
                poMaster.approve_status = "Not Approved";
                poMaster.is_deleted = false;
                poMaster.batch_id = 0;
                poMaster.order_no = orderNo;
                poMaster.tax_total = purchaseOrderModel.PoMasterData.tax_total;
                poMaster.total_amount_without_vattax = purchaseOrderModel.PoMasterData.total_amount_without_vattax;

                _entities.purchase_order_master.Add(poMaster);
                _entities.SaveChanges();
                long purchaseOrderMasterId = poMaster.purchase_order_master_id;
                foreach (var item in poDetailsList)
                {
                    var poDetails = new purchase_order_details
                    {
                        purchase_order_master_id = purchaseOrderMasterId,
                        product_id = item.product_id,
                        unit_id = item.unit_id,
                        unit_price = item.unit_price,
                        quantity = item.quantity,
                        product_version_id = item.product_version_id,
                        amount = item.amount,
                        brand_id = item.brand_id,
                        color_id = item.color_id,
                        receive_qty = 0,
                        pi_quantity = 0,
                        vat_amount = item.vat_amount,
                        last_modified_date = DateTime.Now,
                        tax_amount = item.tax_amount,
                        line_total = item.line_total,
                        size_id = item.size_id,
                        status = item.status,
                        vat_pcnt = item.vat_pcnt,
                        tax_pcnt = item.tax_pcnt,
                        product_category_id = item.product_category_id

                    };

                    _entities.purchase_order_details.Add(poDetails);
                    _entities.SaveChanges();
                }
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }


        }

        public PurchaseOrderModel GetPurchaseOrderById(long purchase_order_master_id)
        {
            PurchaseOrderModel purchaseOrderModel = new PurchaseOrderModel();

            purchaseOrderModel.PoMasterData = _entities.purchase_order_master.Find(purchase_order_master_id);

            purchaseOrderModel.PoDetailsList =
                _entities.purchase_order_details
                    .Join(_entities.products, jp => jp.product_id, p => p.product_id, (jp, p) => new { jp, p })
                    .Join(_entities.product_category, jcat => jcat.jp.product_category_id,
                        cat => cat.product_category_id, (jcat, cat) => new { jcat, cat })
                    .Join(_entities.brands, jb => jb.jcat.jp.brand_id, b => b.brand_id, (jb, b) => new { jb, b })
                     .GroupJoin(_entities.colors, jc => jc.jb.jcat.jp.color_id, c => c.color_id, (jc, c) => new { jc, nc = c.FirstOrDefault() })
                    .Join(_entities.units, ju => ju.jc.jb.jcat.jp.unit_id, u => u.unit_id, (ju, u) => new { ju, u })
                   // .GroupJoin(_entities.product_version, jpv => jpv.ju.jc.jb.jcat.jp.product_version_id, jpv => jpv.product_version_id, (jpv, pp) => new { jpv, pp })
                     .GroupJoin(_entities.product_version, jpv => jpv.ju.jc.jb.jcat.jp.product_version_id,pv=>pv.product_version_id,(jpv,pv)=> new { jpv, vc=pv.FirstOrDefault() })
                    .Where(k => k.jpv.ju.jc.jb.jcat.jp.purchase_order_master_id == purchase_order_master_id)
                    .Select(i => new PurchaseOrderDetailsModel
                    {
                        purchase_order_details_id = i.jpv.ju.jc.jb.jcat.jp.purchase_order_details_id,
                        product_category_id = i.jpv.ju.jc.jb.cat.product_category_id,
                        product_category_name = i.jpv.ju.jc.jb.cat.product_category_name,
                        product_id = i.jpv.ju.jc.jb.jcat.jp.product_id,
                        product_name = i.jpv.ju.jc.jb.jcat.p.product_name,
                        has_serial = (bool)i.jpv.ju.jc.jb.jcat.p.has_serial,
                        brand_id = i.jpv.ju.jc.jb.jcat.jp.brand_id,
                        brand_name = i.jpv.ju.jc.b.brand_name,
                        color_id=i.jpv.ju.nc.color_id,
                        color_name = string.IsNullOrEmpty(i.jpv.ju.nc.color_name) ? "" : i.jpv.ju.nc.color_name,
                        quantity = i.jpv.ju.jc.jb.jcat.jp.quantity,
                        pi_quantity = i.jpv.ju.jc.jb.jcat.jp.pi_quantity,
                        product_version_id = i.vc.product_version_id,
                        product_version_name = string.IsNullOrEmpty(i.vc.product_version_name) ? "" : i.vc.product_version_name,
                        receive_qty = i.jpv.ju.jc.jb.jcat.jp.receive_qty,
                        unit_price = i.jpv.ju.jc.jb.jcat.jp.unit_price,
                        line_total = i.jpv.ju.jc.jb.jcat.jp.line_total,
                        purchase_order_master_id = i.jpv.ju.jc.jb.jcat.jp.purchase_order_master_id,
                        last_modified_date = i.jpv.ju.jc.jb.jcat.jp.last_modified_date,
                        unit_id = i.jpv.u.unit_id,
                        unit_name = i.jpv.u.unit_name,
                        status = i.jpv.ju.jc.jb.jcat.jp.status,
                        vat_pcnt = i.jpv.ju.jc.jb.jcat.jp.vat_pcnt,
                        tax_pcnt = i.jpv.ju.jc.jb.jcat.jp.tax_pcnt,
                        vat_amount = i.jpv.ju.jc.jb.jcat.jp.vat_amount,
                        tax_amount = i.jpv.ju.jc.jb.jcat.jp.tax_amount,
                        amount = i.jpv.ju.jc.jb.jcat.jp.amount
                    }).OrderByDescending(p => p.purchase_order_details_id).ToList();


            return purchaseOrderModel;

        }

        public object GetPurchaseOrderExcelData(string from_date, string to_date)
        {
            try
            {

                string baseQuiry = "select pom.order_no,to_char(to_date(pom.order_date,'DD/MM/YYYY'),'YYYY-MM-DD') as order_date,p.product_name,c.color_name,pod.quantity from purchase_order_master as pom inner join purchase_order_details pod on pom.purchase_order_master_id=pod.purchase_order_master_id inner join product p on p.product_id=pod.product_id inner join color c on c.color_id=pod.color_id WHERE to_date(pom.order_date, 'DD/MM/YYYY') BETWEEN to_date('" + from_date + "', 'DD/MM/YYYY') and to_date('" + to_date + "', 'DD/MM/YYYY') order by pom.order_no asc";

                var data = _entities.Database.SqlQuery<PurchaseOrderExcelModel>(baseQuiry).ToList();
                return data;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public bool EditPurchaseOrder(PurchaseOrderModel purchaseOrderModel)
        {
            try
            {
                var poMaster = purchaseOrderModel.PoMasterData;
                var poDetailsList = purchaseOrderModel.PoDetailsList;
                purchase_order_master masterData = _entities.purchase_order_master.Find(poMaster.purchase_order_master_id);

                masterData.supplier_id = purchaseOrderModel.PoMasterData.supplier_id;
                masterData.currency_id = purchaseOrderModel.PoMasterData.currency_id;
                masterData.total_amount_including_vattax = purchaseOrderModel.PoMasterData.total_amount_including_vattax;
                masterData.status = "Updated";
                masterData.company_id = masterData.company_id;
                masterData.order_date = masterData.order_date;
                masterData.shipping_terms_id = purchaseOrderModel.PoMasterData.shipping_terms_id;
                masterData.expected_shipment_date = masterData.expected_shipment_date;
                masterData.shipping_method_id = purchaseOrderModel.PoMasterData.shipping_method_id;
                masterData.vat_total = purchaseOrderModel.PoMasterData.vat_total;
                masterData.terms_n_condition = purchaseOrderModel.PoMasterData.terms_n_condition;
                masterData.created_by = masterData.created_by;
                masterData.created_date = masterData.created_date;
                masterData.updated_by = purchaseOrderModel.PoMasterData.updated_by;
                masterData.updated_date = DateTime.Now;
                masterData.noc_number = purchaseOrderModel.PoMasterData.noc_number;
                masterData.remarks = purchaseOrderModel.PoMasterData.remarks;
                masterData.batch_id = 0;
                masterData.order_no = purchaseOrderModel.PoMasterData.order_no;
                masterData.tax_total = purchaseOrderModel.PoMasterData.tax_total;
                masterData.total_amount_without_vattax = purchaseOrderModel.PoMasterData.total_amount_without_vattax;
                _entities.SaveChanges();

                foreach (var item in poDetailsList)
                {
                    purchase_order_details detailsData =
                        _entities.purchase_order_details.FirstOrDefault(
                            pd =>
                                pd.purchase_order_master_id == poMaster.purchase_order_master_id &&
                                pd.purchase_order_details_id == item.purchase_order_details_id);
                    if (detailsData != null)
                    {
                        detailsData.purchase_order_master_id = poMaster.purchase_order_master_id;
                        detailsData.product_id = item.product_id;
                        detailsData.unit_id = item.unit_id;
                        detailsData.unit_price = item.unit_price;
                        detailsData.quantity = item.quantity;
                        detailsData.amount = item.amount;
                        detailsData.brand_id = item.brand_id;
                        detailsData.color_id = item.color_id;

                        detailsData.pi_quantity = detailsData.pi_quantity;
                        detailsData.product_version_id = item.product_version_id;

                        detailsData.receive_qty = detailsData.receive_qty;
                        detailsData.vat_amount = item.vat_amount;

                        detailsData.last_modified_date = detailsData.last_modified_date;
                        detailsData.tax_amount = item.tax_amount;
                        detailsData.line_total = item.line_total;
                        detailsData.size_id = item.size_id;
                        detailsData.status = item.status;
                        detailsData.vat_pcnt = item.vat_pcnt;
                        detailsData.tax_pcnt = item.tax_pcnt;
                        detailsData.product_category_id = item.product_category_id;
                        _entities.SaveChanges();

                    }
                    else
                    {
                        var poDetails = new purchase_order_details
                        {
                            purchase_order_master_id = poMaster.purchase_order_master_id,
                            product_id = item.product_id,
                            unit_id = item.unit_id,
                            unit_price = item.unit_price,
                            quantity = item.quantity,
                            pi_quantity = 0,
                            product_version_id = item.product_version_id,
                            amount = item.amount,
                            brand_id = item.brand_id,
                            color_id = item.color_id,
                            receive_qty = 0,
                            last_modified_date = DateTime.Now,
                            vat_amount = item.vat_amount,
                            tax_amount = item.tax_amount,
                            line_total = item.line_total,
                            size_id = item.size_id,
                            status = item.status,
                            vat_pcnt = item.vat_pcnt,
                            tax_pcnt = item.tax_pcnt,
                            product_category_id = item.product_category_id
                        };

                        _entities.purchase_order_details.Add(poDetails);
                        _entities.SaveChanges();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdatePiInfoOnPo(PurchaseOrderModel purchaseOrderModel)
        {
            try
            {
                var poMaster = purchaseOrderModel.PoMasterData;
                var poDetailsList = purchaseOrderModel.PoDetailsList;
                purchase_order_master masterData = _entities.purchase_order_master.Find(poMaster.purchase_order_master_id);

                if (string.IsNullOrEmpty(masterData.pi_attachment_location))
                {
                    return false;
                }

                masterData.supplier_id = purchaseOrderModel.PoMasterData.supplier_id;
                masterData.pi_number = purchaseOrderModel.PoMasterData.pi_number;
                masterData.currency_id = purchaseOrderModel.PoMasterData.currency_id;
                masterData.total_amount_including_vattax = purchaseOrderModel.PoMasterData.total_amount_including_vattax;
                masterData.status = "Updated";
                masterData.company_id = masterData.company_id;
                masterData.order_date = masterData.order_date;
                masterData.shipping_terms_id = masterData.shipping_terms_id;
                masterData.expected_shipment_date = masterData.expected_shipment_date;
                masterData.shipping_method_id = masterData.shipping_method_id;
                masterData.vat_total = purchaseOrderModel.PoMasterData.vat_total;
                masterData.terms_n_condition = purchaseOrderModel.PoMasterData.terms_n_condition;
                masterData.pi_terms_n_condition = purchaseOrderModel.PoMasterData.pi_terms_n_condition;
                masterData.created_by = masterData.created_by;
                masterData.created_date = masterData.created_date;
                masterData.updated_by = purchaseOrderModel.PoMasterData.updated_by;
                masterData.updated_date = DateTime.Now;
                masterData.noc_number = purchaseOrderModel.PoMasterData.noc_number;
                masterData.remarks = purchaseOrderModel.PoMasterData.remarks;
                masterData.batch_id = 0;
                masterData.order_no = purchaseOrderModel.PoMasterData.order_no;
                masterData.tax_total = purchaseOrderModel.PoMasterData.tax_total;
                masterData.total_amount_without_vattax = purchaseOrderModel.PoMasterData.total_amount_without_vattax;
                _entities.SaveChanges();

                foreach (var item in poDetailsList)
                {
                    purchase_order_details detailsData =
                        _entities.purchase_order_details.FirstOrDefault(
                            pd =>
                                pd.purchase_order_master_id == poMaster.purchase_order_master_id &&
                                pd.purchase_order_details_id == item.purchase_order_details_id);
                    if (detailsData != null)
                    {
                        detailsData.purchase_order_master_id = poMaster.purchase_order_master_id;
                        detailsData.product_id = item.product_id;
                        detailsData.unit_id = item.unit_id;
                        detailsData.unit_price = item.unit_price;
                        detailsData.quantity = item.quantity;
                        detailsData.amount = item.amount;
                        detailsData.brand_id = item.brand_id;
                        detailsData.color_id = item.color_id;

                        detailsData.pi_quantity = item.pi_quantity;
                        detailsData.product_version_id = item.product_version_id;

                        detailsData.receive_qty = detailsData.receive_qty;
                        detailsData.vat_amount = item.vat_amount;

                        detailsData.last_modified_date = detailsData.last_modified_date;
                        detailsData.tax_amount = item.tax_amount;
                        detailsData.line_total = item.line_total;
                        detailsData.size_id = item.size_id;
                        detailsData.status = item.status;
                        detailsData.vat_pcnt = item.vat_pcnt;
                        detailsData.tax_pcnt = item.tax_pcnt;
                        detailsData.product_category_id = item.product_category_id;
                        _entities.SaveChanges();
                    }

                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateLcNoOnPo(PurchaseOrderModel purchaseOrderModel)
        {
            try
            {
                var poMaster = purchaseOrderModel.PoMasterData;
                purchase_order_master masterData = _entities.purchase_order_master.Find(poMaster.purchase_order_master_id);

                masterData.lc_number = purchaseOrderModel.PoMasterData.lc_number;
                _entities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeletePurchaseOrder(long purchase_order_master_id)
        {
            try
            {
                List<grn_master> grnList =
                    _entities.grn_master.Where(g => g.purchase_order_master_id == purchase_order_master_id).ToList();
                if (grnList.Count == 0)
                {
                    purchase_order_master oPurchaseOrderMaster = _entities.purchase_order_master.Find(purchase_order_master_id);
                    oPurchaseOrderMaster.is_active = false;
                    oPurchaseOrderMaster.is_deleted = true;
                    _entities.SaveChanges();

                    //List<purchase_order_details> poDetailsList =
                    //    _entities.purchase_order_details.Where(
                    //        pd => pd.purchase_order_master_id == purchase_order_master_id).ToList();
                    //if (poDetailsList.Count > 0)
                    //{
                    //    foreach (var item in poDetailsList)
                    //    {
                    //        purchase_order_details oPurchaseOrderDetails = _entities.purchase_order_details.Find(item.purchase_order_details_id);
                    //        _entities.purchase_order_details.Attach(oPurchaseOrderDetails);
                    //        _entities.purchase_order_details.Remove(oPurchaseOrderDetails);
                    //        _entities.SaveChanges();
                    //    }
                    //}

                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeletePurchaseOrderDetailsById(long purchase_order_details_id)
        {
            try
            {
                purchase_order_details oPurchaseOrderDetails = _entities.purchase_order_details.Find(purchase_order_details_id);
                _entities.purchase_order_details.Attach(oPurchaseOrderDetails);
                _entities.purchase_order_details.Remove(oPurchaseOrderDetails);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
