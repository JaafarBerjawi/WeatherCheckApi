using Common.Web;
using Microsoft.AspNetCore.Mvc;
using WeatherCheck.Business;
using WeatherCheck.Entities;

namespace WeatherCheckApi.Controllers
{
    [ApiController]
    [Route("api/weather")]
    public class WeatherController : BaseApiController
    {
        private readonly IWeatherAPIService _weatherAPIService;
        private readonly CurrentWeatherService _currentWeatherService;
        public WeatherController(IWeatherAPIService weatherAPIService, CurrentWeatherService currentWeatherService)
        {
            _weatherAPIService = weatherAPIService;
            _currentWeatherService = currentWeatherService;
        }

        [HttpGet("current")]
        public object GetCurrentWeatherCondition(string city)
        {
            return GetAPIResponse(_weatherAPIService.GetCurrentWeatherCondition(city));
        }

        [HttpGet("savedhistory")]
        public object GetSavedWeatherConditions(string city)
        {
            var userId = (int)HttpContext.Items["UserId"];
            return GetAPIResponse(_currentWeatherService.GetSavedWeatherConditions(city, userId));
        }

        [HttpPost("savecurrentweather")]
        public object SaveCurrentWeather(CurrentWeatherCondition condition)
        {
            var userId = (int)HttpContext.Items["UserId"];
            condition.UserId = userId;
            return GetAPIResponse(_currentWeatherService.AddCurrentWeatherCondition(condition));
        }
    }
}