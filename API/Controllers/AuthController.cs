using API.Dtos;
using Domain.Entities;
using Membership.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<ExtendedIdentityUser> _signInManager;
        private readonly UserManager<ExtendedIdentityUser> _userManager;
        private readonly IJwtProvider _jwtProvider;

        public AuthController(SignInManager<ExtendedIdentityUser> signInManager, UserManager<ExtendedIdentityUser> userManager, IJwtProvider jwtProvider)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtProvider = jwtProvider;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: false);
            
            if (signInResult.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent:  false);
            }

            var tokenResult = _jwtProvider.Generate(User);

            return Ok(new LoginResponse
            {
                User = user,
                AccessToken = tokenResult.AccessToken,
                RefreshToken = tokenResult.RefreshToken
            });
        }
    }
}
