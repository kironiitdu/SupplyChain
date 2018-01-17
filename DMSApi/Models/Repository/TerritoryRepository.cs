using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMSApi.Models.IRepository;
using Newtonsoft.Json.Linq;

namespace DMSApi.Models.Repository
{
    public class TerritoryRepository : ITerritoryRepository
    {
        private DMSEntities _entities;

        public TerritoryRepository()
        {
            this._entities = new DMSEntities();
        }
        public object GetAllTerritoryByAreaId(long area_id)
        {
            try
            {
                var territoryByAreaId = (from ter in _entities.territories
                                         where ter.area_id == area_id

                                         select new
                                         {
                                             territory_id = ter.territory_id,
                                             territory_name = ter.territory_name
                                             // area_id=ter.area_id
                                         }).ToList();

                return territoryByAreaId;

            }
            catch (Exception)
            {

                return 0;
            }
        }
        public object GetAllTerritory()
        {

            try
            {
                var territorys = (from t in _entities.territories
                                  join a in _entities.areas on t.area_id equals a.area_id
                                  join r in _entities.regions on a.region_id equals r.region_id
                                  select new
                                  {
                                      region_id = r.region_id,
                                      region_name = r.region_name,
                                      area_id = a.area_id,
                                      area_name = a.area_name,
                                      territory_id = t.territory_id,
                                      territory_name = t.territory_name,
                                      territory_code = t.territory_code,
                                      is_active = t.is_active
                                  }).OrderBy(o => o.area_name).ToList();

                return territorys;

            }
            catch (Exception)
            {

                return 0;
            }
        }
        public bool AddTerritory(territory oTerritory)
        {
            try
            {
                var iTerritory = new territory
                {
                    area_id = oTerritory.area_id,
                    territory_name = oTerritory.territory_name,
                    territory_code = oTerritory.territory_code,
                    is_active = oTerritory.is_active,
                    created_date = DateTime.Now,
                    created_by = oTerritory.created_by,
                    updated_date = DateTime.Now,
                    updated_by = oTerritory.updated_by
                };
                _entities.territories.Add(iTerritory);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public territory GetTerritoryById(long Territory_id)
        {
            throw new NotImplementedException();
        }

        public bool EditTerritory(territory oTerritory)
        {
            try
            {
                territory t = _entities.territories.Find(oTerritory.territory_id);
                t.area_id = oTerritory.area_id;
                t.territory_name = oTerritory.territory_name;
                t.territory_code = oTerritory.territory_code;
                t.is_active = oTerritory.is_active;
                t.updated_by = oTerritory.updated_by;
                t.updated_date = DateTime.Now;
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteTerritory(long territory_id)
        {
            try
            {
                territory objTerritory = _entities.territories.FirstOrDefault(t => t.territory_id == territory_id);
                _entities.territories.Attach(objTerritory);
                _entities.territories.Remove(objTerritory);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }




        public bool CheckDuplicateAreaTerritory(territory mTerritory)
        {
            var checkDuplicateTerritory = _entities.territories.FirstOrDefault(a => a.territory_name == mTerritory.territory_name && a.area_id == mTerritory.area_id && a.territory_id != mTerritory.area_id);
            bool returnType = (checkDuplicateTerritory != null);
            return returnType;
        }

        public object GetPartyWiseTerritory(long party_id)
        {
            try
            {
               


                var ttt = (from p in _entities.parties
                           join r in _entities.regions on p.region_id equals r.region_id
                           join a in _entities.areas on p.area_id equals a.area_id
                           join t in _entities.territories on p.territory_id equals t.territory_id                          
                           where p.party_id == party_id
                           select new
                           {
                               region_id = p.region_id,
                               area_id = p.area_id,
                               territory_id = p.territory_id,
                               mobile = p.mobile
                           }).ToList().FirstOrDefault();

                return ttt;
               
            }
            catch (Exception)
            {
                throw;
            }
        }

        public long GetPartywiseTerritoryId(long party_id)
        {
            throw new NotImplementedException();
        }
    }
}