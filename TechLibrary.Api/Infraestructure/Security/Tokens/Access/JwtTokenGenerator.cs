﻿using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TechLibrary.Api.Domain.Entities;


namespace TechLibrary.Api.Infraestructure.Security.Tokens.Access
{
    public class JwtTokenGenerator
    {
       

        public string Generate(User user)
        {
            var claims = new List<Claim>()
            {
                 new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),

            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                  Expires = DateTime.UtcNow.AddMinutes(60),
                  Subject = new ClaimsIdentity(claims),
                  SigningCredentials = new SigningCredentials(SecurityKey(), SecurityAlgorithms.HmacSha256Signature)
            };  

            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken =  tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);

        }

        private  SymmetricSecurityKey SecurityKey()
        {
             var signingKey = "yhS59qst2gr6a99eqgy236seewwppomn";
            
             var symmetricKey =  System.Text.Encoding.UTF8.GetBytes(signingKey);

             return new SymmetricSecurityKey(symmetricKey);
        
        }
    }
}
