using Application.Features.Agencies.Commands;
using Application.Features.Agencies.Queries;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class TravelAgenciesController : BaseApiController
{
    [HttpPost]
    public async Task<IActionResult> AddOrUpdate(AddOrUpdateTravelAgencyCommand command) => ApiResponse(await Sender.Send(command));

    [HttpGet]
    public async Task<IActionResult> GetList(GetAllTravelAgencyQuery query) => ApiResponse(await Sender.Send(query));

    [HttpGet]
    public async Task<IActionResult> GetById(GetTravelAgencyByIdQuery query) => ApiResponse(await Sender.Send(query));
}
