using Common.Business;
using Common.Entities;
using System.Net;
using System.Text.Json.Serialization;
using WeatherCheck.Entities;

namespace WeatherCheck.Business
{
    //Concrete implementation, for api.weatherapi.com
    public class WeatherAPIService : IWeatherAPIService
    {
        private readonly static string baseUrl = "http://api.weatherapi.com/v1";
        private readonly static string key = "5cbc7df1f574486bb9c184811232907";
        public APIResponseResult<CurrentWeatherCondition> GetCurrentWeatherCondition(string city)
        {
            var queryParams = new Dictionary<string, object>
            {
                { "key", key },
                { "q", city },
            };
            var method = "current.json";
            try
            {
                var data = APIService.Get<WeatherData>(new HttpGetContext
                {
                    BaseUrl = baseUrl,
                    HttpMethod = method,
                    QueryParams = queryParams
                });
                if (data.StatusCode != HttpStatusCode.OK)
                {
                    //Did not process error, sent it as it is from data provider
                    return APIResponseHelper.GetBadRequestResponse<CurrentWeatherCondition>(data?.Error?.Code, data?.Error?.Message);
                }
                return APIResponseHelper.GetSuccessfulResponse(data.MapToCurrentWeatherCondition(city));
            }
            catch
            {
                return APIResponseHelper.GetInternalServerErrorResponse<CurrentWeatherCondition>();
            }
        }

        public class WeatherData : IHttpResponse
        {
            public HttpStatusCode StatusCode { get; set; }
            [JsonPropertyName("current")]
            public CurrentWeather Current { get; set; }

            [JsonPropertyName("error")]
            public Error Error { get; set; }
            /// <summary>
            /// Structures the reponse of weather api as the response the application respods with
            /// </summary>
            /// <param name="city"></param>
            /// <returns></returns>
            public CurrentWeatherCondition MapToCurrentWeatherCondition(string city)
            {
                return new CurrentWeatherCondition
                {
                    City = city,
                    Humidity = Current.Humidity,
                    RequestTime = DateTime.Now,
                    Temperature = Current.Temp_C,
                    WindSpeed = Current.Wind_Kph
                };
            }
        }

        public class CurrentWeather
        {

            [JsonPropertyName("temp_c")]
            public double Temp_C { get; set; }

            [JsonPropertyName("wind_kph")]
            public double Wind_Kph { get; set; }

            [JsonPropertyName("humidity")]
            public int Humidity { get; set; }

        }

        public class Error
        {
            [JsonPropertyName("message")]
            public string Message { get; set; }
            [JsonPropertyName("code")]
            public string Code { get; set; }
        }

    }
}
