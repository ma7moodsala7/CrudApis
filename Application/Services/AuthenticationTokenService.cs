using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Application.Contracts.Services;
using Domain.Entities;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services
{
    public class AuthenticationTokenService : IAuthenticationTokenService
    {
        // Symmetric Encryption is a type of encryption where only one key is used to both encrypt
        // and decrypt electronic information  (make sure the signature is valid)
        private readonly SymmetricSecurityKey _key;

        public AuthenticationTokenService(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }

        public string CreateToken(AppUser user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Username),
                new Claim("uid", user.Id.ToString())
                //new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}

