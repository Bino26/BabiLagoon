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
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("BabiLagoonString")));

builder.Services.AddDbContext<ApplicationAuthDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("BabiLagoonAuthString")));


builder.Services.AddScoped<IVillaRepository, VillaRepository>();
builder.Services.AddScoped<IAmenityRepository, AmenityRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IIdentityService, IdentityService>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationAuthDbContext>()
    .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>("BabiLagoon")
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
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

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))

    });



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
