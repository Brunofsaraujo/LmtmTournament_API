using LmtmTournament.API.Extensions;
using LmtmTournament.API.Interfaces;
using LmtmTournament.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LmtmTournament.API.Services
{
    public class JwtService : IJwtService
    {
        public string CreateJsonWebToken(Atleta atleta, AppSettings appSettings)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var expires = DateTime.UtcNow.AddMinutes(appSettings.ExpiracaoMinutos);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, atleta.Codigo),
                    new Claim(ClaimTypes.Role, atleta.Permissao.ToString()),
                }),
                Expires = expires,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }
    }
}
