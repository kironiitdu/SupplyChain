using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMSApi.Models.crystal_models;
using DMSApi.Models.IRepository;
using DMSApi.Models.StronglyType;

namespace DMSApi.Models.Repository
{
    public class ReceiveRepository : IReceiveRepository
    {
        private DMSEntities _entities;
        private PartyJournalRepository partyJournal;
        //private 


        public ReceiveRepository()
        {
            this._entities = new DMSEntities();
            this.partyJournal = new PartyJournalRepository();
        }
        public object GetAllReceives()
        {
            var paymentsGridData = (from pay in _entities.receives
                                    join par in _entities.parties on pay.party_id equals par.party_id into tempPar
                                    from par in tempPar.DefaultIfEmpty()
                                    join xxx in _entities.bank_branch on pay.bank_branch_id equals xxx.bank_branch_id into tempXxx
                                    from xxx in tempXxx.DefaultIfEmpty()
                                    join ban in _entities.banks on pay.bank_id equals ban.bank_id into tempBan
                                    from ban in tempBan.DefaultIfEmpty()
                                    join cre in _entities.users on pay.created_by equals cre.user_id into tempCre
                                    from cre in tempCre.DefaultIfEmpty()
                                    join up in _entities.users on pay.updated_by equals up.user_id into tempUp
                                    from up in tempUp.DefaultIfEmpty()
                                    join app in _entities.users on pay.approved_by equals app.user_id into tempApp
                                    from app in tempApp.DefaultIfEmpty()
                                    join pm in _entities.payment_method on pay.payment_method_id equals pm.payment_method_id into temPm
                                    from pm in temPm.DefaultIfEmpty()

                                    join pty in _entities.parties on pay.party_id equals pty.party_id into tempPty
                                    from pty in tempPty.DefaultIfEmpty()
                                    join teri in _entities.territories on pty.territory_id equals teri.territory_id into temTeri
                                    from teri in temTeri.DefaultIfEmpty()

                                    select new
                                    {
                                        receive_id = pay.receive_id,
                                        receipt_no = pay.receipt_no,
                                        receive_date = pay.receive_date,
                                        party_name = par.party_name,
                                        payment_method_name = pm.payment_method_name,
                                        amount = pay.amount,
                                        bank_name = ban.bank_name,
                                        branch_name = xxx.bank_branch_name,//new
                                        reference_no = pay.cheque_no,
                                        bank_charge = pay.bank_charge,
                                        invoice_no = pay.invoice_no,//new
                                        last_invoice_balance = pay.last_invoice_balance,
                                        representative = pay.representative,
                                        remarks = pay.remarks,
                                        payment_req_id = pay.payment_req_id,
                                        document_attachment = pay.document_attachment,
                                        opening_balance = (from pj in _entities.party_journal
                                                           where pj.party_id == pay.party_id
                                                           orderby pj.party_journal_id descending
                                                           select new { pj.closing_balance }).FirstOrDefault().closing_balance??0,//new
                                        status = pay.status ?? "Not Approved",
                                        approved_by = app.full_name ?? "Pending",
                                        created_by = cre.full_name,
                                        created_date = pay.created_date,
                                        updated_by = up.full_name,//new
                                        updated_date = pay.updated_date,
                                        territory_name = teri.territory_name//new
                                    }).OrderByDescending(p => p.receive_id).ToList().DefaultIfEmpty();

            return paymentsGridData;
        }

        public List<receive> GetReceiveList()
        {
            var payment = _entities.receives.OrderByDescending(p => p.receive_id).ToList();
            return payment;
        }

        public long AddReceive(receive receive)
        {
            try
            {


                //Get Party type Prefix by party Id :Kiron:27/10/2016

                var partyTypePrefix = (from ptype in _entities.party_type
                                       join par in _entities.parties on ptype.party_type_id equals par.party_type_id
                                       where par.party_id == receive.party_id
                                       select new
                                       {
                                           party_prefix = ptype.party_prefix

                                       }).FirstOrDefault();

                int prnSerial = _entities.receives.Max(po => (int?)po.receive_id) ?? 0;

                if (prnSerial != 0)
                {
                    prnSerial++;

                }
                else
                {
                    prnSerial++;
                }
                var prnString = prnSerial.ToString().PadLeft(7, '0');


                string prnNo = "MRN-" + partyTypePrefix.party_prefix + "-" + prnString;
                receive insert_payment = new receive
                {

                    receipt_no = prnNo,
                    receive_date = System.DateTime.Now,//receive.receive_date,
                    party_id = receive.party_id,
                    payment_method_id = receive.payment_method_id,
                    cheque_no = receive.cheque_no,
                    bank_id = receive.bank_id,
                    bank_branch_id = receive.bank_branch_id,
                    last_invoice_balance = receive.last_invoice_balance,
                    bank_account_id = receive.bank_account_id,
                    amount = receive.amount,
                    invoice_no = receive.invoice_no,
                    representative = receive.representative,
                    remarks = receive.remarks,
                    payment_req_id = receive.payment_req_id,
                    document_attachment = receive.document_attachment,
                    bank_charge = receive.bank_charge,
                    is_varified = receive.is_varified,
                    created_date = System.DateTime.Now,
                    created_by = receive.created_by,
                    is_active = true,
                    is_deleted = false,
                };
                _entities.receives.Add(insert_payment);
                long saved = _entities.SaveChanges();
                long receiveId = insert_payment.receive_id;

                //mohi uddin(18.05.2017) start
                long rqId = 0;
                var paymentRequestData = _entities.payment_request.Find(receive.payment_req_id);
                if (paymentRequestData != null)
                {
                    rqId = paymentRequestData.requisition_master_id ?? 0;
                    if (rqId != 0)
                    {
                        receive rcvData = _entities.receives.Find(receiveId);
                        rcvData.requisition_master_id = rqId;
                        _entities.SaveChanges();
                    }
                   
                }
                //end(18.05.2017)

                if (saved > 0)
                {
                    PaymentRequestRepository paymentRequest = new PaymentRequestRepository();
                    paymentRequest.UpdateStatus(insert_payment.payment_req_id, insert_payment.created_by);
                }
                long last_insert_id = insert_payment.receive_id;
                return last_insert_id;


            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        //For Edit
        public receive GetReceiveById(long receive_id)
        {


            var receiveById = _entities.receives.Find(receive_id);
            return receiveById;

        }
        public bool EditReceive(receive objPayment)
        {
            try
            {

                receive payment = _entities.receives.Find(objPayment.receive_id);
                payment.receive_date = System.DateTime.Now;//objPayment.receive_date;
                payment.party_id = objPayment.party_id;
                payment.payment_method_id = objPayment.payment_method_id;
                payment.cheque_no = objPayment.cheque_no;
                payment.bank_id = objPayment.bank_id;
                payment.bank_branch_id = objPayment.bank_branch_id;
                payment.bank_account_id = objPayment.bank_account_id;
                payment.amount = objPayment.amount;
                payment.invoice_no = objPayment.invoice_no;
                payment.representative = objPayment.representative;
                payment.remarks = objPayment.remarks;
                payment.payment_req_id = objPayment.payment_req_id;
                payment.document_attachment = objPayment.document_attachment;
                payment.bank_charge = objPayment.bank_charge;
                payment.is_varified = objPayment.is_varified;
                payment.updated_date = System.DateTime.Now;
                payment.updated_by = objPayment.updated_by;
                payment.is_active = true;
                payment.is_deleted = false;
                _entities.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public long DeleteReceive(long receive_id, long updated_by)
        {
            try
            {
                var payStatus = _entities.receives.Where(pay => pay.status == "Recieved" && pay.receive_id == receive_id).ToList();

                if (payStatus.Count != 0)
                {
                    return 1;
                }

                else
                {
                    receive objPayment = _entities.receives.FirstOrDefault(pay => pay.receive_id == receive_id);
                    objPayment.is_deleted = true;
                    objPayment.updated_by = updated_by;
                    objPayment.updated_date = System.DateTime.Now;
                    _entities.SaveChanges();
                    return 2;
                }
            }
            catch (Exception)
            {

                return 0;
            }

        }




        public bool UpdateStatus(long receive_id, long user_id)
        {

            try
            {
                var receive = _entities.receives.Find(receive_id);
                var isVarifyStatus = receive.is_varified;
                if (isVarifyStatus == true)
                {
                    receive.status = "Approved";
                    receive.approved_by = user_id;

                    receive.updated_date = System.DateTime.Now;
                    int saved = _entities.SaveChanges();
                    if (saved > 0)
                    {
                        partyJournal.PartyJournalEntry("RECEIVE", receive.party_id ?? 0, receive.amount ?? 0,
                            "Payment Received", receive.approved_by ?? 0, receive.receipt_no);
                    }
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


        public decimal GetClousingBalance(long party_id)
        {
            try
            {

                var partyTransaction =
                      _entities.party_journal.Where(w => w.party_id == party_id)
                          .OrderByDescending(o => o.party_journal_id)
                          .FirstOrDefault();
                decimal closingBalance = 0;
                if (partyTransaction != null)
                {
                    closingBalance = partyTransaction.closing_balance ?? 0;
                }
                return closingBalance;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public object GetInvoiceNumber(long party_id)
        {
            try
            {
                ReceiveRepository closingBalance = new ReceiveRepository();
                decimal clousingBalance = closingBalance.GetClousingBalance(party_id);
                //int prnSerial = _entities.invoice_master.Max(ims=>ims.invoice_no.Where(pid =>pid == party_id));
                var lastInvoiceNo = _entities.invoice_master
                                .Where(p => p.party_id == party_id)
                                .OrderByDescending(p => p.invoice_master_id)
                                .FirstOrDefault();


                string invoice_no = "";
                if (lastInvoiceNo != null && clousingBalance != 0)
                {
                    invoice_no = lastInvoiceNo.invoice_no;
                }
                else
                {
                    string msg = "No Due and Invoice No";
                    return msg;
                }
                return invoice_no;


            }
            catch (Exception)
            {

                throw;
            }
        }


        public object GetMoneyReceiptReport(long receive_id)
        {
            try
            {
                string query = "select distinct receive.receipt_no ,receive.representative ,us.full_name as user_name ,party.party_name as receipt_from ,receive.created_date as address ,party.party_code as proprietor_name ,party.mobile ,party.party_name as customer_name ,receive.remarks ,payment_method.payment_method_name ,receive.cheque_no ,bank.bank_name ,'BDT' as currency ,receive.amount ,receive.last_invoice_balance as invoice_amount ,receive.invoice_no from receive left join users us on receive.approved_by=us.user_id left join users on receive.party_id=users.party_id left join party on receive.party_id=party.party_id left join payment_method on receive.payment_method_id=payment_method.payment_method_id left join bank on receive.bank_id=bank.bank_id where receive.receive_id='" + receive_id + "'";


                var moneyReceptData = _entities.Database.SqlQuery<MoneyReceiptModels>(query).ToList();

                return moneyReceptData;
            }

            catch (Exception)
            {

                return 0;
            }
        }


        public payment_request ProcessPaymentRequiest(long payment_req_id)
        {
            var loadAddPageByPayment_req_id = _entities.payment_request.Find(payment_req_id);
            return loadAddPageByPayment_req_id;
        }


        public object PaymentHistory(string from_date, string to_date)
        {
            try
            {
                //string query = "SELECT DISTINCT receive.receive_date as date ,receive.amount ,payment_method.payment_method_name as payment_mode ,COALESCE(bank.bank_name,'Cash') as deposite_to ,COALESCE(receive.cheque_no,'-')as cheque_no ,COALESCE(receive.received_invoice_no,'0') as invoice_no ,COALESCE(invoice_master.invoice_date,'-') as invoice_date ,COALESCE(invoice_master.invoice_total,'0')as invoice_amount ,COALESCE((select SUM(quantity) FROM requisition_details WHERE requisition_master_id=requisition_master.requisition_master_id),'0') as invoice_qty ,COALESCE(receive.receive_id,'0') as receive_id ,COALESCE(invoice_master.invoice_master_id,'0')as invoice_master_id ,COALESCE(requisition_master.requisition_master_id,'0')as requisition_master_id FROM receive LEFT JOIN payment_method ON receive.payment_method_id=payment_method.payment_method_id LEFT JOIN bank ON receive.bank_id=bank.bank_id LEFT JOIN invoice_master ON receive.received_invoice_no=invoice_master.invoice_no LEFT JOIN requisition_master ON invoice_master.requisition_master_id=requisition_master.requisition_master_id LEFT JOIN requisition_details ON requisition_details.requisition_master_id=requisition_master.requisition_master_id where to_date(receive.receive_date,'DD/MM/YYYY') BETWEEN to_date('"+from_date+"', 'DD/MM/YYYY') AND to_date('"+to_date+"', 'DD/MM/YYYY')";

                string query = "SELECT DISTINCT party.party_name ,party.party_code ,receive.receive_date as date ,receive.amount ,payment_method.payment_method_name as payment_mode ,COALESCE(bank.bank_name,'Cash') as deposite_to ,COALESCE(receive.cheque_no,'-')as cheque_no ,COALESCE(receive.received_invoice_no,'0') as invoice_no ,COALESCE(invoice_master.invoice_date,'-') as invoice_date ,COALESCE(invoice_master.invoice_total,'0')as invoice_amount ,COALESCE((select SUM(quantity) FROM requisition_details WHERE requisition_master_id=requisition_master.requisition_master_id),'0') as invoice_qty ,COALESCE(receive.receive_id,'0') as receive_id ,COALESCE(invoice_master.invoice_master_id,'0')as invoice_master_id ,COALESCE(requisition_master.requisition_master_id,'0')as requisition_master_id FROM receive LEFT JOIN party ON receive.party_id=party.party_id LEFT JOIN payment_method ON receive.payment_method_id=payment_method.payment_method_id LEFT JOIN bank ON receive.bank_id=bank.bank_id LEFT JOIN invoice_master ON receive.received_invoice_no=invoice_master.invoice_no LEFT JOIN requisition_master ON invoice_master.requisition_master_id=requisition_master.requisition_master_id LEFT JOIN requisition_details ON requisition_details.requisition_master_id=requisition_master.requisition_master_id WHERE to_date(receive.receive_date,'DD/MM/YYYY') BETWEEN to_date('" + from_date + "', 'DD/MM/YYYY') AND to_date('" + to_date + "', 'DD/MM/YYYY')";
                var paymentHistory = _entities.Database.SqlQuery<PaymentHistoryModel>(query).ToList();

                return paymentHistory;
            }

            catch (Exception)
            {

                return 0;
            }
        }

        //Added By :Kiron 28-11-2016
        public object ProductLiftingAndPaymentSummery(string from_date, string to_date, long party_id)
        {
            try
            {
                // string query = "SELECT invoice_log.invoice_date ,invoice_log.invoice_no ,invoice_log.lifting_quantity ,invoice_log.item_total as product_billing_amount ,invoice_log.rebate_total ,0 as live_dummey ,invoice_log.price_protected_amount as adjusment ,invoice_log.invoice_total as net_bill ,invoice_log.received_amount ,invoice_log.balance_amount ,(CASE WHEN invoice_log.balance_amount =0 THEN 'Full Paid' WHEN invoice_log.balance_amount >0 THEN 'Advance' WHEN invoice_log.balance_amount <0 THEN 'Due' END) as balance_status ,party.party_id ,party.party_name ,party.party_code ,party.address ,party_type.party_type_name ,'Meraj Vai Do not work on it' as area_name ,'Meraj Vai Do not work on it' as zone_name ,sum(invoice_log.invoice_total)as total_net_billing_amount ,invoice_log.previous_invoice_due as previous_due ,invoice_log.previous_invoice_advance as previous_advance ,sum(invoice_log.received_amount)as sum_received_amount ,(SELECT SUM(balance_amount) FROM invoice_log )as sum_advance_amount ,'Not Availabe' as representaive_name FROM invoice_log INNER JOIN party ON invoice_log.party_id=party.party_id INNER JOIN party_type on party.party_type_id=party_type.party_type_id GROUP BY invoice_log.invoice_date ,invoice_log.invoice_no ,invoice_log.lifting_quantity ,invoice_log.item_total ,invoice_log.rebate_total ,invoice_log.price_protected_amount ,invoice_log.invoice_total ,invoice_log.received_amount ,invoice_log.balance_amount ,party.party_id ,party.party_name ,party.party_code ,party.address ,party_type.party_type_name ,invoice_log.previous_invoice_due ,invoice_log.previous_invoice_advance ,invoice_log.received_amount ,invoice_log.balance_amount";
                //string query = "SELECT invoice_log.invoice_date ,invoice_log.invoice_no ,invoice_log.lifting_quantity ,invoice_log.item_total as product_billing_amount ,invoice_log.rebate_total ,0 as live_dummey ,invoice_log.price_protected_amount as adjusment ,invoice_log.invoice_total as net_bill ,invoice_log.received_amount ,invoice_log.balance_amount ,(CASE WHEN invoice_log.balance_amount =0 THEN 'Full Paid' WHEN invoice_log.balance_amount >0 THEN 'Advance' WHEN invoice_log.balance_amount <0 THEN 'Due' END ) as balance_status ,party.party_id ,party.party_name ,party.party_code ,party.address ,party_type.party_type_name ,COALESCE(area.area_name,'Set Area Name on Party' ) as area_name ,COALESCE(zone.zone_name,'Set Zone Name on Party') as zone_name ,sum(invoice_log.invoice_total)as total_net_billing_amount ,invoice_log.previous_invoice_due as previous_due ,invoice_log.previous_invoice_advance as previous_advance ,sum(invoice_log.received_amount)as sum_received_amount ,(SELECT SUM(balance_amount) FROM invoice_log )as sum_advance_amount ,'Set Representative' as representaive_name FROM invoice_log INNER JOIN party ON invoice_log.party_id=party.party_id INNER JOIN party_type on party.party_type_id=party_type.party_type_id INNER JOIN area on party.area_id=area.area_id INNER JOIN zone ON party.zone_id=zone.zone_id WHERE invoice_log.party_id='"+party_id+"' AND to_date(invoice_log.invoice_date, 'MM/DD/YYYY') BETWEEN to_date('"+from_date+"', 'MM/DD/YYYY') AND to_date('"+to_date+"', 'MM/DD/YYYY') GROUP BY invoice_log.invoice_date ,invoice_log.invoice_no ,invoice_log.lifting_quantity ,invoice_log.item_total ,invoice_log.rebate_total ,invoice_log.price_protected_amount ,invoice_log.invoice_total ,invoice_log.received_amount ,invoice_log.balance_amount ,party.party_id ,party.party_name ,party.party_code ,party.address ,party_type.party_type_name ,invoice_log.previous_invoice_due ,invoice_log.previous_invoice_advance ,invoice_log.received_amount ,invoice_log.balance_amount,area.area_name ,zone.zone_name";
                string query = "SELECT COALESCE(invoice_log.invoice_date ,'-') as invoice_date ,COALESCE(invoice_log.invoice_no ,'-') as invoice_no ,COALESCE(invoice_log.lifting_quantity ,0) as lifting_quantity ,COALESCE(invoice_log.item_total ,0) as product_billing_amount ,COALESCE(invoice_log.rebate_total ,0) as rebate_total ,COALESCE(0,0) as live_dummey ,COALESCE(invoice_log.price_protected_amount ,0) as adjusment ,COALESCE(invoice_log.invoice_total ,0) as net_bill ,COALESCE(invoice_log.received_amount ,0) as received_amount ,COALESCE(invoice_log.balance_amount ,0) as balance_amount ,(CASE WHEN invoice_log.balance_amount =0 THEN 'Full Paid' WHEN invoice_log.balance_amount >0 THEN 'Advance' WHEN invoice_log.balance_amount <0 THEN 'Due' END ) as balance_status ,COALESCE(party.party_id ,0) as party_id ,COALESCE(party.party_name ,'-') as party_name ,COALESCE(party.party_code ,'-') as party_code ,COALESCE(party.address ,'-') as address ,COALESCE(party_type.party_type_name ,'-') as party_type_name ,COALESCE(area.area_name,'Set Area Name on Party' ) as area_name ,COALESCE(zone.zone_name,'Set Zone Name on Party') as zone_name ,sum(invoice_log.invoice_total)as total_net_billing_amount ,COALESCE(invoice_log.previous_invoice_due,0) as previous_due ,COALESCE(invoice_log.previous_invoice_advance ,0) as previous_advance ,sum(invoice_log.received_amount)as sum_received_amount ,(SELECT SUM(balance_amount) FROM invoice_log )as sum_advance_amount ,'Set Representative' as representaive_name FROM invoice_log INNER JOIN party ON invoice_log.party_id=party.party_id INNER JOIN party_type on party.party_type_id=party_type.party_type_id INNER JOIN area on party.area_id=area.area_id INNER JOIN zone ON party.zone_id=zone.zone_id WHERE invoice_log.party_id='" + party_id + "' AND to_date(invoice_log.invoice_date, 'MM/DD/YYYY') BETWEEN to_date('" + from_date + "', 'MM/DD/YYYY') AND to_date('" + to_date + "', 'MM/DD/YYYY') GROUP BY invoice_log.invoice_date ,invoice_log.invoice_no ,invoice_log.lifting_quantity ,invoice_log.item_total ,invoice_log.rebate_total ,invoice_log.price_protected_amount ,invoice_log.invoice_total ,invoice_log.received_amount ,invoice_log.balance_amount ,party.party_id ,party.party_name ,party.party_code ,party.address ,party_type.party_type_name ,invoice_log.previous_invoice_due ,invoice_log.previous_invoice_advance ,invoice_log.received_amount ,invoice_log.balance_amount,area.area_name ,zone.zone_name";
                var liftingSummery = _entities.Database.SqlQuery<ProductLiftingAndPaymentSummery>(query).ToList();

                return liftingSummery;
            }

            catch (Exception)
            {

                return 0;
            }
        }


        public object GetAllUnReceivedPaymentList()
        {
            string query = "select pr.payment_req_id ,rcv.receive_id,bb.bank_branch_name, pr.amount , FORMAT(pr.deposite_date , 'dd/MM/yyyy HH:mm:ss tt') as deposite_date , pm.payment_method_id , pm.payment_method_name , pr.cheque_no , pr.approved , pr.document_attachment , p.party_id, p.party_name , pt.party_type_id , pr.bank_branch_id , ba.bank_account_name , pt.party_type_name , t.territory_name , isnull(rcv.status,'Not Approved') as status , b.bank_name , bb.bank_branch_name , isnull(u.full_name,'Pending') as approved_by , (select top 1 CAST(isnull(party_journal.closing_balance,0) as decimal) from party_journal where party_journal.party_id=p.party_id order by party_journal.party_journal_id desc) as opening_balance , isnull(rcv.receipt_no,'Processing') as receipt_no from payment_request pr left join bank b on pr.bank_id=b.bank_id left join bank_branch bb on pr.bank_branch_id=bb.bank_branch_id left join bank_account ba on pr.bank_account_id= ba.bank_account_id left join party p on pr.party_id=p.party_id left join party_type pt on p.party_type_id=pt.party_type_id left join payment_method pm on pr.payment_method_id=pm.payment_method_id left join receive rcv on pr.payment_req_id=rcv.payment_req_id left join party p1 on pr.party_id=p1.party_id left join territory t on p1.territory_id=t.territory_id left join users u on rcv.approved_by=u.user_id where rcv.status is null and rcv.receive_id is not null order by pr.payment_req_id desc";

            var reData = _entities.Database.SqlQuery<AllPaymentRequest>(query).ToList();

            return reData;
        }


        public object GetAllPaymentReceivedList(DateTime fromDate, DateTime toDate, long partyId)
        {
            try
            {
                var toDayes = toDate.AddDays(1);
                if (partyId != 0)
                {
                    var paymentsReceivedData = (from pay in _entities.receives
                        join par in _entities.parties on pay.party_id equals par.party_id into tempPar
                        from par in tempPar.DefaultIfEmpty()
                        join xxx in _entities.bank_branch on pay.bank_branch_id equals xxx.bank_branch_id into tempXxx
                        from xxx in tempXxx.DefaultIfEmpty()
                        join ban in _entities.banks on pay.bank_id equals ban.bank_id into tempBan
                        from ban in tempBan.DefaultIfEmpty()
                        join cre in _entities.users on pay.created_by equals cre.user_id into tempCre
                        from cre in tempCre.DefaultIfEmpty()
                        join up in _entities.users on pay.updated_by equals up.user_id into tempUp
                        from up in tempUp.DefaultIfEmpty()
                        join app in _entities.users on pay.approved_by equals app.user_id into tempApp
                        from app in tempApp.DefaultIfEmpty()
                        join pm in _entities.payment_method on pay.payment_method_id equals pm.payment_method_id into
                            temPm
                        from pm in temPm.DefaultIfEmpty()

                        join pty in _entities.parties on pay.party_id equals pty.party_id into tempPty
                        from pty in tempPty.DefaultIfEmpty()
                        join teri in _entities.territories on pty.territory_id equals teri.territory_id into temTeri
                        from teri in temTeri.DefaultIfEmpty()
                        where pay.party_id == partyId && (pay.receive_date >= fromDate && pay.receive_date <= toDayes)
                        select new
                        {
                            receive_id = pay.receive_id,
                            receipt_no = pay.receipt_no,
                            receive_date = pay.receive_date,
                            party_name = par.party_name,
                            payment_method_name = pm.payment_method_name,
                            amount = pay.amount,
                            bank_name = ban.bank_name,
                            branch_name = xxx.bank_branch_name, //new
                            reference_no = pay.cheque_no,
                            bank_charge = pay.bank_charge,
                            invoice_no = pay.invoice_no, //new
                            last_invoice_balance = pay.last_invoice_balance,
                            representative = pay.representative,
                            remarks = pay.remarks,
                            payment_req_id = pay.payment_req_id,
                            document_attachment = pay.document_attachment,
                            opening_balance = (from pj in _entities.party_journal
                                where pj.party_id == pay.party_id
                                orderby pj.party_journal_id descending
                                select new {pj.closing_balance}).FirstOrDefault().closing_balance ?? 0, //new
                            status = pay.status ?? "Not Approved",
                            approved_by = app.full_name ?? "Pending",
                            created_by = cre.full_name,
                            created_date = pay.created_date,
                            updated_by = up.full_name, //new
                            updated_date = pay.updated_date,
                            territory_name = teri.territory_name //new
                        }).OrderByDescending(p => p.receive_id).ToList();

                    return paymentsReceivedData;
                }
                else
                {
                   
                    var paymentsReceivedData = (from pay in _entities.receives
                                                join par in _entities.parties on pay.party_id equals par.party_id into tempPar
                                                from par in tempPar.DefaultIfEmpty()
                                                join xxx in _entities.bank_branch on pay.bank_branch_id equals xxx.bank_branch_id into tempXxx
                                                from xxx in tempXxx.DefaultIfEmpty()
                                                join ban in _entities.banks on pay.bank_id equals ban.bank_id into tempBan
                                                from ban in tempBan.DefaultIfEmpty()
                                                join cre in _entities.users on pay.created_by equals cre.user_id into tempCre
                                                from cre in tempCre.DefaultIfEmpty()
                                                join up in _entities.users on pay.updated_by equals up.user_id into tempUp
                                                from up in tempUp.DefaultIfEmpty()
                                                join app in _entities.users on pay.approved_by equals app.user_id into tempApp
                                                from app in tempApp.DefaultIfEmpty()
                                                join pm in _entities.payment_method on pay.payment_method_id equals pm.payment_method_id into temPm
                                                from pm in temPm.DefaultIfEmpty()

                                                join pty in _entities.parties on pay.party_id equals pty.party_id into tempPty
                                                from pty in tempPty.DefaultIfEmpty()
                                                join teri in _entities.territories on pty.territory_id equals teri.territory_id into temTeri
                                                from teri in temTeri.DefaultIfEmpty()
                                                where pay.updated_date >= fromDate && pay.updated_date <= toDayes

                                                select new
                                                {
                                                    receive_id = pay.receive_id,
                                                    receipt_no = pay.receipt_no,
                                                    receive_date = pay.receive_date,
                                                    party_name = par.party_name,
                                                    payment_method_name = pm.payment_method_name,
                                                    amount = pay.amount,
                                                    bank_name = ban.bank_name,
                                                    branch_name = xxx.bank_branch_name,//new
                                                    reference_no = pay.cheque_no,
                                                    bank_charge = pay.bank_charge,
                                                    invoice_no = pay.invoice_no,//new
                                                    last_invoice_balance = pay.last_invoice_balance,
                                                    representative = pay.representative,
                                                    remarks = pay.remarks,
                                                    payment_req_id = pay.payment_req_id,
                                                    document_attachment = pay.document_attachment,
                                                    opening_balance = (from pj in _entities.party_journal
                                                                       where pj.party_id == pay.party_id
                                                                       orderby pj.party_journal_id descending
                                                                       select new { pj.closing_balance }).FirstOrDefault().closing_balance ?? 0,//new
                                                    status = pay.status ?? "Not Approved",
                                                    approved_by = app.full_name ?? "Pending",
                                                    created_by = cre.full_name,
                                                    created_date = pay.created_date,
                                                    updated_by = up.full_name,//new
                                                    updated_date = pay.updated_date,
                                                    territory_name = teri.territory_name//new
                                                }).OrderByDescending(p => p.receive_id).ToList();

                    return paymentsReceivedData;
                }
               
            }
            catch (Exception ex)
            {

                return ex;
            }
        }
    }
}