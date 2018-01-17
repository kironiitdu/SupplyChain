using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMSApi.Models.crystal_models;
using DMSApi.Models.StronglyType;

namespace DMSApi.Models.IRepository
{
    public interface IMrrRepository
    {
        object GetGrnByGrnMasterId(long grnMasterId);
        object GetAllGrnNo();
        long CreateMrr(MrrModel objMrrModel);
        object GetImeiForMrr(long imei1, int warehouseId);
        object GetAllMrr();
        object GetMrrReportById(long mrrMasterId);
        List<MRRReportModel> GetMrrInformationById(long mrrMasterId);
        object GetAllEmail();
        object GetLoginUserMail(long userId);

    }
}
