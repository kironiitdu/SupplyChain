using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSApi.Models.IRepository
{
    public interface IAreaRepository
    {
        object GetAllAreas();
        bool AddArea(area area);
        area GetAreaById(long area_id);
        bool EditArea(area area);
        bool DeleteArea(long area_id);
        bool CheckDuplicateZoneArea(area mArea);
        List<area> GetRegionwiseAreaForDropdown(long region_id);
    }
}
