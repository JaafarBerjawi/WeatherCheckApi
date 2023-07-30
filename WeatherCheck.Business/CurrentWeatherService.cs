using Common.Business;
using Common.Entities;
using WeatherCheck.Data;
using WeatherCheck.Entities;

namespace WeatherCheck.Business
{
    public class CurrentWeatherService
    {
        private readonly IWeatherConditionDataManager _weatherConditionDataManager;
        public CurrentWeatherService(IWeatherConditionDataManager weatherConditionDataManager)
        {
            _weatherConditionDataManager = weatherConditionDataManager;
        }
        /// <summary>
        /// Gets Saved Weather Conditions by a user for a specific  city
        /// </summary>
        /// <param name="city"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public APIResponseResult<List<CurrentWeatherCondition>> GetSavedWeatherConditions(string city, long userId)
        {
            //No need to check for user validity, this was already done by Authentication middleware
            try
            {
                return APIResponseHelper.GetSuccessfulResponse(_weatherConditionDataManager.GetSavedCurrentWeatherConditionsByCity(city, userId));
            }
            catch
            {
                return APIResponseHelper.GetInternalServerErrorResponse<List<CurrentWeatherCondition>>();
            }
        }
        /// <summary>
        /// Saves a Current Weather Condition
        /// </summary>
        /// <param name="currentWeatherCondition"></param>
        /// <returns></returns>
        public APIResponseResult<object> AddCurrentWeatherCondition(CurrentWeatherCondition currentWeatherCondition)
        {
            try
            {
                _weatherConditionDataManager.SaveCurrentWeatherCondition(currentWeatherCondition);
                return APIResponseHelper.GetSuccessfulResponse<object>(new { });
            }
            catch
            {
                return APIResponseHelper.GetInternalServerErrorResponse<object>();
            }
        }
    }
}
