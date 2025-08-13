using Microsoft.EntityFrameworkCore;
using RentalService.API.Middleware;
using RentalService.Core;
using RentalService.Infrastructure;
using RentalService.Infrastructure.DbContexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Add API Controllers
builder.Services.AddControllers();

builder.Services.AddDbContext<RentalDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresDB"),
        x => x.MigrationsAssembly("RentalService.Infrastructure"));
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Add user-defined services
builder.Services.AddInfrastructureLayer();
builder.Services.AddCoreLayer();

//Add OpenAPI support and Swagger
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//Add global exception handling middleware
app.UseGlobalExceptionHandlingMiddleware();

//Https supports
app.UseHsts();
app.UseHttpsRedirection();

//Use Swagger
app.UseSwagger();
app.UseSwaggerUI();

//Use Routing
app.UseRouting();

//Mapping API controllers 
app.MapControllers();

//Authentication && Authorization
app.UseAuthentication();
app.UseAuthorization();
app.Run();