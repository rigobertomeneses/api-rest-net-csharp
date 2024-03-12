using ApiService.Models;
using ApiService.Repository.Interfaces;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace backend.Helpers
{
    public class CustomAuthorization : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {

            if (filterContext != null)
            {
                var token = filterContext.HttpContext.Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");

                if (token != null && token != "")
                {
                    if (IsValidToken(filterContext, token))
                    {
                        filterContext.HttpContext.Response.Headers.Add("authToken", token);
                        filterContext.HttpContext.Response.Headers.Add("AuthStatus", "Authorized");
                        filterContext.HttpContext.Response.Headers.Add("storeAccessiblity", "Authorized");

                        return;
                    }
                }
                else
                {

                    filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
                    filterContext.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Por favor autenticarse por medio del Token";

                    ApiResponse apiResponse = new ApiResponse();

                    apiResponse.StatusCode = HttpStatusCode.Forbidden;
                    apiResponse.IsSuccess = false;
                    apiResponse.ErrorMessages.Add("Por favor autenticarse por medio del Token");

                    filterContext.Result = new JsonResult("Por favor autenticarse por medio del Token")
                    {
                        Value = apiResponse
                    };

                }

            }

        }

        public bool IsValidToken(AuthorizationFilterContext filterContext, string token)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("Rigoberto Meneses");
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;

            return true;
        }

    }

}