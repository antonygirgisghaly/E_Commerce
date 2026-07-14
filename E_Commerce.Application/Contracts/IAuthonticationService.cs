using E_Commerce.Application.Comman;
using E_Commerce.Application.DTOs.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Contracts
{
    public interface IAuthonticationService
    {
        Task<Result<UserDto>> LoginAsync(LoginDto login, CancellationToken ct = default);
        Task<Result<UserDto>> RegisterAsync(RegisterDto register, CancellationToken ct = default);
    }
}
