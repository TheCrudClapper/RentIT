using RentalService.API.Extensions;
using RentalService.API.Middleware;
using RentalService.Core;
using RentalService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Add API Controllers
builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Add user-defined services
builder.Services.AddInfrastructureLayer(builder.Configuration);
builder.Services.AddCoreLayer();

//Add OpenAPI support and Swagger
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

//Add global exception handling middleware
app.UseGlobalExceptionHandlingMiddleware();


await app.MigrateDatabaseAsync(builder.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//Https supports
app.UseHsts();
//app.UseHttpsRedirection();

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