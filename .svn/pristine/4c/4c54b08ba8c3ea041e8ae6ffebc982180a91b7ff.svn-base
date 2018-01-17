using DMSApi.Models.StronglyType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DMSApi.Models.IRepository
{
   public interface IPaymentRequestRepository
    {
       object GetAllPaymentRequest(long party_id);
       object GetAllPaymentRequestRawSql(long party_id);//28.05.2017
       object GetAllPaymentRequest(DateTime fromDate, DateTime toDate,long party_id);
       List<PaymentRequestModel> GetPaymentRequestByID(long payment_req_id);
       int InsertPaymentRequest();
       int UpdatePaymentRequest();
       bool DeletePaymentRequest(long payment_req_id, long? updated_by);
       bool UpdateStatus(long? payment_req_id, long? user_id);
       HttpResponseMessage GetImage(long payment_req_id);
       object GetPartyTypeName(long party_id);
       object GetPartyPaymentAndRequisitionInfo(long party_id);
       object GetPartyAccountNumber(long party_id);
       object GetAllPaymentRequest();
       object GetAllPaymentRequestRawSql();//28.05.2017
       object GetAllUnProcessedPaymentList();
      
       
    }
}
