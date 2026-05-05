using AdminService.Application.Shared.Results;
using AdminService.Application.Users.Interfaces;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;


namespace AdminService.Infrastructure.ProfileApi;

public sealed class ProfileApiClient(HttpClient httpClient, IHttpContextAccessor httpContextAccessor) : IProfileApiClient
{
    public async Task<Result> CreateProfileAsync(string userId, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(userId)) 
            return Result.Failure(ErrorTypes.BadRequest, ProfileApiClientErrors.UserIdIsRequired);

        // Hämtar Authorization-headern från requesten som kom in till AdminService från frontend
        string? authHeader = httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString();

        // Skapar requesten till Profile API manuellt för att kunna lägga till headers på requesten. 
        // usin =  när koden är klar med objektet, städa upp det automatiskt
        using HttpRequestMessage requestMessage = new(HttpMethod.Post, "api/profile/create");

        // Sätter JSON-body på requesten
        requestMessage.Content = JsonContent.Create(new CreateProfileRequest(userId));

        // Om Authorization-headern finns, skicka den vidare till Profile API
        if (!string.IsNullOrWhiteSpace(authHeader))
        {
            requestMessage.Headers.Authorization = AuthenticationHeaderValue.Parse(authHeader);
        }

        HttpResponseMessage response = await httpClient.SendAsync(requestMessage, ct);

        if (!response.IsSuccessStatusCode)
        {
            string profileApiError = await response.Content.ReadAsStringAsync(ct);

            return Result.Failure(ErrorTypes.ExternalServiceError, ProfileApiClientErrors.ProfileCreationFailed, profileApiError);
        }

        return Result.Success();
    }
}

