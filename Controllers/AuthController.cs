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
        [HttpPost]
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
    }
}
