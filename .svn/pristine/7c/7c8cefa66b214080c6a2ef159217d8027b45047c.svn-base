using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSApi.Models.IRepository
{
    public interface IDashboardRepository
    {
        object GetPoStatus();
        object GetPoStatusPanel();
        object GetPiStatusPanel();
        object GetGrnStatusPanel();
        object GetToStatusPanel();
        object GetStockStatusPanel();
        object GetUserInfoStatusPanel();
        object GetApprovalRequisitionStatus();
        object GetRequisitionStatus();
        object GetRequisitionStatusLine();
        object GetSystemMemoryConsumption();
        object GetTotalSystemMemoryConsumptionStatus();
        object TopTenDealerChart(string fromDate,string toDate);
        object BestSellingProducts(string fromDate, string toDate);
        object GetProductLiftingStatus(DateTime from_date, DateTime to_date, string region_id, string area_id, string territory_id);
        object GetTopTenDealerReport(DateTime from_date, DateTime to_date);
        object BestSellingProductsReport(DateTime from_date, DateTime to_date);
        object GetSalesTrendStatus(DateTime from_date, DateTime to_date, string product_id);
    }
}
