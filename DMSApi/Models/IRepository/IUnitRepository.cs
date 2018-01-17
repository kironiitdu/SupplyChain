using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSApi.Models.IRepository
{
    public interface IUnitRepository
    {
        List<unit> GetAllUnits();
        long AddUnit(unit unit);
        unit GetUnitById(long unit_id);
        bool EditUnit(unit unit);
        bool DeleteUnit(long unit_id);
    }
}
