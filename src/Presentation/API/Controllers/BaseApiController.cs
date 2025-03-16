using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKarnel.Contracts;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class BaseApiController : ControllerBase
{
    private ISender _sender;
    protected ISender Sender => _sender ?? (_sender = HttpContext.RequestServices.GetRequiredService<ISender>());

    [NonAction]
    public IActionResult ApiResponse<T>(Result<T> result)
    {
        return result.IsSuccess
            ? Ok(result)
            : BadRequest(result);
    }
}
