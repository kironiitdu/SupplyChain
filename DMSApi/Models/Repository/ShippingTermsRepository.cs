using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMSApi.Models.IRepository;

namespace DMSApi.Models.Repository
{
    public class ShippingTermsRepository : IShippingTermsRepository
    {
        private DMSEntities _entities;

        public ShippingTermsRepository()
        {
            this._entities = new DMSEntities();
        }

        public List<shipping_terms> GetAllShippingTerms()
        {
            List<shipping_terms> data = _entities.shipping_terms.Where(c => c.is_deleted != true).OrderBy(c => c.shipping_terms_name).ToList();
            return data;
        }

        public shipping_terms GetShippingTermsByID(long shipping_terms_id)
        {
            throw new NotImplementedException();
        }

        public bool InsertShippingTerms(shipping_terms oShippingTerms)
        {
            try
            {
                shipping_terms insertShippingTerms = new shipping_terms
                {
                    shipping_terms_name = oShippingTerms.shipping_terms_name,
                    created_by = oShippingTerms.created_by,
                    created_date = oShippingTerms.created_date,
                    updated_by = oShippingTerms.updated_by,
                    updated_date = oShippingTerms.updated_date,
                    is_active = oShippingTerms.is_active,
                    is_deleted = oShippingTerms.is_deleted
                };
                _entities.shipping_terms.Add(insertShippingTerms);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteShippingTerms(long shipping_terms_id)
        {
            try
            {

                shipping_terms oShippingTerms = _entities.shipping_terms.FirstOrDefault(st => st.shipping_terms_id == shipping_terms_id);
                oShippingTerms.is_deleted = true;
                _entities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateShippingTerms(shipping_terms oShippingTerms)
        {
            try
            {
                shipping_terms con = _entities.shipping_terms.Find(oShippingTerms.shipping_terms_id);
                con.shipping_terms_name = oShippingTerms.shipping_terms_name;
                con.updated_date = DateTime.Now;
                con.is_active = oShippingTerms.is_active;
                _entities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool CheckDuplicateShippingTerms(string ShippingTermsName)
        {
            var checkDuplicateShippingTerms = _entities.shipping_terms.FirstOrDefault(c => c.shipping_terms_name == ShippingTermsName);

            bool return_type = checkDuplicateShippingTerms == null ? false : true;
            return return_type;
        }
    }
}