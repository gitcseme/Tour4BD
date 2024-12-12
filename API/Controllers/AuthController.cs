using Application.DTOs;
using Application.Features.Auth.Commands;
using Membership;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseApiController
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand command, CancellationToken ctn = default)
            => ApiResponse(await Sender.Send(command, ctn));

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterCommand command, CancellationToken ctn = default) 
            => ApiResponse(await Sender.Send(command, ctn));
    }
}
