using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSApi.Models.IRepository
{
    public interface IShippingTermsRepository
    {
        List<shipping_terms> GetAllShippingTerms();
        shipping_terms GetShippingTermsByID(long shipping_terms_id);
        bool InsertShippingTerms(shipping_terms oShippingTerms);
        bool DeleteShippingTerms(long shipping_terms_id);
        bool UpdateShippingTerms(shipping_terms oShippingTerms);
        bool CheckDuplicateShippingTerms(string ShippingTermsName);
    }
}
