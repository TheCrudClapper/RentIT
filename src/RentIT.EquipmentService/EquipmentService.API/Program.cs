using EquipmentService.API.Extensions;
using EquipmentService.API.Middleware;
using EquipmentService.Core;
using EquipmentService.Core.Domain.HtppClientContracts;
using EquipmentService.Infrastructure;
using EquipmentService.Infrastructure.HttpClients;

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

builder.Services.AddHttpClient<IUsersMicroserviceClient, UsersMicroserviceClient>( client =>
{
    client.BaseAddress = new Uri($"http://{builder.Configuration["USERS_MICROSERVICE_NAME"]}:{builder.Configuration["USERS_MICROSERVICE_PORT"]}");
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//Add global exception handling middleware
app.UseGlobalExceptionHandlingMiddleware();

await app.MigrateDatabaseAsync(builder.Services);

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