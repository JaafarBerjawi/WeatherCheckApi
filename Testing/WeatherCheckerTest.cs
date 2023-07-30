using Common.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Security.Business;
using Security.Data;
using Security.Data.Mock;
using WeatherCheck.Business;
using WeatherCheck.Data;
using WeatherCheck.Data.Mock;
using WeatherCheck.Entities;

namespace Testing
{
    public class WeatherCheckerTest
    {
        private readonly AuthenticationService _authenticationService;
        private readonly CurrentWeatherService _currentWeatherService;
        private readonly IWeatherAPIService _weatherAPIService;
        public WeatherCheckerTest()
        {
            //Configure Dependencies
            var services = new ServiceCollection();

            services.AddTransient<AuthenticationService>();
            services.AddTransient<TokenService>();
            services.AddTransient<IWeatherAPIService, WeatherAPIService>();
            services.AddTransient<CurrentWeatherService>();

            //Data Managers as Mock
            services.AddTransient<IAuthenticationDataManager, AuthenticationDataManager>();
            services.AddTransient<IWeatherConditionDataManager, WeatherConditionDataManager>();

            services.AddSingleton<EncryptionService>();
            services.AddSingleton<IConfiguration, ConfigurationManager>();

            var serviceProvider = services.BuildServiceProvider();
            _authenticationService = serviceProvider.GetService<AuthenticationService>();
            _currentWeatherService = serviceProvider.GetService<CurrentWeatherService>();
            _weatherAPIService = serviceProvider.GetService<IWeatherAPIService>();

        }
        [Fact]
        public void TestGetCurrentWeather_Success()
        {
            var currentWeatherResponse = _weatherAPIService.GetCurrentWeatherCondition("Beirut");
            Assert.Equal(currentWeatherResponse.StatusCode, APIResponseStatusCode.Success);
        }

        [Fact]
        public void TestGetCurrentWeather_Fail()
        {
            var currentWeatherResponse = _weatherAPIService.GetCurrentWeatherCondition("a");
            Assert.NotEqual(currentWeatherResponse.StatusCode, APIResponseStatusCode.Success);
        }
        [Fact]
        public void TestRegister_Success()
        {
            var response = _authenticationService.RegisterUser("TestUser3", "password");
            Assert.Equal(response.StatusCode, APIResponseStatusCode.Success);
        }

        [Fact]
        public void TestRegister_Fail()
        {
            var response = _authenticationService.RegisterUser("TestUser2", "password");
            Assert.NotEqual(response.StatusCode, APIResponseStatusCode.Success);
        }
        [Fact]
        public void TestGenerateToken_Success()
        {
            var response = _authenticationService.GetToken("TestUser2", "TestPassword2");
            Assert.Equal(response.StatusCode, APIResponseStatusCode.Success);
        }
        [Fact]
        public void TestGenerateToken_FailWrongPassword()
        {
            var response = _authenticationService.GetToken("TestUser2", "wrong_password");
            Assert.NotEqual(response.StatusCode, APIResponseStatusCode.Success);
        }
        [Fact]
        public void TestGenerateToken_FailWrongUsername()
        {
            var response = _authenticationService.GetToken("Wrong_TestUser2", "TestPassword2");
            Assert.NotEqual(response.StatusCode, APIResponseStatusCode.Success);
        }
        [Fact]
        public void ValidateToken_Success()
        {
            var response = _authenticationService.ValidateToken("TestToken2", out var userId);
            Assert.True(response);
            Assert.Equal(userId, 2);
        }
        [Fact]
        public void ValidateToken_Fail()
        {
            var response = _authenticationService.ValidateToken("Invalid_TestToken2", out var userId);
            Assert.Equal(response, false);
        }
        [Fact]
        public void ValidateToken_FailExpiredToken()
        {
            var response = _authenticationService.ValidateToken("TestToken1", out var userId);
            Assert.Equal(response, false);
        }
        [Fact]
        public void GetSavedWeatherConditions_Success()
        {
            var response = _currentWeatherService.GetSavedWeatherConditions("TestCity1", 1);
            Assert.Equal(response.DataObject.Data.Count, 2);
        }
        [Fact]
        public void SaveWeatherCondition_Success()
        {
            var response = _currentWeatherService.AddCurrentWeatherCondition(new CurrentWeatherCondition { City = "TestCity1", Humidity = 40, RequestTime = DateTime.Now, Temperature = 30, UserId = 1, WindSpeed = 25 });
            Assert.Equal(response.StatusCode, APIResponseStatusCode.Success);
        }
    }
}