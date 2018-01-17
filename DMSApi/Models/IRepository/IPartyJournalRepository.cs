using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSApi.Models.IRepository
{
    public interface IPartyJournalRepository
    {
        long PartyJournalEntry(string transactionType, long partyId, decimal transactionAmount, string remarks, long userId, string documentCode);
        //object GetPartyJournalReportById(long party_id, string from_date, string to_date);
        object GetPartyJournalReportById(long party_id, DateTime from_date, DateTime to_date);
        decimal GetPartyOpeningBalance(long party_id);//28.01.2017
        int SaveInitialBalance(party_journal partyJournal);
    }
}
