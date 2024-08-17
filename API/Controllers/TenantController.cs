using System.Net;

using Application.Features.TenantFeatures.Queries;
using Application.Features.TenantFeatures.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public class TenantsController : BaseApiController
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TenantResponseDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        var tenantList = await Sender.Send(new GetAllTenantQuery(), cancellationToken);
        return Ok(tenantList);
    }

    [HttpGet("{tenantId:int}/users")]
    public async Task<IActionResult> GetAllUsers(int tenantId, CancellationToken cancellationToken = default)
    {
        var tenantUsers = await Sender.Send(new GetAllTenantUsersQuery(tenantId), cancellationToken);
        return Ok(tenantUsers);
    }
}

