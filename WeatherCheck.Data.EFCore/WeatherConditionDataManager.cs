using Data.EFCore;
using WeatherCheck.Entities;

namespace WeatherCheck.Data.EfCore
{
    //Data Access using EF Core => for application
    public class WeatherConditionDataManager : IWeatherConditionDataManager
    {
        private readonly ApplicationDBContext _applicationDBContext;
        public WeatherConditionDataManager(ApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }

        public List<CurrentWeatherCondition> GetSavedCurrentWeatherConditionsByCity(string city, long userId)
        {
            return _applicationDBContext.WeatherConditions.Where(x => x.City == city && x.UserId == userId).ToList();
        }

        public void SaveCurrentWeatherCondition(CurrentWeatherCondition currentWeatherCondition)
        {
            _applicationDBContext.WeatherConditions.Add(currentWeatherCondition);
            _applicationDBContext.SaveChanges();
        }
    }
}