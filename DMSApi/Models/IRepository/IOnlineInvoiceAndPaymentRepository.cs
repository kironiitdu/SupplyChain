using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMSApi.Models.StronglyType;

namespace DMSApi.Models.IRepository
{
    public interface IOnlineInvoiceAndPaymentRepository
    {
        long AddOnlineInvoice(OnlineInvoiceModel onlineInvoice);
        object GetAllOnlineInvoiceAndPayment();
        object GetInvoiceNo(long masterId);
        object GetAmount(long masterId);
        bool InsertOnlinePaymentReceive();  
    }
}
