using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto, CancellationToken ct = default)
          => ToActionResult(await _authonticationService.LoginAsync(loginDto, ct));


        [HttpPost("Register")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto, CancellationToken ct = default)
          => ToActionResult(await _authonticationService.RegisterAsync(registerDto, ct));

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> EmailExists([FromQuery] string email, CancellationToken ct = default)
          => ToActionResult(await _authonticationService.CheckEmailExistsAsync(email, ct));


        [Authorize]
        [HttpGet("currentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser(CancellationToken ct = default)
            => ToActionResult(await _authonticationService.GetCurrentUserAsync(GetEmailFromToken(), ct));
        

        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDto>> GetCurrentUserAddress(CancellationToken ct = default)
           =>  ToActionResult(await _authonticationService.GetUserAddressAsync(GetEmailFromToken(), ct));

        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto address, CancellationToken ct = default)
            => ToActionResult(await _authonticationService.UpSertUserAddressAsync(GetEmailFromToken(), address, ct));
    }
}
