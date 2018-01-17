using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMSApi.Models.IRepository;

namespace DMSApi.Models.Repository
{
    public class CurrencyRepository:ICurrencyRepository
    {
        private DMSEntities _entities;

        public CurrencyRepository()
        {
            this._entities = new DMSEntities();
        }

        public List<currency> GetAllCurrency()
        {
            var data = _entities.currencies.OrderBy(u => u.currency_name).ToList();
            return data;
        }

        public bool CheckDuplicateCurrency(string currency_name)
        {
            var checkDuplicate = _entities.currencies.FirstOrDefault(c => c.currency_name == currency_name);
            bool return_type = checkDuplicate == null ? false : true;
            return return_type;
        }

        public long AddCurrency(currency currency)
        {
            try
            {
                currency insert_currency = new currency
                {
                    currency_name = currency.currency_name

                };
                _entities.currencies.Add(insert_currency);
                _entities.SaveChanges();
                long last_insert_id = insert_currency.currency_id;
                return last_insert_id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        
        public currency GetCurrencyById(long currency_id)
        {
            var data = _entities.currencies.Find(currency_id);
            return data;
        }

        public bool EditCurrency(currency currency)
        {
            try
            {

                currency emp = _entities.currencies.Find(currency.currency_id);
                emp.currency_name = currency.currency_name;
                _entities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteCurrency(long currency_id)
        {
            try
            {
                currency oCurrency = _entities.currencies.FirstOrDefault(c => c.currency_id == currency_id);
                _entities.currencies.Attach(oCurrency);
                _entities.currencies.Remove(oCurrency);
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