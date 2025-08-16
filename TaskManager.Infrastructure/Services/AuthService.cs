using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using TaskManager.Application.DTOs.AuthDtos;
using TaskManager.Application.Interfaces;
using TaskManager.Domain;
using TaskManager.Infrastructure.Persistence.DatabaseContext;

namespace TaskManager.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private readonly IJwtTokenService _jwt;

        public AuthService(AppDbContext databaseContext, IJwtTokenService jwt)
        {
            _db = databaseContext;
            _jwt = jwt;
        }

        public async Task<AuthTokenDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _db.Users.
                FirstOrDefaultAsync(u => u.UserName == loginDto.UserNameOrEmail 
                || u.Email == loginDto.UserNameOrEmail);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid credentials");

            return _jwt.CreateToken(user);
        }

        public async Task RegisterAsync(RegisterDto dto)
        {
            if (await _db.Users.AnyAsync(u => u.UserName == dto.UserName || u.Email == dto.Email))
            {
                throw new InvalidOperationException("User already exists");
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = dto.UserName,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = "USER"

            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();
        }
    }
}
