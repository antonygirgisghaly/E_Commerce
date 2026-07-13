using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    public class AuthonticationController : ApiBaseController
    {
        private readonly IAuthonticationService _authonticationService;

        public AuthonticationController(IAuthonticationService authonticationService)
        {
            _authonticationService = authonticationService;
        }
        //Login
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
          => ToActionResult(await _authonticationService.LoginAsync(loginDto));
    }
}
