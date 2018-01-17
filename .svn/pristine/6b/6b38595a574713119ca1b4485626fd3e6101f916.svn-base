using System;
using System.Collections.Generic;
using System.Linq;
using DMSApi.Models.IRepository;

namespace DMSApi.Models.Repository
{
    public class PaymentMethodRepository : IPaymentMethodRepository
    {
        private DMSEntities _entities;

        public PaymentMethodRepository()
        {
            this._entities = new DMSEntities();
        }

        public List<payment_method> GetAllPaymentMethod(long company_id)
        {
            return _entities.payment_method.Where(p => p.company_id == company_id).ToList();
        }

        public payment_method GetSinglePaymentMethod(long payment_method_id)
        {
            return _entities.payment_method.Find(payment_method_id);
        }

        public bool InsertPaymentMethod(payment_method payment_method, long? created_by)
        {
            payment_method insert_payment_method = new payment_method
            {
                payment_method_name = payment_method.payment_method_name,
                is_active = true,
                is_deleted = false,
                created_by =created_by,
                created_date = DateTime.Now,
               
            };
             _entities.payment_method.Add(insert_payment_method);
                 _entities.SaveChanges();
            return true;
        }

        public bool UpdatePaymentMethod(payment_method payment_method, long? updated_by)
        {
            var obj = _entities.payment_method.Find(payment_method.payment_method_id);
            obj.payment_method_name = payment_method.payment_method_name;
            obj.is_active = payment_method.is_active;          
            obj.updated_by = updated_by;
            obj.updated_date = DateTime.Now;
            _entities.SaveChanges();

            return true;
        }

        public bool DeletePaymentMethod(long payment_method_id, long? updated_by)
        {
            var obj = _entities.payment_method.Find(payment_method_id);
            obj.is_deleted = true;
            obj.updated_by = updated_by;
            obj.updated_date = DateTime.Now;
            _entities.SaveChanges();

            return true;
        }

        public bool CheckDuplicatePaymentMethod(string payment_method_name)
        {
            var checkDuplicatePaymentMethod = _entities.payment_method.FirstOrDefault(p => p.payment_method_name == payment_method_name);
            bool return_PaymentMethod = checkDuplicatePaymentMethod == null ? false : true;
            return return_PaymentMethod;
        }





        public List<payment_method> GetAllPaymentMethod()
        {
            return _entities.payment_method.OrderByDescending(pm => pm.payment_method_id).Where(pm=>pm.is_deleted==false).ToList();
        }
    }
}