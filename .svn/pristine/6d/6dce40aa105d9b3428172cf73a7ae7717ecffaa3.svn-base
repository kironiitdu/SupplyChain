using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSApi.Models.IRepository
{
    public interface IShippingMethodRepository
    {
        List<shipping_method> GetShippingMethods();
        shipping_method GetShippingMethodsByID(long shipping_method_id);
        bool InsertShippingMethods(shipping_method oShippingMethods);
        bool DeleteShippingMethods(long shipping_method_id);
        bool UpdateShippingMethods(shipping_method oShippingMethods);
        bool CheckDuplicateShippingMethods(string ShippingMethodsName);
    }
}
