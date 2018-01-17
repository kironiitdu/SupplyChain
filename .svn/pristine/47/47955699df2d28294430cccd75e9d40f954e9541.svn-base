using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSApi.Models.IRepository
{
    public interface ITerritoryRepository
    {

        object GetAllTerritory();
        object GetAllTerritoryByAreaId(long area_id);
        bool AddTerritory(territory aTerritory);
        territory GetTerritoryById(long territory_id);
        bool EditTerritory(territory aTerritory);
        bool DeleteTerritory(long territory_id);
        bool CheckDuplicateAreaTerritory(territory aTerritory);
        object GetPartyWiseTerritory(long party_id);
        long GetPartywiseTerritoryId(long party_id);
    }
}
