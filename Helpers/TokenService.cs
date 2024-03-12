using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ApiService.Models;
using Microsoft.IdentityModel.Tokens;

namespace ApiService.Services
{
    public class TokenService
    {
        private const int ExpirationMinutes = 30;
        private readonly IConfiguration _config;
        public TokenService(IConfiguration config)
        {

            _config = config;
        }
        public string CreateToken(Users user)
        {
            var expiration = DateTime.UtcNow.AddMinutes(ExpirationMinutes);
            var token = CreateJwtToken(
                CreateClaims(user),
                CreateSigningCredentials(),
                expiration
            );
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        private JwtSecurityToken CreateJwtToken(List<Claim> claims, SigningCredentials credentials,
            DateTime expiration) =>
            new(
                "Rigoberto Meneses",
				"Rigoberto Meneses",
                claims,
                expires: expiration,
                signingCredentials: credentials
            );

        private List<Claim> CreateClaims(Users user)
        {
            try
            {               

                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, "Rigoberto Meneses"),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };
                return claims;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        private SigningCredentials CreateSigningCredentials()
        {
            return new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_config.GetValue<string>("ApiSettings:SecretPassword"))
                ),
                SecurityAlgorithms.HmacSha256
            );
        }
    }
}
