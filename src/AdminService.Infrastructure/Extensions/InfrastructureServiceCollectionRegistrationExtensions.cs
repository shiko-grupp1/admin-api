using AdminService.Application.Users.Interfaces;
using AdminService.Infrastructure.Auth;
using AdminService.Infrastructure.ProfileApi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdminService.Infrastructure.Extensions;

public static class InfrastructureServiceCollectionRegistrationExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        services.AddHttpContextAccessor();

        // configuration hämtar baseUrl från appsettings.dev, som är roten till API:t, inte endpointen
        services.AddHttpClient<IAuthApiClient, AuthApiClient>(client =>
        {
            client.BaseAddress = new Uri(configuration["AuthApi:BaseUrl"]!);
        });

        services.AddHttpClient<IProfileApiClient, ProfileApiClient>(client =>
        {
            client.BaseAddress = new Uri(configuration["ProfileApi:BaseUrl"]!);
        });


        return services;
    }
}

