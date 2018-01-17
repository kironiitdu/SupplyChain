using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMSApi.Models.StronglyType;

namespace DMSApi.Models.IRepository
{
    public interface ICiPlRepository
    {
        object GetAllCiPl();
        bool CheckDuplicateCiNo(string ciNo);
        long AddCiPl(CiPlModel ciPlModel);
        CiPlModel GetCiPlById(long ci_pl_master_id);
        bool EditCiPl(CiPlModel ciPlModel);
        bool DeleteCiPl(long ci_pl_master_id);
        bool DeleteCiPlDetailsById(long ci_pl_details_id);

        object GetNewCiPl();
    }
}
