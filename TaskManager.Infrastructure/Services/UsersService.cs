using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Application.Interfaces;
using TaskManager.Domain;
using TaskManager.Infrastructure.Persistence.DatabaseContext;

namespace TaskManager.Infrastructure.Services
{
    public class UsersService : IUserService
    {
        private readonly AppDbContext _db;

        public UsersService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            var users = await _db.Users.ToListAsync();
            return users;
        }
    }
}
