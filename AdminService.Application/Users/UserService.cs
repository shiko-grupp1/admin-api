using AdminService.Application.Shared;
using AdminService.Application.Shared.Results;
using AdminService.Application.Users.Inputs;
using AdminService.Application.Users.Interfaces;

namespace AdminService.Application.Users;

public sealed class UserService(IUserRepository userRepository, IUnitOfWork unitOfWork) : IUserService
{
    public async Task<Result> CreateUserAsync(CreateUserInput input, CancellationToken ct = default)
    {
        if (input is null)
            return Result.Failure(new ResultError(ErrorTypes.BadRequest, UserServiceErrors.InputIsRequired));


    }
}
