using AdminService.Application.Shared.Results;
using AdminService.Application.Users.Inputs;
using AdminService.Application.Users.Interfaces;
using AdminService.Application.Users.Outputs;

namespace AdminService.Application.Users;

public sealed class UserService(IAuthApiClient authApiClient, IProfileApiClient profileApiClient) : IUserService
{
    public async Task<Result> CreateUserAsync(CreateUserInput input, CancellationToken ct = default)
    {
        if (input is null)
            return Result.Failure(ErrorTypes.BadRequest, UserServiceErrors.UserDetailsAreRequired);

        if (string.IsNullOrWhiteSpace(input.Email))
            return Result.Failure(ErrorTypes.BadRequest, UserServiceErrors.EmailIsRequired);

        if (string.IsNullOrWhiteSpace(input.Role))
            return Result.Failure(ErrorTypes.BadRequest, UserServiceErrors.RoleIsRequired);


        Result<CreateAuthUserOutput> authResult = await authApiClient.CreateAuthUserAsync(input.Email, input.Role, ct);

        if (authResult.IsFailure)
            return Result.Failure(authResult.Error!);

        string userId = authResult.Value!.UserId;

        Result profileResult = await profileApiClient.CreateProfileAsync(userId, ct);

        if (profileResult.IsFailure)
        {
            Result deleteResult = await authApiClient.DeleteAuthUserAsync(userId, ct);

            if (deleteResult.IsFailure)
            {
                return Result.Failure(ErrorTypes.ExternalServiceError, UserServiceErrors.ProfileCreationAndRollbackFailed);
            }

            return Result.Failure(profileResult.Error!);
        }

        return Result.Success();
    }
}




