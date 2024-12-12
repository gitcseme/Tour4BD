using Application.Features.Companies.Commands;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class CompaniesController : BaseApiController
    {
        public CompaniesController() { }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCompanyCommand command, CancellationToken ctn = default) =>
            ApiResponse(await Sender.Send(command, ctn));
        
    }
}
