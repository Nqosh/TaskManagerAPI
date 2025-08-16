using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Application.DTOs.AuthDtos;

namespace TaskManager.Application.Interfaces
{
    public interface IAuthService
    {
       Task RegisterAsync(RegisterDto dto); 
       Task<AuthTokenDto> LoginAsync(LoginDto loginDto);    
    }
}
