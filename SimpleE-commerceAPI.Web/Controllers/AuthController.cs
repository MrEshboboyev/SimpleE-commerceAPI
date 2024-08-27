using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleE_commerceAPI.Application.Common.Models;
using SimpleE_commerceAPI.Application.Services.Interfaces;

namespace SimpleE_commerceAPI.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        // inject Auth Service
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginModel model)
        {
            var result = await _authService.LoginAsync(model);
            if (result == null)
                return BadRequest("Email or password incorrect.");
            return Ok(new { YourToken = result });
        }
        
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel model)
        {
            var result = await _authService.RegisterAsync(model);
            if (result == null)
                return BadRequest("Registration failed!");
            return Ok("Registration successfully!");
        }
    }
}
