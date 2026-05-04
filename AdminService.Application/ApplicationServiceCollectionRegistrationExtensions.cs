using AdminService.Application.Users;
using AdminService.Application.Users.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AdminService.Application;

public static class ApplicationServiceCollectionRegistrationExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

        services.AddScoped<IUserService, UserService>();

            return services;
    }
}
