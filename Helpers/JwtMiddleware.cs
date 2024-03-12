namespace ApiService.Helpers;

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using ApiService.Repository.Interfaces;
using System.Net;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using Newtonsoft.Json;
using AutoMapper;
using ApiService.Models;
using System.Security.Claims;
using XAct.Users;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IMapper _mapper;
    public JwtMiddleware(RequestDelegate next, IMapper mapper)
    {
        _next = next;
        _mapper = mapper;
    }

    public async Task Invoke(HttpContext context, IUsersRepository userService)
    {
        HttpResponseMessage response = new HttpResponseMessage();

        try
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                attachUserToContext(context, userService, token);

            await _next(context);

        }
        catch (SecurityTokenExpiredException eSecurityTokenExpiredException)
        {

            await HandleExceptionAsync(context, "Token Expirado");
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e.Message);
        }

    }

    private async Task HandleExceptionAsync(HttpContext httpContext, string mensaje)
    {

        ApiResponse apiResponse = new ApiResponse();

        apiResponse.StatusCode = HttpStatusCode.Forbidden;
        apiResponse.IsSuccess = false;
        apiResponse.ErrorMessages.Add(mensaje);

        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(apiResponse));

    }

    private void attachUserToContext(HttpContext context, IUsersRepository userService, string token)
    {
        try
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

            var NameIdentifier = jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            context.Items["User"] = NameIdentifier;

        }

        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }
}