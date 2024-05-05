using AutoMapper;
using BabiLagoon.Application.Common.DTOs;
using BabiLagoon.Application.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BabiLagoon.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;

        public IdentityService(
            UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration,IMapper mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.mapper = mapper;
        }

        public async Task<bool> CreateUserAsync(CreateUserDto createUserDto)
        {
            var user = new IdentityUser
            { 
                UserName = createUserDto.Username,
                Email = createUserDto.Email, 
            };
            var result = await userManager.CreateAsync(user, createUserDto.Password);
            if (result.Succeeded && createUserDto.Roles != null && createUserDto.Roles.Any())
            { 
                result = await userManager.AddToRolesAsync(user, createUserDto.Roles); }
                return result.Succeeded;
        }

        //public Task<IActionResult> DeleteUserAsync()
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<string> GenerateJwtTokenAsync(ApplicationUserDto user, List<string> roles)
        //{

        //    var claims = new List<Claim>
        //{
        //    new Claim(ClaimTypes.NameIdentifier, user.Id),
        //    new Claim(ClaimTypes.Email, user.Email),
        //    new Claim("username", user.UserName),

        //};

        //    foreach (var role in roles)
        //    {
        //        claims.Add(new Claim(ClaimTypes.Role, role));
        //    }

        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

        //    var credentails = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //    var token = new JwtSecurityToken(
        //        configuration["Jwt:Issuer"],
        //        configuration["Jwt:Audience"],
        //        claims,
        //        expires: DateTime.Now.AddMinutes(60),
        //        signingCredentials: credentails);

        //    var tokenString= new JwtSecurityTokenHandler().WriteToken(token);
        //    return Task.FromResult(tokenString);

        //}

        //public Task<IActionResult> GetUserAsync()
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.Email);
            if (user != null)
            {
                var signInResult = await signInManager.PasswordSignInAsync(user, loginRequestDto.Password, isPersistent: false, lockoutOnFailure: false);

            
            if (signInResult.Succeeded)
            {
                 var roles = await userManager.GetRolesAsync(user);
                 var tokenHandler = new JwtSecurityTokenHandler();
                 var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                   {
                    new Claim(ClaimTypes.Name, user.UserName.ToString()),
                    new Claim(ClaimTypes.Role, roles.FirstOrDefault())
                   }),
                        Expires = DateTime.UtcNow.AddDays(7),
                        SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    LoginResponseDto loginResponseDTO = new LoginResponseDto()
                    {
                        JwtToken = tokenHandler.WriteToken(token),
                        //User = mapper.Map<UserDto>(user),

                    };
                    return loginResponseDTO;
                }
            }

            return null;
            
            }

            public async Task<IActionResult> LogOutAsync()
        {
            await signInManager.SignOutAsync();
            return new OkObjectResult("User was logged out successfully");
        }

        //public Task<UpdateUserDto> UpdateAsync(UpdateUserDto updateUserDto)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
