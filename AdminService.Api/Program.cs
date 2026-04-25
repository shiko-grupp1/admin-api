using AdminService.Api.OpenApi;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApiConfiguration();

builder.Services.AddControllers();

var app = builder.Build();

app.UseOpenApiConfiguration();

app.UseCors("Frontend");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();




