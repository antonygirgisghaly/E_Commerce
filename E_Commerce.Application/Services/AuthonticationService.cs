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
        private readonly ITokenService _tokenService;

        public AuthonticationService(IIdentityService identityService, ITokenService tokenService)
        {
            _identityService = identityService;
            _tokenService = tokenService;
        }

        public async Task<Result<bool>> CheckEmailExistsAsync(string email, CancellationToken ct = default)
        {
            var result = await _identityService.EmailExistsAsync(email, ct);
            return result;
        }

        public async Task<Result<UserDto>> GetCurrentUserAsync(string email, CancellationToken ct = default)
        {
            var result = await _identityService.FindUserByEmailAsync(email, ct);
            var rolesResult = await _identityService.GetUserRole(result.Data.Email, ct);
            var token = _tokenService.CreateToken(result.Data.Id, result.Data.Email, result.Data.UserName, rolesResult.Data);
            return new UserDto
            {
                Email = result.Data.Email,
                DisplayName = result.Data.DisplayName,
                Token = token
            };
        }

        public async Task<Result<AddressDto>> GetUserAddressAsync(string email, CancellationToken ct = default)
        {
           var result = await _identityService.GetUserAddressAsync(email, ct);
           return result;
        }

        public async Task<Result<UserDto>> LoginAsync(LoginDto login, CancellationToken ct = default)
        {
           var result = await _identityService.FindUserByEmailAsync(login.Email, ct);
           if(!result.IsSuccess)
                return Result<UserDto>.Fail(result.Errors);
           var checkPassword = await _identityService.CheckPasswordAsync(login.Email, login.Password, ct);
            if(!checkPassword.IsSuccess)
                return Result<UserDto>.Fail(Error.Unothorized("Invalid Email or Password"));
            var user = result.Data;
            var rolesResult = await _identityService.GetUserRole(user.Email, ct);
            var roles = rolesResult.Data;
            var token = _tokenService.CreateToken(user.Id, user.Email, user.UserName,roles);
            return Result<UserDto>.Ok(new UserDto
                {
                    Email = result.Data.Email,
                    DisplayName = result.Data.DisplayName,
                    Token = token
                });
        }

        public async Task<Result<UserDto>> RegisterAsync(RegisterDto register, CancellationToken ct = default)
        {
            var result = await _identityService.CreateUserAsync(register, ct);
            if(!result.IsSuccess)
                return Result<UserDto>.Fail(result.Errors);
            var user = result.Data;
            var rolesResult = await _identityService.GetUserRole(user.Email, ct);
            var roles = rolesResult.Data;
            var token = _tokenService.CreateToken(user.Id, user.Email, user.UserName, roles);
            return Result<UserDto>.Ok(new UserDto
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = token
            });   
        }

        public async Task<Result<AddressDto>> UpSertUserAddressAsync(string email, AddressDto address, CancellationToken ct = default)
        {
            return await _identityService.UpSertUserAddressAsync(email, address, ct);
        }
    }
}
