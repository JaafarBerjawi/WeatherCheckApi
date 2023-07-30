using Common.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Security.Business
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, [FromServices] AuthenticationService authenticationService)
        {
            if (authenticationService.IsAllowAnonymousAction(context))
            {
                // Allow anonymous access, skip the authentication logic
                await _next(context);
            }
            else
            {
                // Check if the request contains authentication information
                if (!authenticationService.IsAuthenticated(context))
                {
                    // If not authenticated, return a 401 Unauthorized response
                    context.Response.StatusCode = (int)APIResponseStatusCode.Unauthorized;
                    var errorResponse = new
                    {
                        ErrorCode = "UNAUTHRZD",
                        ErrorMessage = "Unauthorized Request"
                    };

                    await context.Response.WriteAsJsonAsync(errorResponse);
                    return;
                }

                // If authenticated, continue processing the request
                await _next(context);
            }
        }
    }
}
