using Common.Entities;
using WeatherCheck.Entities;

namespace WeatherCheck.Business
{
    //Interface, in case we want to use other data provider
    public interface IWeatherAPIService
    {
        APIResponseResult<CurrentWeatherCondition> GetCurrentWeatherCondition(string city);
    }
}