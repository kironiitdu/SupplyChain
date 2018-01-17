using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMSApi.Models.StronglyType;

namespace DMSApi.Models.IRepository
{
    public interface IOnlineReturnRepository
    {
        bool AddOnlineReturn(OnlineReturnModel onlineReturnModel);
        object GetOnlineReturnList();
        object GetReturnChallanReport(long returnMasterId);

        bool CheckQuantity(long returnMasterId,long pId,long colId,long verId,int qty);
        object GetReturnRequisitionDetailsList(int partyId, DateTime reqFrom, DateTime reqTo);
    }
}
