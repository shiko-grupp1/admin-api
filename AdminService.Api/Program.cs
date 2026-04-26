using AdminService.Api.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApiConfiguration();

builder.Services.AddControllers();

builder.Services.AddDbContext<PersistenceContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.UseOpenApiConfiguration();

app.UseCors("Frontend");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();




