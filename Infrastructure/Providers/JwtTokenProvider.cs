using Domain.JwtModels;
using Domain.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Providers
{
    public class JwtTokenProvider : IJwtTokenProvider
    {
        private readonly JwtOptions _options;
        public JwtTokenProvider(IOptions<JwtOptions> options)
        {
            _options = options.Value;
        }
        public string GenerateToken(User user)
        {
            List<Claim> claims = new()
            {
                new("userLogin", user.Login) 
            };

            var signingCredentionals = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
                SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: claims.ToArray(),
                signingCredentials: signingCredentionals,
                expires: DateTime.UtcNow.AddHours(_options.ExpiresHours));
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
