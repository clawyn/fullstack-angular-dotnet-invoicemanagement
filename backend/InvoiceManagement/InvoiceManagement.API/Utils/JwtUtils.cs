using InvoiceManagement.DL.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InvoiceManagement.API.Utils
{
    public class JwtUtils
    {

        private readonly string _issuer, _audience, _secret;
        public JwtUtils(IConfiguration config)
        {
            _issuer = config.GetSection("TokenInfo").GetSection("issuer").Value!;
            _audience = config.GetSection("TokenInfo").GetSection("audience").Value!;
            _secret = config.GetSection("TokenInfo").GetSection("secret").Value!;
        }
        public string GenerateToken(User user)
        {
            if (user is null) throw new ArgumentNullException();

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));

            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

            Claim[] claims = new Claim[]
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Role,"Admin"),
            };

            JwtSecurityToken token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: credentials,
                issuer: _issuer,
                audience: _audience
            );

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            return handler.WriteToken(token);
        }
    }
}