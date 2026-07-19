using E_Commerce.Application.Comman;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.Identity;
using E_Commerce.Infrastracture.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Result<bool>> EmailExistsAsync(string email, CancellationToken ct = default)
            => await _userManager.FindByEmailAsync(email) is not null;

        public async Task<Result<IdentityUserResult>> FindUserByEmailAsync(string email, CancellationToken ct = default)
        {
            var result = await _userManager.FindByEmailAsync(email);
            if (result == null)
            {
                return Result<IdentityUserResult>.Fail(Error.NotFound("User not found", $"User not found with email: {email} not found"));
            }
            else
            {
                return Result<IdentityUserResult>.Ok(new IdentityUserResult(result.Id, result.DisplayName, result.Email, result.UserName));
            }
        }

        public async Task<Result<AddressDto>> GetUserAddressAsync(string email, CancellationToken ct = default)
        {
            var address = await _userManager.Users.Include(x => x.Address).FirstOrDefaultAsync(u => u.Email == email, ct);
            if (address!.Address == null)
            {
                return Result<AddressDto>.Fail(Error.NotFound("Address not found", $"Address not found for user with email: {email}"));
            }
            return new AddressDto
            {
                City = address.Address.City,
                Street = address.Address.Street,
                Country = address.Address.Country,
                FirstName = address.Address.FirstName,
                LastName = address.Address.LastName
            };
        }

        public async Task<Result<IReadOnlyList<string>>> GetUserRole(string email, CancellationToken ct = default)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return Error.NotFound("User not found", $"User with email {email} not found");
            var roles = await _userManager.GetRolesAsync(user);
            return roles.ToList();
        }

        public async Task<Result<AddressDto>> UpSertUserAddressAsync(string email, AddressDto address, CancellationToken ct = default)
        {
            var user = await _userManager.Users.Include(x => x.Address).FirstOrDefaultAsync(u => u.Email == email, ct);
            if (user?.Address == null)
            {
                user.Address = new Address
                {
                    City = address.City,
                    Street = address.Street,
                    Country = address.Country,
                    FirstName = address.FirstName,
                    LastName = address.LastName
                };

            }
            else
            {
                user.Address.FirstName = address.FirstName;
                user.Address.LastName = address.LastName;
                user.Address.Street = address.Street;
                user.Address.City = address.City;
                user.Address.Country = address.Country;
            }
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return Result<AddressDto>.Ok(address);

            }
            else
            {
                var errors = result.Errors.Select(e => new Error(e.Code, e.Description)).ToList();
                return Result<AddressDto>.Fail(errors);
            }
        }
    }
}
