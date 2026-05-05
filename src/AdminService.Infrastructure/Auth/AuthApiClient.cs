using AdminService.Application.Shared.Results;
using AdminService.Application.Users.Interfaces;
using AdminService.Application.Users.Outputs;
using System.Net.Http.Json;

namespace AdminService.Infrastructure.Auth;
// sends the HTTP-request to auth-api, gets the response and maps it to CreateAuthUserOutput.
public class AuthApiClient(HttpClient httpClient) : IAuthApiClient
{
    public async Task<Result<CreateAuthUserOutput>> CreateAuthUserAsync(string email, string role, CancellationToken ct)
    {
        CreateAuthUserRequest request = new(email, role);

        // matcha auth-apins riktiga route
        // returnerar aldrig null
        HttpResponseMessage response = await httpClient.PostAsJsonAsync("api/auth/users", request, ct);

        if (!response.IsSuccessStatusCode)
        {
            string authApiError = await response.Content.ReadAsStringAsync(ct);

            return Result<CreateAuthUserOutput>.Failure(ErrorTypes.ExternalServiceError, AuthApiClientErrors.UserCreationFailed, authApiError);
        }


        CreateAuthUserOutput? output = await response.Content.ReadFromJsonAsync<CreateAuthUserOutput>(cancellationToken: ct);


        if (output is null || string.IsNullOrWhiteSpace(output.UserId))
            return Result<CreateAuthUserOutput>.Failure(ErrorTypes.ExternalServiceError, AuthApiClientErrors.InvalidUserId);


        return Result<CreateAuthUserOutput>.Success(output);
    }
}
