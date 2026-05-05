using AdminService.Application.Shared.Results;
using AdminService.Application.Users.Outputs;

namespace AdminService.Application.Users.Interfaces;

public interface IAuthApiClient
{
    Task<Result<CreateAuthUserOutput>> CreateAuthUserAsync(string email, string role, CancellationToken ct);
    Task<Result> DeleteAuthUserAsync(string userId, CancellationToken ct);
}
