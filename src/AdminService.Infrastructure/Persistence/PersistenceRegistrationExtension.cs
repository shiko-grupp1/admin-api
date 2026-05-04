using AdminService.Application.Shared;
using AdminService.Application.Users.Interfaces;
using AdminService.Infrastructure.Persistence.Contexts;
using AdminService.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdminService.Infrastructure.Persistence;

public static class PersistenceRegistrationExtension
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException("Connection string 'DefaultConnection' is missing.");
        }

        services.AddDbContext<PersistenceContext>(options =>
         options.UseSqlServer(connectionString));


        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<PersistenceContext>());


        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
