using DMSApi.Models;
using DMSApi.Models.IRepository;
using System;
using System.Linq;

namespace DMSApi.Models.Repository
{
    public class CityRepository : ICityRepository
    {
        private DMSEntities _entities;

        public CityRepository()
        {
            this._entities = new DMSEntities();
        }

        public city GetCityByID(long city_id)
        {
            throw new NotImplementedException();
        }

        public city GetCityBYName(string city_name)
        {
            throw new NotImplementedException();
        }

        public city GetCityByDetails(string city_details)
        {
            throw new NotImplementedException();
        }

        public city GetCityByCountryID(long country_id)
        {
            throw new NotImplementedException();
        }

        public bool CheckCityForDuplicateByName(string city_name)
        {
            var checkCityIsExist = _entities.cities.FirstOrDefault(co => co.city_name == city_name);
            bool return_type = checkCityIsExist == null ? false : true;
            return return_type;
        }

        public bool InsertCity(city ocity)
        {
            try
            {
                city insert_city = new city
                {
                    city_name = ocity.city_name,                  
                    created_by = ocity.created_by,
                    created_date = ocity.created_date,
                    updated_by = ocity.updated_by,
                    updated_date = ocity.updated_date,
                    is_active = ocity.is_active,
                    is_deleted = false
                };
                _entities.cities.Add(insert_city);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateCity(city ocity)
        {
            try
            {
                city ci = _entities.cities.Find(ocity.city_id);
                ci.city_name = ocity.city_name;            
                ci.updated_by = ocity.updated_by;
                ci.updated_date = ocity.updated_date;
                ci.is_active = ocity.is_active;
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteCity(long city_id)
        {
            try
            {
                city oCity = _entities.cities.FirstOrDefault(c => c.city_id == city_id);
                _entities.cities.Attach(oCity);
                _entities.cities.Remove(oCity);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public object GetAllCities()
        {
            var cities = (from c in _entities.cities
                         
                         
                          select new
                          {
                              city_id = c.city_id,
                              city_name = c.city_name,
                              created_by = c.created_by,
                              created_date = c.created_date,
                              updated_by = c.updated_by,
                              updated_date = c.updated_date,
                              is_active = c.is_active
                            
                            

                          }).OrderByDescending(c=>c.city_id).ToList();

            return cities;
        }
    }
}