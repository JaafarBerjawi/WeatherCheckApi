using WeatherCheck.Entities;

namespace WeatherCheck.Data.Mock
{
    //Data Access using mock data => for tests
    public class WeatherConditionDataManager : IWeatherConditionDataManager
    {
        private static readonly List<CurrentWeatherCondition> currentWeatherConditions = new List<CurrentWeatherCondition>
        {
            new CurrentWeatherCondition { Id = 1, City = "TestCity1", UserId = 1, Humidity = 60, Temperature = 35, WindSpeed = 40, RequestTime = DateTime.Now.AddDays(-5) },
            new CurrentWeatherCondition { Id = 2, City = "TestCity1", UserId = 2, Humidity = 70, Temperature = 36, WindSpeed = 50, RequestTime = DateTime.Now.AddDays(-4) },
            new CurrentWeatherCondition { Id = 3, City = "TestCity2", UserId = 1, Humidity = 80, Temperature = 37, WindSpeed = 60, RequestTime = DateTime.Now.AddDays(-3) },
            new CurrentWeatherCondition { Id = 4, City = "TestCity2", UserId = 2, Humidity = 90, Temperature = 38, WindSpeed = 70, RequestTime = DateTime.Now.AddDays(-2) },
            new CurrentWeatherCondition { Id = 5, City = "TestCity1", UserId = 1, Humidity = 90, Temperature = 39, WindSpeed = 80, RequestTime = DateTime.Now.AddDays(-1) },
        };
        public List<CurrentWeatherCondition> GetSavedCurrentWeatherConditionsByCity(string city, long userId)
        {
            return currentWeatherConditions.Where(x => x.City == city && x.UserId == userId)?.ToList();
        }

        public void SaveCurrentWeatherCondition(CurrentWeatherCondition currentWeatherCondition)
        {
            var maxId = currentWeatherConditions.Max(x => x.Id);
            currentWeatherCondition.Id = maxId + 1;
            currentWeatherConditions.Add(currentWeatherCondition);
        }
    }
}