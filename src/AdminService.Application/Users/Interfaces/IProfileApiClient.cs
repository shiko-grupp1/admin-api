using AdminService.Application.Shared.Results;

namespace AdminService.Application.Users.Interfaces;

public interface IProfileApiClient
{
    Task<Result> CreateProfileAsync(string userId, CancellationToken ct = default);
}
