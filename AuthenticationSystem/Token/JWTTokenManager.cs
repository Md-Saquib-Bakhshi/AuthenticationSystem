using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthenticationSystem.Token
{
    public class JwtTokenManager : IJwtTokenManager
    {
        private readonly IConfiguration _configuration;

        public JwtTokenManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Authenticate(string username) 
        {
            var key = _configuration.GetValue<string>("JwtConfig:Key");
            var keyBytes = Encoding.UTF8.GetBytes(key);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "YourIssuer",
                Audience = "YourAudience",
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username) 
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(keyBytes),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
