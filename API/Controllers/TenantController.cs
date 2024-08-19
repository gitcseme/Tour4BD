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
    public async Task<IActionResult> GetAll(CancellationToken ctn = default)
    {
        var tenantList = await Sender.Send(new GetAllTenantQuery(), ctn);
        return Ok(tenantList);
    }

    [HttpGet("{tenantId:int}/users")]
    [ProducesResponseType(typeof(IEnumerable<TenantUserResponse>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetAllUsers(int tenantId, CancellationToken ctn = default)
    {
        var tenantUsers = await Sender.Send(new GetAllTenantUsersQuery(tenantId), ctn);
        return Ok(tenantUsers);
    }

    [HttpGet("{tenantId:int}/users/{userId}")]
    public async Task<IActionResult> GetUserDetail(int tenantId, int userId, CancellationToken ctn = default)
    {
        var userDetailData = await Sender.Send(new GetUserDetailQuery(tenantId, userId), ctn);
        return Ok(userDetailData);
    }
}

