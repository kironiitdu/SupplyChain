using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSApi.Models.IRepository
{
    public interface IRegionRepository
    {
        object GetAllRegions();
        bool CheckDuplicateRegions(region mRegion);
        long AddRegion(region region);
        region GetRegionById(long region_id);
        bool EditRegion(region region);
        bool DeleteRegion(long region_id);
    }
}
