using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseApiController : ControllerBase
{
    private ISender _sender;
    protected ISender Sender => _sender ?? (_sender = HttpContext.RequestServices.GetRequiredService<ISender>());
}
