using AdminService.Application.Shared.Results;
using AdminService.Application.Users.Interfaces;
using AdminService.Application.Users.Outputs;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace AdminService.Infrastructure.Auth;

// Sends HTTP requests to Auth API and maps the responses to Result objects.
public class AuthApiClient(HttpClient httpClient, IHttpContextAccessor httpContextAccessor) : IAuthApiClient
{
    public async Task<Result<CreateAuthUserOutput>> CreateAuthUserAsync(string email, string role, CancellationToken ct)
    {
        string? authHeader = httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString();

        CreateAuthUserRequest request = new(email, role);

        using HttpRequestMessage requestMessage = new(HttpMethod.Post, "api/authentication/admin/register")

        requestMessage.Content = JsonContent.Create(request);

        if (!string.IsNullOrWhiteSpace(authHeader))
        {
            requestMessage.Headers.Authorization = AuthenticationHeaderValue.Parse(authHeader);
        }

        HttpResponseMessage response = await httpClient.SendAsync(requestMessage, ct);

        if (!response.IsSuccessStatusCode)
        {
            string authApiError = await response.Content.ReadAsStringAsync(ct);

            return Result<CreateAuthUserOutput>.Failure(ErrorTypes.ExternalServiceError, AuthApiClientErrors.UserCreationFailed, authApiError);
        }

        CreateAuthUserOutput? output = await response.Content.ReadFromJsonAsync<CreateAuthUserOutput>(cancellationToken: ct);

        if (string.IsNullOrWhiteSpace(output?.UserId))
            return Result<CreateAuthUserOutput>.Failure(ErrorTypes.ExternalServiceError, AuthApiClientErrors.InvalidUserId);

        return Result<CreateAuthUserOutput>.Success(output!);
    }



    public async Task<Result> DeleteAuthUserAsync(string userId, CancellationToken ct)
    {
        string? authHeader = httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString();

        DeleteAuthUserRequest deleteAuthUserRequest = new(userId);

        using HttpRequestMessage requestMessage = new(HttpMethod.Post, "api/authentication/admin/delete");

        requestMessage.Content = JsonContent.Create(deleteAuthUserRequest);

        if (!string.IsNullOrWhiteSpace(authHeader))
        {
            requestMessage.Headers.Authorization = AuthenticationHeaderValue.Parse(authHeader);
        }

        HttpResponseMessage response = await httpClient.SendAsync(requestMessage, ct);

        if (!response.IsSuccessStatusCode)
        {
            string profileApiError = await response.Content.ReadAsStringAsync(ct);

            return Result.Failure(ErrorTypes.ExternalServiceError, AuthApiClientErrors.UserDeletionFailed, profileApiError);
        }

        return Result.Success();
    }
}
