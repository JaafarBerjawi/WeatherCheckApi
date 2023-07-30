using Common.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Security.Business;
using Security.Entities;

namespace WeatherCheckApi.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticationController : BaseApiController
    {
        private readonly AuthenticationService _authenticationService;
        public AuthenticationController(AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public object Register(User user)
        {
            return GetAPIResponse(_authenticationService.RegisterUser(user.Username, user.Password));
        }

        [HttpPost("generatetoken")]
        public object GetToken(GenerateTokeInput input)
        {
            return GetAPIResponse(_authenticationService.GetToken(input.Username, input.Password));
        }
    }
}
