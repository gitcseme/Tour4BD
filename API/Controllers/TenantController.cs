using System.Net;

using Application.Features.TenantFeatures.Queries;
using Application.Features.TenantFeatures.Responses;

using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class TenantsController : BaseApiController
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TenantResponseDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default) 
    {
        var tenantList = await Sender.Send(new GetAllTenantQuery(), cancellationToken);
        return Ok(tenantList);
    }
}
