using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using DMSApi.Models.IRepository;

namespace DMSApi.Models.Repository
{
    public class OnlineInvoiceAndPaymentRepository : IOnlineInvoiceAndPaymentRepository
    {
        private DMSEntities _entities;
        private PartyJournalRepository partyJournalRepository;

        public OnlineInvoiceAndPaymentRepository()
        {
            _entities = new DMSEntities();
            this.partyJournalRepository = new PartyJournalRepository();
        }
        public long AddOnlineInvoice(StronglyType.OnlineInvoiceModel onlineInvoice)
        {
            try
            {
                Int64 OnlineMasterId = 0;

                var onlineInvoiceMaster = onlineInvoice.OnlineInvoiceMaster;
                var onlineInvoiceDetails = onlineInvoice.OnlinePaymentProductModels;

                //Online Invoice Master Save

                var partyTypePrefix = (from ptype in _entities.party_type
                                       join par in _entities.parties on ptype.party_type_id equals par.party_type_id
                                       where par.party_id == onlineInvoiceMaster.party_id
                                       select new
                                       {
                                           party_prefix = ptype.party_prefix

                                       }).FirstOrDefault();

                int InvoiceSerial = _entities.online_invoice_master.Max(rq => (int?)rq.online_invoice_master_id) ?? 0;
                InvoiceSerial++;

                var rqStr = InvoiceSerial.ToString().PadLeft(7, '0');
                string invoiceNo = "INV-" + partyTypePrefix.party_prefix + "-" + rqStr;

                onlineInvoiceMaster.online_invoice_no = invoiceNo;
                onlineInvoiceMaster.created_by = onlineInvoice.OnlineInvoiceMaster.created_by;
                onlineInvoiceMaster.created_date = DateTime.Now;
                onlineInvoiceMaster.party_id = onlineInvoice.OnlineInvoiceMaster.party_id;
                onlineInvoiceMaster.online_invoice_date = DateTime.Now;
                onlineInvoiceMaster.item_total = onlineInvoiceDetails.Sum(a => a.invoice_quantity);
                onlineInvoiceMaster.invoice_total = onlineInvoiceDetails.Sum(a => a.invoice_quantity);

                _entities.online_invoice_master.Add(onlineInvoiceMaster);                
                int i  = _entities.SaveChanges();
                OnlineMasterId = onlineInvoiceMaster.online_invoice_master_id;

                if (i > 0)
                {
                    foreach (var onlinePaymentProductModel in onlineInvoiceDetails)
                    {
                        //invoice details save
                        if (onlinePaymentProductModel.invoice_quantity != 0)
                        {
                            var pro = _entities.products.Find(onlinePaymentProductModel.product_id);
                            var requisitionForCheck =
                                _entities.requisition_details.SingleOrDefault(
                                    a => a.requisition_details_id == onlinePaymentProductModel.requisition_details_id);
                            var forCheckIvoice = requisitionForCheck.invoice_quantity;
                            var requisitionD =
                                _entities.requisition_details.SingleOrDefault(
                                    a => a.requisition_details_id == onlinePaymentProductModel.requisition_details_id);
                            
                            var invoiceDetails = new online_invoice_details();

                            invoiceDetails.brand_id = pro.brand_id;
                            invoiceDetails.color_id = onlinePaymentProductModel.color_id;
                            invoiceDetails.product_category_id = pro.product_category_id;
                            invoiceDetails.product_id = pro.product_id;
                            invoiceDetails.product_version_id = onlinePaymentProductModel.product_version_id;
                            invoiceDetails.unit_id = pro.unit_id;
                            invoiceDetails.online_invoice_master_id = OnlineMasterId;
                            invoiceDetails.quantity = onlinePaymentProductModel.invoice_quantity;
                            invoiceDetails.price = onlinePaymentProductModel.price;
                            invoiceDetails.discount = requisitionD.discount;
                            invoiceDetails.price_amount = onlinePaymentProductModel.price*
                                                          onlinePaymentProductModel.invoice_quantity;
                            invoiceDetails.discount_amount = (requisitionD.discount_amount/requisitionD.quantity)*
                                                             onlinePaymentProductModel.invoice_quantity;
                            invoiceDetails.line_total = (requisitionD.line_total/requisitionD.quantity)*
                                                        onlinePaymentProductModel.invoice_quantity;

                            if (requisitionD.promotion_master_id != null)
                            {
                                _entities.online_invoice_details.Add(invoiceDetails);
                                _entities.SaveChanges();

                                // requisition details update    
                                requisitionD.invoice_quantity += onlinePaymentProductModel.invoice_quantity;
                                _entities.SaveChanges();   
                            }                           

                            //promotion invoice add

                            if (requisitionD.promotion_master_id != null)
                            {
                                var promotion = _entities.promotion_master.Find(requisitionD.promotion_master_id);
                                var promoDetails =
                                    _entities.promotion_details.Where(
                                        a => a.promotion_master_id == promotion.promotion_master_id).ToList();
                                if (promotion.promotion_type == "Product")
                                {
                                    var tQty = requisitionD.invoice_quantity;
                                    if (forCheckIvoice >= promotion.lifting_quantity_for_promotion)
                                    {
                                        tQty = onlinePaymentProductModel.invoice_quantity;
                                    }
                                    if (tQty >= promotion.lifting_quantity_for_promotion)
                                    {
                                        foreach (var productModel in onlineInvoiceDetails)
                                        {
                                            if (productModel.is_gift == true)
                                            {
                                                foreach (var promotionDetailse in promoDetails)
                                                {
                                                    if (promotionDetailse.product_id == productModel.product_id)
                                                    {
                                                        var actualGiftQuantity = (int)(tQty / promotion.lifting_quantity_for_promotion);


                                                        var pro1 = _entities.products.Find(productModel.product_id);
                                                        var requisitionD1 =
                                                            _entities.requisition_details.SingleOrDefault(
                                                                a => a.requisition_details_id == productModel.requisition_details_id);
                                                        var invoiceDetails1 = new online_invoice_details();

                                                        invoiceDetails1.brand_id = pro1.brand_id;
                                                        invoiceDetails1.color_id = productModel.color_id;
                                                        invoiceDetails1.product_category_id = pro1.product_category_id;
                                                        invoiceDetails1.product_id = pro1.product_id;
                                                        invoiceDetails1.product_version_id = productModel.product_version_id;
                                                        invoiceDetails1.unit_id = pro1.unit_id;
                                                        invoiceDetails1.online_invoice_master_id = OnlineMasterId;

                                                        invoiceDetails1.quantity = promotionDetailse.gift_quantity * actualGiftQuantity;
                                                        
                                                        invoiceDetails1.price = 0;
                                                        invoiceDetails1.discount = 0;
                                                        invoiceDetails1.price_amount = 0;
                                                        invoiceDetails1.discount_amount = 0;
                                                        invoiceDetails1.line_total = 0;


                                                        _entities.online_invoice_details.Add(invoiceDetails1);
                                                        _entities.SaveChanges();


                                                        // requisition details update
                                                        requisitionD1.invoice_quantity += (promotionDetailse.gift_quantity * actualGiftQuantity);
                                                        _entities.SaveChanges();    
                                                    }
                                                }
                                                
    
                                            }
                                            
                                        }
                                    }
                                }                                
                            }

                        }

                    }
                    
                    //Requisition Master Update and mapping add
                    foreach (var onlinePaymentProductModel in onlineInvoiceDetails)
                    {
                        if (onlinePaymentProductModel.invoice_quantity != 0)
                        {
                            var findR = _entities.requisition_master.SingleOrDefault(a => a.requisition_master_id == onlinePaymentProductModel.requisition_master_id);

                            if (findR.is_requisition_closed == false)
                            {
                                var listOfD =
                                    _entities.requisition_details.Where(
                                        a => a.requisition_master_id == findR.requisition_master_id).ToList();
                                var countInvoiceQuant = listOfD.Where(a=>a.is_gift == null).Sum(a => a.quantity);
                                var sumOfIgrid = listOfD.Where(a => a.is_gift == null).Sum(a => a.invoice_quantity);
                                var sumOfRgrid = listOfD.Where(a => a.is_gift == null).Sum(a => a.return_quantity);
                                var nValue = sumOfIgrid + sumOfRgrid;

                                if (nValue == countInvoiceQuant)
                                {
                                    findR.invoicable_quantity = countInvoiceQuant;
                                    findR.is_requisition_closed = true;
                                }
                                else
                                {
                                    findR.invoicable_quantity += onlinePaymentProductModel.invoice_quantity;
                                    findR.is_requisition_closed = false;
                                }

                                _entities.SaveChanges();

                                var check =
                                    _entities.online_invoice_requisition_mapping.SingleOrDefault(
                                        a =>
                                            a.requisition_master_id == findR.requisition_master_id &&
                                            a.online_invoice_master_id == OnlineMasterId);
                                if (check == null)
                                {
                                    var mapping = new online_invoice_requisition_mapping
                                    {
                                        online_invoice_master_id = OnlineMasterId,
                                        requisition_master_id = findR.requisition_master_id
                                    };

                                    _entities.online_invoice_requisition_mapping.Add(mapping);
                                    _entities.SaveChanges();
                                }

                            }
                        }
                        
                    }

                    //update party journal
                    //For Party Journal

                    //GET ACCOUNT BALANCE FROM PARTY JOURNAL
                    var partyJournal =
                        _entities.party_journal.Where(pj => pj.party_id == onlineInvoice.OnlineInvoiceMaster.party_id)
                            .OrderByDescending(p => p.party_journal_id)
                            .FirstOrDefault();
                    decimal partyAccountBalance = 0;
                    if (partyJournal != null)
                    {
                        partyAccountBalance = partyJournal.closing_balance ?? 0;
                    }

                    //invoce amount
                    var ttt =
                        _entities.online_invoice_details.Where(a => a.online_invoice_master_id == OnlineMasterId)
                            .ToList();

                    //CALCULATING INVOICE TOTAL
                    decimal invoiceTotal = 0;
                    invoiceTotal = ttt.Sum(a => a.line_total) ?? 0; //insert in both invoice master and party journal table

                    //ACCOUNT BALANCE
                    decimal accountBalance = 0;
                    accountBalance = invoiceTotal + partyAccountBalance;

                    partyJournalRepository.PartyJournalEntry("INVOICE", onlineInvoice.OnlineInvoiceMaster.party_id ?? 0, invoiceTotal, "Invoice", onlineInvoice.OnlineInvoiceMaster.created_by ?? 0, invoiceNo);



                }

                return OnlineMasterId;
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }


        public object GetAllOnlineInvoiceAndPayment()
        {
            try
            {                

                var list = (from e in _entities.online_invoice_master
                    join p in _entities.parties on e.party_id equals p.party_id
                    select new
                    {
                        online_invoice_master_id= e.online_invoice_master_id,
                        online_invoice_date = e.online_invoice_date,
                        party_id = e.party_id,
                        party_name = p.party_name,
                        is_active = e.is_active,
                        online_invoice_no = e.online_invoice_no
                        
                    }).ToList();


                return list.OrderByDescending(a=>a.online_invoice_master_id);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public string ReqNo(long id)
        {
            try
            {
                string re = "";
                int i = 1;
                var p =
                    _entities.online_invoice_requisition_mapping.Where(a => a.online_invoice_master_id == id).ToList();
                
                foreach (var onlineInvoiceRequisitionMapping in p)
                {
                    if (p.Count == i)
                    {
                        var t = _entities.requisition_master.Find(onlineInvoiceRequisitionMapping.requisition_master_id);
                        re += t.requisition_code;
                    }
                    else
                    {
                        var t = _entities.requisition_master.Find(onlineInvoiceRequisitionMapping.requisition_master_id);
                        re += t.requisition_code + ",";
                        i += i;
                    }
                    
                }
                return re;
            }
            catch (Exception)
            {
                
                throw;
            }
        }


        public object GetInvoiceNo(long masterId)
        {
            try
            {
                var kk = _entities.online_invoice_master.SingleOrDefault(a => a.online_invoice_master_id == masterId);
                return kk;
            }
            catch (Exception)
            {
                
                throw;
            }
        }


        public object GetAmount(long masterId)
        {
            try
            {
                var lll =
                    _entities.online_invoice_details.Where(a => a.online_invoice_master_id == masterId)
                        .Sum(a => a.line_total);
                return lll;
            }
            catch (Exception)
            {
                
                throw;
            }
        }


        public bool InsertOnlinePaymentReceive()
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
                        _entities.online_invoice_master.SingleOrDefault(
                            a => a.online_invoice_master_id == invoice_master_id);
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
                            invoice_no = invoice.online_invoice_no,
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