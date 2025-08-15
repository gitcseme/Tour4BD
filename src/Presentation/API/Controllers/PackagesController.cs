using Application.Features.Packages.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class PackagesController : BaseApiController
{
    [HttpPost("list")]
    public async Task<IActionResult> GetList(GetAllPackagesQuery query) => ApiResponse(await Sender.Send(query));
}