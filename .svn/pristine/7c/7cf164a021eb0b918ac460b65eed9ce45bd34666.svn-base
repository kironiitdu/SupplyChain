using System.Collections.Generic;

namespace DMSApi.Models.IRepository
{
    public interface IPaymentMethodRepository
    {
        List<payment_method> GetAllPaymentMethod(long company_id);
        List<payment_method> GetAllPaymentMethod();

        payment_method GetSinglePaymentMethod(long payment_method_id);

        bool InsertPaymentMethod(payment_method payment_method, long? created_by);

        bool UpdatePaymentMethod(payment_method payment_method, long? updated_by);

        bool DeletePaymentMethod(long payment_method_id, long? updated_by);

        bool CheckDuplicatePaymentMethod(string payment_method_name);
    }
}