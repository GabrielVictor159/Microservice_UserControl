using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.Infraestructure.Database.Entities;
using API.Infraestructure.Messages;
using Microsoft.IdentityModel.Tokens;

namespace API.Services.Token
{
    public class TokenService : ITokenService
    {
        private readonly IPublisher _publisher;
        public TokenService(IPublisher publisher)
        {
            _publisher = publisher;
        }
        public string GenerateToken(Users user)
        {
            var Secret = Environment.GetEnvironmentVariable("Secret");
            if (Secret == null)
            {
                throw new InvalidOperationException("Secret environment variable not deified");
            }
            var messageHostname = Environment.GetEnvironmentVariable("messagesHostname");
            if (messageHostname == null)
            {
                throw new InvalidOperationException("messagesHostname environment variable not deified");
            }
            var tokenExpire = Environment.GetEnvironmentVariable("tokenExpires");
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.Name),
                        new Claim(ClaimTypes.Role, user.Role),
                        new Claim("Id", user.Id.ToString()),
                    }),
                Expires = DateTime.UtcNow.AddHours(tokenExpire != null ? int.Parse(tokenExpire) : 8),
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            string tokenFinal = tokenHandler.WriteToken(token);
            _publisher.Initialize(messageHostname, "Tokens");
            _publisher.Publish(tokenFinal, "");
            return tokenFinal;
        }

    }
}