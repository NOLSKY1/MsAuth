using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ms_Auth.Dto;
using Ms_Auth.Services;

namespace Ms_Auth.Controllers
{
    [Route("/")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }
        [HttpPost("register")]
        public IActionResult Register(RegisterDto dto)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
            AuthResponse response = authService.Register(dto);
            if (response == null)
            {
                return StatusCode(500);
            }
            return StatusCode(201, response);
        }
        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
            AuthResponse response = authService.Login(dto);
            if (response == null)
            {
                return Unauthorized("Invalid Credentials");
            }
            return Ok(response);
        }
        [HttpPost("change-password")]
        [Authorize]
        public IActionResult ChangePassword(string oldPassword , string newPassword)
        {
            var response = new { message = "Ur passowrd was successfully changed" };
            return Ok(response);
        }
    }
}
