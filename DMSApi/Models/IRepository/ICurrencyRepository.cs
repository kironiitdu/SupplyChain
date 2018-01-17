using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSApi.Models.IRepository
{
    public interface ICurrencyRepository
    {
        List<currency> GetAllCurrency();
        bool CheckDuplicateCurrency(string currency_name);
        long AddCurrency(currency currency);
        currency GetCurrencyById(long currency_id);
        bool EditCurrency(currency currency);
        bool DeleteCurrency(long currency_id);
    }
}
