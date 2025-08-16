using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Application.DTOs.AuthDtos;
using TaskManager.Domain;

namespace TaskManager.Application.Interfaces
{
    public interface IJwtTokenService
    {
        AuthTokenDto CreateToken(User user);
    }
}
