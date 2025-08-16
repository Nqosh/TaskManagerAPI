using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.DTOs.AuthDtos;
using TaskManager.Application.Interfaces;


namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;
        
        public AuthController(IAuthService auth)
        {
            _auth = auth;   
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            await _auth.RegisterAsync(dto);
            return Ok("Registered");
        }

        [HttpPost("login")] 
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var token  = await _auth.LoginAsync(dto);   
            return Ok(token);
        }
    }
}
