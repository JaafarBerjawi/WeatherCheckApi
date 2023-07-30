using WeatherCheck.Entities;

namespace WeatherCheck.Data
{
    public interface IWeatherConditionDataManager
    {
        List<CurrentWeatherCondition> GetSavedCurrentWeatherConditionsByCity(string city, long userId);
        void SaveCurrentWeatherCondition(CurrentWeatherCondition currentWeatherCondition);
    }
}