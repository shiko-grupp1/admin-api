using AdminService.Api.Requests;
using AdminService.Api.Shared.Extensions;
using AdminService.Application.Users.Inputs;
using AdminService.Application.Users.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AdminService.Api.Controllers;
// Controller -> Handler -> Service -> Repository -> Database

[Route("api/[controller]")]
[ApiController]
public class UsersController(IUserService userService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserRequest request, CancellationToken ct = default)
    {
        var input = new CreateUserInput(request.Email, request.Password);

        var result = await userService.CreateUserAsync(input, ct);

        return result.IsSuccess
            ? Ok()
            : ResultMapper.MapToActionResult(result);  
    }
}

/*
return result.IsSuccess
    ? CreatedAtAction(nameof(GetById), new { id = result.Value }, null)
    : ResultMapper.MapToActionResult(result);
*/