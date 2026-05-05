using AdminService.Application.Users.Interfaces;
using AdminService.Infrastructure.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdminService.Infrastructure.Extensions;

public static class InfrastructureServiceCollectionRegistrationExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        // configuration hämtar baseUrl från appsettings.dev, som är roten till API:t, inte endpointen
        services.AddHttpClient<IAuthApiClient, AuthApiClient>(client =>
        {
            client.BaseAddress = new Uri(configuration["AuthApi:BaseUrl"]!);
        });



        return services;
    }
}
