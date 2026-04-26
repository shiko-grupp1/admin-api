using Scalar.AspNetCore;

namespace AdminService.Api.OpenApi;
// OpenApi + Scalar setup for the API. This is separated from Program.cs to keep it clean.
public static class OpenApiConfiguration
{
    public static IServiceCollection AddOpenApiConfiguration(this IServiceCollection services)
    {
        services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer((document, context, cancellationToken) =>
            {
                document.Info.Title = "Admin Service API - LMS";
                document.Info.Version = "v1";
                document.Info.Description = "API for managing users and roles";

                return Task.CompletedTask;
            });
        });

        return services;
    }

    public static WebApplication UseOpenApiConfiguration(this WebApplication app)
    {
        app.MapOpenApi();

        if (app.Environment.IsDevelopment())
        {
            app.MapScalarApiReference("/docs");
        }

        return app;
    }
}
