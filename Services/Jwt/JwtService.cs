using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ms_Auth.Services.Jwt
{

    public class JwtService : IjwtService
    {
        public readonly IConfiguration configuration;
        public JwtService(IConfiguration configuration)
        {
           this.configuration = configuration; 
        }
        public string GenerateToken(string userId)
        {
            // adding the signing key (which contain our key + algo)
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwt:key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            // getting values from appsettings
            string issuer = configuration["jwt:Issuer"] ?? "";
            string audience = configuration["jwt:Audience"] ?? "";
            int expirationMinutes = configuration.GetValue<int>("jwt:expirationMinutes", 10);
            // adding claims (payload)
            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub , userId),
                new Claim(JwtRegisteredClaimNames.Iat , DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString())
            };
            var token = new JwtSecurityToken(
                 issuer: issuer,
                 expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
                 audience: audience,
                 claims : claims,
                 signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
