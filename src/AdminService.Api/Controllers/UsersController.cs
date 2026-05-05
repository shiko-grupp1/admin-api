using AdminService.Api.Requests;
using AdminService.Api.Shared.Extensions;
using AdminService.Application.Shared.Results;
using AdminService.Application.Users.Inputs;
using AdminService.Application.Users.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminService.Api.Controllers;

[Authorize(Roles = "Admin")]
[Route("api/[controller]")]
[ApiController]
public class UsersController(IUserService userService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserRequest request, CancellationToken ct = default)
    {
        CreateUserInput input = new(request.Email, request.Role);

        Result result = await userService.CreateUserAsync(input, ct);

        return result.IsSuccess
            ? Ok()
            : ResultMapper.MapToActionResult(result);
    }
}
