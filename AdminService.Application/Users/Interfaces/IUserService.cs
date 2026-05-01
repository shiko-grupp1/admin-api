using AdminService.Application.Shared.Results;
using AdminService.Application.Users.Inputs;

namespace AdminService.Application.Users.Interfaces;

public interface IUserService
{
    Task<Result> CreateUserAsync(CreateUserInput input, CancellationToken ct = default);
}
