using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTOS.User;
using RestaurantService.Interfaces;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [Authorize(Roles ="Admin")]
        [HttpPost("create-admin")]
        public async Task<IActionResult> CreateAdmin([FromForm] CreateAdminDTO dTO) {
            await _authService.CreateAdminAsync(dTO);
            return Ok("Admin Created Successfully");

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] Models.DTOS.User.RegisterDto dto)
        {
            try
            {
                var token = await _authService.RegisterAsync(dto);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] Models.DTOS.User.LoginDto dto)
        {
            try
            {
                var token = await _authService.LoginAsync(dto);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
