using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSApi.Models.IRepository
{
   public interface IReportRepository
    {
       object GetAccountsDueAdvanceReport(long partyId, long regoinId,long areaId, long partyTypeId);
       object GetDailySalesReport(string fromDate, string toDate, long productCategoryId, long productId);
       object GetDailySalesReportPDF(string fromDate, string toDate, long productCategoryId, long productId);
       object InvoiceWiseImeiReport(string fromDate, string toDate, long invoiceMasterId);
       object GetInvoiceWiseImeiReportPDF(long invoiceMasterId);
       object GetProductHistoryReport(string fromDate, string toDate, long productCategoryId, long productId,long colorId);
       object GetProductHistoryPdfReport(string fromDate, string toDate, long productCategoryId, long productId, long colorId);
       object GetSalableAndNonSalableStock();

       object DailyPaymentReport(string fromDate, string toDate, long partyTypeId,
           long partyId);
       object GetAllInvoiceNo(long partyId);
       object GetAllInvoiceNo();
       object GetAccountsReport(string fromDate, string toDate, long partyTypeId,
         long partyId, string receivedInvoiceNo);

    }
}
