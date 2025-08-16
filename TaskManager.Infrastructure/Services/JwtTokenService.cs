using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Abstractions;
using TaskManager.Application.DTOs.AuthDtos;
using TaskManager.Application.Interfaces;
using TaskManager.Domain;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text.Encodings.Web;

namespace TaskManager.Infrastructure.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly string _issuer;
        private readonly string _key;
        private readonly int _minutes;


        public JwtTokenService(IConfiguration configuration)
        {
            _issuer = configuration["Jwt:Issuer"] ?? "taskmgr";
            _key = configuration["Jwt:Key"] ?? "ThisIsAStrongAndSecureSecretKeyForHS256";
            _minutes = int.TryParse(configuration["Jwt:Minutes"], out var m) ? m : 60;
        }
        public AuthTokenDto CreateToken(User user)
        {
            var claims = new[]
            {
                new Claim (JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };

            string encoded = string.Empty;
            DateTime expires = DateTime.MinValue;

            try
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                expires = DateTime.UtcNow.AddMinutes(_minutes);
                var token = new JwtSecurityToken(_issuer, null, claims, expires: expires, signingCredentials: creds);
                encoded = new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
           
            return new AuthTokenDto { Token = encoded , ExpiresAtUtc  = expires };
        } 
    }
}
