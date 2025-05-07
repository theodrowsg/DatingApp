using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Interfaces;
using API.Entities;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class TokenService(IConfiguration config) : ITokenService
    {

        public string CreateToken(AppUser user)
        {
            var _tokenKey = config["TokenKey"] ?? throw new Exception("Can not access tokenKey from appsettings");

            if(_tokenKey.Length < 64) throw new Exception("TokenKey must be at least 64 characters long");

            var _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenKey));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName)
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}