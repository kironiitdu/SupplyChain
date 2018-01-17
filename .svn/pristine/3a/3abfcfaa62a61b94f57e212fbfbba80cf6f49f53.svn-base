using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMSApi.Models.IRepository;
using DMSApi.Models.StronglyType;
using DMSApi.Models.crystal_models;
using Newtonsoft.Json.Linq;

namespace DMSApi.Models.Repository
{
    public class PartyJournalRepository:IPartyJournalRepository
    {
        private readonly DMSEntities _entities;
        public PartyJournalRepository()
        {
            this._entities = new DMSEntities();
        }
        public long PartyJournalEntry(string transactionType, long partyId, decimal transactionAmount, string remarks, long userId, string documentCode)
        {
            try
            {
                // GET TRANSACTION CONFIGURATION
                var tranConfig = _entities.transaction_configuration.FirstOrDefault(w => w.transaction_type == transactionType);
                if (tranConfig != null)
                {
                    party_journal partyJournal = new party_journal
                    {
                        transaction_date = DateTime.Now,
                        transaction_type = transactionType,
                        party_id = partyId,
                        opening_balance = 0,
                        dr_amount = 0,
                        cr_amount = 0,
                        closing_balance = 0,
                        remarks = remarks,
                        created_by = userId,
                        document_code = documentCode,
                        created_date = DateTime.Now,
                        updated_by = userId,
                        updated_date = DateTime.Now,
                    };

                    // GET OPENNIG BALANCE
                    var partyTransaction =
                        _entities.party_journal.Where(w => w.party_id == partyId)
                            .OrderByDescending(o => o.party_journal_id)
                            .FirstOrDefault();
                    decimal closingBalance = 0;
                    if (partyTransaction != null)
                    {
                        closingBalance = partyTransaction.closing_balance??0;
                    }

                    partyJournal.opening_balance = closingBalance;
                        // DEBIT OR CREDIT
                        if (tranConfig.is_dr??false)
                        {
                            partyJournal.dr_amount = transactionAmount;
                        }
                        else if (tranConfig.is_cr??false)
                        {
                            partyJournal.cr_amount = transactionAmount;
                        }
                        partyJournal.closing_balance = partyJournal.opening_balance + partyJournal.dr_amount - partyJournal.cr_amount;

                        // INSERT PARTY JOURNAL
                        _entities.party_journal.Add(partyJournal);
                        _entities.SaveChanges();
                        return partyJournal.party_journal_id;
                    
                }
            }
            catch (Exception)
            {
                return 0;
            }
            return 0;
        }
        //public object GetPartyJournalReportById(int party_id, string from_date, string to_date)
        public object GetPartyJournalReportById(long party_id, DateTime from_dt, DateTime to_dt)
        {
            try
            {
               // DateTime fdate=from_dt.ToString("dd")
                //where to_date(party_journal.transaction_date,'DD/MM/YYYY') between to_date('" + from_dt + "', 'DD/MM/YYYY') and to_date('" + to_dt + "', 'DD/MM/YYYY') and party_journal.party_id = " + party_id + " order by party_journal.transaction_date asc ";
                if (party_id != null && from_dt != null && to_dt != null)
                {

                    //string query = "select party_journal.party_journal_id, party_journal.transaction_date, party_journal.transaction_type, party_journal.party_id, party_journal.opening_balance, party_journal.dr_amount, party_journal.cr_amount, party_journal.closing_balance, party_journal.remarks, party_journal.created_by, party_journal.created_date, party_journal.updated_by, party_journal.updated_date, party.party_name, users.full_name, party.address,party.proprietor_name,party.phone, party.mobile, party.email, location.location_name from party_journal inner join party on party_journal.party_id=party.party_id inner join users on party_journal.created_by=users.user_id inner join party_type on party.party_type_id=party_type.party_type_id left join location on party.location_id=location.location_id where to_date(party_journal.transaction_date,'DD/MM/YYYY') between to_date('" + from_dt +"', 'DD/MM/YYYY') and to_date('" + to_dt + "', 'DD/MM/YYYY') and party_journal.party_id = " + party_id + " order by party_journal.transaction_date asc";
                    string query = @" select party_journal.party_journal_id, party_journal.transaction_date, party_journal.transaction_type, party_journal.party_id, party_journal.opening_balance, 
                                   party_journal.dr_amount, party_journal.cr_amount, party_journal.closing_balance, party_journal.remarks, party_journal.created_by, party_journal.created_date,  
                                   party_journal.updated_by, party_journal.updated_date, party.party_name, users.full_name, party.address,party.proprietor_name,party.phone, party.mobile, party.email,  
                                   party_journal.document_code, payment_method.payment_method_name, area.area_name 
                                   from party_journal 
                                   inner join party on party_journal.party_id=party.party_id inner join users on party_journal.created_by=users.user_id inner join party_type on party.party_type_id=party_type.party_type_id 
                                   
                                   left join receive on party_journal.document_code=receive.receipt_no 
                                   left join payment_method on receive.payment_method_id=payment_method.payment_method_id 
                                   
                                   left join area on party.area_id = area.area_id 
                                   where (party_journal.transaction_date between '" + from_dt + "' and '" + to_dt + "') and party_journal.party_id = " + party_id + " order by party_journal.transaction_date asc ";
   
                    var reData = _entities.Database.SqlQuery<PartyJournalReportModel>(query).ToList();
                    
                    //Update By Rabbi
                    if (reData.Count == 0)
                    {
                        string query1 = "select party.party_name, party.address,party.proprietor_name,party.phone, party.mobile, party.email, area.area_name from party inner join party_type on party.party_type_id=party_type.party_type_id left join area on party.area_id = area.area_id where party.party_id = " + party_id;
                        var reData1 = _entities.Database.SqlQuery<PartyJournalReportModel>(query1).ToList();
                        return reData1;
                    }
                    else
                    {
                        return reData;    
                    }

                }
//                else if (party_id != null && from_dt == null && to_dt == null)
//                {
//                    //string query = "select party_journal.party_journal_id, party_journal.transaction_date, party_journal.transaction_type, party_journal.party_id, party_journal.opening_balance, party_journal.dr_amount, party_journal.cr_amount, party_journal.closing_balance, party_journal.remarks, party_journal.created_by, party_journal.created_date, party_journal.updated_by, party_journal.updated_date, party.party_name, users.full_name, party.address,party.proprietor_name,party.phone, party.mobile, party.email, location.location_name from party_journa inner join party on party_journal.party_id=party.party_id inner join users on party_journal.created_by=users.user_id where inner join party_type on party.party_type_id=party_type.party_type_id left join location on party.location_id=location.location_id party_journal.party_id = " + party_id + " order by party_journal.transaction_date asc";
//                    string query = @" select party_journal.party_journal_id, party_journal.transaction_date, party_journal.transaction_type, party_journal.party_id, party_journal.opening_balance,  
//                                     party_journal.dr_amount, party_journal.cr_amount, party_journal.closing_balance, party_journal.remarks, party_journal.created_by, party_journal.created_date,  party_journal.updated_by, party_journal.updated_date, party.party_name, users.full_name, party.address, party.proprietor_name,party.phone, party.mobile, party.email, location.location_name  party_journal.document_code, payment_method.payment_method_name, zone.zone_name, area.area_name  from party_journa inner  join party on party_journal.party_id=party.party_id inner join users on party_journal.created_by=users.user_id where inner join party_type on party.party_type_id=party_type.party_type_id  left join location on party.location_id=location.location_id  left join receive on party_journal.document_code=receive.receipt_no  left join payment_method on receive.payment_method_id=payment_method.payment_method_id  left join zone on party.zone_id = zone.zone_id  left join area on party.area_id = area.area_id  where party_journal.party_id = " + party_id + " order by party_journal.transaction_date asc";

//                    var reData = _entities.Database.SqlQuery<PartyJournalReportModel>(query).ToList();
//                    return reData;
//                }
                return null;
            }
            catch (Exception ex )
            {
                var kkk = ex.Message;
                return kkk;
            }

        }

        public decimal GetPartyOpeningBalance(long party_id)
        {
            try
            {
                decimal openingBalance = 0;
                var opening_balance = _entities.party_journal.Where(w => w.party_id == party_id).OrderByDescending(o => o.party_journal_id).FirstOrDefault();
                if (opening_balance != null)
                {
                    openingBalance = -opening_balance.closing_balance ?? 0;
                }

                return openingBalance;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public int SaveInitialBalance(party_journal partyJournal)
        {
            try
            {
                var objPartyJournal = new party_journal
                {
                    transaction_date = DateTime.Now,
                    transaction_type = "INITIAL-BALANCE",
                    party_id = partyJournal.party_id,
                    opening_balance = partyJournal.opening_balance,
                    dr_amount = 0,
                    cr_amount = 0,
                    closing_balance = partyJournal.opening_balance,
                    remarks = "Initial Balance",
                    created_by = partyJournal.created_by,
                    document_code = "INITIAL-BALANCE",
                    created_date = DateTime.Now,
                    updated_by = partyJournal.created_by,
                    updated_date = DateTime.Now,
                };
                _entities.party_journal.Add(objPartyJournal);
                _entities.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}