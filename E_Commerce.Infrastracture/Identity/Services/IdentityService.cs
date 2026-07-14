using E_Commerce.Application.Comman;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.Identity;
using E_Commerce.Infrastracture.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastracture.Identity.Services
{
    internal class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<Result<bool>> CheckPasswordAsync(string email, string password, CancellationToken ct = default)
        {
            var result = await _userManager.FindByEmailAsync(email);
            if (result == null)
            {
                return Result<bool>.Fail(Error.NotFound("User not found", $"User not found with email: {email} not found"));
            }
            else
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(result, password);
                if (passwordCheck)
                {
                    return Result<bool>.Ok(true);
                }
                else
                {
                    return Result<bool>.Fail(Error.NotFound("Invalid password", $"Invalid password"));
                }
            }
        }

        public async Task<Result<IdentityUserResult>> CreateUserAsync(RegisterDto registerDto, CancellationToken ct = default)
        {
            var user = new ApplicationUser
            {
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.UserName,
                DisplayName = registerDto.DisplayName                       
            };

            var identityResult = await _userManager.CreateAsync(user, registerDto.Password);
            if (!identityResult.Succeeded)
            {
                var errors = identityResult.Errors.Select(e => new Error(e.Code, e.Description)).ToList();
                return Result<IdentityUserResult>.Fail(errors);
            }

            return Result<IdentityUserResult>.Ok(new IdentityUserResult(user.Id, user.DisplayName, user.Email, user.UserName));
        }

        public async Task<Result<IdentityUserResult>> FindUserByEmailAsync(string email, CancellationToken ct = default)
        {
            var result = await _userManager.FindByEmailAsync(email);
            if (result == null)
            {
                return Result<IdentityUserResult>.Fail(Error.NotFound("User not found",$"User not found with email: {email} not found"));
            }
            else
            {
                return Result<IdentityUserResult>.Ok(new IdentityUserResult(result.Id,result.DisplayName,result.Email,result.UserName));
            }
        }
    } 
}
