namespace DMSApi.Models.IRepository
{
    public interface ICityRepository
    {
        object GetAllCities();

        city GetCityByID(long city_id);

        city GetCityBYName(string city_name);

        city GetCityByDetails(string city_details);

        city GetCityByCountryID(long country_id);

        bool CheckCityForDuplicateByName(string city_name);

        bool InsertCity(city ocity);

        bool UpdateCity(city ocity);

        bool DeleteCity(long city_id);
    }
}