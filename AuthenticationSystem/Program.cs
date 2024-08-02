using AuthenticationSystem.Repository;
using AuthenticationSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AuthenticationSystem.Token;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add DbContext with SQL Server
builder.Services.AddDbContext<RegistrationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("RegistrationConnectionString");
    options.UseSqlServer(connectionString);
});

// Register application services
builder.Services.AddScoped<IRegistrationService, RegistrationService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IJwtTokenManager, JwtTokenManager>();

// Configure JWT authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(jwtOptions =>
{
    var key = builder.Configuration.GetValue<string>("JwtConfig:Key");
    var keyBytes = Encoding.ASCII.GetBytes(key);
    jwtOptions.SaveToken = true;
    jwtOptions.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtConfig:Issuer"],
        ValidAudience = builder.Configuration["JwtConfig:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes)
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseAuthentication(); // Add this line to use authentication
app.UseAuthorization();

app.MapControllers();

app.Run();
