using BabiLagoon.Application.Common.DTOs;
using BabiLagoon.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BabiLagoon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IIdentityService identityService;
        private readonly IIdentityService authService;

        public UserController(IIdentityService authService)
        {
            this.authService = authService;
        }

        [HttpPost]
        [Route("register")]

        public async Task<IActionResult> Register([FromBody] CreateUserDto createUserDto)
        {
            var user = await authService.CreateUserAsync(createUserDto);
           if(user != false)
            {
                return Ok("User registred successfuly");
            }
            return BadRequest("Something went wrong");
        }

        [HttpPost]
        [Route("login")]

        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var loginResponse = await authService.LoginAsync(loginRequestDto);
            if (loginResponse == null || string.IsNullOrEmpty(loginResponse.JwtToken))
            {
                return BadRequest("Your password or username is incorrect");
               
            }

            return Ok("User logged successfuly");

        }

        [HttpGet]
        [Authorize]
        [Route("logout")]

        public async Task<IActionResult> LogOut()
        {
            await authService.LogOutAsync();
            return Ok("User was logged out successfuly");
        }
    }
}
