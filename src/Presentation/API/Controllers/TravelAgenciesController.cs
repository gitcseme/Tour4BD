using Application.Features.Agencies.Commands;
using Application.Features.Agencies.Queries;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class TravelAgenciesController : BaseApiController
{
    [HttpPost]
    public async Task<IActionResult> AddOrUpdate(AddOrUpdateTravelAgencyCommand command) => ApiResponse(await Sender.Send(command));

    [HttpPost("list")]
    public async Task<IActionResult> GetList(GetAllTravelAgencyQuery query) => ApiResponse(await Sender.Send(query));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id) => 
        ApiResponse(await Sender.Send(new GetTravelAgencyByIdQuery { Id = id }));
}
