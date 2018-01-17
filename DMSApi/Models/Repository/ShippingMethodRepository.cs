using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMSApi.Models.IRepository;

namespace DMSApi.Models.Repository
{
    public class ShippingMethodRepository:IShippingMethodRepository
    {
        private DMSEntities _entities;

        public ShippingMethodRepository()
        {
            this._entities = new DMSEntities();
        }

        public List<shipping_method> GetShippingMethods()
        {
            List<shipping_method> data = _entities.shipping_method.Where(c => c.is_deleted != true).OrderBy(c => c.shipping_method_name).ToList();
            return data;
        }

        public shipping_method GetShippingMethodsByID(long shipping_method_id)
        {
            throw new NotImplementedException();
        }

        public bool InsertShippingMethods(shipping_method oShippingMethods)
        {
            try
            {
                shipping_method insertShippingMethods = new shipping_method
                {
                    shipping_method_name = oShippingMethods.shipping_method_name,
                    created_by = oShippingMethods.created_by,
                    created_date = oShippingMethods.created_date,
                    updated_by = oShippingMethods.updated_by,
                    updated_date = oShippingMethods.updated_date,
                    is_active = oShippingMethods.is_active,
                    is_deleted = oShippingMethods.is_deleted
                };
                _entities.shipping_method.Add(insertShippingMethods);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteShippingMethods(long shipping_method_id)
        {
            try
            {

                shipping_method oShippingMethods = _entities.shipping_method.FirstOrDefault(st => st.shipping_method_id == shipping_method_id);
                oShippingMethods.is_deleted = true;
                _entities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateShippingMethods(shipping_method oShippingMethods)
        {
            try
            {
                shipping_method con = _entities.shipping_method.Find(oShippingMethods.shipping_method_id);
                con.shipping_method_name = oShippingMethods.shipping_method_name;
                con.updated_date = DateTime.Now;
                con.is_active = oShippingMethods.is_active;
                _entities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool CheckDuplicateShippingMethods(string ShippingMethodsName)
        {
            var checkDuplicateShippingMethods = _entities.shipping_method.FirstOrDefault(c => c.shipping_method_name == ShippingMethodsName);
            bool return_type = checkDuplicateShippingMethods == null ? false : true;
            return return_type;
        }
    }
}