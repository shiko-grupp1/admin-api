using Microsoft.AspNetCore.Mvc;

namespace AdminService.Api.Controllers;
// Controller -> Handler -> Service -> Repository -> Database

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create()
    {

    }

}
