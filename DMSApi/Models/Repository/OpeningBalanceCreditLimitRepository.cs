using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMSApi.Models.IRepository;
using DMSApi.Models.StronglyType;

namespace DMSApi.Models.Repository
{
    public class OpeningBalanceCreditLimitRepository:IOpeningBalanceCreditLimitRepository
    {
         private DMSEntities _entities;

         public OpeningBalanceCreditLimitRepository()
        {
            this._entities = new DMSEntities();
        }

        public object GetOpeningBalanceCreditLimit()
        {
            var balanceNCreditLmt = "select pt.party_type_name, pt.party_prefix, p.party_name, p.party_id, p.party_code, p.credit_limit, "
                                    +
                                    " isnull((select top 1 closing_balance from party_journal where party_id=p.party_id order by party_journal_id asc),0) as opening_Balance, "
                                    +
                                    " (select top 1 transaction_type from party_journal where party_id=p.party_id order by party_journal_id asc) as transaction_type  "
                                    + " from party p "
                                    + " inner join party_type pt on p.party_type_id=pt.party_type_id ";

            var data = _entities.Database.SqlQuery<OpeningNCreditLimitModel>(balanceNCreditLmt).ToList();

            //OpeningNCreditLimitModel ob = new OpeningNCreditLimitModel();
            var openingNCreditLimits = new List<OpeningNCreditLimitModel>();
            foreach (var item in data)
            {
                //(contRow == 1 && item.transaction_type != "INITIAL-BALANCE")
                var rowCount = _entities.party_journal.Count(w => w.party_id == item.party_id);
                switch (rowCount)
                {
                    case 0:
                        item.has_transaction = false;
                        break;
                    case 1:
                        if (item.transaction_type == "INITIAL-BALANCE")
                        {
                            item.has_transaction = false;
                        }
                        else
                        {
                            item.has_transaction = true;
                        }
                    break;
                    default:
                        item.has_transaction = true;
                    break;

                }
                openingNCreditLimits.Add(item);
            }

            return openingNCreditLimits;

        }
    }
}