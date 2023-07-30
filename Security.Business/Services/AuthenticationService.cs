using Common.Business;
using Common.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Security.Data;
using Security.Entities;

namespace Security.Business
{
    public class AuthenticationService
    {
        private readonly EncryptionService _passwordEncryptionService;
        private readonly IAuthenticationDataManager _authenticationDataManager;
        private readonly TokenService _tokenService;
        private readonly IConfiguration _configuration;

        public AuthenticationService(EncryptionService passwordEncryptionService, IAuthenticationDataManager authenticationDataManager, TokenService tokenService, IConfiguration iConfig)
        {
            _passwordEncryptionService = passwordEncryptionService;
            _authenticationDataManager = authenticationDataManager;
            _tokenService = tokenService;
            _configuration = iConfig;
        }

        public APIResponseResult<object> RegisterUser(string username, string password)
        {
            try
            {
                //User already exists, return BadRequest response
                if (_authenticationDataManager.GetUserByUsername(username) != null)
                {
                    return APIResponseHelper.GetBadRequestResponse<object>("USR-ARDY-EXSTS", "User with the same username already exists");
                }
                var user = new User
                {
                    Username = username,
                    Password = _passwordEncryptionService.Encrypt(password)
                };
                _authenticationDataManager.RegisterUser(user);
                //user registered successfully
                return APIResponseHelper.GetSuccessfulResponse<object>(new { });
            }
            catch
            {
                return APIResponseHelper.GetInternalServerErrorResponse<object>();
            }


        }
        public APIResponseResult<UserToken> GetToken(string username, string password)
        {
            try
            {
                var user = _authenticationDataManager.GetUserByUsername(username);
                //Can't find user, return BadRequest response
                if (user == null)
                    return APIResponseHelper.GetBadRequestResponse<UserToken>("USR-NOT-FOUND", "User not found");

                var encryptedPassword = _passwordEncryptionService.Encrypt(password);

                //wrong password
                if (user.Password != encryptedPassword)
                    return APIResponseHelper.GetBadRequestResponse<UserToken>("INCRRCT-PSWRD", "Incorrect Password");

                var userToken = _authenticationDataManager.GetUserTokenByUserId(user.Id);

                var token = _tokenService.GenerateToken();
                //Token validity can be configured in appsettings
                var tokeValidityTimeSpan = _configuration.GetValue<TimeSpan>("TokenValidityTimeSpan");

                //Couldn't find token, Add new (First Time)
                if (userToken == null)
                {
                    userToken = new UserToken
                    {
                        UserId = user.Id,
                        Token = token,
                        ExpirationDate = DateTime.Now.AddSeconds(tokeValidityTimeSpan.TotalSeconds)
                    };
                    _authenticationDataManager.AddUserToker(userToken);
                }
                else // Has a token
                {
                    userToken.Token = token;
                    userToken.ExpirationDate = DateTime.Now.AddSeconds(tokeValidityTimeSpan.TotalSeconds);
                    _authenticationDataManager.UpdateUserToken(userToken);
                }

                return APIResponseHelper.GetSuccessfulResponse(userToken);
            }
            catch
            {
                return APIResponseHelper.GetInternalServerErrorResponse<UserToken>();
            }
        }
        public bool IsAuthenticated(HttpContext context)
        {
            string token = context.Request.Headers["Authorization"];

            if (string.IsNullOrEmpty(token))
                return false;

            if(ValidateToken(token, out var userId))
            {
                context.Items["UserId"] = userId.Value;
                return true;
            }
            return false;
        }

        public bool ValidateToken(string token, out int? userId)
        {
            var userToken = _authenticationDataManager.GetActiveUserToken(token);
            userId = null;
            if(userToken != null)
            {
                userId = userToken.UserId;
                return true;
            }
            return false;
        }
        public bool IsAllowAnonymousAction(HttpContext context)
        {
            var endpoint = context.GetEndpoint();
            //If controller is set as AllowAnonymous
            if (endpoint?.Metadata.GetMetadata<AllowAnonymousAttribute>() != null)
            {
                return true;
            }

            //If Action
            var controllerActionDescriptor = endpoint?.Metadata.GetMetadata<ControllerActionDescriptor>();
            if (controllerActionDescriptor?.ControllerTypeInfo.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Any() == true)
            {
                return true;
            }
            return false;
        }
    }
}
