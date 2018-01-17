using System.Collections.Generic;

namespace DMSApi.Models.IRepository
{
    public interface ICountryRepository
    {
        List<country> GetAllCountries();

        country GetCountryByID(long country_id);

        bool InsertCountry(country oCountry);

        bool DeleteCountry(long country_id);

        bool UpdateCountry(country oCountry);

        bool CheckDuplicateCountry(string CountryName);

        
    }
}