using BabiLagoon.Application.Common.Interfaces;
using BabiLagoon.Application.Common.Interfaces.Base;
using BabiLagoon.Application.Common.Mapping;
using BabiLagoon.Infrastructure.Data;
using BabiLagoon.Infrastructure.Repositories;
using BabiLagoon.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("BabiLagoonString")));


builder.Services.AddScoped<IVillaRepository, VillaRepository>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
