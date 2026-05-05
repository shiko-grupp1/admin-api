using AdminService.Application.Shared.Results;
using AdminService.Application.Users.Inputs;
using AdminService.Application.Users.Interfaces;

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


        var authResult = await authApiClient.CreateAuthUserAsync(input.Email, input.Role, ct);

        if (authResult.IsFailure)
            return Result.Failure(authResult.Error!);

        var userId = authResult.Value!.UserId;

        var profileResult = await profileApiClient.CreateProfileAsync(userId, ct);

        if (profileResult.IsFailure)
            return Result.Failure(profileResult.Error!);

        return Result.Success();
    }
}

/*
Fortsätt med AuthApiClient och ProfileApiClient, där du implementerar de faktiska HTTP-anropen till respektive API. Se till att hantera eventuella fel som kan uppstå under dessa anrop och returnera lämpliga Result-objekt baserat på utfallet av varje operation.
Commita var för sig.
Skapa IProfileApiClient, ProfileApiClient, CreateProfileAsync i IProfileApiClient och ProfileApiClient. OutPut?
*/

/*
 
Undvika att auth lyckas med profile misslyckas
if (profileResult.IsFailure)
{
    await authApiClient.DeleteUserAsync(userId);
    return Result.Failure(...);
}
*/
