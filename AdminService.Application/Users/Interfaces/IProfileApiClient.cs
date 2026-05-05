using AdminService.Application.Shared.Results;
using AdminService.Application.Users.Outputs;

namespace AdminService.Application.Users.Interfaces;

public interface IProfileApiClient
{
    Task<Result> CreateProfileAsync(string userId, CancellationToken ct = default);
}
