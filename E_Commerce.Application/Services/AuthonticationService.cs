using E_Commerce.Application.Comman;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services
{
    internal class AuthonticationService : IAuthonticationService
    {
        private readonly IIdentityService _identityService;

        public AuthonticationService(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<Result<UserDto>> LoginAsync(LoginDto login, CancellationToken ct = default)
        {
           var result = await _identityService.FindUserByEmailAsync(login.Email, ct);
           if(!result.IsSuccess)
                return Result<UserDto>.Fail(result.Errors);
           var checkPassword = await _identityService.CheckPasswordAsync(login.Email, login.Password, ct);
            if(!checkPassword.IsSuccess)
                return Result<UserDto>.Fail(checkPassword.Errors);
            if(!checkPassword.Data)
                return Result<UserDto>.Fail(Error.Unothorized("Invalid Email or Password"));
            return Result<UserDto>.Ok(new UserDto
                {
                    Email = result.Data.Email,
                    DisplayName = result.Data.DisplayName,
                    Token = "Token"
                });
        }

        public async Task<Result<UserDto>> RegisterAsync(RegisterDto register, CancellationToken ct = default)
        {
            var result = await _identityService.CreateUserAsync(register, ct);
            if(!result.IsSuccess)
                return Result<UserDto>.Fail(result.Errors);
            var user = result.Data;
            return Result<UserDto>.Ok(new UserDto
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = "Token"
            });   
        }
    }
}
