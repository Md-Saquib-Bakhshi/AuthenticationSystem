using AuthenticationSystem.Repository;
using AuthenticationSystem.Data;
using Microsoft.EntityFrameworkCore;
using AuthenticationSystem.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IRegistrationService, RegistrationService>();

builder.Services.AddScoped<ILoginService, LoginService>();

builder.Services.AddDbContext<RegistrationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("RegistrationConnectionString");
    options.UseSqlServer(connectionString);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseAuthenticationMiddleware();

app.UseAuthorization();

app.MapControllers();

app.Run();
