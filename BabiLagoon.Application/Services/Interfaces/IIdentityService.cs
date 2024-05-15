using BabiLagoon.Application.Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BabiLagoon.Application.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<bool> CreateUserAsync(CreateUserDto createUserDto);
        Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto);

        Task<IActionResult> UpdateAsync(UpdateUserDto updateUserDto , ClaimsPrincipal user);

        //Task<IActionResult> GetUserAsync();
        ////Task<string> GenerateJwtTokenAsync(ApplicationUserDto user , List<string>roles);
        Task<IActionResult> DeleteUserAsync(ClaimsPrincipal user);

        Task<IActionResult> LogOutAsync();


    }
}
