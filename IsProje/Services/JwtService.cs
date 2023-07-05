using IsProje.Data;
using IsProje.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IsProje.Services
{
    public class JwtService
    {
        private readonly IConfiguration _config;
        public JwtService(IConfiguration config)
        {

            _config = config;


        }
        public string GenerateJwtToken(User user)
        {
            var claims = new Claim[]
 {
 new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
 new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
 new Claim(JwtRegisteredClaimNames.Email, user.UserName),
 new Claim(JwtRegisteredClaimNames.GivenName, user.Name),
 new Claim(JwtRegisteredClaimNames.FamilyName, user.SurName),
 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // Token İd'si
                            };
            var encodedKey = Encoding.UTF8.GetBytes(_config["Jwt:Secret"]); // Jwt de bulunan Secret'ı encode ettik.

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(encodedKey), SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(issuer: _config["Jwt:Issuer"], audience: _config["Jwt:Audience"], claims: claims,
            expires: DateTime.Now.AddMinutes(15), signingCredentials: signingCredentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
