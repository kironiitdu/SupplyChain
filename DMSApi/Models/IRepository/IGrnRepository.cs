
using System.Collections.Generic;
using DMSApi.Models.StronglyType;

namespace DMSApi.Models.IRepository
{
    public interface IGrnRepository
    {
        object GetAllGrns();
        object GetGrnReportById(long grn_master_id);
        object GetGrnExcelReportByGrnMasterId(long grn_master_id);
        object GetGrnExcelReportByGrnMasterIdProductIdColorId(long grn_master_id, long product_id, long color_id);
        object GetGrnExcelData(string from_date, string to_date, string product_id, string color_id);
        object GetProductGrnDetailsData(string from_date, string to_date, string product_id, string color_id);
        long AddGrn(GrnModel grnModel);
        GrnModel GetGrnById(long grn_master_id);
        bool EditGrn(GrnModel grnModel); 
        bool DeleteGrn(long grn_master_id);
        Confirmation GetModelIdsByNames(List<string> models);
        Confirmation GetColorIdsByNames(List<string> colors);
        object GetProductInformation(long imei_no);
    }
}
