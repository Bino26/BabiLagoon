using BabiLagoon.Application;
using BabiLagoon.Application.Common.Interfaces;
using BabiLagoon.Application.Common.Interfaces.Base;
using BabiLagoon.Application.Common.Mapping;
using BabiLagoon.Application.Services.Interfaces;
using BabiLagoon.Infrastructure.Data;
using BabiLagoon.Infrastructure.Identity;
using BabiLagoon.Infrastructure.Repositories;
using BabiLagoon.Infrastructure.Repositories.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSwaggerServices();
builder.Services.AddDatabaseServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddApplicationServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

public static class DatabaseServiceExtensions
{
    public static IServiceCollection AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("BabiLagoonString")));

        services.AddDbContext<ApplicationAuthDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("BabiLagoonAuthString")));

        return services;
    }
}

public static class IdentityServiceExtensions
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationAuthDbContext>()
            .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>("BabiLagoon")
            .AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(options =>
        {
            options.Password = new PasswordOptions
            {
                RequireDigit = false,
                RequireLowercase = false,
                RequireNonAlphanumeric = false,
                RequireUppercase = false,
                RequiredLength = 6,
                RequiredUniqueChars = 1
            };
            options.SignIn = new SignInOptions
            {
                RequireConfirmedAccount = false,
                RequireConfirmedEmail = false
            };
            options.User = new UserOptions
            {
                AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ.@1234567890!#$%&'*+-/=?^_`{|}~",
                RequireUniqueEmail = true
            };
        });

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
            });

        return services;
    }
}

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IVillaRepository, VillaRepository>();
        services.AddScoped<IAmenityRepository, AmenityRepository>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IIdentityService, IdentityService>();

        services.AddAutoMapper(typeof(AutoMapperProfile));

        return services;
    }
}

public static class SwaggerServiceExtensions
{
    public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
};