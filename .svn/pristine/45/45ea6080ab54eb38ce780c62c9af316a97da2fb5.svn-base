using DMSApi.Models.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DMSApi.Models.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private DMSEntities _entities;

        public CountryRepository()
        {
            this._entities = new DMSEntities();
        }

        public List<country> GetAllCountries()
        {
            List<country> countries = _entities.countries.Where(c=>c.is_deleted!=true).OrderBy(c=>c.country_name).ToList();
            return countries;
        }

        public country GetCountryByID(long country_id)
        {
            throw new NotImplementedException();
        }

        public bool InsertCountry(country oCountry)
        {
            try
            {
                country insert_country = new country
                {
                    country_name = oCountry.country_name,
                    country_code = oCountry.country_code,    
                    created_by = oCountry.created_by,
                    created_date = oCountry.created_date,
                    updated_by = oCountry.updated_by,
                    updated_date = oCountry.updated_date,                   
                    is_active = oCountry.is_active,
                    is_deleted = false
                };
                _entities.countries.Add(insert_country);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteCountry(long country_id)
        {
            try
            {
                country oCountry = _entities.countries.FirstOrDefault(c => c.country_id == country_id);
                _entities.countries.Attach(oCountry);
                _entities.countries.Remove(oCountry);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateCountry(country oCountry)
        {
            try
            {
                country con = _entities.countries.Find(oCountry.country_id);
                con.country_name = oCountry.country_name;
                con.country_code = oCountry.country_code;  
                con.updated_by = oCountry.updated_by;
                con.updated_date = oCountry.updated_date;              
                con.is_active = oCountry.is_active;
                _entities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool CheckDuplicateCountry(string CountryName)
        {
            var checkDuplicateCountry = _entities.countries.FirstOrDefault(c => c.country_name == CountryName);

            bool return_type = checkDuplicateCountry == null ? false : true;
            return return_type;
        }

       
    }
}